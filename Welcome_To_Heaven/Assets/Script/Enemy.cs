using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private Vector3 initialPos, destination;
	[SerializeField] private Camera mainCam = null;
	//upside = 0.825f, downside = 0.135f;
	[SerializeField] private float offset_y_1 = 0.135f, offset_y_2 = 0.825f;
	[SerializeField] private SpriteRenderer spriteRend = null;

	[SerializeField] private float shootForce = 100;
	[SerializeField] private GameObject musicNote = null;
	[SerializeField] private int noteCount = 5;
	[SerializeField] private float interval = 1;
	[SerializeField] private Transform bulletSpawn = null;
	GameObject[] bullets = null;
	private GameObject player;
	private Vector3 dir;
	private bool shoot;
	GameEvent gameEvent;
	[SerializeField] GameObject eventManager = null;

	private void Awake(){
		bullets = new GameObject[noteCount];
		for(int i = 0; i < noteCount; i++){
			GameObject bullet = Instantiate (musicNote);
			bullets [i] = bullet;
			bullet.SetActive (false);
		}
		gameEvent = eventManager.GetComponent<GameEvent> ();
	}

	private void Start(){
		gameObject.SetActive (false);
	}

	//**adjust the tag of player after game end
	private void OnEnable () {
		player = GameObject.Find ("Player");

		if (gameEvent.isPreview == false) {
			transform.SetParent (mainCam.transform);
			StartCoroutine (MoveToDestination (offset_y_1, offset_y_2));
		}
	}

	private void Update(){
		if (shoot) {
			dir = (player.transform.position - transform.position) / (player.transform.position - transform.position).magnitude;
		}
	}

	//if camera is moving, place this object into camera to follow the movement
	private IEnumerator MoveToDestination(float side1, float side2){
		spriteRend.flipX = false;
		Vector3 temp_initialPos = Camera.main.ViewportToWorldPoint(new Vector3(0f, side1));
		initialPos = new Vector3 (temp_initialPos.x, temp_initialPos.y, 0);
		Vector3 temp_initial_destination = Camera.main.ViewportToWorldPoint (new Vector3 (1.25f, side1));
		destination = new Vector3 (temp_initial_destination.x, temp_initial_destination.y, 0);
		transform.position = initialPos;
		ShootMusicNotes ();
		while (transform.position != destination) {
			Vector3 temp_destination = Camera.main.ViewportToWorldPoint (new Vector3 (1.25f, side1));
			destination = new Vector3 (temp_destination.x, temp_destination.y, 0);
			transform.position = Vector3.MoveTowards (transform.position, destination, 4.5f * Time.deltaTime);
			yield return null;
		}
		yield return new WaitForSeconds(1.5f);
		spriteRend.flipX = true;
		temp_initialPos = Camera.main.ViewportToWorldPoint(new Vector3(1.25f, side2));
		initialPos = new Vector3 (temp_initialPos.x, temp_initialPos.y, 0);
		temp_initial_destination = Camera.main.ViewportToWorldPoint (new Vector3 (-0.25f, side2));
		destination = new Vector3 (temp_initial_destination.x, temp_initial_destination.y, 0);
		transform.position = initialPos;
		ShootMusicNotes ();
		while (transform.position != destination) {
			Vector3 temp_destination = Camera.main.ViewportToWorldPoint (new Vector3 (-0.25f, side2));
			destination = new Vector3 (temp_destination.x, temp_destination.y, 0);
			transform.position = Vector3.MoveTowards (transform.position, destination, 4.5f * Time.deltaTime);
			yield return null;
		}
		gameObject.SetActive (false);
		yield return null;
	}

	public void ShootMusicNotes(){
		shoot = true;
		Invoke ("Delay", 0.45f);
	}

	private void Delay(){
		if (gameObject.activeSelf)
			StartCoroutine (ShootOneByOne ());
	}

	public IEnumerator ShootOneByOne(){
		for (int i = 0; i < bullets.Length; i++) {
			bullets [i].SetActive (true);
			bullets [i].transform.position = bulletSpawn.transform.position;
			bullets [i].GetComponent<Rigidbody> ().AddForce (dir*shootForce, ForceMode.Impulse);
			yield return new WaitForSeconds (interval);
		}
		yield return shoot = false;
		yield return null;
	}

	public void CallEnemy(){
		gameObject.SetActive (true);
	}
}
