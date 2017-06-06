using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainCameraScript : MonoBehaviour
{

    public GameObject player;

    private Vector3 offset;

    Camera camera;

    GameObject ui;

    float timerM;
    float timerS;
    float timermS;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - player.transform.position;
        camera = gameObject.GetComponent<Camera>();
        ui = GameObject.Find("Panel");

        InvokeRepeating("Timer",0f,0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.position = player.transform.position + offset;



            if (player.GetComponent<Player>().levelFinished)
            {
                LevelComplete();
            }
        }
    }

    public void LevelComplete()
    {
        StopCoroutine("Timer");
        GameObject t = GameObject.Find("TimeTaken");
        string finalTime = timerM + ":" + timerS + "." + timermS;
        t.GetComponent<Text>().text = finalTime;
        
        ui.transform.Translate(0f, -550f, 0f);

        Invoke("LoadNextLevel", 3f);
    }

    public void LevelFail()
    {
        StopCoroutine("Timer");
        GameObject t = GameObject.Find("GameOverPanel");

        t.transform.Translate(0f, -550f, 0f);

        Invoke("RestartLevel", 2f);
    }

    void Timer()
    {
        timermS = timermS + 1;
        if(timermS == 10)
        {
            timerS++;
            timermS = 0;
            if(timerS == 60)
            {
                timerM++;
                timerS = 0;
            }
        }
    }

    void ResetTimer()
    {
        timerM = 0;
        timerS = 0;
        timermS = 0;
    }

    void LoadNextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        ResetTimer();
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResetTimer();
    }


}
