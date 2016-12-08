using UnityEngine;
using System.Collections;

public class OutpostManager : MonoBehaviour
{
    public GameObject[] minions;
    public GameObject defaultCircle;
    public GameObject capturedCircle;

    //public bool isCaptured = false;
    private bool allDied = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.name == "Outpost 1")
        {
            if (GameController.gameController.outpost1Captured)
            {
                defaultCircle.SetActive(false);
                capturedCircle.SetActive(true);
            }
        }
        else if (this.gameObject.name == "Outpost 2")
        {
            if (GameController.gameController.outpost2Captured)
            {
                defaultCircle.SetActive(false);
                capturedCircle.SetActive(true);
            }
        }
        else if (this.gameObject.name == "Outpost 3")
        {
            if (GameController.gameController.outpost3Captured)
            {
                defaultCircle.SetActive(false);
                capturedCircle.SetActive(true);
            }
        }
        else if (this.gameObject.name == "Outpost 4")
        {
            if (GameController.gameController.outpost4Captured)
            {
                defaultCircle.SetActive(false);
                capturedCircle.SetActive(true);
            }
        }

        if (defaultCircle.activeSelf)
        {
            for (int i = 0; i < minions.Length; i++)
            {
                allDied = false;
                if (minions[i] == null)
                {
                    allDied = true;
                }
                else
                {
                    break;
                }
            }

            if (allDied)
            {
                if (this.gameObject.name == "Outpost 1")
                {
                    GameController.gameController.outpost1Captured = true;
                }
                else if (this.gameObject.name == "Outpost 2")
                {
                    GameController.gameController.outpost2Captured = true;
                }
                else if (this.gameObject.name == "Outpost 3")
                {
                    GameController.gameController.outpost3Captured = true;
                }
                else if (this.gameObject.name == "Outpost 4")
                {
                    GameController.gameController.outpost4Captured = true;
                }
                defaultCircle.SetActive(false);
                capturedCircle.SetActive(true);
            }
        }
    }
}
