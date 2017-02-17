using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CreditsSceneManager : MonoBehaviour {

    public GameObject thankyouText;

    public float timeTillThankYouAppear;
    private float tempCreditsTime;

    AsyncOperation asyncOp;

	// Use this for initialization
	void Start () {
        tempCreditsTime = 0;
        thankyouText.SetActive(false);

        asyncOp = SceneManager.LoadSceneAsync("MainMenu");
        asyncOp.allowSceneActivation = false;
	}
	
	// Update is called once per frame
	void Update () {
        tempCreditsTime += Time.deltaTime;

        if(tempCreditsTime > timeTillThankYouAppear)
        {
            thankyouText.SetActive(true);
        }
	}

    public void crdReturnToMenuBtnPressed()
    {
        asyncOp.allowSceneActivation = true;
    }
}
