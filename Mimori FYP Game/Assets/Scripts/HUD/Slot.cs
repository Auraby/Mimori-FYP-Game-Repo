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

	private GameObject[] slotobjectlist = new GameObject[4];
	private bool[] slotobjectisTaken = new bool[4];
	void Start(){
		i = 0;
	}

	public void ManageSlots(GameObject newskills){
		if (this.gameObject == slot1) {
			DragHandler.slotchecklist [0] = 1;
			DragHandler.SlotIsTaken [0] = true;
			slotobjectisTaken[0] = true;
			slotobjectlist [0] = DragHandler.itemBeingDragged.gameObject;
			currskill = newskills;
		} else if (this.gameObject == slot2) {
			DragHandler.slotchecklist [1] = 2;
			DragHandler.SlotIsTaken [1] = true;
			slotobjectisTaken[1] = true;
			slotobjectlist [1] = DragHandler.itemBeingDragged.gameObject;
			currskill = newskills;
		} else if (this.gameObject == slot3) {
			DragHandler.slotchecklist [2] = 3;
			DragHandler.SlotIsTaken [2] = true;
			slotobjectisTaken[2] = true;
			slotobjectlist [2] = DragHandler.itemBeingDragged.gameObject;
			currskill = newskills;
		} else {
			DragHandler.slotchecklist [3] = 4;
			DragHandler.SlotIsTaken [3] = true;
			slotobjectisTaken[3] = true;
			slotobjectlist [3] = DragHandler.itemBeingDragged.gameObject;
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


	public string getgameobject(int index){
		if (index == 1) {
			return slotobjectlist [0].gameObject.name.ToString();
		} else if (index == 2) {
			return slotobjectlist [1].gameObject.name.ToString();
		} else if (index == 3) {
			return slotobjectlist [2].gameObject.name.ToString();
		} else {
			return slotobjectlist [3].gameObject.name.ToString();
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
