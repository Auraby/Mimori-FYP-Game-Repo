using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class DialogueManager : MonoBehaviour {
    public Text dText;
    public string[] puzzleDialogLines, passwordDialogueLines, forestStartDialogues, isaacBackstoryDialogues, zoltranBattleDialogues, 
        zoltranAfterBattleDialogues, templeExtDialogues;
    public int currentLine;
    public GameObject mainCanvas;
    public Image gameoverBlackPanel;
    public Text gameoverText, gameoverTextSubtitle;

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
        //if (this.gameObject.activeSelf) {
        //    Time.timeScale = 0;
        //    FirstPersonController.isPaused = true;
        //    mainCanvas.SetActive(false);
        //}

        
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
