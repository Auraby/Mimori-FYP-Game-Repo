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
    AsyncOperation asyncOp;

    // Use this for initialization
    void Start () {
        //GameOver Screen
        gameoverBlackPanel.canvasRenderer.SetAlpha(0.0f);
        gameoverText.canvasRenderer.SetAlpha(0.0f);
        gameoverTextSubtitle.canvasRenderer.SetAlpha(0.0f);
        //Async
        asyncOp = SceneManager.LoadSceneAsync("MainMenu");
        asyncOp.allowSceneActivation = false;
        
    }
	
	// Update is called once per frame
	void Update () {
        #region Lose Conditions
        //Lose Conditions here
        if (Health.instance.currentHealth <= 0)
        {
            loseGame = true;
            playerDie = true;
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
            gameoverTime += Time.deltaTime;

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

            if (gameoverTime > gameOverWaitTime)
            {
                //load checkpoint here(after a while) (if want do buttons then change the code)
                asyncOp.allowSceneActivation = true;
            }

        }
        #endregion
    }
}
