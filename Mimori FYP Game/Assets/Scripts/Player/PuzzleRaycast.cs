using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PuzzleRaycast : MonoBehaviour {
    int input;
    float raycastCD;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (raycastCD <= 0) {
            raycastCD = 0;
        }
        if (raycastCD > 0) {
            raycastCD -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Fire1")) {
            if (SceneManager.GetActiveScene().name == "Temple of Aphelion")
            {
                raycastCD = 0.5f;
                RaycastHit hit;

                Vector3 forward = transform.TransformDirection(Vector3.forward);

                if (Physics.Raycast(transform.position, forward, out hit, 30))
                {
                    if (hit.collider.gameObject.tag == "PuzzlePlate" && raycastCD <= 0)
                    {
                        input = System.Int32.Parse(hit.collider.gameObject.name);//convert string to int
                        //Debug.Log(num);
                        if (!PuzzleController.on1 && !PuzzleController.on2 && !PuzzleController.on3 && !PuzzleController.on4)
                        {
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
                    }

                }
            }
        }
        
    }
}
