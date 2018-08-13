using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour {
	private string s_player = "Player";
	[SerializeField] private GameObject eventManager = null;
	GameEvent gameEvent;
	[SerializeField] private bool toBed = false;
	private Collider m_col;

	[SerializeField] private GameObject player = null;
	PlayerController plyrCtrl;

	[SerializeField] private GameObject audioManager = null;

	private void OnEnable(){
		gameEvent = eventManager.GetComponent<GameEvent> ();
		m_col = GetComponent<Collider> ();
		plyrCtrl = player.GetComponent<PlayerController> ();
	}

	private void OnTriggerEnter(Collider col){
		if (col.CompareTag (s_player)) {
			col.tag = "Untagged";
			if (toBed) {
				audioManager.GetComponent<AudioManager> ().WinSound ();
				plyrCtrl.isPlaying = false;
				gameEvent.StartCoroutine (gameEvent.GoToBed ());
				m_col.enabled = false;
			} else {
				audioManager.GetComponent<AudioManager> ().LoseSound ();
				plyrCtrl.isPlaying = false;
				gameEvent.StartCoroutine (gameEvent.GoToHeaven ());
				m_col.enabled = false;
			}
		}
	}
}
