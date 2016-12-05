using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public string currentScene;
	public static bool loadingGame = false;
	public static GameController gameController;
	//Player Position
	public float playerPositionX;
	public float playerPositionY;
	public float playerPositionZ;
    //Player Rotation

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

    // Use this for initialization
	void Awake(){
		if (gameController == null) {
			DontDestroyOnLoad (gameObject);
			gameController = this;
		} else if (gameController != this) {
			Destroy (gameObject);
		}
	}

	void Start () {
        //GameOver Screen
        gameoverBlackPanel.canvasRenderer.SetAlpha(0.0f);
        gameoverText.canvasRenderer.SetAlpha(0.0f);
        gameoverTextSubtitle.canvasRenderer.SetAlpha(0.0f);
	}

	void Update(){
		if (SceneManager.GetActiveScene ().name != "MainMenu") {
			currentScene = SceneManager.GetActiveScene ().name;
        }

        #region Lose Conditions
        //Lose Conditions here
        if (Health.instance.currentHealth <= 0)
        {
            loseGame = true;
            playerDie = true;
        }

        if (Level1Controller.instance.currentWallHealth <= 0)
        {
            loseGame = true;
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

            if (Level1Controller.instance.wallDestroyed == true)
            {
                gameoverTextSubtitle.text = "Wall Destroyed";
            }

            if (gameoverTime > gameOverWaitTime)
            {
                //load checkpoint here(after a while) (if want do buttons then change the code
                //SceneManager.LoadScene("Gate of Telluris");
            }

        }
        #endregion


        //Debug.Log (currentScene);
	}

	public void Save(){
		//Create a BinaryFormatter & a file
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/PlayerInfo.mi");

		//Create an object to save the data to
		PlayerData pData = new PlayerData();
		pData.playerPosX = playerPositionX;
		pData.playerPosY = playerPositionY;
		pData.playerPosZ = playerPositionZ;

		pData.curScene = currentScene;

		//Write the object to the file & close it
		bf.Serialize(file,pData);
		file.Close ();
	}

	public void Load(){
		if(File.Exists(Application.persistentDataPath + "/PlayerInfo.mi")){
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/PlayerInfo.mi", FileMode.Open);

			PlayerData pData = (PlayerData)bf.Deserialize (file);
			file.Close ();

			playerPositionX = pData.playerPosX;
			playerPositionY = pData.playerPosY;
			playerPositionZ = pData.playerPosZ;

			currentScene = pData.curScene;
		}
	}

	public void Delete(){
		if(File.Exists(Application.persistentDataPath + "/PlayerInfo.mi")){
			File.Delete (Application.persistentDataPath + "/PlayerInfo.mi");
		}
	}
}

[Serializable]
class PlayerData {
	public string curScene;
	//Player Position
	public float playerPosX;
	public float playerPosY;
	public float playerPosZ;
	//Player Rotation
}
