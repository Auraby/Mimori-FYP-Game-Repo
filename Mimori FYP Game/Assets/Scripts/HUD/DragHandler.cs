using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler {
	//public GameObject testVariable;
	SkillTree sktree;
	public static GameObject itemBeingDragged;
	public GameObject SkillTreeCanvas;
	private  GameObject itemBeingDragged_Duplicate;
	private static bool isDragging = true;
	public static bool isDraggedIntoSlot;
	public static GameObject CurrSlotItem;
	Vector3 startPosition;
	Transform startParent;
	private string GetSkillName;

	public static string[] skillName = new string[4];
	public static bool[] SlotIsTaken = new bool[4] {false, false, false, false};
	public static int[] slotchecklist = new int[4] {0,0,0,0};

	public static GameObject[] getGameObjectSkill = new GameObject[4];

	//public static DragHandler instance { get; set; }

	void Start (){
		
		//instance = this;
	}

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		if (isDraggedIntoSlot) {
			Destroy (itemBeingDragged);
			Destroy (itemBeingDragged_Duplicate);
		}
		if (gameObject.GetComponent<Button> ().IsInteractable()) {
			itemBeingDragged = gameObject;
			isDragging = true;
			DuplicateSkillOnDrag ();
			startPosition = transform.position;
			startParent = transform.parent;
			GetComponent<CanvasGroup> ().blocksRaycasts = false;
		}
	}
		
	#endregion

	//public void CheckIfSkillUnlocked(){
	//	GetSkillName = itemBeingDragged.gameObject.name;
		
	//}

	void Update(){
		if (!isDragging) {
			Destroy (itemBeingDragged);
		}
	}

	public void DuplicateSkillOnDrag(){
		if (isDragging) {
			itemBeingDragged_Duplicate = Instantiate (itemBeingDragged) as GameObject;
			itemBeingDragged_Duplicate.transform.SetParent (SkillTreeCanvas.transform, false);
			itemBeingDragged_Duplicate.name = itemBeingDragged_Duplicate.name.Replace ("(Clone)", "");
		}
	}

	#region IDragHandler implementation
	public void OnDrag (PointerEventData eventData)
	{
		if (gameObject.GetComponent<Button> ().IsInteractable ()) {
			isDragging = true;
			transform.position = Input.mousePosition;
		}
	}
	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		
		if (!isDraggedIntoSlot) {
			isDragging = false;
		}

		//Debug.Log (isDragging);
		//Debug.Log (isDraggedIntoSlot);
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		if (transform.parent == startParent) {
			transform.position = startPosition;
		}

		 	//Invoke ("OnTriggerStay2D", 0f);
	}

	#endregion
}
