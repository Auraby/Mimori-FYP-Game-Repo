using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScriptForDebuggingStuff : MonoBehaviour {

    public GameObject dustParticle;
    GameObject dustParticleGO;
    public GameObject leftSide;
    public GameObject rightSide;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider other)
    {
        if(Level1Controller.instance.levelProgress == Level1Controller.LevelState.Win)
        {
            if (other.gameObject.tag == "Player")
            {
                Level1Controller.instance.aSyncOp.allowSceneActivation = true;
            }
        }

        if(Level1Controller.instance.levelProgress == Level1Controller.LevelState.Playing)
        {
            if(other.gameObject.name == "LeftHandCollider")
            {
                EnmarController.instance.PlaySlamSound();
                Level1Controller.instance.currentWallHealth -= 30;
                dustParticleGO = (GameObject)Instantiate(dustParticle, leftSide.transform.position, leftSide.transform.rotation);
            }

            if (other.gameObject.name == "RightHandCollider")
            {
                EnmarController.instance.PlaySlamSound();
                Level1Controller.instance.currentWallHealth -= 30;
                dustParticleGO = (GameObject)Instantiate(dustParticle, rightSide.transform.position, rightSide.transform.rotation);
            }
        }
       
    }

    public void OnTriggerExit(Collider otherThing)
    {
        if (Level1Controller.instance.levelProgress == Level1Controller.LevelState.Playing)
        {
            if (otherThing.gameObject.name == "LeftHandCollider")
            {
                Destroy(dustParticleGO);
            }

            if (otherThing.gameObject.name == "RightHandCollider")
            {
                Destroy(dustParticleGO);
            }
        }
    }
}
