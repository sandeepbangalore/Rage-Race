using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer1 : MonoBehaviour {

    public Text timerText;
    private bool finished = false;
    private bool start = false;
    private float ti = -1;
    private GameManager gameManager;

    // Use this for initialization
    void Start () {
        gameManager = GameManager.Instance;
        //ti = Time.timeSinceLevelLoad;
        timerText = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
        //timerText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (finished || gameManager.getGameState() == GameManager.States.Finish)
        {
            timerText.color = Color.yellow;
            return;
        }
        if (gameManager.getGameState() == GameManager.States.Race && timerText.color != Color.yellow)
        {
            if (ti == -1)
            {
                ti = Time.timeSinceLevelLoad;
            }
            float t = Time.timeSinceLevelLoad - ti;
            string minutes = ((int)t / 60).ToString("00");
            string seconds = (t % 60).ToString("00.000");
            timerText.text = minutes + ":" + seconds;
        }
        //if (finished)
        //    return;
        //if (start)
        //{
        //    float t = Time.timeSinceLevelLoad - ti;
        //    string minutes = ((int)t / 60).ToString("f2");
        //    string seconds = (t % 60).ToString("f2");
        //    timerText.text = minutes + ":" + seconds;
        //}

    }

    public void Finish()
    {
        finished = true;
        //Debug.Log ("Timer has Finished");
        //start = false;
        //      timerText.color = Color.yellow;
    }

    public void StartTimer()
    {
        //System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
        //print("starttimer was called by " + stackTrace.GetFrame(1).GetMethod().Name);
        //ti = Time.timeSinceLevelLoad;
        //start = true;
    }
}
