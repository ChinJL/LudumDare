using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFlying : MonoBehaviour {

	[SerializeField] private Transform bird = null;
	[SerializeField] private SpriteRenderer[] birdSprites = null;
	[SerializeField] private Transform birdDestination = null, birdDefaultPos = null;
	private bool birdFlip;

	private void Update(){
		if (!birdFlip) {
			foreach (SpriteRenderer bSprite in birdSprites) {
				bSprite.flipX = true;
			}
			bird.transform.position = Vector3.MoveTowards (bird.transform.position, birdDestination.position, 2 * Time.deltaTime);
			if (bird.transform.position == birdDestination.position) {
				birdFlip = !birdFlip;
			}
		}
		else if (birdFlip) {
			foreach (SpriteRenderer bSprite in birdSprites) {
				bSprite.flipX = false;
			}
			bird.transform.position = Vector3.MoveTowards (bird.transform.position, birdDefaultPos.position, 2 * Time.deltaTime);
			if (bird.transform.position == birdDefaultPos.position) {
				birdFlip = !birdFlip;
			}
		}
	}
}
