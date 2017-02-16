using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class OutpostManager : MonoBehaviour
{
    public GameObject[] minions;
    public GameObject defaultCircle;
    public GameObject capturedCircle;
    public GameObject teleTo, teleBack,player;
    public Image transition;
    public Canvas mainCanvas;

    public AudioClip winning;
    FirstPersonController fpc;

    float originalWalkSpd, originalRunSpd;
    Color transitionAlpha;

    AudioSource bgm;
    //public bool isCaptured = false;
    private bool allDied = false;
    // Use this for initialization
    void Start()
    {
        fpc = player.GetComponent<FirstPersonController>();
        bgm = GameObject.Find("BGM").GetComponent<AudioSource>();
        transitionAlpha = transition.color;
        originalWalkSpd = fpc.m_WalkSpeed;
        originalRunSpd = fpc.m_RunSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.name == "Outpost 1")
        {
            if (GameController.gameController.outpost1Captured)
            {
                defaultCircle.SetActive(false);
                capturedCircle.SetActive(true);
            }
        }
        else if (this.gameObject.name == "Outpost 2")
        {
            if (GameController.gameController.outpost2Captured)
            {
                defaultCircle.SetActive(false);
                capturedCircle.SetActive(true);
            }
        }
        else if (this.gameObject.name == "Outpost 3")
        {
            if (GameController.gameController.outpost3Captured)
            {
                defaultCircle.SetActive(false);
                capturedCircle.SetActive(true);
            }
        }
        else if (this.gameObject.name == "Outpost 4")
        {
            if (GameController.gameController.outpost4Captured)
            {
                defaultCircle.SetActive(false);
                capturedCircle.SetActive(true);
            }
        }

        if (defaultCircle.activeSelf)
        {
            for (int i = 0; i < minions.Length; i++)
            {
                allDied = false;
                if (minions[i] == null)
                {
                    allDied = true;
                }
                else
                {
                    break;
                }
            }

            if (allDied)
            {
                if (this.gameObject.name == "Outpost 1")
                {
                    GameController.gameController.outpost1Captured = true;
                    
                }
                else if (this.gameObject.name == "Outpost 2")
                {
                    GameController.gameController.outpost2Captured = true;
                }
                else if (this.gameObject.name == "Outpost 3")
                {
                    GameController.gameController.outpost3Captured = true;
                }
                else if (this.gameObject.name == "Outpost 4")
                {
                    GameController.gameController.outpost4Captured = true;
                }
                if (!Player.wSoundPlayed)
                {
                    //bgm.clip = winning;
                    //bgm.Play();
                    player.transform.position = teleBack.transform.position;
                    GameController.gameController.checkSkillPoint++;
                    Player.wSoundPlayed = true;
                }
                defaultCircle.SetActive(false);
                capturedCircle.SetActive(true);
            }
        }
        if (transitionAlpha.a >= 1f)
        {
            fpc.m_WalkSpeed = originalWalkSpd;
            fpc.m_RunSpeed = originalRunSpd;
            player.transform.position = teleTo.transform.position;
            transitionAlpha.a = 0;
            mainCanvas.gameObject.SetActive(true);
            if (transition.gameObject.activeSelf)
            {
                transition.gameObject.SetActive(false);
            }
            transition.color = transitionAlpha;
        }
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Player") {
            if (gameObject.name == "Outpost 1" && !GameController.gameController.outpost1Captured)
            {
                EnterBattle();
            }
            if (gameObject.name == "Outpost 2" && !GameController.gameController.outpost2Captured)
            {
                EnterBattle();
            }
            if (gameObject.name == "Outpost 3" && !GameController.gameController.outpost3Captured)
            {
                EnterBattle();
            }
            if (gameObject.name == "Outpost 4" && !GameController.gameController.outpost4Captured)
            {
                EnterBattle();
            }
        }
    }

    void EnterBattle() {
        if (!transition.gameObject.activeSelf)
        {
            transition.gameObject.SetActive(true);
        }
        if (transitionAlpha.a <= 1f)
        {
            fpc.m_WalkSpeed = 0;
            fpc.m_RunSpeed = 0;
            mainCanvas.gameObject.SetActive(false);
            transitionAlpha.a += Time.deltaTime / 3;
        }
        transition.color = transitionAlpha;
    }
}
