using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class DialogueManager : MonoBehaviour {
    public Text dText;
    public string[] enmarBeforeDialogues, enmarAppearedDialogues, enmarDiedDialogues, puzzleDialogLines, passwordDialogueLines, 
        forestStartDialogues, isaacBackstoryDialogues, zoltranBattleDialogues, zoltranAfterBattleDialogues, templeExtDialogues;
    public int currentLine;
    public GameObject mainCanvas;
    public Image gameoverBlackPanel;
    public Text gameoverText, gameoverTextSubtitle;

    public static int enmarDialogueCount = 0;
    public static int templeIDialogueCount = 0;
    public static int forestDialogueCount = 2;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return)) {
            currentLine++;
        }

        if (SceneManager.GetActiveScene().name == "Gate of Telluris") {
            //GATE OF TELLURIS DIALOGUES
            //Before Enmar spawn dialogues
            if (currentLine >= enmarBeforeDialogues.Length && enmarDialogueCount == 0)
            {
                DialogueHandler();
                enmarDialogueCount++;
                Level1Controller.instance.levelProgress = Level1Controller.LevelState.Playing;
                Level1Controller.instance.startTime = 0;
                EnmarController.instance.enmarState = EnmarController.FSMState.Walking;
            }
            if (currentLine >= enmarAppearedDialogues.Length && enmarDialogueCount == 1)
            {
                DialogueHandler();
                enmarDialogueCount++;
            }
            if (currentLine >= enmarDiedDialogues.Length && enmarDialogueCount == 2)
            {
                DialogueHandler();
                enmarDialogueCount++;
            }

            if (enmarDialogueCount == 0)
            {
                dText.text = enmarBeforeDialogues[currentLine];
            }
            if (enmarDialogueCount == 1)
            {
                dText.text = enmarAppearedDialogues[currentLine];
            }
            if (enmarDialogueCount == 2)
            {
                dText.text = enmarDiedDialogues[currentLine];
            }
        }

        if (SceneManager.GetActiveScene().name == "Forest of Misery")
        {
            //FOREST DIALOGUES
            //Forest Start dialogues
            if (currentLine >= forestStartDialogues.Length && forestDialogueCount == 0)
            {
                DialogueHandler();
                forestDialogueCount++;
            }
            //Isaac backstory dialogues
            if (currentLine >= isaacBackstoryDialogues.Length && forestDialogueCount == 1)
            {
                DialogueHandler();
                forestDialogueCount++;
            }
            if (currentLine >= zoltranBattleDialogues.Length && forestDialogueCount == 2)
            {
                DialogueHandler();
                forestDialogueCount++;
            }
            if (currentLine >= zoltranAfterBattleDialogues.Length && forestDialogueCount == 3)
            {
                DialogueHandler();
                forestDialogueCount++;
            }
            if (currentLine >= templeExtDialogues.Length && forestDialogueCount == 4)
            {
                DialogueHandler();
                forestDialogueCount++;
            }

            if (forestDialogueCount == 0)
            {
                dText.text = forestStartDialogues[currentLine];
            }
            else if (forestDialogueCount == 1)
            {
                dText.text = isaacBackstoryDialogues[currentLine];
            }
            else if (forestDialogueCount == 2)
            {
                dText.text = zoltranBattleDialogues[currentLine];
            }
            else if (forestDialogueCount == 3)
            {
                dText.text = zoltranAfterBattleDialogues[currentLine];
            }
            else if (forestDialogueCount == 4)
            {
                dText.text = templeExtDialogues[currentLine];
            }
        }

        if (SceneManager.GetActiveScene().name == "Temple of Aphelion")
        {
            //TEMPLE DIALOGUES
            //Puzzle Dialogues
            if (currentLine >= puzzleDialogLines.Length && templeIDialogueCount == 0)
            {
                DialogueHandler();
                templeIDialogueCount++;
            }
            //Pasword Dialogues
            if (currentLine >= passwordDialogueLines.Length && templeIDialogueCount == 1)
            {
                DialogueHandler();
                templeIDialogueCount++;
            }

            if (templeIDialogueCount == 0)
            {
                dText.text = puzzleDialogLines[currentLine];
            }
            else if (templeIDialogueCount == 1)
            {
                dText.text = passwordDialogueLines[currentLine];
            }
        }
        
    }

    void DialogueHandler() {
        FirstPersonController.isPaused = false;
        Time.timeScale = 1;
        currentLine = 0;
        mainCanvas.SetActive(true);
        gameoverBlackPanel.canvasRenderer.SetAlpha(0.0f);
        gameoverText.canvasRenderer.SetAlpha(0.0f);
        gameoverTextSubtitle.canvasRenderer.SetAlpha(0.0f);
        gameObject.SetActive(false);
    }
}
