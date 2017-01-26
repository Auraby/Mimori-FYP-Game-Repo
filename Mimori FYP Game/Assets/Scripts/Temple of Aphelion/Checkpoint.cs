using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
    public GameObject player;

    public static Vector3 savedPos;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //if (this.gameObject.name == "Checkpoint 1")
        //{
        //    Debug.Log(player.transform.position);
        //}
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            if (this.gameObject.name == "Checkpoint 1")
            {
                savedPos = new Vector3(0, 56, 40);
            }

            if (this.gameObject.name == "Checkpoint 2")
            {
                savedPos = new Vector3(0, 56, 110);
            }
            if (this.gameObject.name == "Checkpoint 3")
            {
                savedPos = new Vector3(-40, 56, 329);
            }
            if (this.gameObject.name == "Checkpoint 4")
            {
                savedPos = new Vector3(0, 56, 470);
            }
            if (this.gameObject.name == "Checkpoint 5")
            {
                savedPos = new Vector3(63, 56, 295);
            }
        }
    }
}
