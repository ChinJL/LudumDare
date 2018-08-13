using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour {
	private string s_player = "Player";
	Enemy enemy;
	[SerializeField] private GameObject m_enemy = null;
	ItemList itemList;
	[SerializeField] private GameObject itemManager = null;

	private void Awake(){
		enemy = m_enemy.GetComponent<Enemy> ();
		itemList = itemManager.GetComponent<ItemList> ();
	}

	private void OnTriggerEnter(Collider col){
		if (col.CompareTag (s_player)) {
			enemy.CallEnemy ();
			for (int i = 0; i < itemList.enemyTrigger.Length; i++) {
				itemList.enemyTrigger [i].GetComponent<Collider> ().enabled = false;
			}
			itemList.ResetEnemyTrigger ();
		}
	}
}
