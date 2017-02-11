using UnityEngine;
using System.Collections;

public class StartZoltran : MonoBehaviour {
    public GameObject entranceBarrier, templeBarrier,zoltran, soulOfZoltran;

    public static bool zoltranDied = false;
    public static bool zoltranStart = false;

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
        if(zoltranDied)
        {
            if (!soulOfZoltran.activeSelf && !GameController.gameController.zoltranAbsorbed) {
                soulOfZoltran.gameObject.SetActive(true);
            }
        }
        if (entranceBarrier.activeSelf && GameController.gameController.zoltranAbsorbed)
        {
            entranceBarrier.SetActive(false);
            templeBarrier.SetActive(false);
        }
        if (zoltranStart && !zoltranDied) {
            if (!entranceBarrier.activeSelf) {
                GameController.gameController.fightingBoss = true;
                entranceBarrier.gameObject.SetActive(true);
                templeBarrier.gameObject.SetActive(true);
                zoltran.gameObject.SetActive(true);
            }
        }
	}
}
