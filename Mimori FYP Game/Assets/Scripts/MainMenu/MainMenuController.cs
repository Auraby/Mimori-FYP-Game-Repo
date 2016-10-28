using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    //TitleScreen Buttons
    [Header("Main Menu Buttons")]
    public RectTransform playBtn;
    public RectTransform optionBtn;
    public RectTransform creditBtn;
    public RectTransform exitBtn;

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
    public float optionBtnXPos;
    public float creditBtnXPos;
    public float exitBtnXPos;
    public float optionsMenuXPos;


	// Use this for initialization
	void Start () {

        playBtn.anchoredPosition = new Vector2(-265, playBtn.anchoredPosition.y);

        optionBtn.anchoredPosition = new Vector2(-265, optionBtn.anchoredPosition.y);

        creditBtn.anchoredPosition = new Vector2(-265, creditBtn.anchoredPosition.y);

        exitBtn.anchoredPosition = new Vector2(-265, exitBtn.anchoredPosition.y);

        optionsMenu.anchoredPosition = new Vector2(335, optionsMenu.anchoredPosition.y);
	}
	
	// Update is called once per frame
	void Update () {

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
    }

    public void optionsButtonPressed()
    {
        MainMenuCameraController.instance.startMoveCameraToOptions();
        playMMMoveoutBool = true;
        playMMMoveinBool = false;

        playOMMoveInBool = true;
        playOMMoveOutBool = false;

        time = 0;
    }

    public void enterMenuButtonPressed()
    {
        playMMMoveinBool = true;
        playMMMoveoutBool = false;

        pressAnyKeyText.CrossFadeAlpha(0.0f, 1f, false);
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
            optionBtn.anchoredPosition = new Vector2(Mathf.Lerp(optionBtn.anchoredPosition.x, -265, lerpValue), optionBtn.anchoredPosition.y);
        }
        if (time > 0.2)
        {
            creditBtn.anchoredPosition = new Vector2(Mathf.Lerp(creditBtn.anchoredPosition.x, -265, lerpValue), creditBtn.anchoredPosition.y);
      
        }
        if (time > 0.3)
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
            optionBtn.anchoredPosition = new Vector2(Mathf.Lerp(optionBtn.anchoredPosition.x, optionBtnXPos, lerpValue), optionBtn.anchoredPosition.y);
        }
        if (time > 0.2)
        {
            creditBtn.anchoredPosition = new Vector2(Mathf.Lerp(creditBtn.anchoredPosition.x, creditBtnXPos, lerpValue), creditBtn.anchoredPosition.y);
           
        }
        if (time > 0.3)
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
