using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gazed : MonoBehaviour {
	
	// Use this for initialization
	public float ProgressBarTotalTime = 0.8f;
	public float TimeBeforeDisable=10;
	private float TimeBefore = 0;
	private float TimeProgressBar;
	
	public GameObject Data;
	private bool isStartedGazed=false;
	private bool isDataTransfert=false;
	public GameObject GUI3D;
	private GameObject[] tableProgressBar;
	public  Sprite RightGUISprite;
	private bool alreadyGazed=false;
	
	
	void Start () 
	{
		Data=GameObject.Instantiate (Data);
		Data.GetComponent<DataTransfer>().GUI3D = GUI3D;
		Data.GetComponent<DataTransfer>().RightGUISprite = RightGUISprite;

	}
	// Update is called once per frame
	void Update () 
	{
	
		if(isDataTransfert)
		{
			
			TimeBefore-=Time.deltaTime;
			if(TimeBefore>0)
			{
				isStartedGazed=false;
			}
			else
			{
				isDataTransfert=false;
				this.alreadyGazed=true;
			}
		}
		if (isStartedGazed ) 
		{
			TimeProgressBar+=Time.deltaTime;
			tableProgressBar = MainGUI.tableProgressBar;
			foreach(GameObject obj in tableProgressBar)
			{
				
				obj.GetComponent<Slider>().value=(TimeProgressBar/ProgressBarTotalTime);
			}
			if(TimeProgressBar>ProgressBarTotalTime)
			{
				
				foreach(GameObject obj in tableProgressBar)
				{
					obj.SetActive (false);
					
				}
				startDataTransfer();
				isStartedGazed=false;
				TimeBefore=TimeBeforeDisable;
				isDataTransfert=true;
			}
		}
		
	}
	
	
	
	
	public void startDataTransfer()
	{
		
		Data.transform.position = transform.position;
		Data.SetActive (true);
		Data.GetComponent <DataTransfer> ().StartTransfer ();
		TimeBefore=TimeBeforeDisable;
		
		
	}
	public void StartGaze()
	{
		if (!this.alreadyGazed) 
		{
			if (!isDataTransfert) 
			{
				isStartedGazed = true;
				TimeProgressBar = 0;
				tableProgressBar = MainGUI.tableProgressBar;
				foreach(GameObject obj in tableProgressBar)
				{
					obj.SetActive (true);
				}
			}
		}
		
		else
		{
			Data.GetComponent <DataTransfer> ().updateGUIRight();
		}
	}
	
	public void EndGaze()
	{
		isStartedGazed = false;
		tableProgressBar = MainGUI.tableProgressBar;
		foreach(GameObject obj in tableProgressBar)
		{
			obj.SetActive (false);
		}
		
		
	}
}
