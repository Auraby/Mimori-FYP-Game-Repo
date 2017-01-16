using UnityEngine;
using System.Collections;

public class DisappearingPlatform : MonoBehaviour
{
    public GameObject dPlatform1;
    public GameObject dPlatform2;

    float set1AppearCD = 2f;
    float set1DisappearCD = 2f;

    float set2AppearCD = 2f;
    float set2DisappearCD = 2f;
    // Use this for initialization
    void Start()
    {
        dPlatform2.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //handling first set of platforms
        //when they are active
        if (dPlatform1.activeSelf)
        {
            set1AppearCD -= Time.deltaTime;
            set1DisappearCD = 2f;
        }
        if (set1AppearCD <= 0)
        {
            dPlatform1.gameObject.SetActive(false);
        }
        //when they are inactive
        if (!dPlatform1.activeSelf)
        {
            set1DisappearCD -= Time.deltaTime;
            set1AppearCD = 2f;
        }
        if (set1DisappearCD <= 0)
        {
            dPlatform1.gameObject.SetActive(true);
        }

        //handling second set of platforms
        //when they are active
        if (dPlatform2.activeSelf)
        {
            set2AppearCD -= Time.deltaTime;
            set2DisappearCD = 2f;
        }
        if (set2AppearCD <= 0)
        {
            dPlatform2.gameObject.SetActive(false);
        }
        //when they are inactive
        if (!dPlatform2.activeSelf)
        {
            set2DisappearCD -= Time.deltaTime;
            set2AppearCD = 2f;
        }
        if (set2DisappearCD <= 0)
        {
            dPlatform2.gameObject.SetActive(true);
        }
    }
}
