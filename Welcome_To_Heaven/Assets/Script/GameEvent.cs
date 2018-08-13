using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEvent : MonoBehaviour {
	[SerializeField] private Text readyUpText = null;
	[SerializeField] private GameObject player = null;
	[SerializeField] private Transform cam = null;
	[SerializeField] private GameObject inventoryCover = null, m_inventory = null;
	[SerializeField] private GameObject spawner = null;

	private void Awake(){
		readyUpText.enabled = false;
		player.SetActive (false);
		inventoryCover.SetActive (false);
		m_inventory.SetActive (false);
		reached_heaven.enabled = false;
		heavenText.enabled = false;
		reached_bed.enabled = false;
		bedText.enabled = false;
	}

	private void Start () {
		Invoke ("StartReadyUp", 0.75f);
	}

	private void StartReadyUp(){
		StartCoroutine (ReadyUp (readyUpText));
	}

	private IEnumerator ReadyUp(Text txt){
		for (int i = 0; i < 4; i++) {
			txt.enabled = true;
			yield return new WaitForSeconds (0.3f);
			txt.enabled = false;
			yield return new WaitForSeconds (0.3f);
		}
		player.SetActive (true);
		inventoryCover.SetActive (true);
		m_inventory.SetActive (true);
		cam.SetParent (player.transform);
		spawner.SetActive (true);
		yield return null;
	}

	[SerializeField] private Transform bed = null;
	[SerializeField] private Animator anim_sleep = null;
	private int isBed = Animator.StringToHash("isBed");
	public IEnumerator GoToBed(){
		while (player.transform.position != bed.position) {
			player.transform.position = Vector3.MoveTowards (player.transform.position, bed.position, 2.5f * Time.deltaTime);
			yield return null;
		}
		cam.SetParent (transform);
		player.SetActive (false);
		anim_sleep.SetFloat (isBed, 1);
		yield return new WaitForSeconds(16.5f);
		CongratsEvent ();
		yield return null;
	}

	public void CongratsEvent(){
		StartCoroutine (Congrats (textPanel_bed, reached_bed, bedText));
	}

	[SerializeField] private Transform heaven = null;

	public IEnumerator GoToHeaven(){
		while (player.transform.position != heaven.position) {
			player.transform.position = Vector3.MoveTowards (player.transform.position, heaven.position, 2.5f * Time.deltaTime);
			yield return null;
		}
		cam.SetParent (transform);
		player.SetActive (false);
		StartCoroutine (Congrats (textPanel_heaven, reached_heaven, heavenText));
		yield return null;
	}

	[SerializeField] private GameObject textPanel_heaven = null, textPanel_bed = null;
	[SerializeField] private Text reached_heaven = null, reached_bed = null, heavenText = null, bedText = null;

	public IEnumerator Congrats(GameObject txtPanel,Text reachTxt, Text locationTxt){
		yield return new WaitForSeconds (0.5f);
		txtPanel.SetActive (true);
		yield return new WaitForSeconds (1f);
		reachTxt.enabled = true;
		yield return new WaitForSeconds (0.75f);
		locationTxt.enabled = true;
		yield return null;
	}
}
