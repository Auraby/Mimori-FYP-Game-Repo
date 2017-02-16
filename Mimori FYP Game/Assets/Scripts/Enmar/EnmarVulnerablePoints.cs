﻿using UnityEngine;
using System.Collections;

public class EnmarVulnerablePoints : MonoBehaviour {

    public enum BodyParts { Head, Body, Arm, Leg }

    public BodyParts currentBodyPart;
    public Material enmarMaterial;

    public static EnmarVulnerablePoints instance { get; set; }
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            switch (currentBodyPart)
            {
                case BodyParts.Head:
                    {
                        EnmarController.instance.enmarCurrentHealth -= 10;
                        StartCoroutine(flashRed());
                       
                    }
                    break;

                case BodyParts.Body:
                    {
                        EnmarController.instance.enmarCurrentHealth -= 2;
                    }
                    break;

                case BodyParts.Arm:
                    {
                        EnmarController.instance.enmarCurrentHealth -= 1;
                    }
                    break;

                case BodyParts.Leg:
                    {
                        EnmarController.instance.enmarCurrentHealth -= 1;
                    }
                    break;
            }
        }
        
    }

    private IEnumerator flashRed()
    {
        enmarMaterial.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        enmarMaterial.color = Color.white;
    }
}
