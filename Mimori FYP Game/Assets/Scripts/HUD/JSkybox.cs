using UnityEngine;
using System.Collections;

public class JSkybox : MonoBehaviour {

	/// number of seconds in a day  
	public float dayCycleLength = 1440;  

	//stars appear
	//public GameObject stars;

	/// current time in game time (0 - dayCycleLength).  
	public float currentCycleTime = 0;  

	/// number of hours per day.  
	public float hoursPerDay;  

	/// The rotation pivot of Sun  
	public Transform rotation;  

	/// current day phase  
	public DayPhase currentPhase;  

	/// Dawn occurs at currentCycleTime = 0.0f, so this offsets the WorldHour time to make  
	/// dawn occur at a specified hour. A value of 3 results in a 5am dawn for a 24 hour world clock.  
	public float dawnTimeOffset;  

	/// calculated hour of the day, based on the hoursPerDay setting.  
	public int worldTimeHour;  

	/// calculated minutes of the day, based on the hoursPerDay setting.  
	public int minutes;  
	private float timePerHour ;  

	/// The scene ambient color used for full daylight.  
	public Color fullLight = new Color(253.0f / 255.0f, 248.0f / 255.0f, 223.0f / 255.0f);  

	/// The scene ambient color used for full night.  
	public Color fullDark = new Color(32.0f / 255.0f, 28.0f / 255.0f, 46.0f / 255.0f);  

	/// The scene fog color to use at dawn and dusk.  
	public Color dawnDuskFog = new Color(133.0f / 255.0f, 124.0f / 255.0f, 102.0f / 255.0f);  

	/// The scene fog color to use during the day.  
	public Color dayFog = new Color(180.0f / 255.0f, 208.0f / 255.0f, 209.0f / 255.0f);  

	/// The scene fog color to use at night.  
	public Color nightFog = new Color(12.0f / 255.0f, 15.0f / 255.0f, 91.0f / 255.0f);  

	/// The calculated time at which dawn occurs based on 1/4 of dayCycleLength.  
	private float dawnTime;   

	/// The calculated time at which day occurs based on 1/4 of dayCycleLength.  
	private float dayTime;  

	/// The calculated time at which dusk occurs based on 1/4 of dayCycleLength.  
	private float duskTime;  

	/// The calculated time at which night occurs based on 1/4 of dayCycleLength.  
	private float nightTime;  

	/// One quarter the value of dayCycleLength.  
	private float quarterDay;  
	private float halfquarterDay;  

	/// The specified intensity of the directional light, if one exists. This value will be  
	/// faded to 0 during dusk, and faded from 0 back to this value during dawn.  
	private float lightIntensity;  

	private Light mainlight;

	// blend value of skybox using SkyBoxBlend Shader in render settings range 0-1  
	private float SkyboxBlendFactor= 0.0f;  

	public Material skyboxmaterial;

	/// Initializes working variables and performs starting calculations.  
	void Initialize()  
	{  
		quarterDay = dayCycleLength * 0.25f;  
		halfquarterDay = dayCycleLength * 0.125f;  
		dawnTime = 0.0f;  
		currentCycleTime = 0;
		dayTime = dawnTime + halfquarterDay;  
		duskTime = dayTime + quarterDay + halfquarterDay;  
		nightTime = duskTime + halfquarterDay;  
		timePerHour = dayCycleLength/hoursPerDay;  
		mainlight = GetComponent<Light> ();
		if (mainlight.GetComponent<Light>() != null)  
		{ lightIntensity = mainlight.intensity; }  
	}  

