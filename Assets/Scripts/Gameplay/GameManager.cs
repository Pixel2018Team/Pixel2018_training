using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public string preloadingSceneName;
    public string firstSceneName;
    private List<Player> players;
    private static GameManager _instance;
    private GameState gameState;
    private TimerManager timerManager;

    private enum GameState
    {
        Preloading,
        NotStarted,
        Running,
        Paused,
        Stopped
    }

    //Initialize stuff here, for the pre-loading scene
    public void Awake()
    {
        DebugLogger.Log("GameManager awake begins", Enum.LoggerMessageType.Important);
        gameState = GameState.Preloading;

        if (_instance == null)
        {
            _instance = this;
        }

        players = new List<Player>();

        timerManager = GetComponent<TimerManager>();
        if(timerManager != null)
        {
            timerManager.timerLocked = true;
            DebugLogger.Log("TimerManager initialized for GameManager", Enum.LoggerMessageType.Important);
        }

        DontDestroyOnLoad(gameObject);
        DebugLogger.Log("GameManager awake done", Enum.LoggerMessageType.Important);
    }

    // Use this for initialization
    void Start()
    {
        var currentSceneName = SceneManager.GetActiveScene().name;

        if(currentSceneName == preloadingSceneName)
        {
            StartPreload();
        }
    }

    //Start method specific to the preloading scene
    private void StartPreload()
    {
        //Preload stuff then go to the menu/main scene
        DebugLogger.Log("GameManager start preloading ", Enum.LoggerMessageType.Important);

        gameState = GameState.NotStarted;
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreatePlayers()
    {
        //Instantiate the player prefab and add players to the list
        if(playerPrefab != null)
        {

        }

    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        gameState = GameState.Paused;
        foreach (var p in players)
        {
            p.canPlay = false;
        }

        //Some code to display a GUI item or something
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        foreach (var p in players)
        {
            p.canPlay = true;
        }

        //Some code to hide the GUI thingies that were displayed
    }

    //Get the current game manager
    public GameManager GetInstance()
    {
        return _instance;
    }

    /*============================= Utility functions ============================= */

    //Pick a random player in the list
    public Player PickRandomPlayer()
    {
        if (players.Count() > 0)
        {
            var randomIndex = UnityEngine.Random.Range(0, players.Count() - 1);

            return players.ElementAt(randomIndex);
        }
        return null;
    }

    //Check if all players are dead / defeated
    public bool AllPlayersAreDefeated()
    {
        return players.All(p => p.IsDead);
    }

    //Get the player(s) with the hight score
    //note : it will return a list, in case two or more players have the same highest score
    public IEnumerable<Player> GetPlayersWithHighestScore()
    {
        if (players.Count() > 0)
        {
            var topPlayers = new List<Player>();

            var topPlayer = players.OrderByDescending(p => p.playerScore).First();
            topPlayers.Add(topPlayer);

            //Check if there are other players with the same score
            var otherTopPlayers = players.Where(p => p.playerId != topPlayer.playerId && p.playerScore == topPlayer.playerScore);

            if (otherTopPlayers.Count() > 0)
            {
                topPlayers.AddRange(otherTopPlayers);
            }

            return topPlayers;
        }

        return null;
    }

}
