using UnityEngine;
using System.Collections;

public class TeleportsPlayer : MonoBehaviour {
    public GameObject player;
    public GameObject teleParticle;
    public AudioClip teleing, teled;

    bool soundPlayed = false;
    AudioSource audio;
    bool cdStart = false;
    float teleCD = 3f;
	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (cdStart) {
            teleCD -= Time.deltaTime;
            teleParticle.gameObject.SetActive(true);
            teleParticle.transform.position = player.transform.position;
            if (!soundPlayed) {
                audio.PlayOneShot(teleing, 0.8f);
                soundPlayed = true;
            }
            
        }

        if (teleCD < 0) {
            teleCD = 0;
        }

        if (teleCD == 0) {
            audio.Stop();
            audio.PlayOneShot(teled, 0.5f);
            audio.Play();
            soundPlayed = false;
            player.transform.position = Checkpoint.savedPos;
            teleCD = 3f;
            cdStart = false;
            teleParticle.gameObject.SetActive(false);
        }
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            cdStart = true;
        }
    }
}
