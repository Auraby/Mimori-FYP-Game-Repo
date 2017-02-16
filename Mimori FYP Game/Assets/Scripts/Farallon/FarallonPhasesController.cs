using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//This script was supposed to be controlling the different attack and movement phases of farallon, but I converted it to
//like a level controller.

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
    public float tempEndTime;

    //Movement and Attack Phases
    public bool isPhase1 = false;
    public bool isPhase2 = false;
    public bool isPhase3 = false;

    public Slider faraHealthSlider;
    public Slider faraWingHPSlider;

  

    #region End Phase Variables
    //End Phase
    [Header("End Phase")]
    public GameObject endPortal;
    public GameObject cinematicCameraPivot;
    public GameObject playerObj;
    public GameObject HudCanvas;
    public GameObject endingCanvas;

    public Image whiteScreen;
    public Text winningText;

    public Transform endPortalLookPoint;

    public float cameraRotateSpd;
    //public float playerLerpSpd;
    //public float playerLerpStr;
    public float playerLerpDelay;

    //End Phase Booleans
    public bool changeToCinematic = false;
    public bool rotateCameraAlready = false;
    public bool isPortalComeOut = false;


    #endregion

    AsyncOperation asyncOp;


    public static FarallonPhasesController instance { get; set; }
	// Use this for initialization
	void Start () {
        instance = this;
        distanceRoar.Play();
        faraHealthSlider.value = FarallonController.instance.currHealth;
        faraWingHPSlider.value = FarallonController.instance.currWingHealth;

        asyncOp = SceneManager.LoadSceneAsync("MainMenu");
        asyncOp.allowSceneActivation = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (currentPhase == Phases.StartPhase)
        //{

        //}

        faraHealthSlider.value = FarallonController.instance.currHealth;
        faraWingHPSlider.value = FarallonController.instance.currWingHealth;

        if (FarallonController.instance.currFaraState == FarallonController.FarallonStates.Dying)
        {
            currentPhase = Phases.EndPhase;
        }

        if (currentPhase == Phases.EndPhase)
        {
            //tempEndTime += Time.deltaTime;

            //if (tempEndTime > endTime)
            //{
            //    asyncOp.allowSceneActivation = true;
            //}
        }

        switch (currentPhase)
        {
            case Phases.StartPhase:
                {
                    tempStartPhaseTime += Time.deltaTime;



                    if (tempStartPhaseTime > startPhaseTime)
                    {
                        BattleMusic.SetActive(true);
                        tempStartPhaseTime = 0;
                        currentPhase = Phases.Phase1;
                    }
                }
                break;

            case Phases.Phase1:
                {
                    isPhase1 = true;
                }
                break;

            case Phases.EndPhase:
                {
                    //tempEndTime += Time.deltaTime;

                    if(changeToCinematic == false)
                    {
                        cinematicCameraPivot.transform.position = playerObj.transform.GetChild(0).transform.position;
                        cinematicCameraPivot.transform.rotation = playerObj.transform.GetChild(0).transform.rotation;
                        cinematicCameraPivot.SetActive(true);
                        playerObj.SetActive(false);
                        HudCanvas.SetActive(false);
                        endingCanvas.SetActive(true);
                        whiteScreen.CrossFadeAlpha(0.0f, 0.001f, false);
                        winningText.CrossFadeAlpha(0.0f, 0.001f, false);
                        changeToCinematic = true;
                    }

                    if(changeToCinematic == true)
                    {
                        tempEndTime += Time.deltaTime;

                        if(tempEndTime > 4)
                        {
                            RotateTowardsTarget(endPortalLookPoint.position, cinematicCameraPivot);
                            if(tempEndTime > 7)
                            {
                                tempEndTime = 0;
                                rotateCameraAlready = true;
                            }
                        }
                    }

                    if(rotateCameraAlready == true)
                    {
                        tempEndTime += Time.deltaTime;

                        endPortal.SetActive(true);

                        if(tempEndTime > 3)
                        {
                            tempEndTime = 0;
                            isPortalComeOut = true;                
                        }
                    }

                    if(isPortalComeOut == true)
                    {
                        //float lerpValue = playerLerpStr / playerLerpSpd;
                        //lerpValue = Mathf.Sin(lerpValue * Mathf.PI * 0.5f);
                        tempEndTime += Time.deltaTime;

                        float lerpValue = Time.deltaTime / playerLerpDelay;
                        lerpValue = Mathf.Sin(lerpValue * Mathf.PI * 0.5f);

                        cinematicCameraPivot.transform.position = Vector3.Lerp(cinematicCameraPivot.transform.position, endPortal.transform.position, lerpValue);

                        if(tempEndTime > 3)
                        {
                            whiteScreen.CrossFadeAlpha(1f, 5, false);
                            winningText.CrossFadeAlpha(1f, 10f, false);
                        }
                        
                    }
                   


                }
                break;
        }
	}


    public void RotateTowardsTarget(Vector3 target, GameObject objToRotate)
    {
        Vector3 targetDir = target - objToRotate.transform.position;
        //targetDir.y = 0;
        float step = cameraRotateSpd * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(objToRotate.transform.forward, targetDir, step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);
        objToRotate.transform.rotation = Quaternion.LookRotation(newDir);
        //if(objToRotate.transform.eulerAngles == newDir)
        //{
        //    rotateCameraAlready = true;
        //    tempEndTime = 0;
        //}
    }

}
