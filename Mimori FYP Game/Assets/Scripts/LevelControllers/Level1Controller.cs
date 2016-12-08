using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Level1Controller : MonoBehaviour {

    public enum LevelState { Start, Playing, Win, Lose}

    public LevelState levelProgress;

    public Text currObjective;
    public Slider objectiveSlider;

    public float wallMaxHealth = 100;
    public float currentWallHealth;

    public GameObject tempPortal;

    [Header("OpeningSequenceVariables")]
    public GameObject openingSequence;
    public Image openingBlackScreen;
    public Image openingblackTopPanel, openingblackBottomPanel;
    public Text openingText;
    public float startTime;
    public float waitTime;

    public bool playerDied = false;
    public bool wallDestroyed = false;

    [HideInInspector]
    public AsyncOperation aSyncOp;

    public static Level1Controller instance { get; set; }
	// Use this for initialization
	void Start () {

        instance = this;
        //Starting Cinematics
        objectiveSlider.gameObject.SetActive(false);

        tempPortal.SetActive(false);


        //gameOverScreen.SetActive(false);
        levelProgress = LevelState.Start;
        currentWallHealth = wallMaxHealth;
        objectiveSlider.value = currentWallHealth;

        aSyncOp = SceneManager.LoadSceneAsync("Mimori");
        aSyncOp.allowSceneActivation = false;
    }
	
	// Update is called once per frame
	void Update () {

        objectiveSlider.value = currentWallHealth;

        //Enmar Health
        if (EnmarController.instance.enmarCurrentHealth <= 0)
        {
            levelProgress = LevelState.Win;
        }

        //Player health
        if(Health.instance.currentHealth <= 0)
        {
            playerDied = true;
            levelProgress = LevelState.Lose;
        }

        //Gate health
        if(currentWallHealth <= 0)
        {
            wallDestroyed = true;
            levelProgress = LevelState.Lose;
        }


        switch (levelProgress)
        {
            case LevelState.Start:
                {
                    startTime += Time.deltaTime;
                    if(startTime > 5)
                    {
                        openingBlackScreen.CrossFadeAlpha(0, 2, false);
                        openingblackTopPanel.CrossFadeAlpha(0, 3, false);
                        openingblackBottomPanel.CrossFadeAlpha(0, 3, false);
                        openingText.CrossFadeAlpha(0, 2, false);
                    }
                   if(startTime > waitTime)
                    {
                        levelProgress = LevelState.Playing;
                        startTime = 0;
                        EnmarController.instance.enmarState = EnmarController.FSMState.Walking;
                    }
                }
                break;

            case LevelState.Playing:
                {
                    currObjective.text = "Prevent Enmar from destroying the gate:";
                    objectiveSlider.gameObject.SetActive(true);                
                }
                break;

            case LevelState.Win:
                {
                    currObjective.text = "Objective Completed";
                    objectiveSlider.gameObject.SetActive(false);
                    tempPortal.SetActive(true);
                }
                break;

            case LevelState.Lose:
                {
                    
                }
                break;
        }
        
	}
}
