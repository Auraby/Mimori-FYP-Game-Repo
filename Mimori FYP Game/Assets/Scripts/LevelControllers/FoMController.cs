using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FoMController : MonoBehaviour {
	public Text cdText;
	public static bool timerStart = false;
	public GameObject[] spawns;
	public GameObject MeleeMinion, RangeMinion;
	public static int minionCount;
	public GameObject blockPlayer, magicBarrier;
    public GameObject generator;
    public AudioClip defaultBGM, hordeBGM, zoltranBGM;
    
    AudioSource audio;
    bool playedDefaultBGM = false;
    bool playedHordeBGM = false;
	bool hordeOn = false;
	float timeRemaining = 10f;
	float spawnCD;
    bool hordeSpawned = false;
	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        audio.clip = defaultBGM;
        audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
		TimerCountdown ();
		SpawnMinions ();
		if (minionCount <= 0) {
			blockPlayer.SetActive (false);
			cdText.text = "";
            if (hordeSpawned)
            {
                GameController.gameController.hordeCleared = true;
            }
        }
        
        if (GameController.gameController.hordeCleared) {
            generator.gameObject.SetActive(false);
            magicBarrier.gameObject.SetActive(false);
            if (playedHordeBGM) {
                audio.volume -= Time.deltaTime;
            }
            if (audio.volume <= 0) {
                playedHordeBGM = false;
            }
        }
        if (GameController.gameController.hordeCleared) {
            if (!playedDefaultBGM && !playedHordeBGM) {
                audio.clip = defaultBGM;
                audio.Play();
                audio.volume = 0.2f;
                if (audio.volume < 0.2f) {
                    audio.volume += Time.deltaTime / 4;
                }
                if (audio.volume >= 0.2f) {
                    playedDefaultBGM = true;
                }
            }
        }
	}

	void TimerCountdown(){
		if (timerStart) {
            if (!playedHordeBGM && !GameController.gameController.hordeCleared) {
                audio.clip = hordeBGM;
                audio.volume = 0.8f;
                audio.Play();
                playedHordeBGM = true;
            }
			cdText.gameObject.SetActive (true);
			blockPlayer.SetActive (true);
			if (timeRemaining > 0) {
				timeRemaining -= Time.deltaTime;
				cdText.text = timeRemaining.ToString("F0");
				hordeOn = true;
			}
			if (timeRemaining <= 0) {
				timeRemaining = 0;
				hordeOn = false;
				if (minionCount > 0) {
					cdText.text = "Clear the remaining monsters";
                    magicBarrier.SetActive(false);
                }
			}

		}
	}

	void SpawnMinions(){
		if (hordeOn) {
            hordeSpawned = true;
			if (minionCount <= 15) {
				if (spawnCD <= 0) {
					int rand = Random.Range (0, 4);
					if (rand == 0) {
						GameObject rangeM = (GameObject)Instantiate (RangeMinion, GetSpawnPoint (), Quaternion.identity);
						spawnCD = 2f;
						minionCount++;
					} else {
						GameObject meleeM = (GameObject)Instantiate (MeleeMinion, GetSpawnPoint (), Quaternion.identity);
						spawnCD = 2f;
						minionCount++;
					}
				}
				if (spawnCD > 0) {
					spawnCD -= Time.deltaTime;
				}
			}
		}
	}

	Vector3 GetSpawnPoint()
	{
		int rand = Random.Range(0, spawns.Length);
		return spawns[rand].transform.position;
	}
}
