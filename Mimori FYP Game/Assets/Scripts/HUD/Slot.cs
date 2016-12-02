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

	public GameObject slot1;
	public GameObject slot2;
	public GameObject slot3;
	public GameObject slot4;

	public int i;

	private GameObject currskill;
	private GameObject newskill;

	void Start(){
		i = 0;
	}

	public void ManageSlots(GameObject newskills){

		if (this.gameObject == slot1) {
			DragHandler.slotchecklist [0] = 1;
			DragHandler.SlotIsTaken [0] = true;
			currskill = newskills;
		} else if (this.gameObject == slot2) {
			DragHandler.slotchecklist [1] = 2;
			DragHandler.SlotIsTaken [1] = true;
			currskill = newskills;
		} else if (this.gameObject == slot3) {
			DragHandler.slotchecklist [2] = 3;
			DragHandler.SlotIsTaken [2] = true;
			currskill = newskills;
		} else {
			DragHandler.slotchecklist [3] = 4;
			DragHandler.SlotIsTaken [3] = true;
			currskill = newskills;
		}
	}

	public void DeleteSlots(){
		if (this.gameObject == slot1) {
			Destroy (currskill);

		} else if (this.gameObject == slot2) {
			Destroy (currskill);
		
		} else if (this.gameObject == slot3) {
			Destroy (currskill);
		
		} else {
			Destroy (currskill);
		
		}

	}

	#region IDropHandler implementation

	public void OnDrop (PointerEventData eventData)
	{
		for (i = 0; i < DragHandler.slotchecklist.Length; i++) {
			newskill = DragHandler.itemBeingDragged.gameObject;
			if (DragHandler.SlotIsTaken [i] == false) {
				//DragHandler.slotchecklist [i] = 1;
				DragHandler.SlotIsTaken [i] = true;
				//DragHandler.getGameObjectSkill [i] = DragHandler.itemBeingDragged.gameObject;
				//DragHandler.skillName [i] = DragHandler.itemBeingDragged.name;
				//DragHandler.CurrSlotItem = DragHandler.itemBeingDragged.gameObject;
				ManageSlots (newskill);
				DragHandler.itemBeingDragged.transform.position = this.gameObject.transform.position;
				DragHandler.itemBeingDragged.transform.SetParent(this.gameObject.transform);
				DragHandler.itemBeingDragged.transform.localScale = new Vector3(DragHandler.itemBeingDragged.transform.localScale.x/1.0f, DragHandler.itemBeingDragged.transform.localScale.y/0.65f, DragHandler.itemBeingDragged.transform.localScale.z/1.0f);
				DragHandler.itemBeingDragged.GetComponent<DragHandler> ().enabled = false;
				DragHandler.itemBeingDragged.transform.SetAsFirstSibling ();
				//Debug.Log (DragHandler.SlotIsTaken [i]);
				//Debug.Log (i);
		
				break;

			} else if(DragHandler.SlotIsTaken [i] == true){
				 //DragHandler.isDraggedIntoSlot = true;
					DeleteSlots ();	
				    ManageSlots(newskill);
					DragHandler.getGameObjectSkill [i] = DragHandler.itemBeingDragged.gameObject;
					DragHandler.CurrSlotItem = DragHandler.itemBeingDragged.gameObject;
					DragHandler.skillName [i] = DragHandler.itemBeingDragged.name;
				    DragHandler.itemBeingDragged.transform.position = this.gameObject.transform.position;
				    DragHandler.itemBeingDragged.transform.SetParent(this.gameObject.transform);
				    DragHandler.itemBeingDragged.transform.localScale = new Vector3(DragHandler.itemBeingDragged.transform.localScale.x/1.0f, DragHandler.itemBeingDragged.transform.localScale.y/0.65f, DragHandler.itemBeingDragged.transform.localScale.z/1.0f);
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
