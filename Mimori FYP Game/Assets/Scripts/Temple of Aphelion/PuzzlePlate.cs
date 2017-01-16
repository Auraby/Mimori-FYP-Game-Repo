using UnityEngine;
using System.Collections;

public class PuzzlePlate : MonoBehaviour {
    bool hitted = false;
    Material material;
	// Use this for initialization
	void Start () {
        material = this.gameObject.GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Bullet" && !hitted) {
            int input = System.Int32.Parse(this.gameObject.name);//convert string to int
            //Debug.Log(num);
            if (!PuzzleController.on1 && !PuzzleController.on2 && !PuzzleController.on3 && !PuzzleController.on4) {
                PuzzleController.on1 = true;
                PuzzleController.userInput[0] = input;
            }
            else if (PuzzleController.on1 && !PuzzleController.on2 && !PuzzleController.on3 && !PuzzleController.on4)
            {
                PuzzleController.on2 = true;
                PuzzleController.userInput[1] = input;
            }
            else if (PuzzleController.on1 && PuzzleController.on2 && !PuzzleController.on3 && !PuzzleController.on4)
            {
                PuzzleController.on3 = true;
                PuzzleController.userInput[2] = input;
            }
            else if (PuzzleController.on1 && PuzzleController.on2 && PuzzleController.on3 && !PuzzleController.on4)
            {
                PuzzleController.on4 = true;
                PuzzleController.userInput[3] = input;
            }
            else if (PuzzleController.on1 && PuzzleController.on2 && PuzzleController.on3 && PuzzleController.on4)
            {
                return;
            }
            material.SetColor("_Color", Color.yellow);
            hitted = true;
        }
    }
}