	/// Sets the script control fields to reasonable default values for an acceptable day/night cycle effect.  
	void Reset()  
	{  
		dayCycleLength = 120.0f;  
		hoursPerDay = 24.0f;  
		dawnTimeOffset = 3.0f;  
		fullDark = new Color(32.0f / 255.0f, 28.0f / 255.0f, 46.0f / 255.0f);  
		fullLight = new Color(253.0f / 255.0f, 248.0f / 255.0f, 223.0f / 255.0f);  
		dawnDuskFog = new Color(133.0f / 255.0f, 124.0f / 255.0f, 102.0f / 255.0f);  
		dayFog = new Color(180.0f / 255.0f, 208.0f / 255.0f, 209.0f / 255.0f);  
		nightFog = new Color(12.0f / 255.0f, 15.0f / 255.0f, 91.0f / 255.0f);  
	}  

	// Use this for initialization  
	void Start()  
	{  
		Initialize();  
	}  

	void OnGUI(){  
		string jam = worldTimeHour.ToString();  
		string mins = minutes.ToString();  
		if (worldTimeHour < 10){  
			jam = "0" + worldTimeHour;  
		}  
		if (minutes < 10){  
			mins = "0"+minutes;  
		}  
		GUI.Button(new Rect(500, 20, 100, 26), currentPhase.ToString() +" : "+jam+":"+mins);  
	}  

	// Update is called once per frame  
	void Update()  
	{  
		// Rudementary phase-check algorithm:  
		if (currentCycleTime > nightTime && currentPhase == DayPhase.Dusk)  
		{  
			SetNight();
			
		}  
		else if (currentCycleTime > duskTime && currentPhase == DayPhase.Day)  
		{  
			SetDusk();
		
		}  
		else if (currentCycleTime > dayTime && currentPhase == DayPhase.Dawn)  
		{  
			SetDay();

		}  
		else if (currentCycleTime > dawnTime && currentCycleTime < dayTime && currentPhase == DayPhase.Night)  
		{  
			SetDawn(); 

		}  

		// Perform standard updates:  
		UpdateWorldTime();  
		UpdateDaylight();  
		UpdateFog();  
		UpdateSkyboxBlendFactor ();
		UpdateDaylight ();
		// Update the current cycle time:  
		currentCycleTime += Time.deltaTime * 5.8f;  
		currentCycleTime = currentCycleTime % dayCycleLength;  
		//rotates the sun
		transform.RotateAround (Vector3.zero, Vector3.right, Time.deltaTime * 0.1f);
		transform.LookAt (Vector3.zero);

	}  

	/// Sets the currentPhase to Dawn, turning on the directional light, if any.  
	public void SetDawn()  
	{  
		if (mainlight.GetComponent<Light>() != null)  
		{ mainlight.enabled = true; } 
		//stars.SetActive (false);
		currentPhase = DayPhase.Dawn;  
	}  

	/// Sets the currentPhase to Day, ensuring full day color ambient light, and full  
	/// directional light intensity, if any.  
	public void SetDay()  
	{  
		RenderSettings.ambientLight = fullLight;  
		if (mainlight.GetComponent<Light>() != null)  
		{ mainlight.intensity = lightIntensity; }  
		currentPhase = DayPhase.Day;  
	}  

	/// Sets the currentPhase to Dusk.  
	public void SetDusk()  
	{  
		currentPhase = DayPhase.Dusk;  
	}  

	/// Sets the currentPhase to Night, ensuring full night color ambient light, and  
	/// turning off the directional light, if any.  
	public void SetNight()  
	{  
		RenderSettings.ambientLight = fullDark;  
		if (mainlight.GetComponent<Light>() != null)  
		{ mainlight.enabled = false; }
//		stars.SetActive (true);
		currentPhase = DayPhase.Night;  
	}  

