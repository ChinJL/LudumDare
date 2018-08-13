using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEvent : MonoBehaviour {
	[SerializeField] private Text readyUpText = null;
	[SerializeField] private GameObject player = null;
	[SerializeField] private Transform cam = null;
	[SerializeField] private GameObject inventoryCover = null, m_inventory = null;
	[SerializeField] private GameObject spawner = null;
	[SerializeField] private GameObject backToMainButton = null;
	[SerializeField] private GameObject audioManager = null;
	public bool isPreview;

	private void Awake(){
		readyUpText.enabled = false;
		player.SetActive (false);
		inventoryCover.SetActive (false);
		m_inventory.SetActive (false);
		reached_heaven.enabled = false;
		heavenText.enabled = false;
		reached_bed.enabled = false;
		bedText.enabled = false;
		isPreview = true;
		backToMainButton.SetActive(false);
	}

	private void Start () {
		if (isPreview) {
			cam.transform.position = new Vector3 (cam.transform.position.x, -26, cam.transform.position.z);
		}
		audioManager.GetComponent<AudioManager> ().PlayMusic ();
	}

	public void StartReadyUp(){
		if (!isPreview) {
			StartCoroutine (ReadyUp (readyUpText));
		}
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
		player_sprite.SetActive (false);
		anim_sleep.SetFloat (isBed, 1);
		yield return new WaitForSeconds(16.5f);
		CongratsEvent ();
		yield return null;
	}

	public void CongratsEvent(){
		StartCoroutine (Congrats (textPanel_bed, reached_bed, bedText));
	}

	[SerializeField] private Transform heaven = null;
	[SerializeField] private GameObject player_sprite = null;
	[SerializeField] private Transform roof = null;
	public IEnumerator GoToHeaven(){
		while (player.transform.position != roof.position) {
			player.transform.position = Vector3.MoveTowards (player.transform.position, roof.position, 2.5f * Time.deltaTime);
			yield return null;
		}
		while (player.transform.position != heaven.position) {
			player.transform.position = Vector3.MoveTowards (player.transform.position, heaven.position, 3.25f * Time.deltaTime);
			yield return null;
		}
		cam.SetParent (transform);
		player_sprite.SetActive (false);
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
		backToMainButton.SetActive(true);
		yield return null;
	}

	public void GoToMainMenu(){
		SceneManager.LoadScene ("MainMenuScene");
	}
}
