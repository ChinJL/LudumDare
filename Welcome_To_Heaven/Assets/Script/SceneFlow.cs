using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlow : MonoBehaviour {
	private bool isPause;
	[SerializeField] private GameObject buttonPanel = null;

	public void LoadGameScene(){
		SceneManager.LoadScene ("GameScene");
	}

	public void Exit(){
		Application.Quit();
	}

	public void PauseToggle(){
		isPause = !isPause;
		if (isPause) {
			Time.timeScale = 0;
		} 
		else {
			Time.timeScale = 1;
		}
		if (buttonPanel != null) {
			if (isPause) {
				buttonPanel.SetActive (true);
			} else {
				buttonPanel.SetActive (false);
			}
		}
	}

	public void Resume(){
		Time.timeScale = 1;
	}
}
