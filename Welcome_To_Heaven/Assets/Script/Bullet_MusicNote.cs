using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_MusicNote : MonoBehaviour {
	private string s_plyer = "Player", s_shield = "Shield";
	private Rigidbody m_rb;

	[SerializeField] private Sprite[] musicNoteSprites = null;
	[SerializeField] private SpriteRenderer spriteRend = null;

	[SerializeField] private float floatingIncrement = 2f;
	[SerializeField] private float effectTime = 1f;
	private GameObject player;
	PlayerController plyrCtrl;

	void OnTriggerEnter(Collider col){
		if (col.CompareTag (s_plyer)) {
			gameObject.SetActive (false);
			plyrCtrl.StartCoroutine(plyrCtrl.ManipulateFloatingForce(floatingIncrement, effectTime));
		}
		if (col.CompareTag (s_shield)) {
			m_rb.velocity = (-m_rb.velocity)/1.75f;
		}
	}

	void OnEnable(){
		int rand = Random.Range (0, musicNoteSprites.Length);
		spriteRend.sprite = musicNoteSprites[rand];
		rand = Random.Range (0, 3);
		if (rand == 0) {
			spriteRend.color = Color.white;
		} else if (rand == 1) {
			spriteRend.color = Color.yellow;
		} else if (rand == 2) {
			spriteRend.color = Color.magenta;
		}
		m_rb = GetComponent<Rigidbody> ();
		Invoke ("Deactivate", 5f);
		player = GameObject.Find ("Player");
		plyrCtrl = player.GetComponent<PlayerController> ();
	}

	void Deactivate(){
		m_rb.velocity = Vector3.zero;
		gameObject.SetActive (false);
	}

	void Update(){
		transform.RotateAround (transform.position, Vector3.forward, 10);
	}
}
