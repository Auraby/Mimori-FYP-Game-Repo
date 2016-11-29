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

    public GameObject gameOverScreen;
    public Image gameoverBlackPanel;
    public Text gameoverText;
    public Text gameoverTextSubtitle;

    public float gameOverWaitTime = 6f;
    public float gameoverTime = 0f;

    public bool playerDied = false;
    public bool wallDestroyed = false;

    public static Level1Controller instance { get; set; }
	// Use this for initialization
	void Start () {

        instance = this;
        gameoverBlackPanel.canvasRenderer.SetAlpha(0.0f);
        gameoverText.canvasRenderer.SetAlpha(0.0f);
        gameoverTextSubtitle.canvasRenderer.SetAlpha(0.0f);
        //gameOverScreen.SetActive(false);
        levelProgress = LevelState.Playing;
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
            case LevelState.Playing:
                {
                    currObjective.text = "Prevent Enmar from destroying the gate:";
                    
                }
                break;

            case LevelState.Win:
                {

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
                            gameoverTextSubtitle.CrossFadeAlpha(1, 5, false);
                        
                        if(gameoverTime > gameOverWaitTime)
                        {
                            //load checkpoint here
                            //SceneManager.LoadScene("Gate of Telluris");
                        }
                       
                    }

                    if(wallDestroyed == true)
                    {

                    }
                }
                break;
        }
        
	}
}
