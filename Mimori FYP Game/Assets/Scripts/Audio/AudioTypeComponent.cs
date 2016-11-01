using UnityEngine;
using System.Collections;

public class AudioTypeComponent : MonoBehaviour {

    public enum audioType { BGM,SFX}

    public audioType currentType;

    public static AudioTypeComponent instance { get; set; }
	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
