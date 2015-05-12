using UnityEngine;
using System.Collections;
using UnityEngine.UI;




public class MainGUI : MonoBehaviour {


	public string StartPopUpsTag;
	public string EndPopUpsTag;
	public float GameTime=180;
	public float timeBeforeQuit=5;
	private GameObject[] tableEndPopUps;
	public static GameObject[] tableProgressBar;
	public string ProgressBarsTag;
	void ShowEndPopUp(){

	

		foreach(GameObject obj in tableEndPopUps)
		{
			obj.SetActive(true);
		}

		Invoke ("quit", timeBeforeQuit);
	}
	void HideStart()
	{
		GameObject [] table=GameObject.FindGameObjectsWithTag(StartPopUpsTag);
		foreach(GameObject obj in table)
		{
			obj.SetActive(false);
		}
	
	}
	// Use this for initialization
	void Start () {

		tableProgressBar=GameObject.FindGameObjectsWithTag(ProgressBarsTag);
		foreach(GameObject obj in tableProgressBar)
		{
			obj.SetActive(false);
		}
		tableEndPopUps=GameObject.FindGameObjectsWithTag(EndPopUpsTag);
		foreach(GameObject obj in tableEndPopUps)
		{
			obj.SetActive(false);
		}

		Invoke ("HideStart", 2);
		Invoke ("ShowEndPopUp", GameTime);
	
	}
	void quit()
	{
		Debug.Log ("quit");
		Application.OpenURL ("http://www.ni-pigeons-ni-espions.fr/");
		//Application.LoadLevel (Application.loadedLevel);
		//Application.Quit ();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
