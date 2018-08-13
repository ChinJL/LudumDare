using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	private string s_player = "Player";
	[SerializeField] private GameObject player = null;
	PlayerController plyerCtrl;
	[SerializeField] private GameObject itemManager = null;
	ItemList itemList;
	[SerializeField] private Sprite itemSprite = null;
	[SerializeField] private GameObject audioManager = null;
	AudioManager audioM;

	private void OnEnable(){
		player = GameObject.Find ("Player");
		if (player != null) {
			plyerCtrl = player.GetComponent<PlayerController> ();
		}
		itemManager = GameObject.Find ("ItemManager");
		itemList = itemManager.GetComponent<ItemList> ();
		audioManager = GameObject.Find ("AudioManager");
		audioM = audioManager.GetComponent<AudioManager> ();
	}

	private void OnTriggerEnter(Collider col){
		if (col.CompareTag (s_player)) {
			for (int i = 0; i < plyerCtrl.slots.Length; i++) {
				if (plyerCtrl.slots [i].sprite == itemList.emptySprite) {
					plyerCtrl.slots[i].sprite = itemSprite;
					audioM.ItemSound ();
					if (itemSprite == itemList.itemSprite [2]) {
						for (int j = 0; j < itemList.shieldTrigger.Length; j++) {
							itemList.shieldTrigger [j].GetComponent<Collider> ().enabled = false;
						}
					}
					gameObject.SetActive(false);
					break;
				}
			}
		}
	}
}
