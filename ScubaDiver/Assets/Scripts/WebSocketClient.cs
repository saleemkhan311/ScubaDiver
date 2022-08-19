using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

public enum RequestApis
{
    highscore,
    leaderboard,
    savedata
}

public class WebSocketClient : MonoBehaviour
{
    private static WebSocketClient _instance;

    public static WebSocketClient Singleton
    {
        get => _instance;
        set
        {
            if (_instance != null)
            {
                Destroy(value.gameObject);
                return;
            }

            _instance = value;
        }
    }

    // [SerializeField] private string host;
    // [SerializeField] private int port;

    private const string URL = "https://yeetrash.herokuapp.com";

    // private WebSocket _socket;

    private void Awake()
    {
        Singleton = this;
    }

    public void RequestApi(RequestApis api, WWWForm data = null)
    {
        var uri = $"{URL}/{api.ToString()}";
        Debug.Log(uri);
        if (api == RequestApis.savedata)
        {
            Debug.Log(data);
            var request = UnityWebRequest.Post(uri, data);
            // request.SetRequestHeader("Content-Type", "application/json");
            StartCoroutine(SendRequest(request, Debug.Log));
        }
        else
        {
            var request = UnityWebRequest.Get(uri);
            StartCoroutine(SendRequest(request, s =>
            {
                Debug.Log(s);
                switch (api)
                {
                    case RequestApis.highscore:
                    {
                        var json = JObject.Parse(s);
                        var value = (int)json.GetValue("score");
                        GameManager.Singleton.HighestScore = value;
                        break;
                    }
                    case RequestApis.leaderboard:
                    {
                        var array = JArray.Parse(s);
                        var dataList = array.ToObject<List<PlayerData>>();
                        if (dataList == null) return;
                        Debug.Log(dataList.Count + "data loaded");
                        foreach (var t in dataList)
                        {
                            // Debug.Log(dataList[i].ToData());
                            GameManager.Singleton.AddDataToLeaderBoard(t);
                        }

                        // foreach (var jToken in array)
                        // {
                        //     var json = (JObject)jToken;
                        //     string pName = null, tName = null;
                        //     var score = 0;
                        //     if (json.TryGetValue("pName", out var value))
                        //     {
                        //         pName = (string)value;
                        //     }
                        //     
                        //     if (json.TryGetValue("pName", out var value2))
                        //     {
                        //         tName = (string)value;
                        //     }
                        //     
                        //     if (json.TryGetValue("pName", out var value3))
                        //     {
                        //         score = (int)value;
                        //     }
                        //     
                        //     var data = new PlayerData
                        //     {
                        //         pName = pName,
                        //         tName = tName,
                        //         score = score
                        //     };
                        //     GameManager.Singleton.AddDataToLeaderBoard(data);
                        // }

                        break;
                    }
                    case RequestApis.savedata:
                    default:
                        // ignore
                        break;
                }
            }));
        }
    }

    private IEnumerator SendRequest(UnityWebRequest request, Action<string> result = null)
    {
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            result?.Invoke(request.downloadHandler.text);
        }
    }

    // public async void StartServer()
    // {
    //     _socket = new WebSocket($"wss://{host}:{port}");
    //     _socket.OnMessage += (bytes) =>
    //     {
    //         var msg = Encoding.ASCII.GetString(bytes);
    //         Debug.Log(msg + " msg from server");
    //         Command(msg);
    //     };
    //     _socket.OnOpen += () => { Debug.Log("Connection Open"); };
    //
    //     _socket.OnError += (e) => { Debug.Log("Error! " + e); };
    //
    //     _socket.OnClose += (e) => { Debug.Log("Connection closed!"); };
    //
    //     InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);
    //     await _socket.Connect();
    // }

//     private void Update()
//     {
// #if !UNITY_WEBGL || UNITY_EDITOR
//         _socket.DispatchMessageQueue();
// #endif
//     }

    // public async void Request(string cmd)
    // {
    //     if (_socket.State != WebSocketState.Open) return;
    //     await _socket.SendText(cmd);
    // }

    // private void Command(string cmd)
    // {
    //     if (!cmd.Contains(":")) return;
    //     try
    //     {
    //         var c = cmd.Split(":");
    //         switch (c[0])
    //         {
    //             case "HighScore":
    //             {
    //                 var highScore = int.Parse(c[1]);
    //                 GameManager.Singleton.HighestScore = highScore;
    //                 break;
    //             }
    //             case "LeaderBoard":
    //             {
    //                 var player = c[1].Split(",");
    //                 var pName = player[0];
    //                 var pClassName = player[1];
    //                 var score = int.Parse(player[2]);
    //                 var data = new PlayerData()
    //                 {
    //                     pName = pName,
    //                     tName = pClassName,
    //                     score = score
    //                 };
    //                 GameManager.Singleton.AddDataToLeaderBoard(data);
    //                 break;
    //             }
    //             default: break;
    //         }
    //     }
    //     catch
    //     {
    //         // ignore
    //     }
    // }

    // private async void OnApplicationQuit()
    // {
    //     await _socket.Close();
    // }
}