using System;
using System.Text;
using NativeWebSocket;
using UnityEngine;

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

    [SerializeField] private string host;
    [SerializeField] private int port;

    private WebSocket _socket;

    private void Awake()
    {
        Singleton = this;
    }

    public async void StartServer()
    {
        _socket = new WebSocket($"ws://{host}:{port}");
        _socket.OnMessage += (bytes) =>
        {
            var msg = Encoding.ASCII.GetString(bytes);
            Debug.Log(msg + " msg from server");
            Command(msg);
        };
        _socket.OnOpen += () => { Debug.Log("Connection Open"); };

        _socket.OnError += (e) => { Debug.Log("Error! " + e); };

        _socket.OnClose += (e) => { Debug.Log("Connection closed!"); };

        InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);
        await _socket.Connect();
    }

    private void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        _socket.DispatchMessageQueue();
#endif
    }

    public async void Request(string cmd)
    {
        if (_socket.State != WebSocketState.Open) return;
        await _socket.SendText(cmd);
    }

    private void Command(string cmd)
    {
        if (!cmd.Contains(":")) return;
        try
        {
            var c = cmd.Split(":");
            switch (c[0])
            {
                case "HighScore":
                {
                    var highScore = int.Parse(c[1]);
                    GameManager.Singleton.HighestScore = highScore;
                    break;
                }
                case "LeaderBoard":
                {
                    var player = c[1].Split(",");
                    var pName = player[0];
                    var pClassName = player[1];
                    var score = int.Parse(player[2]);
                    var data = new PlayerData()
                    {
                        playerName = pName,
                        className = pClassName,
                        score = score
                    };
                    GameManager.Singleton.AddDataToLeaderBoard(data);
                    break;
                }
                default: break;
            }
        }
        catch
        {
            // ignore
        }
    }

    private async void OnApplicationQuit()
    {
        await _socket.Close();
    }
}