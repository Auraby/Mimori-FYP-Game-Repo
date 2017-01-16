using UnityEngine;
using System.Collections;

public class EyeOfEnmarSkillSelf : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "MeleeMinion")
        {
            other.gameObject.GetComponent<MeleeMinionFSM>().getStun(1);
        }
        if (other.gameObject.tag == "HordeMeleeMinion")
        {
            other.gameObject.GetComponent<HordeMeleeMinion>().getStun(1);
        }
        if (other.gameObject.tag == "RangeMinion")
        {
            other.gameObject.GetComponent<RangeMinionFSM>().getStun(1);
        }
        if (other.gameObject.tag == "HordeRangeMinion")
        {
            other.gameObject.GetComponent<HordeRangeMinion>().getStun(1);
        }
    }
}
