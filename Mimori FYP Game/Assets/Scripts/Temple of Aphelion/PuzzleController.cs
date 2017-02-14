using UnityEngine;
using System.Collections;

public class PuzzleController : MonoBehaviour {
    //puzzle answer : 4139
    //public GameObject plate1, plate2, plate3, plate4,plate5,plate6,plate7,plate8,plate9;
    public GameObject[] plates;
    public GameObject door;
    public AudioClip opening;
    public GameObject portal;

    public static bool on1 = false;
    public static bool on2 = false;
    public static bool on3 = false;
    public static bool on4 = false;
    public static bool checkPuzzle = false;

    bool passChecked = false;
    float resetCD = 2f;
    bool resetted = false;
    bool soundPlayed = false;

    int[] set1 = new int[] { 1, 4, 2, 3 };
    public static int[] userInput = new int[] { 0, 0, 0, 0 };
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        CheckPuzzleRightWrong();
        if (plates[3].GetComponent<Renderer>().material.color == Color.green && door.transform.position.y < 60) {
            transform.Translate(Vector3.up * 3.9f * Time.deltaTime);
            if (!soundPlayed) {
                GetComponent<AudioSource>().PlayOneShot(opening, 10);
                soundPlayed = true;
            }
            
        }
        ResetPlates();

        if (GameController.gameController.ishiraAbsorbed)
        {
            portal.SetActive(true);
        }
        else {
            portal.SetActive(false);
        }
    }

    void ResetPlates() {
        if (plates[3].GetComponent<Renderer>().material.color == Color.red)
        {
            resetCD -= Time.deltaTime;
        }

        if (resetCD <= 0) {
            resetCD = 0;
        }

        if (resetCD == 0) {
            for (int i = 0; i < plates.Length; i++)
            {
                plates[i].GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            }
            passChecked = false;
            resetCD = 2f;
            on1 = false;
            on2 = false;
            on3 = false;
            on4 = false;
            checkPuzzle = false;
        }
    }

    void CheckPuzzleRightWrong() {
        if (checkPuzzle && !passChecked) {
            if (plates[3].GetComponent<Renderer>().material.color == Color.yellow &&
                plates[0].GetComponent<Renderer>().material.color == Color.yellow &&
                plates[2].GetComponent<Renderer>().material.color == Color.yellow &&
                plates[8].GetComponent<Renderer>().material.color == Color.yellow)
            {
                for (int i = 0; i < plates.Length; i++)
                {
                    plates[i].GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                }
                passChecked = true;
            }
            else {
                for (int i = 0; i < plates.Length; i++)
                {
                    plates[i].GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                }
                passChecked = true;
            }
        }
    }
}
