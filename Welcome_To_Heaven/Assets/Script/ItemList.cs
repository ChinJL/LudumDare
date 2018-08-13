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
}
