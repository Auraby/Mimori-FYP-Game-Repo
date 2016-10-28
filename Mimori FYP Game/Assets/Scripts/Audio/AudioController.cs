using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AudioController : MonoBehaviour {


    //Sliders
    public Slider MasterVolSlider;
    public Slider BGMSlider;
    public Slider SFXSlider;

    //Floats
    public const float defaultVolume = 1f;

    //Lists & Arrays
    
    //BGM
    public GameObject[] BGMGO;
    public List<AudioSource> BGMList = new List<AudioSource>();

    //SFX
    public GameObject[] SFXGO;
    public List<AudioSource> SFXList = new List<AudioSource>();

    //For Testing SFX
    public AudioSource test1;

    public static AudioController instance { get; set; }

    void Awake()
    {
        //Refresh for new audio sources
        ClearAudioSources();

        //Get new audio sources
        GetAllBGMAudioSources();
        GetAllSFXAudioSources();
    }

	// Use this for initialization
	void Start () {

        instance = this;

        #region Master Volume
        if (PlayerPrefs.HasKey("MasterVolumeLevel"))
        {
            MasterVolSlider.normalizedValue = PlayerPrefs.GetFloat("MasterVolumeLevel");
            AudioListener.volume = PlayerPrefs.GetFloat("MasterVolumeLevel");

            Debug.Log("Got Master Volume Save");
        }
        else
        {
            MasterVolSlider.normalizedValue = defaultVolume;
            AudioListener.volume = defaultVolume;

            Debug.Log("No Master Volume Save");
        }
        #endregion

        #region BGM Volume
        if (PlayerPrefs.HasKey("BGMVolumeLevel"))
        {
            BGMSlider.normalizedValue = PlayerPrefs.GetFloat("BGMVolumeLevel");
            if (BGMList != null)
            {
                foreach (AudioSource bgmSource in BGMList)
                {
                    bgmSource.volume = PlayerPrefs.GetFloat("BGMVolumeLevel");
                }
            }
        }
        else
        {           
            BGMSlider.normalizedValue = defaultVolume;
            if (BGMList != null)
            {
                foreach (AudioSource bgmSource in BGMList)
                {
                    bgmSource.volume = defaultVolume;
                }
            }
        }
        #endregion

        #region SFX
        if (PlayerPrefs.HasKey("SFXVolumeLevel"))
        {
            SFXSlider.normalizedValue = PlayerPrefs.GetFloat("SFXVolumeLevel");
            if (SFXList != null)
            {
                foreach (AudioSource sfxSource in SFXList)
                {
                    sfxSource.volume = PlayerPrefs.GetFloat("SFXVolumeLevel");
                }
            }
        }
        else
        {         
            SFXSlider.normalizedValue = defaultVolume;
            if (SFXList != null)
            {
                foreach (AudioSource sfxSource in SFXList)
                {
                    sfxSource.volume = defaultVolume;
                }
            }
        }
        #endregion

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void updateMasterVolume()
    {
        AudioListener.volume = MasterVolSlider.normalizedValue;

        PlayerPrefs.SetFloat("MasterVolumeLevel", MasterVolSlider.normalizedValue);
    }

    public void updateBGMVolume()
    {
        if (BGMList != null)
        {
            foreach (AudioSource bgmSource in BGMList)
            {
                bgmSource.volume = BGMSlider.normalizedValue;

                
            }
        }

        PlayerPrefs.SetFloat("BGMVolumeLevel", BGMSlider.normalizedValue);
    }

    public void updateSFXVolume()
    {
        if (SFXList != null)
        {
            foreach (AudioSource sfxSource in SFXList)
            {
                sfxSource.volume = SFXSlider.normalizedValue;

               
            }
        }

        PlayerPrefs.SetFloat("SFXVolumeLevel", SFXSlider.normalizedValue);
      
    }

    public void GetAllBGMAudioSources()
    {
        
        if (BGMGO == null)
        {
            BGMGO = GameObject.FindGameObjectsWithTag("BGM");

            if (BGMGO != null)
            {
                foreach (GameObject bgmGameObject in BGMGO)
                {
                    if (bgmGameObject.GetComponent<AudioSource>() != null)
                    {
                        BGMList.Add(bgmGameObject.GetComponent<AudioSource>());
                    }
                }
            }
            else
            {
                Debug.Log("No BGM sources found");
            }
        }      
        
    }

    //Might need to change, depending on other codes, some sfx objects may need other tags
    public void GetAllSFXAudioSources()
    {

        if (SFXGO == null)
        {
            SFXGO = GameObject.FindGameObjectsWithTag("SFX");

            if (SFXGO != null)
            {
                foreach (GameObject sfxGameObject in SFXGO)
                {
                    if (sfxGameObject.GetComponent<AudioSource>() != null)
                    {
                        SFXList.Add(sfxGameObject.GetComponent<AudioSource>());
                    }

                }
            }
            else
            {
                Debug.Log("No SFX sources found");
            }
        }        
    }

    //Reset Data
    public void ClearAudioSources()
    {
        BGMGO = null;
        SFXGO = null;

        BGMList.Clear();
        SFXList.Clear();
    }

    public void PlayTestSounds()
    {
        test1.Play();
    }
}
