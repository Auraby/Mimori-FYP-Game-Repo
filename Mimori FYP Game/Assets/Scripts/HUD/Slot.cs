using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour , IDropHandler{
	//DragHandler drag = new DragHandler ();
	/*public void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("HIT SkillSlot");
		if(other.gameObject.tag == "ActiveSkill"){
			other.transform.position = transform.position;
		}
	}*/

	public   static GameObject Slot1;
	public  static GameObject Slot2;
	public  static GameObject Slot3;
	public  static GameObject Slot4;
	public   static GameObject[] slotobjectlist = new GameObject[4];
	public  static bool[] slotobjectisTaken = new bool[4]{false, false, false, false};
	public 	static int[] counterforloop = new int[4];
	public   int i;
	private  string getcurrskillname;
	public  GameObject currskill;
	private  GameObject newskill;
	public GameObject prevskill;
	public bool checkforloop = false;

	public GameObject FPSctrl;

	void Start(){
		i = 0;
		FPSctrl.GetComponent<SkillTree> ();
	}

	public void ManageSlots(GameObject newskills){

		if (this.gameObject.name == "Slot 1") {
			DragHandler.slotchecklist [0] = 1;
			DragHandler.SlotIsTaken [0] = true;
			slotobjectlist [0] = DragHandler.itemBeingDragged.gameObject;
			slotobjectisTaken[0] = true;
			FPSctrl.GetComponent<SkillTree> ().slot1Skill = slotobjectlist[0].name;
			counterforloop [0] = 1;
			

			if (currskill != null) {
				prevskill = currskill;
				currskill = newskills;
			} else {
				currskill = newskills;
			}

			if (prevskill != null) {
				Destroy (prevskill);
				prevskill = null;
			}
			Debug.Log ("ManageSlots1");

		} else if (this.gameObject.name == "Slot 2") {
			DragHandler.slotchecklist [1] = 2;
			DragHandler.SlotIsTaken [1] = true;
			slotobjectisTaken[1] = true;
			slotobjectlist [1] = DragHandler.itemBeingDragged.gameObject;
			FPSctrl.GetComponent<SkillTree> ().slot2Skill = slotobjectlist[1].name;
			counterforloop [1] = 2;

			if (currskill != null) {
				prevskill = currskill;
				currskill = newskills;
			}
			else {
				currskill = newskills;
			}
			if (prevskill != null) {
				Destroy (prevskill);
				prevskill = null;
			} 
			Debug.Log ("ManageSlots2");
		} else if (this.gameObject.name == "Slot 3") {
			DragHandler.slotchecklist [2] = 3;
			DragHandler.SlotIsTaken [2] = true;
			slotobjectisTaken[2] = true;
			slotobjectlist [2] = DragHandler.itemBeingDragged.gameObject;
			FPSctrl.GetComponent<SkillTree> ().slot3Skill = slotobjectlist[2].name;
			counterforloop [2] = 3;
			if (currskill != null) {
				prevskill = currskill;
				currskill = newskills;
			}
			else {
				currskill = newskills;
			}
			if (prevskill != null) {
				Destroy (prevskill);
				prevskill = null;
			} 
			Debug.Log ("ManageSlots3");
		} else {
			DragHandler.slotchecklist [3] = 4;
			DragHandler.SlotIsTaken [3] = true;
			slotobjectisTaken[3] = true;
			slotobjectlist [3] = DragHandler.itemBeingDragged.gameObject;
			FPSctrl.GetComponent<SkillTree> ().slot4Skill = slotobjectlist[3].name;
			counterforloop [3] = 4;
			Debug.Log ("ManageSlots4");
			if (currskill != null) {
				prevskill = currskill;
				currskill = newskills;
			}
			else {
				currskill = newskills;
			}
			if (prevskill != null) {
				Destroy (prevskill);
				prevskill = null;
			} 
		}

		/*for (int c = 0; c < slotobjectlist.Length; c++) {
			if (slotobjectlist [c].gameObject != null) {
				if (slotobjectlist [c].gameObject.name.Contains (DragHandler.itemBeingDragged.gameObject.name)) {
					Debug.Log ("GAMEOBJECT DELETED");
					slotobjectisTaken [c] = false;
					Destroy (slotobjectlist [c].gameObject);
					break;
				}
			}

		}*/
		//DeleteSlots ();
	}
		

	public void DeleteSlots(){

		for (int c = 0; c < slotobjectlist.Length; c++) {
			if (slotobjectlist [c] == null) {
			//	Debug.Log (c);
			//	Debug.Log (slotobjectlist [c]);
			//	Debug.Log ("NULL GAMEOBJECT");
			} else {
				if (slotobjectlist [c].gameObject != null) {
					if (slotobjectlist [c].gameObject.name.Contains (DragHandler.itemBeingDragged.gameObject.name)) {
						Debug.Log ("GAMEOBJECT DELETED");
						slotobjectisTaken [c] = false;
						Destroy (slotobjectlist [c].gameObject);
						switch (c) {
						case 0:
							FPSctrl.GetComponent<SkillTree> ().slot1Skill = "";
							break;
						case 1:
							FPSctrl.GetComponent<SkillTree> ().slot2Skill = "";
							break;
						case 2:
							FPSctrl.GetComponent<SkillTree> ().slot3Skill = "";
							break;
						case 3:
							FPSctrl.GetComponent<SkillTree> ().slot4Skill = "";
							break;
						default:
							break;
						}

						//break;
					}
				}
			 
			}
		}

		ManageSlots (newskill);

	}


	public string getgameobject(int index){
		if (index == 1) {
			return slotobjectlist [0].gameObject.name;
		} else if (index == 2) {
			return slotobjectlist [1].gameObject.name;
		} else if (index == 3) {
			return slotobjectlist [2].gameObject.name;
		} else {
			return slotobjectlist [3].gameObject.name;
		}

		
		
	}

	public void ClearSlots(){

		for (int c = 0; c < slotobjectlist.Length; c++) {
			if (slotobjectlist [c] == null) {
				//	Debug.Log (c);
				//	Debug.Log (slotobjectlist [c]);
				//	Debug.Log ("NULL GAMEOBJECT");
			} else {
				if (slotobjectlist [c].gameObject != null) {
					if (slotobjectlist [c].gameObject.name.Contains (DragHandler.itemBeingDragged.gameObject.name)) {
						Debug.Log ("Skills Cleared");
						slotobjectisTaken [c] = false;
						DragHandler.slotchecklist [c] = 0;
						DragHandler.SlotIsTaken [c] = false;
						currskill = null;
						prevskill = null;
						counterforloop [c] = 0;
						Destroy (slotobjectlist [c].gameObject);
						FPSctrl.GetComponent<SkillTree> ().slot1Skill = "";
						FPSctrl.GetComponent<SkillTree> ().slot2Skill = "";
						FPSctrl.GetComponent<SkillTree> ().slot3Skill = "";
						FPSctrl.GetComponent<SkillTree> ().slot4Skill = "";
						

						//break;
					}
				}

			}
		}

		ManageSlots (newskill);

	}


	#region IDropHandler implementation

	public void OnDrop (PointerEventData eventData)
	{
		newskill = DragHandler.itemBeingDragged.gameObject;
		for (i = 0; i < 5; i++) {
				//Debug.Log (counterforloop.GetValue (i));
			//Debug.Log(counterforloop.GetValue(i));
			if (slotobjectisTaken [i] == false && counterforloop [i] == 0) {
					//DragHandler.slotchecklist [i] = 1;
					//DragHandler.SlotIsTaken [i] = true;
					//slotobjectisTaken [i] = true;
					//Debug.Log (counterforloop.GetValue (i));
					//Debug.Log (i);
					
					//DragHandler.getGameObjectSkill [i] = DragHandler.itemBeingDragged.gameObject;
					//DragHandler.skillName [i] = DragHandler.itemBeingDragged.name;
					//DragHandler.CurrSlotItem = DragHandler.itemBeingDragged.gameObject;

					//ManageSlots (newskill);
				DeleteSlots();
			
					DragHandler.itemBeingDragged.transform.position = this.gameObject.transform.position;
				DragHandler.itemBeingDragged.transform.SetParent (this.gameObject.transform);
					DragHandler.itemBeingDragged.transform.localScale = new Vector3 (DragHandler.itemBeingDragged.transform.localScale.x / 1.45f, DragHandler.itemBeingDragged.transform.localScale.y / 1.15f, DragHandler.itemBeingDragged.transform.localScale.z / 1.0f);
					DragHandler.itemBeingDragged.GetComponent<DragHandler> ().enabled = false;
					DragHandler.itemBeingDragged.transform.SetAsFirstSibling ();
					//Debug.Log (DragHandler.SlotIsTaken [i]);
					//Debug.Log (i);
					break;

			} else if(slotobjectisTaken [i] == true && counterforloop [i] == i + 1){
				
				
				 //DragHandler.isDraggedIntoSlot = true;
					//Debug.Log(slotobjectisTaken [i]);
					DeleteSlots ();	
					//DragHandler.getGameObjectSkill [i] = DragHandler.itemBeingDragged.gameObject;
					//DragHandler.CurrSlotItem = DragHandler.itemBeingDragged.gameObject;
					//DragHandler.skillName [i] = DragHandler.itemBeingDragged.name;
				    DragHandler.itemBeingDragged.transform.position = this.gameObject.transform.position;
				    DragHandler.itemBeingDragged.transform.SetParent(this.gameObject.transform);
				    DragHandler.itemBeingDragged.transform.localScale = new Vector3(DragHandler.itemBeingDragged.transform.localScale.x/1.45f, DragHandler.itemBeingDragged.transform.localScale.y/1.15f, DragHandler.itemBeingDragged.transform.localScale.z/1.0f);
					DragHandler.itemBeingDragged.GetComponent<DragHandler> ().enabled = false;
				    DragHandler.itemBeingDragged.transform.SetAsFirstSibling ();
					//Debug.Log (DragHandler.SlotIsTaken [i]);
					//Debug.Log (i);
				    
					break;
			}

		} 
			
			//Debug.Log ("SkillDropped");
			//ExecuteEvents.ExecuteHierarchy<ItemHasChanged>(gameObject,null,(x,y) => x.HasChanged ());

	}	
	#endregion
}
