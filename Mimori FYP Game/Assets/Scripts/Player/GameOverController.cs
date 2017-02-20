using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {
    #region Game Over Variables
    [Header("GameOverScreen Variables")]
    public GameObject gameOverScreen;
    public Image gameoverBlackPanel;
    public Text gameoverText;
    public Text gameoverTextSubtitle;

    public float gameOverWaitTime = 6f;
    public float gameoverTime = 0f;

    //Make this true if game over from anything
    public bool loseGame = false;
    public bool playerDie = false;
    #endregion
    //Async
    //AsyncOperation asyncOp;

    // Use this for initialization
    void Start () {
        //GameOver Screen
        gameoverBlackPanel.canvasRenderer.SetAlpha(0.0f);
        gameoverText.canvasRenderer.SetAlpha(0.0f);
        gameoverTextSubtitle.canvasRenderer.SetAlpha(0.0f);
        //Async
        //asyncOp = SceneManager.LoadSceneAsync("MainMenu");
        //asyncOp.allowSceneActivation = false;
        
    }
	
	// Update is called once per frame
	void Update () {
        #region Lose Conditions
        //Lose Conditions here
        if (Health.instance.currentHealth <= 0)
        {
			if(Health.instance.gameObject.GetComponent<SkillTree>().unlockLifePassive2){
				if(gameObject.GetComponent<SkillTree>().deadoraliveactivated == false){
					gameObject.GetComponent<SkillTree>().deadoraliveactivated = true;
					gameObject.GetComponent<SkillTree>().StartCoroutine(gameObject.GetComponent<SkillTree>().PassiveDurations(gameObject.GetComponent<SkillTree>().deadoraliveactivated, gameObject.GetComponent<SkillTree>().deadoralivecd));
					Health.instance.currentHealth += Health.instance.maxhealth / 2;
					Health.instance.healthbarslider.value += Health.instance.maxhealth / 2;
				}
			}else{
            	loseGame = true;
            	playerDie = true;
			
			}
        }

        if (SceneManager.GetActiveScene().name == "Gate Of Telluris")
        {
            if (Level1Controller.instance.currentWallHealth <= 0)
            {
                loseGame = true;
            }
        }

        #endregion

        #region Gameover screen
        if (loseGame == true)
        {
            

            if (SceneManager.GetActiveScene().name == "Gate of Telluris")
            {
                gameoverTime += Time.deltaTime;
                if(gameoverTime > 20)
                {
                    gameoverBlackPanel.CrossFadeAlpha(1, 2, false);
                    gameoverText.CrossFadeAlpha(1, 3, false);
                    gameoverTextSubtitle.CrossFadeAlpha(1, 5, false);

                    // Change the subtitles according to how the game is lost
                    if (playerDie == true)
                    {
                        gameoverTextSubtitle.text = "You Died";
                    }

                    if (SceneManager.GetActiveScene().name == "Gate Of Telluris")
                    {
                        if (Level1Controller.instance.wallDestroyed == true)
                        {
                            gameoverTextSubtitle.text = "Wall Destroyed";
                        }
                    }

                    if (gameoverTime > 30)
                    {
                        //load checkpoint here(after a while) (if want do buttons then change the code)
                        //asyncOp.allowSceneActivation = true;
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        Debug.Log("Loaded level");
                    }
                }
                
            }

            else
            {
                gameoverTime += Time.deltaTime;

                gameoverBlackPanel.CrossFadeAlpha(1, 2, false);
                gameoverText.CrossFadeAlpha(1, 3, false);
                gameoverTextSubtitle.CrossFadeAlpha(1, 5, false);

                // Change the subtitles according to how the game is lost
                if (playerDie == true)
                {
                    gameoverTextSubtitle.text = "You Died";
                }

                //if (SceneManager.GetActiveScene().name == "Gate Of Telluris")
                //{
                //    if (Level1Controller.instance.wallDestroyed == true)
                //    {
                //        gameoverTextSubtitle.text = "Wall Destroyed";
                //    }
                //}

                if (gameoverTime > gameOverWaitTime)
                {
                    //load checkpoint here(after a while) (if want do buttons then change the code)
                    // asyncOp.allowSceneActivation = true;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }

           

        }
        #endregion
    }
}
