using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GunModSkills : MonoBehaviour
{
	//EoE
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

	//SoZ
	public GameObject decoy;
	public GameObject tempDecoy;
	bool castingDecoy = false;
	public GameObject terrain;
	public Camera camera;
	bool castingOut = false;
	Vector3 castingOriginalPos;
	Vector3 lookPos;

	//HoI
	public GameObject HoiLightning;
	public GameObject HoiCharge;
	public GameObject HoiMuzzleFlash;
	public float HoiDamage;
	private float HoIChargeTime;
	private float HoIDestroyTime;
	private float HoICD;
	bool startHoIDestroy = false;
	bool isLightningStrike = false;


	// Use this for initialization
	void Start()
	{
		originalSize = eoeSkillPref.transform.localScale;
		castingOriginalPos = tempDecoy.transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		if (SceneManager.GetActiveScene().name != "Gate of Telluris")
		{
			//EyeOfEnmar();
			HeartOfIshira();
			if (SceneManager.GetActiveScene().name == "Forest of Misery")
			{
				//SoulOfZoltran();
			}
		}


	}

	public void EyeOfEnmar()
	{
		//Debug.Log (originalSize+","+endSize);
		//Charging Eye of Enmar's skill
		if (gameObject.GetComponent<Shoot>().gunmodcounter == 0)
		{
			if (eoe != null)
			{
				scale = eoe.transform.localScale;
			}

			if (eoeCD <= 0)
			{
				if (Input.GetButton("Fire2"))
				{
					if (eoeChargeTime < 15)
					{
						eoeChargeTime += Time.deltaTime * 5;
						//endSize = new Vector3 (eoeChargeTime+1, eoeChargeTime+1, eoeChargeTime+1);
						//eoeSkillPref.transform.localScale = Vector3.Lerp (originalSize, endSize, eoeChargeTime);
					}
				}
				if (Input.GetButtonUp("Fire2"))
				{
					//eoeChargeTime = 0;
					//eoeSkillPref.transform.localScale = originalSize;
					eoeCD = 5;
					eoe = (GameObject)Instantiate(eoeSkillPref, gunEnd.position, gunEnd.rotation);
					startDestroy = true;
				}
			}

			if (startDestroy)
			{
				//eoe.transform.localScale = Vector3.Lerp (originalSize, endSize, Time.deltaTime*10);
				if (scale.x < eoeChargeTime)
				{
					scale.x += Time.deltaTime * 2.5f;
					scale.y += Time.deltaTime * 2.5f;
					scale.z += Time.deltaTime * 2.5f;
				}

				if (eoeDestroyTime < 2)
				{
					eoeDestroyTime += Time.deltaTime;
				}
			}
			if (eoeDestroyTime >= 2)
			{
				eoeChargeTime = 0;
				//eoeSkillPref.transform.localScale = originalSize;
				scale = originalSize;
				startDestroy = false;
				eoeDestroyTime = 0;
				Destroy(GameObject.FindGameObjectWithTag("EoESkill"));
			}

			if (eoeCD >= 0)
			{
				eoeCD -= Time.deltaTime;
			}

			if (eoe != null)
			{
				eoe.transform.localScale = scale;
			}
		}
	}

	public void SoulOfZoltran()
	{
		if (gameObject.GetComponent<Shoot>().gunmodcounter == 1)
		{
			if (Input.GetButtonDown("Fire2"))
			{
				if (!castingDecoy)
				{
					castingDecoy = true;
				}
				else
				{
					castingDecoy = false;
					tempDecoy.transform.position = castingOriginalPos;
				}
			}

			if (castingDecoy)
			{
				RaycastHit hit;
				Ray ray = new Ray(camera.transform.position, camera.transform.forward);
				if (terrain.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity))
				{ //only if player click on the terrain then will call this
					lookPos = hit.point; //to get the location of the mouse click
					Vector3 magicCirlePlacement = lookPos;
					magicCirlePlacement.y = lookPos.y + 2;
					tempDecoy.transform.position = magicCirlePlacement;
					if (Vector3.Distance(lookPos, transform.position) > 30)
					{
						tempDecoy.GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 0, 0, 0.5f));
					}
					else
					{
						tempDecoy.GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 1, 1, 0.5f));
					}
					//mousePos = lookPos;
				}
				if (Input.GetButton("Fire1"))
				{
					if (Vector3.Distance(lookPos, transform.position) <= 30)
					{
						tempDecoy.transform.position = castingOriginalPos;
						GameObject dropDecoy = (GameObject)Instantiate(decoy, new Vector3(lookPos.x, lookPos.y + 2, lookPos.z), Quaternion.identity);
						castingDecoy = false;
						dropDecoy.tag = "Player";
						this.gameObject.tag = "PlayerTemp";
					}
				}
				//else if (goTerrain.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity))
				//{ //only if player click on the terrain then will call this
				//    Vector3 lookPos = hit.point; //to get the location of the mouse click
				//                                 //GameObject newMagicCircle = (GameObject)Instantiate (AOEMagic, lookPos, MagicCastingPosition.rotation);
				//    Vector3 magicCirlePlacement = lookPos;
				//    magicCirlePlacement.y = lookPos.y + 1;
				//    //AOEMagic.transform.position = magicCirlePlacement;
				//    //mousePos = lookPos;
				//}
			}
		}
	}

	public void HeartOfIshira()
	{
		if (gameObject.GetComponent<Shoot>().gunmodcounter == 2)
		{
		if (Input.GetButton("Fire2"))
		{
			if(HoICD <= 0)
			{
				HoiCharge.SetActive(true);
			}           

		}

		if (Input.GetButtonUp("Fire2"))
		{
			HoICD = 5;
			HoiCharge.SetActive(false);
			HoiMuzzleFlash.SetActive(true);
			HoiLightning.SetActive(true);
			startHoIDestroy = true;
			isLightningStrike = true;

			if(isLightningStrike == true)
			{
				RaycastHit hit;
				//Ray ray = new Ray(camera.transform.position, camera.transform.forward);
				Physics.Raycast(camera.transform.position, camera.transform.forward, out hit);
				 if (hit.collider.gameObject.name == "Farallon")
				{
					//  hit.collider.gameObject.GetComponent<FarallonController>().currWingHealth -= HoiDamage;
				}
				isLightningStrike = false;
			}

		}
		if(startHoIDestroy == true)
		{
			HoIDestroyTime += Time.deltaTime;
			HoICD -= Time.deltaTime;
			if (HoIDestroyTime > 5)
			{
				HoiMuzzleFlash.SetActive(false);
				HoiLightning.SetActive(false);
				startHoIDestroy = false;
				HoIDestroyTime = 0;
			}
		}

		}
	}
}
