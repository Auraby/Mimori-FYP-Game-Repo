using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EyeOfEnmarSkill : MonoBehaviour {
	public GameObject eoeSkillPref;
	public Transform gunEnd;
	public Image eoeImg;

	float eoeChargeTime;
	float eoeDestroyTime;
	float eoeCD;
	bool startDestroy = false;
	Vector3 originalSize;
	Vector3 endSize;
	GameObject eoe;

	Vector3 scale;
	// Use this for initialization
	void Start () {
		originalSize = eoeSkillPref.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        if(SceneManager.GetActiveScene().name == "Mimori")
        {
            if (eoeImg.IsActive())
            {
                EyeOfEnmar();
            }
        }
		

	}

	void EyeOfEnmar(){
		//Debug.Log (originalSize+","+endSize);
		//Charging Eye of Enmar's skill
		if (eoe != null) {
			scale = eoe.transform.localScale;
		}

		if (eoeCD <= 0) {
			if(Input.GetButton("Fire2")){
				if (eoeChargeTime < 15) {
					eoeChargeTime += Time.deltaTime*5;
					//endSize = new Vector3 (eoeChargeTime+1, eoeChargeTime+1, eoeChargeTime+1);
					//eoeSkillPref.transform.localScale = Vector3.Lerp (originalSize, endSize, eoeChargeTime);
				}
			}
			if(Input.GetButtonUp("Fire2")){
				//eoeChargeTime = 0;
				//eoeSkillPref.transform.localScale = originalSize;
				eoeCD = 5;
				eoe = (GameObject)Instantiate (eoeSkillPref, gunEnd.position, gunEnd.rotation);
				startDestroy = true;
			}
		}

		if (startDestroy) {
			//eoe.transform.localScale = Vector3.Lerp (originalSize, endSize, Time.deltaTime*10);
			if(scale.x < eoeChargeTime){
				scale.x += Time.deltaTime *2.5f;
				scale.y += Time.deltaTime *2.5f;
				scale.z += Time.deltaTime *2.5f;
			}

			if (eoeDestroyTime < 2) {
				eoeDestroyTime += Time.deltaTime;
			}
		}
		if (eoeDestroyTime >= 2) {
			eoeChargeTime = 0;
			//eoeSkillPref.transform.localScale = originalSize;
			scale = originalSize;
			startDestroy = false;
			eoeDestroyTime = 0;
			Destroy (GameObject.FindGameObjectWithTag("EoESkill"));
		}

		if (eoeCD >= 0) {
			eoeCD -= Time.deltaTime;
		}

		if (eoe != null) {
			eoe.transform.localScale = scale;
		}
	}
}
