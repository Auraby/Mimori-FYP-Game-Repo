using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FarallonPhasesController : MonoBehaviour {

    public enum Phases { StartPhase, Phase1, Phase2, Phase3, EndPhase}
    public Phases currentPhase;


    public GameObject BattleMusic;

    //AudioClips
    public AudioSource distanceRoar;

    //Time
    public float startPhaseTime = 5;
    private float tempStartPhaseTime;

    private float endTime = 10;
    private float tempEndTime;

    //Movement and Attack Phases
    public bool isPhase1 = false;
    public bool isPhase2 = false;
    public bool isPhase3 = false;

    ////Air Phases
    //public bool isAirPhase1 = false;
    //public bool isAirPhase2 = false;
    //public bool isAirPhase3 = false;

    ////Ground Phases
    //public bool isgroundPhase1 = false;
    //public bool isgroundPhase2 = false;
    //public bool isgroundPhase3 = false;
    AsyncOperation asyncOp;


    public static FarallonPhasesController instance { get; set; }
	// Use this for initialization
	void Start () {
        instance = this;
        distanceRoar.Play();

        asyncOp = SceneManager.LoadSceneAsync("MainMenu");
        asyncOp.allowSceneActivation = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (currentPhase == Phases.StartPhase)
        {
            tempStartPhaseTime += Time.deltaTime;

            

            if (tempStartPhaseTime > startPhaseTime)
            {
                BattleMusic.SetActive(true);
                tempStartPhaseTime = 0;
                currentPhase = Phases.Phase1;
            }
        }

        if (FarallonController.instance.currFaraState == FarallonController.FarallonStates.Dying)
        {
            currentPhase = Phases.EndPhase;
        }

        if (currentPhase == Phases.EndPhase)
        {
            tempEndTime += Time.deltaTime;

            if (tempEndTime > endTime)
            {
                asyncOp.allowSceneActivation = true;
            }
        }

        switch (currentPhase)
        {
            case Phases.Phase1:
                {
                    isPhase1 = true;
                }
                break;

            case Phases.Phase2:
                {
                    isPhase2 = true;
                }
                break;

            case Phases.Phase3:
                {

                    isPhase3 = true;
                }
                break;
        }
	}

    
}
