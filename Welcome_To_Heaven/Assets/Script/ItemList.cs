using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour {

	public Sprite[] itemSprite;
	public Sprite emptySprite;
	public float[] speedReduction = null;
	public float[] effectTime = null;
	public GameObject[] shieldTrigger = null;
	public float shieldTriggerCd = 10;
	public GameObject[] items;
	public GameObject[] enemyTrigger = null;
	[SerializeField] private float enemyTriggerCD = 5f;

	public void ResetEnemyTrigger(){
		StartCoroutine (EnableEnemyTrigger ());
	}

	private IEnumerator EnableEnemyTrigger(){
		yield return new WaitForSeconds (enemyTriggerCD);
		for (int i = 0; i < enemyTrigger.Length; i++) {
			enemyTrigger [i].GetComponent<Collider> ().enabled = true;
		}
	}
}
