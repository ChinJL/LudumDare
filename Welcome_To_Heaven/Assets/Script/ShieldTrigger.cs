using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTrigger : MonoBehaviour {
	private string s_player = "Player";
	[SerializeField] private GameObject spawner = null;
	Spawner spawn;
	[SerializeField] private GameObject itemManager = null;
	ItemList itemList;

	private void OnEnable(){
		spawn = spawner.GetComponent<Spawner> ();
		itemList = itemManager.GetComponent<ItemList> ();
	}

	private void OnTriggerEnter(Collider col){
		if (col.CompareTag (s_player)) {
			spawn.item3.SetActive (true);
			Vector3 destination = Camera.main.ViewportToWorldPoint (new Vector3 (-0.1f, 0.5f));
			spawn.item3.transform.position = new Vector3 (destination.x, destination.y, spawn.item3.transform.position.z);
			for(int i = 0; i < itemList.shieldTrigger.Length; i++){
				itemList.shieldTrigger [i].GetComponent<Collider> ().enabled = false;
			}
		}
	}
}
