using UnityEngine;
using System.Collections;

public class SwitchMusicOnLoad : MonoBehaviour
{

    public AudioClip newTrack;

    private BGMManager BGMManager;

    // Use this for initialization
    void Start()
    {
        BGMManager = FindObjectOfType<BGMManager>();

        if(newTrack)
            BGMManager.ChangeBGM(newTrack);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
