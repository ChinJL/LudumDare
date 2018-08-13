using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public static AudioManager audioManager;
	[SerializeField] private AudioClip[] audios = null;
	[SerializeField] private AudioSource bgmSource = null, sfxSource = null;
	private bool isBGM, isSFX;

	public enum Sfx{
		angel, item, win, lose, button
	}

	private void Awake(){
		audioManager = this;
		sfxSource = audioManager.GetComponent<AudioSource> ();
		isBGM = true;
	}

	public void PlayMusic(){
		audioManager.bgmSource.Play ();
	}

	private void PlaySfx(Sfx element){
		if (element == Sfx.angel) {
			audioManager.sfxSource.PlayOneShot (audioManager.audios[0]);
		}
		if (element == Sfx.item) {
			audioManager.sfxSource.PlayOneShot (audioManager.audios [1]);
		}
		if (element == Sfx.lose) {
			audioManager.sfxSource.PlayOneShot (audioManager.audios [3]);
		}
		if (element == Sfx.button) {
			audioManager.sfxSource.PlayOneShot (audioManager.audios [4]);
		}
		if (element == Sfx.win) {
			audioManager.sfxSource.PlayOneShot (audioManager.audios [2]);
		}
	}

	public void ClickButton(){
		PlaySfx (Sfx.button);
	}

	public void WinSound(){
		PlaySfx (Sfx.win);
	}

	public void LoseSound(){
		PlaySfx (Sfx.lose);
	}

	public void ItemSound(){
		PlaySfx (Sfx.item);
	}

	public void Angel(){
		PlaySfx (Sfx.angel);
	}

	public void ToggleBgm(){
		isBGM = !isBGM;
		if (isBGM) {
			bgmSource.volume = 1;
		} else {
			bgmSource.volume = 0;
		}
	}

	public void ToggleSfx(){
		isSFX = !isSFX;
		if (isSFX) {
			sfxSource.volume = 1;
		} else {
			sfxSource.volume = 0;
		}
	}
}
