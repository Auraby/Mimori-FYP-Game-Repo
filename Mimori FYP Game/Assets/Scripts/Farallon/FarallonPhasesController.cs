using UnityEngine;
using System.Collections;

public class FarallonPhasesController : MonoBehaviour {

    public enum Phases { Phase1, Phase2, Phase3}
    public Phases currentPhase;

    //Movement and Attack Phases
    public bool isPhase1 = false;
    public bool isPhase2 = false;
    public bool isPhase3 = false;

    ////Air Phases
    //public bool isAirPhase1 = false;
    //public bool isAirPhase2 = false;
    //public bool isAirPhase3 = false;

    ////Ground Phases
    //public bool isgroundPhase1 = false;
    //public bool isgroundPhase2 = false;
    //public bool isgroundPhase3 = false;



    public static FarallonPhasesController instance { get; set; }
	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update ()
    {

        switch (currentPhase)
        {
            case Phases.Phase1:
                {
                    isPhase1 = true;
                }
                break;

            case Phases.Phase2:
                {
                    isPhase2 = true;
                }
                break;

            case Phases.Phase3:
                {

                    isPhase3 = true;
                }
                break;
        }
	}

    
}
