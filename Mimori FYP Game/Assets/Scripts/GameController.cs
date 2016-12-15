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
    public string curScene;
    //Player Position
    public float playerPosX;
    public float playerPosY;
    public float playerPosZ;
    //Outpost Captured
    public bool o1Captured = false;
    public bool o2Captured = false;
    public bool o3Captured = false;
    public bool o4Captured = false;
    public bool htActivated = false;
}
