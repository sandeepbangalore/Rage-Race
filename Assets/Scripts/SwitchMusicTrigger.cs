using UnityEngine;
using System.Collections;


//CURRENTLY NOT USED - use when winner/loser

public class SwitchMusicTrigger : MonoBehaviour
{

    public AudioClip newTrack;

    private BGMManager BGMManager;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(newTrack != null)
            {
                BGMManager = FindObjectOfType<BGMManager>();
                BGMManager.ChangeBGM(newTrack);
            }
                
        }
    }
}
