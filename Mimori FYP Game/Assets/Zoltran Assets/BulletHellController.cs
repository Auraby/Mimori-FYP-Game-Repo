using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletHellController : MonoBehaviour {
    public enum Pattern { One, Two, Three}

    public Pattern currentPattern;

    public int patternNo;
    public GameObject[] patternGroups = new GameObject[3];

    public Vector3 pos1, pos2, pos3;
    public Quaternion rot1, rot2, rot3;

    public static BulletHellController instance { get; set; }

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {

        switch (currentPattern)
        {
            case Pattern.One:
                {
                    GetPatternPositions(1);
                }
                break;

            case Pattern.Two:
                {
                    GetPatternPositions(2);
                }
                break;

            case Pattern.Three:
                {
                    GetPatternPositions(3);
                }
                break;
        }
	}

    public void GetPatternPositions(int patNo)
    {
        if(patNo == 1)
        {
            pos1 = patternGroups[0].transform.GetChild(0).transform.position;
            pos2 = patternGroups[0].transform.GetChild(1).transform.position;
            pos3 = patternGroups[0].transform.GetChild(2).transform.position;

            rot1 = patternGroups[0].transform.GetChild(0).rotation;
            rot2 = patternGroups[0].transform.GetChild(1).rotation;
            rot3 = patternGroups[0].transform.GetChild(2).rotation;
        }

        if(patNo == 2)
        {
            pos1 = patternGroups[1].transform.GetChild(0).transform.position;
            pos2 = patternGroups[1].transform.GetChild(1).transform.position;
            pos3 = patternGroups[1].transform.GetChild(2).transform.position;

            rot1 = patternGroups[1].transform.GetChild(0).rotation;
            rot2 = patternGroups[1].transform.GetChild(1).rotation;
            rot3 = patternGroups[1].transform.GetChild(2).rotation;
        }

        if (patNo == 3)
        {
            pos1 = patternGroups[2].transform.GetChild(0).transform.position;
            pos2 = patternGroups[2].transform.GetChild(1).transform.position;
            pos3 = patternGroups[2].transform.GetChild(2).transform.position;

            rot1 = patternGroups[2].transform.GetChild(0).rotation;
            rot2 = patternGroups[2].transform.GetChild(1).rotation;
            rot3 = patternGroups[2].transform.GetChild(2).rotation;
        }

    }
}
