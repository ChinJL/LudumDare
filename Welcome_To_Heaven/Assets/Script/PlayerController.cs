using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	
	private Rigidbody m_rb;
	[SerializeField] private float speed = 15;
	private float hAxis;
	private Vector3 movement;
	private float m_speed;
	public GameObject shield = null;
	[SerializeField] private Transform spriteObj = null;
	private SpriteRenderer m_spriteRend;

	public float floatForce = 1;
	public float speedModifier = 1;

	[SerializeField] private GameObject inventoryManager = null;
	Inventory inventory;
	public Image[] slots = null;

	public KeyCode[] slotKey = null;
	public KeyCode slot1 = KeyCode.Z, slot2 = KeyCode.X, slot3 = KeyCode.C;

	ItemList itemList;
	[SerializeField] private GameObject itemManager = null;

	[SerializeField] private Animator anim = null;
	private int isMove = Animator.StringToHash("isMove");

	public bool isPlaying = true;

	private void OnEnable(){
		m_rb = GetComponent<Rigidbody> ();
		m_rb.useGravity = false;
		m_speed = speed * Time.deltaTime;
		m_spriteRend = spriteObj.GetComponent<SpriteRenderer> ();
		inventory = inventoryManager.GetComponent<Inventory> ();
		slotKey = new KeyCode[3];
		slotKey [0] = slot1;
		slotKey [1] = slot2;
		slotKey [2] = slot3;
		itemList = itemManager.GetComponent<ItemList> ();
	}

	private void Update(){
		if (isPlaying) {
			hAxis = Input.GetAxis ("Horizontal");
			movement = new Vector3 (hAxis, (floatForce + speedModifier) * Time.deltaTime, 0) * m_speed;

			if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A)) {
				m_spriteRend.flipX = true;
			}
			if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D)) {
				m_spriteRend.flipX = false;
			}

			if (Input.GetButtonDown ("Horizontal")) {
				anim.SetFloat (isMove, 1);
			}

			if (Input.GetButtonUp ("Horizontal")) {
				anim.SetFloat (isMove, 0);
			}

			if (Input.GetKeyDown (slotKey [0])) {
				int i = 0;
				if (slots [i].sprite != itemList.emptySprite) {
					slotKey [i] = KeyCode.None;
					StartCoroutine (inventory.UseItem (slots [i], i));
				}
			}
			if (Input.GetKeyDown (slotKey [1])) {
				int i = 1;
				if (slots [i].sprite != itemList.emptySprite) {
					slotKey [i] = KeyCode.None;
					StartCoroutine (inventory.UseItem (slots [i], i));
				}
			}
			if (Input.GetKeyDown (slotKey [2])) {
				int i = 2;
				if (slots [i].sprite != itemList.emptySprite) {
					slotKey [i] = KeyCode.None;
					StartCoroutine (inventory.UseItem (slots [i], i));
				}
			}
		}
	}

	private void FixedUpdate(){
		if (isPlaying) {
			m_rb.MovePosition (transform.position + movement);
		}
	}

	public IEnumerator ManipulateFloatingForce(float floatIncrement, float effectTime){
		speedModifier = speedModifier + floatIncrement;
		yield return new WaitForSeconds (effectTime);
		speedModifier = speedModifier - floatIncrement;
		yield return null;
	}

	public IEnumerator ScaleImg(Image img, float duration){
		img.fillAmount = duration;
		while (img.fillAmount > 0) {
			img.fillAmount -= Time.deltaTime/duration;
			yield return null;
		}
		img.fillAmount = 1;
		img.fillAmount += 1;
		yield return null;
	}
}
