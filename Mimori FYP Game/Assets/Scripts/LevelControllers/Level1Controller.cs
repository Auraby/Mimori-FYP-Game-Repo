using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Level1Controller : MonoBehaviour {

    public enum LevelState { Start, Playing, Win, Lose }

    public LevelState levelProgress;

    [Header("Win Lose Variables")]
    public Text currObjective;
    public Slider objectiveSlider;

    public float wallMaxHealth = 100;
    public float currentWallHealth;

    public bool playerDied = false;
    public bool wallDestroyed = false;

    public FracturedObject fracturedGate;
    public FracturedObject fracturedMetalGate;
    //public fracture

    [Header("Boss Information")]
    public GameObject bossInfoPanel;
    public Text bossNameText;
    public Image bossImage;
    public Slider bossHealthSlider;

    public GameObject tempPortal;

    [Header("OpeningSequenceVariables")]
    public GameObject openingSequence;
    public Image openingBlackScreen;
    //public Image openingblackTopPanel, openingblackBottomPanel;
    public Text openingText;
    public float startTime;
    public float waitTime;

    

    [HideInInspector]
    public AsyncOperation aSyncOp;

    public static Level1Controller instance { get; set; }
	// Use this for initialization
	void Start () {

        instance = this;
        //Starting Cinematics
        objectiveSlider.gameObject.SetActive(false);
        bossInfoPanel.SetActive(false);

        tempPortal.SetActive(false);

        bossHealthSlider.maxValue = EnmarController.instance.enmarMaxHealth;

        //gameOverScreen.SetActive(false);
        levelProgress = LevelState.Start;
        currentWallHealth = wallMaxHealth;
        bossHealthSlider.value = EnmarController.instance.enmarCurrentHealth;

        aSyncOp = SceneManager.LoadSceneAsync("Mimori");
        aSyncOp.allowSceneActivation = false;
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(startTime+","+DialogueManager.enmarDialogueCount);
        bossHealthSlider.value = EnmarController.instance.enmarCurrentHealth;

        //Enmar Health
        if (EnmarController.instance.enmarCurrentHealth <= 0)
        {
            levelProgress = LevelState.Win;
        }

        if (EnmarController.enmarDied)
        {
            GameController.gameController.fightingBoss = false;
        }
        else {
            GameController.gameController.fightingBoss = true;
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
                        //openingblackTopPanel.CrossFadeAlpha(0, 3, false);
                        //openingblackBottomPanel.CrossFadeAlpha(0, 3, false);
                        openingText.CrossFadeAlpha(0, 2, false);
                    }
                   //if(startTime > waitTime)
                   // {
                   //     levelProgress = LevelState.Playing;
                   //     startTime = 0;
                   //     EnmarController.instance.enmarState = EnmarController.FSMState.Walking;
                   // }
                }
                break;

            case LevelState.Playing:
                {
                    bossNameText.text = "Prevent Enmar from destroying the gate:";
                    currObjective.text = "Prevent Enmar from destroying the gate:";
                    objectiveSlider.gameObject.SetActive(true);

                    bossInfoPanel.SetActive(true);             
                }
                break;

            case LevelState.Win:
                {
                    currObjective.text = "Objective Completed";
                    objectiveSlider.gameObject.SetActive(false);
                    bossInfoPanel.SetActive(false);
                    tempPortal.SetActive(true);
                }
                break;

            case LevelState.Lose:
                {
                    EnmarController.instance.enmarState = EnmarController.FSMState.GameOver;
                    StartCoroutine(WaitToExplode(10));
                    //fracturedMetalGate.Explode(fracturedMetalGate.gameObject.transform.position, 10);
                    //fracturedGate.CollapseChunks();
                    //fracturedMetalGate.CollapseChunks();
                }
                break;
        }
        
	}

    private IEnumerator WaitToExplode(float sec)
    {
        yield return new WaitForSeconds(sec);
        fracturedGate.Explode(fracturedGate.gameObject.transform.position, 10);
    }
}
