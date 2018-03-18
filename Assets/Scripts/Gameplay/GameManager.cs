using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private List<Player> players;
    private static GameManager instance;
    public GameObject playerPrefab;

    public enum GameState
    {
        NotStarted,
        Running,
        Paused,
        Stopped
    }

    public void Awake()
    {
        players = new List<Player>();
    }

    // Use this for initialization
    void Start()
    {
        //DebugLogger.Log("Test", Enum.LoggerMessageType.Important);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreatePlayers()
    {
        //Instantiate the player prefab and add players to the list
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
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
