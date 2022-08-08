using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    public Image shootFill;
    public Text scoreText;
    public Text totalNTrash;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: "+GameManager.Singleton.score.ToString();
        totalNTrash.text = "Total Number of Trash: " + GameManager.Singleton.totalTrash+"/30";

        float num = (float)GameManager.Singleton.collectedTrash;
        shootFill.fillAmount =  Mathf.Lerp(shootFill.fillAmount, num / 3.0f, 3*Time.deltaTime);
    }
}
