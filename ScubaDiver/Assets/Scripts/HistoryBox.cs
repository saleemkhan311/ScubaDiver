using TMPro;
using UnityEngine;

public class HistoryBox : MonoBehaviour
{
    [SerializeField] private TMP_Text playerName;
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text team;

    public void SetData(PlayerData data)
    {
        playerName.text = data.pName;
        team.text = data.tName;
        score.text = $"{data.score}".PadLeft(3, '0');
    }
}
