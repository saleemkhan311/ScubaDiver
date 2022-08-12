public struct PlayerData
{
    public string playerName;
    public string className;
    public int score;

    // public static PlayerData LoadJson(string json)
    // {
    //     return JsonUtility.FromJson<PlayerData>(json);
    // }
    //
    // public static string ToJson(PlayerData data)
    // {
    //     return JsonUtility.ToJson(data);
    // }
    //
    // public string SaveQuery()
    // {
    //     return $"INSERT INTO scores (playerName, team, score) VALUES (\'{playerName}\', \'{team}\',{score});";
    // }
    //
    public override string ToString()
    {
        return $"{playerName} from team {className} has score {score}";
    }

    public string ToData()
    {
        return $"saveData:{playerName},{className},{score}";
    }
    
    // public Message ToRipMsg()
    // {
    //     var msg = Message.Create(MessageSendMode.reliable, ClientToServer.SaveMyData);
    //     msg.AddString(playerName);
    //     msg.AddString(className);
    //     msg.AddInt(score);
    //     return msg;
    // }
    //
    // public static PlayerData LoadRipMessage(Message msg)
    // {
    //     var data = new PlayerData
    //     {
    //         playerName = msg.GetString(),
    //         className = msg.GetString(),
    //         score = msg.GetInt()
    //     };
    //     return data;
    // }
}