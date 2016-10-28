using UnityEngine;
using System.Collections;

public class MainMenuCameraController : MonoBehaviour {

    //Camera
    [Header("Camera pivot and Camera")]
    public GameObject CameraRotGO;
    public GameObject MainCamera;

    //Transition points
    [Header("Transition Points")]
    public Transform mainMenuViewTransform;
    public Transform optionsMenuViewTransform;
    public Transform creditsScreenTransform;
    [HideInInspector]
    public Transform pointToTransition;

    //Values
    [Header("Higher value = slower lerp speed")]
    public float lerpDelay;

    [HideInInspector]
    public float time;

    [Header("Camera Zoom in and out values")]
    public float zoomInValue;
    public float zoomOutValue;

    //Option Screen View Rotation
    [Header("Options Screen View Rotation")]
    public float opViewRotX;
    public float opViewRotY;
    public float opViewRotZ;
    // Quaternion.Euler(-3.1f, 166.25f, 0)

    //Credits Screen View Rotation
    [Header("Credits Screen View Rotation")]
    public float crViewRotX;
    public float crViewRotY;
    public float crViewRotZ;

    //bool
    bool moveToOptions = false;
    bool moveToMain = false;
    bool moveToCredits = false;

    public static MainMenuCameraController instance { get; set; }

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
        if(moveToMain == true)
        {
            time += Time.deltaTime;
            moveCameraUpdate();

            if(time > 3)
            {
                stopMoveCamera();
            }
        }
        else if (moveToOptions == true)
        {
            time += Time.deltaTime;
            moveCameraUpdate();

            if (time > 3)
            {
                stopMoveCamera();
            }
        }
	}

    public void moveCameraUpdate()
    {
        float lerpValue = time / lerpDelay;
        lerpValue = Mathf.Sin(lerpValue * Mathf.PI * 0.5f);


        if (moveToOptions == true)
        {
            CameraRotGO.transform.position = Vector3.Lerp(CameraRotGO.transform.position, pointToTransition.position, lerpValue);
            MainCamera.transform.localPosition = Vector3.Lerp(MainCamera.transform.localPosition, new Vector3(0, 0.3f, -3), lerpValue);
            CameraRotGO.transform.rotation = Quaternion.Slerp(CameraRotGO.transform.rotation, Quaternion.Euler(opViewRotX, opViewRotY, opViewRotZ), lerpValue);
        }

        else if (moveToCredits == true)
        {
            CameraRotGO.transform.position = Vector3.Lerp(CameraRotGO.transform.position, pointToTransition.position, lerpValue);
            MainCamera.transform.localPosition = Vector3.Lerp(MainCamera.transform.localPosition, new Vector3(0, 0.3f, -3), lerpValue);
            CameraRotGO.transform.rotation = Quaternion.Slerp(CameraRotGO.transform.rotation, Quaternion.Euler(crViewRotX, crViewRotY, crViewRotZ), lerpValue);
        }

        else if (moveToMain == true)
        {
            CameraRotGO.transform.position = Vector3.Lerp(CameraRotGO.transform.position, pointToTransition.position, lerpValue);
            MainCamera.transform.localPosition = Vector3.Lerp(MainCamera.transform.localPosition, new Vector3(0, 6f, -13.89f), lerpValue);
            CameraRotGO.transform.rotation = Quaternion.Slerp(CameraRotGO.transform.rotation, Quaternion.Euler(-2.59f, 0, 0), lerpValue);
        }
      
    }

    public void startMoveCameraToTitleScreen()
    {
        moveToMain = true;
        moveToOptions = false;
        moveToCredits = false;
        time = 0;

        pointToTransition = mainMenuViewTransform;
    }

    public void startMoveCameraToOptions()
    {
        moveToOptions = true;
        moveToMain = false;
        moveToCredits = false;
        time = 0;

        pointToTransition = optionsMenuViewTransform;
        
    }

    public void stopMoveCamera()
    {
        moveToMain = false;
        moveToOptions = false;
        moveToCredits = false;
        time = 0;

        pointToTransition = null;
    }
}
