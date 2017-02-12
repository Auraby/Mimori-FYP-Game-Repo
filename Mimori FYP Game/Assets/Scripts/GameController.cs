using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public string currentScene;
    public static bool loadingGame = false;
    public static GameController gameController;

    //Skill point
    public int checkSkillPoint;
    //Gun Mods
    public bool enmarAbsorbed = false;
    public bool zoltranAbsorbed = false;
    public bool ishiraAbsorbed = false;
    //Journal status
    public bool journal2Unlocked = false;
    public bool journal3Unlocked = false;
    public bool journal4Unlocked = false;
    //Combat status
    public bool fightingBoss = false;
    //Player Position
    public float playerPositionX;
    public float playerPositionY;
    public float playerPositionZ;
    //Outpost captured
    public bool outpost1Captured = false;
    public bool outpost2Captured = false;
    public bool outpost3Captured = false;
    public bool outpost4Captured = false;
    public bool houseTrapActivated = false;
    //Forest of Misery
    public bool hordeCleared = false;
    // Use this for initialization
    void Awake()
    {
        if (gameController == null)
        {
            DontDestroyOnLoad(gameObject);
            gameController = this;
        }
        else if (gameController != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        Debug.Log(enmarAbsorbed);
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            currentScene = SceneManager.GetActiveScene().name;
        }
        //Debug.Log (currentScene);
    }

    public void Save()
    {
        //Create a BinaryFormatter & a file
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/PlayerInfo.mi");

        //Create an object to save the data to
        PlayerData pData = new PlayerData();
        //Save Player Position
        pData.playerPosX = playerPositionX;
        pData.playerPosY = playerPositionY;
        pData.playerPosZ = playerPositionZ;
        //Save Player @ scene
        pData.curScene = currentScene;
        //Save Outpost Captured
        pData.o1Captured = outpost1Captured;
        pData.o2Captured = outpost2Captured;
        pData.o3Captured = outpost3Captured;
        pData.o4Captured = outpost4Captured;
        pData.htActivated = houseTrapActivated;
        //Save Horde Progress
        pData.hCleared = hordeCleared;
        //Dialogues
        pData.dialogueCountMimori = DialogueManager.mimoriDialogueCount;
        pData.dialogueCountGate = DialogueManager.enmarDialogueCount;
        pData.dialogueCountForest = DialogueManager.forestDialogueCount;
        pData.dialogueCountTemple = DialogueManager.templeIDialogueCount;
        //Skill point
        pData.skillPoint = checkSkillPoint;
        //Boss Cleared or not
        pData.eDied = EnmarController.enmarDied;
        pData.zDied = StartZoltran.zoltranDied;
        //Gun mod
        pData.eoeUnlocked = enmarAbsorbed;
        pData.sozUnlocked = zoltranAbsorbed;
        pData.hoiUnlocked = ishiraAbsorbed;
        //journals
        pData.j2Unlocked = journal2Unlocked;
        pData.j3Unlocked = journal3Unlocked;
        pData.j4Unlocked = journal4Unlocked;
        //Write the object to the file & close it
        bf.Serialize(file, pData);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerInfo.mi"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PlayerInfo.mi", FileMode.Open);

            PlayerData pData = (PlayerData)bf.Deserialize(file);
            file.Close();
            //load player position
            playerPositionX = pData.playerPosX;
            playerPositionY = pData.playerPosY;
            playerPositionZ = pData.playerPosZ;
            //load player scene
            currentScene = pData.curScene;
            //load outpost captured
            outpost1Captured = pData.o1Captured;
            outpost2Captured = pData.o2Captured;
            outpost3Captured = pData.o3Captured;
            outpost4Captured = pData.o4Captured;
            houseTrapActivated = pData.htActivated;
            //load horde progress
            hordeCleared = pData.hCleared;
            //Dialogues
            DialogueManager.mimoriDialogueCount = pData.dialogueCountMimori;
            DialogueManager.enmarDialogueCount = pData.dialogueCountGate;
            DialogueManager.forestDialogueCount = pData.dialogueCountForest;
            DialogueManager.templeIDialogueCount = pData.dialogueCountTemple;
            //skill point
            checkSkillPoint = pData.skillPoint;
            //Boss Cleared or not
            EnmarController.enmarDied = pData.eDied;
            StartZoltran.zoltranDied = pData.zDied;
            //Gun Mod
            enmarAbsorbed = pData.eoeUnlocked;
            zoltranAbsorbed = pData.sozUnlocked;
            ishiraAbsorbed = pData.hoiUnlocked;
        }
    }

    public void Delete()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerInfo.mi"))
        {
            File.Delete(Application.persistentDataPath + "/PlayerInfo.mi");
        }
    }
}

[Serializable]
class PlayerData
{
    //status
    public string curScene;
    public int skillPoint;
    //Player Position
    public float playerPosX;
    public float playerPosY;
    public float playerPosZ;
    //Gate of Telluris
    public bool eDied = false;
    public int dialogueCountGate;
    //Outskirt
    public int dialogueCountMimori;
    //Outpost Captured
    public bool o1Captured = false;
    public bool o2Captured = false;
    public bool o3Captured = false;
    public bool o4Captured = false;
    public bool htActivated = false;
    //Forest of Misery
    public bool zDied = false;
    public bool hCleared = false;
    public int dialogueCountForest;
    //Temple of Aphellion
    public int dialogueCountTemple;
    //Gun Mod Unlocked
    public bool eoeUnlocked = false;
    public bool sozUnlocked = false;
    public bool hoiUnlocked = false;
    //Journal unlocked
    public bool j2Unlocked = false;
    public bool j3Unlocked = false;
    public bool j4Unlocked = false;
}
