using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager _singleton;

    public static GameManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
            {
                _singleton = value;
                return;
            }

            Destroy(value.gameObject);
        }
    }

    //--------------------------- User Interface -----------------------------------
    [Header("In Game UI")] [SerializeField]
    private Text scoreText;

    [SerializeField] private Text highestScore;
    [SerializeField] private Text totalNTrash;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Start Menu")] [SerializeField]
    private GameObject startMenu;

    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_Dropdown teamPicker;
    [SerializeField] private GameObject inGameMenu;

    [Header("LeaderBoard")] [SerializeField]
    private RectTransform leaderBoardParent;

    [SerializeField] private GameObject historyBoxPrefab;

    private int _totalDataCount = 0;
    //------------------------ player tracking--------------------------------

    public int HighestScore
    {
        set => highestScore.text = $"HighScore: {value}";
    }

    private int _score;
    private int _totalTrash;

    public bool gameOver;

    private bool _startedGame = false;

    public int TotalTrash
    {
        get => _totalTrash;
        set
        {
            if (value >= maxTrashNumber) GameOver();
            _totalTrash = value;
            totalNTrash.text = $"{value}/{maxTrashNumber}";
        }
    }

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            scoreText.text = $"Score: {value}";
        }
    }

    //------------------------------ Marine Animals--------------------------------------
    [Header("Animals")] [SerializeField] private int animalsWave;
    [SerializeField] private GameObject[] animals;
    [SerializeField] private Transform animalParent;

    //---------------------------Trash--------------------------------------
    [Header("Trash")] [SerializeField] private GameObject[] trash;
    [SerializeField] private int maxTrashNumber;
    [SerializeField] private float trashSpawnDelay;
    [SerializeField] private int trashSpawnFrequency;
    [SerializeField] private Transform trashParent;

    //--------------------------Player Audio -----------------------------------------
    [Header("Audio")] [SerializeField] private GameObject audioPrefab;

    //---------- Data attributes---------------------------------
    private PlayerData _data;
    // private IDbConnection _dbConnection;
    //
    // public IDbConnection DbConnection
    // {
    //     get
    //     {
    //         if (_dbConnection != null) return _dbConnection;
    //         var path = Application.dataPath + "/PlayerData/";
    //         try
    //         {
    //             if (!File.Exists(path)) File.Create(path);
    //         }
    //         catch
    //         {
    //             //ignore
    //         }
    //
    //         var db = "URI=file:" + path + "database.db";
    //         Debug.Log(db);
    //         _dbConnection = new SqliteConnection(db);
    //         _dbConnection.Open();
    //         return _dbConnection;
    //     }
    // }

    private void Start()
    {
        Singleton = this;
        WebSocketClient.Singleton.StartServer();
        gameOver = false;
        TotalTrash = Score = 0;
        SpawnRandomAnimals();
        SpawnTrash();
        Time.timeScale = 0;
        inGameMenu.SetActive(false);
        startMenu.SetActive(true);
    }

    private void Update()
    {
        if (!_startedGame) return;
        if (gameOver) return;
        if (Time.fixedTime % 120 == 0)
        {
            trashSpawnDelay -= .5f;
            trashSpawnDelay = Mathf.Clamp(trashSpawnDelay, 2, 5);
            trashSpawnFrequency++;
            trashSpawnFrequency = Mathf.Clamp(trashSpawnFrequency, 1, 3);
        }
    }

    private void SpawnRandomAnimals()
    {
        for (var i = 0; i < animalsWave; i++)
        {
            Instantiate(animals[Random.Range(0, animals.Length)],
                new Vector2(Random.Range(-30, -5), Random.Range(0, 5)), Quaternion.identity, animalParent);
        }
    }

    private void SpawnTrash()
    {
        for (var i = 0; i < trashSpawnFrequency; i++)
        {
            Instantiate(trash[Random.Range(0, trash.Length)], new Vector2(Random.Range(-8, 8), 6), Quaternion.identity,
                trashParent);
            TotalTrash++;
        }

        if (!gameOver)
        {
            Invoke(nameof(SpawnTrash), trashSpawnDelay);
        }
    }

    public void GameOver()
    {
        RequestLeaderBoardData();
        gameOver = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        _data.score = Score;
        SavePlayerData();
    }

    public void PlayAudio(AudioClip clip)
    {
        var audioSource = Instantiate(audioPrefab, transform).GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void StartGame()
    {
        if (string.IsNullOrEmpty(nameInput.text)) return;
        _data = new PlayerData
        {
            playerName = nameInput.text,
            className = teamPicker.options[teamPicker.value].text,
            score = 0
        };
        Time.timeScale = 1;
        startMenu.SetActive(false);
        inGameMenu.SetActive(true);
        _startedGame = true;
        RequestHighScore();
    }

    //-------------------------------------------------------Data Manager-----------------------------------------
    private void SavePlayerData()
    {
        WebSocketClient.Singleton.Request(_data.ToData()); // SaveData:
        // RipNetwork.Singleton.client.Send(_data.ToRipMsg());
    }

    private void RequestHighScore()
    {
        WebSocketClient.Singleton.Request("highscore");
        // var msg = Message.Create(MessageSendMode.reliable, ClientToServer.RequestHighScore);
        // msg.AddInt(0);
        // RipNetwork.Singleton.client.Send(msg);
        // using (var cmd = DbConnection.CreateCommand())
        // {
        //     var query = "select max(score) as highscore from scores";
        //     cmd.CommandText = query;
        //     using (var reader = cmd.ExecuteReader())
        //     {
        //         while (reader.Read())
        //         {
        //             _highestScore = reader.GetInt32(0);
        //             highestScore.text = $"HighScore: {_highestScore}";
        //         }
        //     }
        // }
    }

    // Loading all the score list for leaderboard
    private void RequestLeaderBoardData()
    {
        Debug.Log(WebSocketClient.Singleton + " web socket");
        WebSocketClient.Singleton.Request("leaderBoard");
        // var msg = Message.Create(MessageSendMode.reliable, ClientToServer.RequestLeaderBoard);
        // msg.AddInt(0);
        // RipNetwork.Singleton.client.Send(msg);
        // var query = "select * from scores order by score desc;";
        // using (var cmd = DbConnection.CreateCommand())
        // {
        //     cmd.CommandText = query;
        //     using (var reader = cmd.ExecuteReader())
        //     {
        //         var count = 0;
        //         while (reader.Read())
        //         {
        //             var data = new PlayerData()
        //             {
        //                 playerName = reader.GetString(1),
        //                 team = reader.GetString(2),
        //                 score = reader.GetInt32(3),
        //             };
        //             var box = Instantiate(historyBoxPrefab, leaderBoardParent).GetComponent<HistoryBox>();
        //             box.SetData(data);
        //             count++;
        //             leaderBoardParent.sizeDelta = new Vector2(380, count * 50);
        //         }
        //     }
        // }
    }

    public void AddDataToLeaderBoard(PlayerData data)
    {
        Debug.Log(data.ToString());
        var box = Instantiate(historyBoxPrefab, leaderBoardParent).GetComponent<HistoryBox>();
        box.SetData(data);
        _totalDataCount++;
        leaderBoardParent.sizeDelta = new Vector2(380, _totalDataCount * 50);
    }

    // private void Update()
    // {
    //     Debug.Log("Collect: "+collectedTrash);
    //     if(totalTrash >=15)
    //     {
    //         gameOver = true;
    //     }
    //
    //     if (gameOver)
    //         Time.timeScale = 0;
    //     if (!gameOver)
    //         Time.timeScale = 1;
    // }
}