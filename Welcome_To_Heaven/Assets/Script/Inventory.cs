using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
	[SerializeField] private GameObject itemManager = null;
	ItemList itemList;
	[SerializeField] private GameObject player = null;
	PlayerController plyerCtrl;

	private void OnEnable(){
		itemList = itemManager.GetComponent<ItemList> ();
		plyerCtrl = player.GetComponent<PlayerController> ();
	}

	public IEnumerator UseItem(Image img, int j){
		if (img.sprite != itemList.emptySprite) {
			for (int i = 0; i < itemList.itemSprite.Length; i++) {
				if (img.sprite == itemList.itemSprite [i]) {
					if (i == 2) {
						plyerCtrl.shield.SetActive (true);
					}
					StartCoroutine (plyerCtrl.ManipulateFloatingForce (itemList.speedReduction [i], itemList.effectTime [i]));
					StartCoroutine (plyerCtrl.ScaleImg(img,itemList.effectTime[i]));
					yield return new WaitForSeconds (itemList.effectTime [i]);
					img.sprite = itemList.emptySprite;
					if (i == 2) {
						plyerCtrl.shield.SetActive (false);
					}
					if (i == 2) {
						yield return new WaitForSeconds (itemList.shieldTriggerCd);
						for (int k = 0; k < itemList.shieldTrigger.Length; k++) {
							itemList.shieldTrigger [k].GetComponent<Collider> ().enabled = true;
						}
					}
				}
			}
			ResetKey (j);
			yield return null;
		}
	}

	private void ResetKey(int i){
		if (i == 0) {
			plyerCtrl.slotKey [i] = plyerCtrl.slot1;
		}
		else if (i == 1) {
			plyerCtrl.slotKey [i] = plyerCtrl.slot2;
		}
		else if (i == 2) {
			plyerCtrl.slotKey [i] = plyerCtrl.slot3;
		}
	}
}
