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

    [Header("OpeningSequenceVariables")]
    public GameObject openingSequence;
    public Image openingBlackScreen;
    public Image openingblackTopPanel, openingblackBottomPanel;
    public Text openingText;
    public float startTime = 0f;

    [Header("GameOverScreen Variables")]
    public GameObject gameOverScreen;
    public Image gameoverBlackPanel;
    public Text gameoverText;
    public Text gameoverTextSubtitleWall;
    public Text gameoverTextSubitilePlayer;

    public float gameOverWaitTime = 6f;
    public float gameoverTime = 0f;

    public bool playerDied = false;
    public bool wallDestroyed = false;

    public static Level1Controller instance { get; set; }
	// Use this for initialization
	void Start () {

        instance = this;
        //Starting Cinematics
        objectiveSlider.gameObject.SetActive(false);

        //GameOver Screen
        gameoverBlackPanel.canvasRenderer.SetAlpha(0.0f);
        gameoverText.canvasRenderer.SetAlpha(0.0f);
        gameoverTextSubtitleWall.canvasRenderer.SetAlpha(0.0f);
        gameoverTextSubitilePlayer.canvasRenderer.SetAlpha(0.0f);


        //gameOverScreen.SetActive(false);
        levelProgress = LevelState.Start;
        currentWallHealth = wallMaxHealth;
        objectiveSlider.value = currentWallHealth;
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
                   if(startTime > 7)
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
                }
                break;

            case LevelState.Lose:
                {
                    gameoverTime += Time.deltaTime;
                    if (playerDied == true)
                    {
                           // gameOverScreen.SetActive(true);
                            gameoverBlackPanel.CrossFadeAlpha(1, 2, false);
                            gameoverText.CrossFadeAlpha(1, 3, false);
                            gameoverTextSubitilePlayer.CrossFadeAlpha(1, 5, false);
                        
                        if(gameoverTime > gameOverWaitTime)
                        {
                            //load checkpoint here
                            //SceneManager.LoadScene("Gate of Telluris");
                        }
                       
                    }

                    if(wallDestroyed == true)
                    {
                        gameoverBlackPanel.CrossFadeAlpha(1, 2, false);
                        gameoverText.CrossFadeAlpha(1, 3, false);
                        gameoverTextSubtitleWall.CrossFadeAlpha(1, 5, false);
                    }
                }
                break;
        }
        
	}
}
