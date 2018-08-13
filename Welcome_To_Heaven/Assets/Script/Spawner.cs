using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	[SerializeField] private int spawnLimit = 20;
	[SerializeField] private float refreshSpawn = 10f;
	[SerializeField] private GameObject player = null;
	private List<GameObject> spawnedItem1 = null, spawnedItem2 = null;
	[SerializeField] private GameObject itemManager = null;
	ItemList itemList;
	private GameObject item3;
	private int lastNo;
	private float minX = -6, maxX = 6;
	private float minY = -23, maxY = 18;
	private float posZ = 0;
	[SerializeField] private float force = 5;
	private bool startSpawn;
	[SerializeField] private float spawnInterval = 1;

	private void Awake(){
		itemList = itemManager.GetComponent<ItemList> ();
		spawnedItem1 = new List <GameObject>();
		spawnedItem2 = new List<GameObject> ();
		for (int i = 0; i < itemList.itemSprite.Length - 1; i++) {
			for (int j = 0; j < spawnLimit; j++) {
				GameObject spawnedItem = Instantiate (itemList.items [i]);
				spawnedItem.transform.SetParent (transform);
				if (i == 0) {
					spawnedItem1.Add (spawnedItem);
				} else if (i == 1) {
					spawnedItem2.Add (spawnedItem);
				}
				spawnedItem.SetActive (false);
			}
		}

		item3 = Instantiate(itemList.items [2]);
		item3.SetActive (false);
		gameObject.SetActive (false);
	}

	private void OnEnable(){
		startSpawn = true;
		Invoke ("StartSpawn", 2f);
	}

	private void StartSpawn(){
		StartCoroutine (SpawnItem ());
	}

	private IEnumerator SpawnItem(){
		while (startSpawn) {
			ChooseSpawnItem ();
			yield return new WaitForSeconds (spawnInterval);
		}
		yield return null;
	}

	private void ChooseSpawnItem(){
		int rand = 0;
		while (rand == lastNo) {
			rand = Random.Range (0, 2);
			if (rand != lastNo) {
				break;
			}
		}
		lastNo = rand;
		if (rand == 0) {
			for (int i = 0; i < spawnedItem1.Count; i++) {
				if (!spawnedItem1 [i].activeInHierarchy) {
					GameObject tempObj = spawnedItem1 [i];
					Spawn (tempObj);
					break;
				}
			}
		}
		if (rand == 1) {
			for (int i = 0; i < spawnedItem2.Count; i++) {
				if (!spawnedItem1 [i].activeInHierarchy) {
					GameObject tempObj = spawnedItem2 [i];
					Spawn (tempObj);
					break;
				}
			}
		}
	}

	private void Spawn(GameObject item){
		Rigidbody item_rb = item.GetComponent<Rigidbody> ();
		float posX = Random.Range (minX, maxX);
		float posY = Random.Range (minY, maxY);
		Vector3 spawnPoint = new Vector3 (posX, posY, posZ);
		item.transform.position = spawnPoint;
		item.SetActive (true);
		if (player.transform.position.y > item.transform.position.y) {
			item_rb.AddForce (Vector3.up * force, ForceMode.Impulse);
		} else if (player.transform.position.y < item.transform.position.y) {
			item_rb.AddForce (Vector3.down * force, ForceMode.Impulse);
		} else {
		}
		StartCoroutine (RefreshSpawn(item, item_rb));
	}

	private IEnumerator RefreshSpawn(GameObject item, Rigidbody m_rb){
		yield return new WaitForSeconds (refreshSpawn);
		m_rb.velocity = Vector3.zero;
		item.SetActive (false);
		yield return null;
	}
}
