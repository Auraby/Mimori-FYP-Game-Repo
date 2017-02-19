using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ZoltransHealthBarsManager : MonoBehaviour {

    //Sliders
    [Header("Health Bars")]
    public Slider mainZoltranSlider;
    public Slider illuOneSlider;
    public Slider illuTwoSlider;

    //Zoltrans
    [Header("Zoltran Gameobjects")]
    public GameObject mainZoltran;
    public GameObject illusionOne;
    public GameObject illusionTwo;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        mainZoltranSlider.value = mainZoltran.GetComponent<ZoltranController>().zCurrentHealth;

        illuOneSlider.value = illusionOne.GetComponent<ZoltranController>().zCurrentHealth;

        illuTwoSlider.value = illusionTwo.GetComponent<ZoltranController>().zCurrentHealth;
	}
}
