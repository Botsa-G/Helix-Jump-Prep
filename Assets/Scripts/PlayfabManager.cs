using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager : MonoBehaviour
{
    
    public int firstScore;
    public int secondScore;
    public int thirdScore;
    // Start is called before the first frame update
    void Start()
    {
        Login();
    }

    void Login()
    {

        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }
    void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful login!");
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Unsuccessful login!");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
          {
              new StatisticUpdate
              {
                  StatisticName = "Best Jumpers", Value= score
              }
          }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);

    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successful leaderboard sent!");
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Best Jumpers",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet( GetLeaderboardResult result)
    {
        foreach(var item in result.Leaderboard)
        {
            Debug.Log(item.Position + "" + item.PlayFabId + "" + item.StatValue);

            
        }

        firstScore = result.Leaderboard[0].StatValue;
        if (result.Leaderboard.Count > 1 )
        { secondScore = result.Leaderboard[1].StatValue; }
        if (result.Leaderboard.Count > 2)
        { thirdScore = result.Leaderboard[2].StatValue; }
    }
}
