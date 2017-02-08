using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenuController : MonoBehaviour {

    //TitleScreen Buttons
    [Header("Main Menu Buttons")]
    public RectTransform playBtn;
	public RectTransform continueBtn;
    public RectTransform optionBtn;
    public RectTransform creditBtn;
    public RectTransform exitBtn;

	public Button continueBtnGUI;

    //OptionScreen Buttons
    [Header("Option Menu Buttons")]
    public RectTransform optionsMenu;

    //lerp speed and time
    [Header("Lerp Values")]
    public float lerpSpeed;
    public float lerpStrength;
    float time;

    //bool
    [Header("Bool Values")]
    bool anyKeyPressed = false;

    bool playMMMoveinBool = false;
    bool playMMMoveoutBool = false;

    bool playOMMoveInBool = false;
    bool playOMMoveOutBool = false;

    //Text blinking
    [Header("Text")]
    public Text pressAnyKeyText;
    //public float blinkInterval = 1f;
    //public float startDelay = 0.5f;
    //public bool currentState = true;
    //public bool defaultState = true;
    //bool isBlinking = false;

    //Transition postition values
    [Header("Transition Position Values")]
    public float playBtnXPos;
	public float continueBtnXPos;
    public float optionBtnXPos;
    public float creditBtnXPos;
    public float exitBtnXPos;
    public float optionsMenuXPos;

    //Async operatiosn
    AsyncOperation aSyncOp;


	// Use this for initialization
	void Start () {

        playBtn.anchoredPosition = new Vector2(-265, playBtn.anchoredPosition.y);

		continueBtn.anchoredPosition = new Vector2 (-265, continueBtn.anchoredPosition.y);

        optionBtn.anchoredPosition = new Vector2(-265, optionBtn.anchoredPosition.y);

        creditBtn.anchoredPosition = new Vector2(-265, creditBtn.anchoredPosition.y);

        exitBtn.anchoredPosition = new Vector2(-265, exitBtn.anchoredPosition.y);

        optionsMenu.anchoredPosition = new Vector2(335, optionsMenu.anchoredPosition.y);

        aSyncOp = SceneManager.LoadSceneAsync("Gate of Telluris");
        aSyncOp.allowSceneActivation = false;
	}
	
	// Update is called once per frame
	void Update () {
		//Check if there's a save file
		//if yes, enable continue button
		if (File.Exists (Application.persistentDataPath + "/PlayerInfo.mi")) {
			continueBtnGUI.interactable = true;
		} else {
			continueBtnGUI.interactable = false;
		}


        if (playMMMoveoutBool == true)
        {
            time += Time.deltaTime;
            playMMButtonsMoveOutAnimation();

            if (time > 3)
            {
                time = 0;
                playMMMoveoutBool = false;
            }
        }

        if(playMMMoveinBool == true)
        {
            time += Time.deltaTime;
            playMMButtonsMoveInAnimation();

            if (time > 3)
            {
                time = 0;
                playMMMoveinBool = false;
            }
        }

        if(playOMMoveInBool == true)
        {
            time += Time.deltaTime;
            playOMMoveInAnimation();

            if (time > 3)
            {
                time = 0;
                playOMMoveInBool = false;
            }
        }

        if (playOMMoveOutBool == true)
        {
            time += Time.deltaTime;
            playOMMoveOutAnimation();

            if (time > 3)
            {
                time = 0;
                playOMMoveOutBool = false;
            }
        }

        if (Input.anyKey && anyKeyPressed == false)
        {
            anyKeyPressed = true;
            enterMenuButtonPressed();
        }

        //if (anyKeyPressed == false)
        //{
        //    InvokeRepeating("fadeText", 1, 1);
        //}

       // do
       // {
         

        //} while (anyKeyPressed == false);
      
        
	}

    public void fadeText()
    {
        if (pressAnyKeyText.color.a == 1)
        {

            pressAnyKeyText.CrossFadeAlpha(0.0f, 1f, false);
        }
        else
        {
            pressAnyKeyText.CrossFadeAlpha(1.0f, 1f, false);
        }
    }

    #region Button Pressed Functions
    public void backToMainMenuPressed()
    {
        MainMenuCameraController.instance.startMoveCameraToTitleScreen();
        playMMMoveinBool = true;
        playMMMoveoutBool = false;

        playOMMoveOutBool = true;
        playOMMoveInBool = false;

        time = 0;

        GetComponent<AudioSource>().Play();
    }

    public void optionsButtonPressed()
    {
        MainMenuCameraController.instance.startMoveCameraToOptions();
        playMMMoveoutBool = true;
        playMMMoveinBool = false;

        playOMMoveInBool = true;
        playOMMoveOutBool = false;

        time = 0;

        GetComponent<AudioSource>().Play();
    }

    public void enterMenuButtonPressed()
    {
        playMMMoveinBool = true;
        playMMMoveoutBool = false;

        pressAnyKeyText.CrossFadeAlpha(0.0f, 1f, false);

        MainMenuCameraController.instance.startMoveCameraToTitleScreen();
    }

    public void playGameButtonPressed()
    {
        //SceneManager.LoadScene("Mimori");
		//If a save file exists, delete it
		GameController.gameController.Delete();
        aSyncOp.allowSceneActivation = true;

        GetComponent<AudioSource>().Play();
    }

    public void exitButtonPressed()
    {
        Application.Quit();
    }

    #endregion

    #region Buttons Animation Update Functions
    public void playMMButtonsMoveOutAnimation()
    {
        float lerpValue = lerpStrength / lerpSpeed;
        lerpValue = Mathf.Sin(lerpValue * Mathf.PI * 0.5f);

        playBtn.anchoredPosition = new Vector2(Mathf.Lerp(playBtn.anchoredPosition.x, -265, lerpValue), playBtn.anchoredPosition.y);

		if (time > 0.1)
		{
			continueBtn.anchoredPosition = new Vector2(Mathf.Lerp(continueBtn.anchoredPosition.x, -265, lerpValue), continueBtn.anchoredPosition.y);
		}
        if (time > 0.2)
        {
            optionBtn.anchoredPosition = new Vector2(Mathf.Lerp(optionBtn.anchoredPosition.x, -265, lerpValue), optionBtn.anchoredPosition.y);
        }
        if (time > 0.3)
        {
            creditBtn.anchoredPosition = new Vector2(Mathf.Lerp(creditBtn.anchoredPosition.x, -265, lerpValue), creditBtn.anchoredPosition.y);
      
        }
        if (time > 0.4)
        {
            exitBtn.anchoredPosition = new Vector2(Mathf.Lerp(exitBtn.anchoredPosition.x, -265, lerpValue), exitBtn.anchoredPosition.y);
           
        }

    }

    public void playMMButtonsMoveInAnimation()
    {
        float lerpValue = lerpStrength / lerpSpeed;
        lerpValue = Mathf.Sin(lerpValue * Mathf.PI * 0.5f);

        playBtn.anchoredPosition = new Vector2(Mathf.Lerp(playBtn.anchoredPosition.x, playBtnXPos, lerpValue), playBtn.anchoredPosition.y);

		if (time > 0.1)
		{
			continueBtn.anchoredPosition = new Vector2(Mathf.Lerp(continueBtn.anchoredPosition.x, optionBtnXPos, lerpValue), continueBtn.anchoredPosition.y);
		}
        if (time > 0.2)
        {
            optionBtn.anchoredPosition = new Vector2(Mathf.Lerp(optionBtn.anchoredPosition.x, optionBtnXPos, lerpValue), optionBtn.anchoredPosition.y);
        }
        if (time > 0.3)
        {
            creditBtn.anchoredPosition = new Vector2(Mathf.Lerp(creditBtn.anchoredPosition.x, creditBtnXPos, lerpValue), creditBtn.anchoredPosition.y);
           
        }
        if (time > 0.4)
        {
            exitBtn.anchoredPosition = new Vector2(Mathf.Lerp(exitBtn.anchoredPosition.x, exitBtnXPos, lerpValue), exitBtn.anchoredPosition.y);
            
        }
    }

    public void playOMMoveInAnimation()
    {
        float lerpValue = lerpStrength / lerpSpeed;
        lerpValue = Mathf.Sin(lerpValue * Mathf.PI * 0.5f);

        if(time > 0.5)
        optionsMenu.anchoredPosition = new Vector2(Mathf.Lerp(optionsMenu.anchoredPosition.x, optionsMenuXPos, lerpValue), optionsMenu.anchoredPosition.y);
      
    }

    public void playOMMoveOutAnimation()
    {
        float lerpValue = lerpStrength / lerpSpeed;
        lerpValue = Mathf.Sin(lerpValue * Mathf.PI * 0.5f);

        optionsMenu.anchoredPosition = new Vector2(Mathf.Lerp(optionsMenu.anchoredPosition.x, 335, lerpValue), optionsMenu.anchoredPosition.y);
        
    }
    #endregion
}
