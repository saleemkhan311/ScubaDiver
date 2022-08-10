using UnityEngine;

public struct PlayerData
{
    public string playerName;
    public string team;
    public int score;

    public static PlayerData LoadJson(string json)
    {
        return JsonUtility.FromJson<PlayerData>(json);
    }

    public static string ToJson(PlayerData data)
    {
        return JsonUtility.ToJson(data);
    }

    public string SaveQuery()
    {
        return $"INSERT INTO scores (playerName, team, score) VALUES (\'{playerName}\', \'{team}\',{score});";
    }
}