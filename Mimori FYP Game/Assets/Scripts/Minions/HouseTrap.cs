using UnityEngine;
using System.Collections;

public class HouseTrap : MonoBehaviour
{
    public GameObject chest;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gameController.houseTrapActivated)
        {
            chest.SetActive(false);
        }
    }
}
