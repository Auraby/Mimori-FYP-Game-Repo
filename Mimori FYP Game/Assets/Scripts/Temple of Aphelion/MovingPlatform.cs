using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {
	public float speed; //2
	public float direction;
    public bool moveZ;

    public float goal1, goal2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!moveZ)
        {
            transform.Translate(Vector3.left * speed * direction * Time.deltaTime);
            if (this.transform.position.x < goal1 || this.transform.position.x > goal2)
            {
                if (direction == 1)
                    direction = -1;
                else
                    direction = 1;
            }
        }
        else if (moveZ){
            transform.Translate(Vector3.forward * speed * direction * Time.deltaTime);
            if (this.transform.position.z < goal1 || this.transform.position.z > goal2)
            {
                if (direction == 1)
                    direction = -1;
                else
                    direction = 1;
            }
        }
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent = gameObject.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
