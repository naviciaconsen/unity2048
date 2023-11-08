using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int score=0;
    public int highestscore=0;
    public Text scoreBoard;
    public Text highestBoard;
    public Image question;

    GameCore gamecore=new GameCore();
    // Start is called before the first frame update
    void Start()
    {
        Screen.fullScreenMode= FullScreenMode.Windowed;
    }

    // Update is called once per frame
    void Update()
    {
        if(score>highestscore)
        {
            highestscore = score;
        }
        scoreBoard.text = "" + score;
        highestBoard.text = "" + highestscore;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
}
