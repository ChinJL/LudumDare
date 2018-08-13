using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {
	private string s_player = "Player";
	[SerializeField] private GameObject eventManager = null;
	GameEvent gameEvent;
	[SerializeField] private bool toBed = false;
	private Collider m_col;

	[SerializeField] private GameObject player = null;
	PlayerController plyrCtrl;

	private void OnEnable(){
		gameEvent = eventManager.GetComponent<GameEvent> ();
		m_col = GetComponent<Collider> ();
		plyrCtrl = player.GetComponent<PlayerController> ();
	}

	private void OnTriggerEnter(Collider col){
		if (col.CompareTag (s_player)) {
			if (toBed) {
				plyrCtrl.isPlaying = false;
				gameEvent.StartCoroutine (gameEvent.GoToBed ());
				m_col.enabled = false;
			} else {
				plyrCtrl.isPlaying = false;
				gameEvent.StartCoroutine (gameEvent.GoToHeaven ());
				m_col.enabled = false;
			}
		}
	}
}
