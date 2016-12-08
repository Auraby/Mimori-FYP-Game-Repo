using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandlerV2 : MonoBehaviour {

	public static string heldskill = "";
	SkillTree sktree;
	Vector3 startPosition;
	Transform startParent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
				
	}
		
	public void DragSkill(){
		startPosition = transform.position;
		startParent = transform.parent;
		transform.position = Input.mousePosition;
	}

	void OnTriggerStay2D(Collider2D other){
		if ((Input.GetMouseButton (0)) && (other.tag == "Slots")) {
			transform.position = other.transform.position;
			heldskill = "";
		}
	}


	void OnMouseOver(){
		if (Input.GetMouseButton (1)) {
			if (heldskill == "") {
				heldskill =  gameObject.name;
				Debug.Log (heldskill);
			}
		}
	}
}
