using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DataTransfer : MonoBehaviour {

	private bool isStarted=false;

	public float TimeTransfer = 10;
	public Transform MainCamPosition;
	private Vector3 TranslateVector;
	private Vector3 initialPos;
	private float TotalTime=0;
	private float TotalTimeLoop=0;
	public int loopCountperTimeTransfer=10;
	public GameObject GUI3D;
	public Sprite RightGUISprite;
	public GameObject RightGUI_R;
	public GameObject RightGUI_L;

	
	// Update is called once per frame
	void Update () {


		if (isStarted) 
		{
			float fTime=Time.deltaTime;

			TotalTime+=fTime;
	
			TotalTimeLoop +=fTime ;

	

				if(TotalTime<TimeTransfer)
				{
					if(TotalTimeLoop<(TimeTransfer/loopCountperTimeTransfer))
					{
						
						transform.position=initialPos+(TotalTimeLoop/(TimeTransfer/loopCountperTimeTransfer))*TranslateVector;
					}
					else
					{
						TotalTimeLoop=0;


					}
				}
				else
				{
					GUI3D.SetActive(true);
					RightGUI_R.SetActive(true);
				RightGUI_L.SetActive(true);
				RightGUI_R.GetComponent<Image>().sprite=RightGUISprite;
				RightGUI_L.GetComponent<Image>().sprite=RightGUISprite;
					StopTransfer();
				}

			
		}
	}

	public void updateGUIRight()
	{
		GUI3D.SetActive(true);
		RightGUI_R.SetActive(true);
		RightGUI_L.SetActive(true);
		RightGUI_R.GetComponent<Image>().sprite=RightGUISprite;
		RightGUI_L.GetComponent<Image>().sprite=RightGUISprite;
		StopTransfer();
	}
	public void StartTransfer(){

		TranslateVector = MainCamPosition.position - transform.position;
		initialPos = transform.position;
		isStarted = true;
		TotalTime = 0;
		TotalTimeLoop = 0;
		if (RightGUI_R && RightGUI_L) 
		{
			RightGUI_R.SetActive (false);
			RightGUI_L.SetActive (false);
		}


	}
	void StopTransfer(){
		

		gameObject.SetActive (false);

		isStarted = false;
	
		
	}
}
