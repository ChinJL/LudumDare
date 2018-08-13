using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour {
	[SerializeField] private GameObject mc_temp = null;
	[SerializeField] Camera mainCam = null;
	private bool lerpBackCam;
	GameEvent gameEvent;
	[SerializeField] private GameObject EventManager = null;
	[SerializeField] private GameObject audioManager = null;

	void Awake(){
		mc_temp.SetActive (false);
		gameEvent = EventManager.GetComponent<GameEvent> ();
	}

	void DisplayTempMC(){
		mc_temp.SetActive (true);
		Invoke ("HideTempMC", 2f);
	}

	void HideTempMC(){
		mc_temp.SetActive (false);
		lerpBackCam = true;
	}

	void StopLerpCam(){
		lerpBackCam = false;
		gameEvent.isPreview = false;
		Invoke ("PlayGame", 2f);
	}

	void PlayGame(){
		gameEvent.StartReadyUp();
		audioManager.GetComponent<AudioManager> ().PlayMusic2 ();
	}

	void Update(){
		if (mc_temp.activeSelf) {
			mc_temp.transform.Translate (Vector3.up * Time.deltaTime);
		}
		if (lerpBackCam) {
			mainCam.transform.position = Vector3.MoveTowards (mainCam.transform.position, new Vector3 (0, 1, -10), 3 * Time.deltaTime);
			if(mainCam.transform.position == new Vector3 (0, 1, -10)){
				StopLerpCam ();
			}
		}
	}
}
