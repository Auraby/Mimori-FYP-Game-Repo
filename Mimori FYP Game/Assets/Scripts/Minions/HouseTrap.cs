using UnityEngine;
using System.Collections;

public class HouseTrap : MonoBehaviour
{
    public GameObject chest;
    public Transform player;
    public GameObject monster;
    public GameObject spawn1, spawn2;

    bool spawned = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!chest.activeSelf && !spawned)
        {
            GameObject spawnedMon1 = (GameObject)Instantiate(monster, spawn1.transform.position, spawn1.transform.rotation);
            GameObject spawnedMon2 = (GameObject)Instantiate(monster, spawn2.transform.position, spawn2.transform.rotation);

            spawnedMon1.GetComponent<MeleeMinionFSM>().patrolPoints[0] = spawn1;
            spawnedMon2.GetComponent<MeleeMinionFSM>().patrolPoints[0] = spawn2;

            spawnedMon1.GetComponent<MeleeMinionFSM>().boundPoint = spawn1;
            spawnedMon2.GetComponent<MeleeMinionFSM>().boundPoint = spawn2;

            spawnedMon1.GetComponent<MeleeMinionFSM>().player = player;
            spawnedMon2.GetComponent<MeleeMinionFSM>().player = player;

            spawned = true;
        }
        if (GameController.gameController.houseTrapActivated)
        {
            chest.SetActive(false);
        }
    }
}
