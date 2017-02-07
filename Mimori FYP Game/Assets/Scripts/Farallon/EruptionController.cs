using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EruptionController : MonoBehaviour {

    public enum EruptionState { Assign, Idle , GetPositions , Erupt}

    public EruptionState currEruptState;

    [Header("General Properties")]
    public float burstRate;
    public float burstNextFire;
    public float warningRate;
    public float warningNextFire;

    [Header("Positions")]
    public GameObject eruptPositionParent;
    public Transform[] eruptPositionsArray = new Transform[5];
    public Transform[] choosenPositionsArray;

    //public List<Transform> choosenPositionsList = new List<Transform>();


    [Header("Particles")]
    private GameObject eruptionParticleGO;
    private GameObject warningCircleGO;

    [Header("Object Pooling")]
    public ObjectPooling eruptionPool;
    public ObjectPooling warningPool;

    [Header("Numbers")]
    public int numberOfEruptions;
    private int eruptNum;

    [Header("Time")]
    public float burstTime;
    public float warningTime;
    private float eruptionTime;
    public float eruptionPeriod;

    [Header("Booleans")]
    public bool isStartEruption = false;
    public bool isFinishEruption = false;

    public static EruptionController instance { get; set; }
	// Use this for initialization
	void Start () {
        instance = this;
        eruptPositionsArray = new Transform[eruptPositionParent.transform.childCount];
    }
	
	// Update is called once per frame
	void Update () {

        switch (currEruptState)
        {

            case EruptionState.Assign:
                {
                    AssignTransformsToArray();
                    if(eruptPositionsArray[32] != null )
                    currEruptState = EruptionState.Idle;
                }
                break;

            case EruptionState.Idle:
                {
                    if(isStartEruption == true)
                    {
                        currEruptState = EruptionState.GetPositions;
                        isFinishEruption = false;
                    }
                }
                break;

            case EruptionState.GetPositions:
                {
                    isStartEruption = false;
                    
                    CycleThroughEruptPositionArray();
                }
                break;

            
            case EruptionState.Erupt:
                {
                    //Erupt();
                    eruptionTime += Time.deltaTime;
                    EruptWithWarning();
                    if (eruptionTime > eruptionPeriod)
                    {
                        eruptionTime = 0;
                        currEruptState = EruptionState.Idle;
                        isFinishEruption = true;
                    }
                    
                }
                break;

        }
	}

    public void CycleThroughEruptPositionArray()
    {
        bool isCyclingDone = false;
        choosenPositionsArray = new Transform[numberOfEruptions];
        for (int i = 0; i <= numberOfEruptions; i++)
        {
            eruptNum = CalculateIntRNG(numberOfEruptions);
            //if (choosenPositionsList.IndexOf(eruptPositionsArray[eruptNum]) == eruptPositionsArray[eruptNum].transform)
            //{
            //    eruptPositionsArray.
            //}
            if(eruptPositionsArray[eruptNum] != choosenPositionsArray[eruptNum])
            {
                choosenPositionsArray[eruptNum] = eruptPositionsArray[eruptNum];
            }

            else
            {
                eruptNum = CalculateIntRNG(numberOfEruptions);
            }

            if(i >= numberOfEruptions)
            {
                isCyclingDone = true;
            }
        }

        if(isCyclingDone == true)
        {
            currEruptState = EruptionState.Erupt;
        }
    }

    public void Erupt()
    {
        if (Time.time > burstNextFire)
        {
            burstNextFire = Time.time + burstRate;
            eruptionParticleGO = eruptionPool.RetrieveInstance();
            if (eruptionParticleGO)
            {
                int numRNG = CalculateIntRNG(numberOfEruptions);
                if (choosenPositionsArray[numRNG])
                {
                    eruptionParticleGO.transform.position = choosenPositionsArray[numRNG].position;
                    eruptionParticleGO.transform.rotation = Quaternion.Euler(-90,0,0);
                }
                
            }
        }

    }

    public void EruptWithWarning()
    {
        if (Time.time > warningNextFire)
        {
            warningNextFire = Time.time + warningRate;
            warningCircleGO = warningPool.RetrieveInstance();
            
            if (warningCircleGO)
            {
                int numRNG = CalculateIntRNG(numberOfEruptions);
                if (choosenPositionsArray[numRNG])
                {
                    warningCircleGO.transform.position = choosenPositionsArray[numRNG].position;
                    warningCircleGO.transform.rotation = Quaternion.Euler(0, 0, 0);

                    StartCoroutine(WaitToErupt(2, numRNG));
                }
                

            }
        }

    }

    public int CalculateIntRNG(int maxExclusive)
    {
        int numRNG = Random.Range(0, maxExclusive);
        return numRNG;
    }

    public void AssignTransformsToArray()
    {
        

        for (int i = 0; i < eruptPositionParent.transform.childCount; i++)
        {
           //f(eruptPositionsArray[i-1] != null)
            //{
                if (eruptPositionsArray[i] != eruptPositionParent.transform.GetChild(i))
                {
                    eruptPositionsArray[i] = eruptPositionParent.transform.GetChild(i);
                }
                Debug.Log(eruptPositionParent.transform.childCount);
                Debug.Log(eruptPositionsArray[i]);
                Debug.Log(eruptPositionParent.transform.GetChild(i));
           // }
            
            
        }
    }


    #region IEnumerators
    public IEnumerator WaitToErupt(float sec, int rng)
    {
        yield return new WaitForSeconds(sec);
        if (Time.time > burstNextFire)
        {
            burstNextFire = Time.time + burstRate;
            eruptionParticleGO = eruptionPool.RetrieveInstance();
            if (eruptionParticleGO)
            {
                if (choosenPositionsArray[rng])
                {
                    eruptionParticleGO.transform.position = choosenPositionsArray[rng].position;
                    eruptionParticleGO.transform.rotation = Quaternion.Euler(-90, 0, 0);
                }

            }
        }
    }

    #endregion
}
