using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioEventManager : MonoBehaviour
{

    //    public AudioClip boxAudio;
    //    public AudioClip playerLandsAudio;
    // public AudioClip playerJumpAudio;
    // public AudioClip playerDeathAudio;

    public AudioClip playerStepAudio;
    public AudioClip powerUpAudio;
    public AudioClip BlockDropAudio;
    public AudioClip GasAudio;
    public AudioClip DrillerAudio;
    public AudioClip HomingAudio;
    // public AudioClip playerLaserStepAudio;
    // public AudioClip playerIceStepAudio;

    // public AudioClip marbleAudio;
    // public AudioClip boulderAudio;
    // public AudioClip capsuleAudio;
    // public AudioClip tireAudio;
    // public AudioClip hammerAudio;
    // public AudioClip woodAudio;




    //    private UnityAction<Vector3> boxCollisionEventListener;
    //    private UnityAction<Vector3> playerLandsEventListener;
    // private UnityAction<Vector3> playerJumpEventListener;
    // private UnityAction<Vector3> playerDeathEventListener;


    private UnityAction<Vector3> playerStepEventListener;
    private UnityAction<Vector3> powerUpEventListener;
    private UnityAction<Vector3> BlockDropEventListener;
    private UnityAction<Vector3> GasEventListener;
    private UnityAction<Vector3> DrillerEventListener;
    private UnityAction<Vector3> HomingEventListener;
    // private UnityAction<Vector3> playerLaserStepEventListener;
    // private UnityAction<Vector3> playerIceStepEventListener;


    // private UnityAction<Vector3> marbleAudioEventListener;
    // private UnityAction<Vector3> boulderAudioEventListener;
    // private UnityAction<Vector3> capsuleAudioEventListener;
    // private UnityAction<Vector3> tireAudioEventListener;
    // private UnityAction<Vector3> hammerAudioEventListener;
    // private UnityAction<Vector3> woodAudioEventListener;



    void Awake()
    {

        //       boxCollisionEventListener = new UnityAction<Vector3>(boxCollisionEventHandler);
        //       playerLandsEventListener = new UnityAction<Vector3>(playerLandsEventHandler);
        // playerJumpEventListener = new UnityAction<Vector3> (playerJumpEventHandler);
        // playerDeathEventListener = new UnityAction<Vector3> (playerDeathEventHandler);

        playerStepEventListener = new UnityAction<Vector3>(playerStepEventHandler);
        powerUpEventListener = new UnityAction<Vector3>(powerUpEventHandler);
        BlockDropEventListener = new UnityAction<Vector3>(blockDropEventHandler);
        GasEventListener = new UnityAction<Vector3>(gasEventHandler);
        DrillerEventListener = new UnityAction<Vector3>(drillerEventHandler);
        HomingEventListener = new UnityAction<Vector3>(homingEventHandler);
        // playerLaserStepEventListener = new UnityAction<Vector3> (playerLaserStepHandler);
        // playerIceStepEventListener = new UnityAction<Vector3> (playerIceStepHandler);


        // marbleAudioEventListener = new UnityAction<Vector3> (marbleAudioEventHandler);
        // boulderAudioEventListener = new UnityAction<Vector3> (boulderAudioEventHandler);
        // capsuleAudioEventListener = new UnityAction<Vector3> (capsuleAudioEventHandler);
        // tireAudioEventListener = new UnityAction<Vector3> (tireAudioEventHandler);
        // hammerAudioEventListener = new UnityAction<Vector3> (hammerAudioEventHandler);
        // woodAudioEventListener = new UnityAction<Vector3> (woodAudioEventHandler);

    }


    // Use this for initialization
    void Start()
    {

    }


    void OnEnable()
    {

        //       EventManager.StartListening<BoxCollisionEvent, Vector3>(boxCollisionEventListener);
        //       EventManager.StartListening<PlayerLandsEvent, Vector3>(playerLandsEventListener);
        // EventManager.StartListening<PlayerJumpEvent, Vector3>(playerJumpEventListener);
        // EventManager.StartListening<DeadAudioEvent, Vector3>(playerDeathEventListener);


        EventManager.StartListening<PlayerStepEvent, Vector3>(playerStepEventListener);
        EventManager.StartListening<PowerUpEvent, Vector3>(powerUpEventListener);
        EventManager.StartListening<GasEvent, Vector3>(GasEventListener);
        EventManager.StartListening<BlockDropEvent, Vector3>(BlockDropEventListener);
        EventManager.StartListening<DrillerEvent, Vector3>(DrillerEventListener);
        EventManager.StartListening<HomingEvent, Vector3>(HomingEventListener);
        // EventManager.StartListening<PlayerLaserStepEvent,Vector3> (playerLaserStepEventListener);
        // EventManager.StartListening<PlayerIceStepEvent,Vector3> (playerIceStepEventListener);


        // EventManager.StartListening<MarbleAudioEvent,Vector3> (marbleAudioEventListener);
        // EventManager.StartListening<BoulderAudioEvent,Vector3> (boulderAudioEventListener);
        // EventManager.StartListening<CapsuleAudioEvent,Vector3> (capsuleAudioEventListener);
        // EventManager.StartListening<TireAudioEvent,Vector3> (tireAudioEventListener);
        // EventManager.StartListening<HammerAudioEvent,Vector3> (hammerAudioEventListener);
        // EventManager.StartListening<WoodAudioEvent,Vector3> (woodAudioEventListener);


    }

    void OnDisable()
    {

        //       EventManager.StopListening<BoxCollisionEvent, Vector3>(boxCollisionEventListener);
        //       EventManager.StopListening<PlayerLandsEvent, Vector3>(playerLandsEventListener);
        // EventManager.StopListening<PlayerJumpEvent, Vector3>(playerJumpEventListener);
        // EventManager.StopListening<DeadAudioEvent, Vector3>(playerDeathEventListener);


        EventManager.StopListening<PlayerStepEvent, Vector3>(playerStepEventListener);
        EventManager.StopListening<PowerUpEvent, Vector3>(powerUpEventListener);
        EventManager.StopListening<GasEvent, Vector3>(GasEventListener);
        EventManager.StopListening<BlockDropEvent, Vector3>(BlockDropEventListener);
        EventManager.StopListening<DrillerEvent, Vector3>(DrillerEventListener);
        EventManager.StopListening<HomingEvent, Vector3>(HomingEventListener);
        // EventManager.StopListening<PlayerLaserStepEvent,Vector3> (playerLaserStepEventListener);
        // EventManager.StopListening<PlayerIceStepEvent,Vector3> (playerIceStepEventListener);

        // EventManager.StopListening<MarbleAudioEvent,Vector3> (marbleAudioEventListener);
        // EventManager.StopListening<BoulderAudioEvent,Vector3> (boulderAudioEventListener);
        // EventManager.StopListening<CapsuleAudioEvent,Vector3> (capsuleAudioEventListener);
        // EventManager.StopListening<TireAudioEvent,Vector3> (tireAudioEventListener);
        // EventManager.StopListening<HammerAudioEvent,Vector3> (hammerAudioEventListener);
        // EventManager.StopListening<WoodAudioEvent,Vector3> (woodAudioEventListener);


    }



    // Update is called once per frame
    void Update()
    {
    }




    //    void boxCollisionEventHandler(Vector3 worldPos)
    //    {
    //        AudioSource.PlayClipAtPoint(this.boxAudio, worldPos);
    //    }

    //    void playerLandsEventHandler(Vector3 worldPos)
    //    {
    //        AudioSource.PlayClipAtPoint(this.playerLandsAudio, worldPos);
    //    }
    // void playerJumpEventHandler(Vector3 worldPos){

    // 	AudioSource.PlayClipAtPoint(this.playerJumpAudio, worldPos);

    // }
    void playerStepEventHandler(Vector3 worldPos)
    {

        AudioSource.PlayClipAtPoint(this.playerStepAudio, worldPos, 1f);
    }

    void powerUpEventHandler(Vector3 worldPos)
    {

        AudioSource.PlayClipAtPoint(this.powerUpAudio, worldPos, 1f);
    }

    void blockDropEventHandler(Vector3 worldPos)
    {

        AudioSource.PlayClipAtPoint(this.BlockDropAudio, worldPos, 1f);
    }

    void gasEventHandler(Vector3 worldPos)
    {

        AudioSource.PlayClipAtPoint(this.GasAudio, worldPos, 1f);
    }

    void drillerEventHandler(Vector3 worldPos)
    {

        AudioSource.PlayClipAtPoint(this.DrillerAudio, worldPos, 1f);
    }

    void homingEventHandler(Vector3 worldPos)
    {

        AudioSource.PlayClipAtPoint(this.HomingAudio, worldPos, 1f);
    }


    // void playerDeathEventHandler(Vector3 worldPos){

    // 	AudioSource.PlayClipAtPoint (this.playerDeathAudio, worldPos,20f);
    // }
    // void playerLaserStepHandler(Vector3 worldPos)
    // {
    // 	AudioSource.PlayClipAtPoint (this.playerLaserStepAudio, worldPos,0.2f);
    // }

    // void playerIceStepHandler(Vector3 worldPos){
    // 	AudioSource.PlayClipAtPoint (this.playerIceStepAudio,worldPos,0.7f);

    // }

    // void marbleAudioEventHandler(Vector3 worldPos){
    // 	AudioSource.PlayClipAtPoint (this.marbleAudio,worldPos,30f);
    // }


    // void boulderAudioEventHandler(Vector3 worldPos){
    // 	AudioSource.PlayClipAtPoint (this.boulderAudio,worldPos,10f);

    // }

    // void capsuleAudioEventHandler(Vector3 worldPos){
    // 	AudioSource.PlayClipAtPoint (this.capsuleAudio,worldPos);
    // }

    // void tireAudioEventHandler(Vector3 worldPos){
    // 	AudioSource.PlayClipAtPoint (this.tireAudio,worldPos);
    // }

    // void hammerAudioEventHandler(Vector3 worldPos){
    // 	AudioSource.PlayClipAtPoint (this.hammerAudio,worldPos,5f);
    // }

    // void woodAudioEventHandler(Vector3 worldPos){
    // 	AudioSource.PlayClipAtPoint (this.woodAudio,worldPos);
    // }
}
