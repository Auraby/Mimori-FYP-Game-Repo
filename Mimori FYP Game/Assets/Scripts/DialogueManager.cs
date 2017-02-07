using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class DialogueManager : MonoBehaviour {
    public Text dText;
    public string[] puzzleDialogLines, passwordDialogueLines;
    public int currentLine;
    public GameObject mainCanvas;
    public Image gameoverBlackPanel;
    public Text gameoverText, gameoverTextSubtitle;

    public static int templeIDialogueCount = 0;

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
        //Puzzle Dialogues
        if (currentLine >= puzzleDialogLines.Length && templeIDialogueCount == 0) {
            FirstPersonController.isPaused = false;
            Time.timeScale = 1;
            currentLine = 0;
            mainCanvas.SetActive(true);
            gameoverBlackPanel.canvasRenderer.SetAlpha(0.0f);
            gameoverText.canvasRenderer.SetAlpha(0.0f);
            gameoverTextSubtitle.canvasRenderer.SetAlpha(0.0f);
            templeIDialogueCount++;
            gameObject.SetActive(false);
        }
        //Pasword Dialogues
        if (currentLine >= passwordDialogueLines.Length && templeIDialogueCount == 1)
        {
            FirstPersonController.isPaused = false;
            Time.timeScale = 1;
            currentLine = 0;
            mainCanvas.SetActive(true);
            gameoverBlackPanel.canvasRenderer.SetAlpha(0.0f);
            gameoverText.canvasRenderer.SetAlpha(0.0f);
            gameoverTextSubtitle.canvasRenderer.SetAlpha(0.0f);
            templeIDialogueCount++;
            gameObject.SetActive(false);
        }

        //temple interior dialogue
        if (templeIDialogueCount == 0) {
            dText.text = puzzleDialogLines[currentLine];
        }
        else if (templeIDialogueCount == 1)
        {
            dText.text = passwordDialogueLines[currentLine];
        }
    }
}