	/// If the currentPhase is dawn or dusk, this method adjusts the ambient light color and direcitonal  
	/// light intensity (if any) to a percentage of full dark or full light as appropriate. Regardless  
	/// of currentPhase, the method also rotates the transform of this component, thereby rotating the  
	/// directional light, if any.  
	private void UpdateDaylight()  
	{  
		if (currentPhase == DayPhase.Dawn)  
		{  
			float relativeTime = currentCycleTime - dawnTime;  
			RenderSettings.ambientLight = Color.Lerp(fullDark, fullLight, relativeTime / halfquarterDay);  
			if (mainlight.GetComponent<Light>() != null)  
			{ mainlight.intensity = lightIntensity * (relativeTime / halfquarterDay); }  
		}  
		else if (currentPhase == DayPhase.Dusk)  
		{  
			float relativeTime = currentCycleTime - duskTime;  
			RenderSettings.ambientLight = Color.Lerp(fullLight, fullDark, relativeTime / halfquarterDay);  
			if (mainlight.GetComponent<Light>() != null)  
			{ mainlight.intensity = lightIntensity * ((halfquarterDay - relativeTime) / halfquarterDay); }  
		}  

		//transform.Rotate(Vector3.up * ((Time.deltaTime / dayCycleLength) * 360.0f), Space.Self);  
		transform.RotateAround (rotation.position, Vector3.forward, ((Time.deltaTime / dayCycleLength) * 360.0f));  
	}  

	private void UpdateSkyboxBlendFactor(){  
		//Debug.Log (SkyboxBlendFactor);
		if (currentPhase == DayPhase.Dawn)  
		{  
			float relativeTime = currentCycleTime - dawnTime;  
			SkyboxBlendFactor = 1 - (relativeTime / halfquarterDay);
			//Debug.Log (SkyboxBlendFactor);
		}  
		else if (currentPhase == DayPhase.Day)  
		{  
			SkyboxBlendFactor = 0.0f;  
			//Debug.Log (SkyboxBlendFactor);
		}  
		else if (currentPhase == DayPhase.Dusk)  
		{  
			float relativeTime = currentCycleTime - duskTime;  
			SkyboxBlendFactor = relativeTime / halfquarterDay;
			//Debug.Log (SkyboxBlendFactor);
		}  
		else if (currentPhase == DayPhase.Night)  
		{  
			SkyboxBlendFactor = 1.0f;  
			//Debug.Log (SkyboxBlendFactor);
		}  

		RenderSettings.skybox.SetFloat("_SkyBlend",SkyboxBlendFactor);  
		skyboxmaterial.SetFloat ("_SkyBlend", SkyboxBlendFactor);
	}  

	/// Interpolates the fog color between the specified phase colors during each phase's transition.  
	/// eg. From DawnDusk to Day, Day to DawnDusk, DawnDusk to Night, and Night to DawnDusk  
	private void UpdateFog()  
	{  
		if (currentPhase == DayPhase.Dawn)  
		{  
			float relativeTime = currentCycleTime - dawnTime;  
			RenderSettings.fogColor = Color.Lerp(dawnDuskFog, dayFog, relativeTime / halfquarterDay);  
		}  
		else if (currentPhase == DayPhase.Day)  
		{  
			float relativeTime = currentCycleTime - dayTime;  
			RenderSettings.fogColor = Color.Lerp(dayFog, dawnDuskFog, relativeTime / (quarterDay+halfquarterDay));  
		}  
		else if (currentPhase == DayPhase.Dusk)  
		{  
			float relativeTime = currentCycleTime - duskTime;  
			RenderSettings.fogColor = Color.Lerp(dawnDuskFog, nightFog, relativeTime / halfquarterDay);  
		}  
		else if (currentPhase == DayPhase.Night)  
		{  
			float relativeTime = currentCycleTime - nightTime;  
			RenderSettings.fogColor = Color.Lerp(nightFog, dawnDuskFog, relativeTime / (quarterDay+halfquarterDay));  
		}  
	}  

	/// Updates the World-time hour based on the current time of day.  
	private void UpdateWorldTime()  
	{  
		worldTimeHour = (int)((Mathf.Ceil((currentCycleTime / dayCycleLength) * hoursPerDay) + dawnTimeOffset) % hoursPerDay) + 1;  
		minutes = (int)(Mathf.Ceil((currentCycleTime* (60/timePerHour))% 60));  
	}  

	public enum DayPhase  
	{  
		Night = 0,  
		Dawn = 1,  
		Day = 2,  
		Dusk = 3  
	}  
} 
