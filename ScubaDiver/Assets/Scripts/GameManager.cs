using System.Data;
using System.IO;
using TMPro;
using Mono.Data.Sqlite;
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
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highestScore;
    [SerializeField] private Text totalNTrash;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject startMenu;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_Dropdown teamPicker;

    [SerializeField] private GameObject inGameMenu;
    //------------------------ player tracking--------------------------------

    private int _highestScore;
    private int _score;
    private int _totalTrash;

    public bool gameOver;

    private bool _startedGame = false;
    public int TotalTrash
    {
        get => _totalTrash;
        set
        {
            if (value >= 30) GameOver();
            _totalTrash = value;
            totalNTrash.text = $"{value}/30";
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
    [SerializeField] private float trashSpawnDelay;
    [SerializeField] private Transform trashParent;

    //--------------------------Player Audio -----------------------------------------
    [Header("Audio")] [SerializeField] private GameObject audioPrefab;

    //---------- Data attributes---------------------------------
    private PlayerData _data;
    private IDbConnection _dbConnection;

    public IDbConnection DbConnection
    {
        get
        {
            if (_dbConnection != null) return _dbConnection;
            var db = "URI=file:" + Application.dataPath + "/PlayerData/database.db";
            _dbConnection = new SqliteConnection(db);
            _dbConnection.Open();
            return _dbConnection;
        }
    }

    private void Start()
    {
        Singleton = this;
        gameOver = false;
        TotalTrash = Score = 0;
        SpawnRandomAnimals();
        SpawnTrash();
        Time.timeScale = 0;
        inGameMenu.SetActive(false);
        startMenu.SetActive(true);
        LoadHighestScore();
    }

    private void Update()
    {
        if (!_startedGame) return;
        if (gameOver) return;
        if (Time.fixedTime % 60 == 0)
        {
            trashSpawnDelay -= .5f;
            trashSpawnDelay = Mathf.Clamp(trashSpawnDelay, 1, 5);
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
        var ran = Random.Range(0, trash.Length);
        Instantiate(trash[ran], new Vector2(Random.Range(-8, 8), 6), Quaternion.identity, trashParent);
        TotalTrash++;
        if (!gameOver)
        {
            Invoke(nameof(SpawnTrash), trashSpawnDelay);
        }
    }

    public void GameOver()
    {
        Debug.Log("trashSpawn: "+trashSpawnDelay);
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
            team = teamPicker.options[teamPicker.value].text,
            score = 0
        };
        Debug.Log(_data.SaveQuery());
        Time.timeScale = 1;
        startMenu.SetActive(false);
        inGameMenu.SetActive(true);
        _startedGame = true;
    }

    //-------------------------------------------------------Data Manager-----------------------------------------
    private void SavePlayerData()
    {
        using (var cmd = DbConnection.CreateCommand())
        {
            cmd.CommandText = _data.SaveQuery();
            cmd.ExecuteScalar();
        }
    }

    private void LoadHighestScore()
    {
        using (var cmd = DbConnection.CreateCommand())
        {
            var query = "select max(score) as highscore from scores";
            cmd.CommandText = query;
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    _highestScore = reader.GetInt32(0);
                    highestScore.text = $"HighScore: {_highestScore}";
                }
            }
        }
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