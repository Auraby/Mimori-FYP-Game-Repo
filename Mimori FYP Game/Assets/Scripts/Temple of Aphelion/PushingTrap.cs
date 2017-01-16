using UnityEngine;
using System.Collections;

public class PushingTrap : MonoBehaviour {
    Vector3 originalPos;
    float startDelay = 1;

    float pushSpeed = 150;

    float revertSpeed;
    bool revert = false;
    float revertCD = 2;
    
    float pushAgainCD = 0;

	// Use this for initialization
	void Start () {
        originalPos = this.transform.position;
        if (this.gameObject.name == "Trap 2") {
            startDelay = 1.6f;
        }
        else if (this.gameObject.name == "Trap 3")
        {
            startDelay = 2.2f;
        }
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(revertCD+","+pushAgainCD+","+revert);
        if (startDelay > 0) {
            startDelay -= Time.deltaTime;
        }
        if (startDelay <= 0) {
            startDelay = 0;
        }
       
        if (startDelay <= 0) {
            //set countdowns to 0 when they are less than or equal 0
            if (revertCD <= 0)
            {
                revertCD = 0;
            }

            if (pushAgainCD <= 0)
            {
                pushAgainCD = 0;
            }

            if (pushAgainCD <= 0)
            {//step 1, move the pushing trap
                transform.Translate(Vector3.right * pushSpeed * Time.deltaTime);
            }

            if (pushAgainCD > 0)
            {
                pushAgainCD -= Time.deltaTime;
            }

            if (revert && revertCD > 0)
            {//step 4
                revertCD -= Time.deltaTime;
            }

            if (revertCD <= 0 && this.transform.position.x >= originalPos.x)
            {
                transform.Translate(Vector3.left * revertSpeed * Time.deltaTime);
                pushAgainCD = 2;
            }

            if (this.transform.position.x <= originalPos.x)
            {//reset all values
                revert = false;
                pushSpeed = 150;
                revertSpeed = 0;
                revertCD = 2;
            }
        }
        



    }

    void OnTriggerEnter(Collider other) {//step 2, check if it collides with the empty gameobject w/ collider, if so stop it from moving
        if (other.gameObject.tag == "TrapStop") {
            pushSpeed = 0;
            revertSpeed = 20;
            revert = true; //step 3, start reverting the trap back to its original position
        }

        if (other.gameObject.tag == "Player") {
            Vector3 direction = other.transform.position - transform.position;
            direction.Normalize();
            other.transform.Translate(direction * 200 * Time.deltaTime, Space.World);
        }
    }

}
