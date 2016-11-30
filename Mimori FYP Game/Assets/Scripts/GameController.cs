using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour {
	public string currentScene;
	public static bool loadingGame = false;
	public static GameController gameController;
	//Player Position
	public float playerPositionX;
	public float playerPositionY;
	public float playerPositionZ;
	//Player Rotation

	// Use this for initialization
	void Awake(){
		if (gameController == null) {
			DontDestroyOnLoad (gameObject);
			gameController = this;
		} else if (gameController != this) {
			Destroy (gameObject);
		}
	}

	void Start () {
		
	}

	void Update(){
		if (SceneManager.GetActiveScene ().name != "MainMenu") {
			currentScene = SceneManager.GetActiveScene ().name;
		}
		//Debug.Log (currentScene);
	}

	public void Save(){
		//Create a BinaryFormatter & a file
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/PlayerInfo.mi");

		//Create an object to save the data to
		PlayerData pData = new PlayerData();
		pData.playerPosX = playerPositionX;
		pData.playerPosY = playerPositionY;
		pData.playerPosZ = playerPositionZ;

		pData.curScene = currentScene;

		//Write the object to the file & close it
		bf.Serialize(file,pData);
		file.Close ();
	}

	public void Load(){
		if(File.Exists(Application.persistentDataPath + "/PlayerInfo.mi")){
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/PlayerInfo.mi", FileMode.Open);

			PlayerData pData = (PlayerData)bf.Deserialize (file);
			file.Close ();

			playerPositionX = pData.playerPosX;
			playerPositionY = pData.playerPosY;
			playerPositionZ = pData.playerPosZ;

			currentScene = pData.curScene;
		}
	}

	public void Delete(){
		if(File.Exists(Application.persistentDataPath + "/PlayerInfo.mi")){
			File.Delete (Application.persistentDataPath + "/PlayerInfo.mi");
		}
	}
}

[Serializable]
class PlayerData {
	public string curScene;
	//Player Position
	public float playerPosX;
	public float playerPosY;
	public float playerPosZ;
	//Player Rotation
}
