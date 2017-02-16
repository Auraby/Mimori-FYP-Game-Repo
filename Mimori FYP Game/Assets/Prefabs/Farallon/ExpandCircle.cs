using UnityEngine;
using System.Collections;
using ParticlePlayground;
public class ExpandCircle : MonoBehaviour {
    //public GameObject _farallonslam;
    // Use this for initialization
    public float expandSpeed;
    public float duration;
    private float tempDuration;
	void Start () {
        //GetComponent<Collider>().isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.localScale += new Vector3(1f, 1f, 0) * Time.deltaTime * expandSpeed;
        //_farallonslam.GetComponent<PlaygroundParticlesC>().sizeMin += 0.05f * Time.deltaTime;
        //_farallonslam.GetComponent<PlaygroundParticlesC>().sizeMax += 0.1f * Time.deltaTime;
        tempDuration += Time.deltaTime;

        if(tempDuration > duration)
        {
            Destroy(gameObject);
        }


    }
}
