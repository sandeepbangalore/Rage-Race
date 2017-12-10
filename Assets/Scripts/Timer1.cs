using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer1 : MonoBehaviour {

    public Text timerText;
    private bool finished = false;
    private bool start = false;
    private float ti;
    // Use this for initialization
    void Start () {
        
        timerText = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (finished)
            return;
        if(start)
        {
			float t = Time.timeSinceLevelLoad - ti;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");
            timerText.text = minutes + ":" + seconds;
        }
        
	}

    public void Finish()
    {
        finished = true;
        timerText.color = Color.yellow;
    }

    public void StartTimer()
    {
        ti = Time.timeSinceLevelLoad;
        start = true;
    }
}
