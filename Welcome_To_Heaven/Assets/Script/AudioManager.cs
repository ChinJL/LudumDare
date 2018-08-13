using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

	public static AudioManager audioManager;
	[SerializeField] private AudioClip[] audios = null;
	[SerializeField] private AudioSource bgmSource1 = null, bgmSource2 = null, sfxSource = null;
	private static bool isBGM = true, isSFX = true;
	[SerializeField] private Image bgmImg = null, sfxImg = null;
	[SerializeField] private Sprite musicOn = null, musicOff = null, soundOn = null, soundOff = null;

	public enum Sfx{
		angel, item, win, lose, button
	}

	private void Awake(){
		audioManager = this;
		sfxSource = audioManager.GetComponent<AudioSource> ();
		isBGM = true;
	}

	private void Start(){
		if (PlayerPrefs.GetFloat ("default") == 0) {
			PlayerPrefs.SetFloat ("default", 1);
			PlayerPrefs.SetFloat ("isBGM", 1);
			PlayerPrefs.SetFloat ("isSFX", 1);
		}
		if (PlayerPrefs.GetFloat ("isBGM") == 1) {
			isBGM = true;
		} else {
			isBGM = false;
		}
		if (PlayerPrefs.GetFloat ("isSFX") == 1) {
			isSFX = true;
		} else {
			isSFX = false;
		}
		if (isBGM) {
			if (bgmImg != null) {
				bgmImg.sprite = musicOn;
			}
			bgmSource1.volume = 1;
			bgmSource2.volume = 1;
		} else {
			if (bgmImg != null) {
				bgmImg.sprite = musicOff;
			}
			bgmSource1.volume = 0;
			bgmSource2.volume = 0;
		}
		if (isSFX) {
			if (sfxImg != null) {
				sfxImg.sprite = soundOn;
			}
			sfxSource.volume = 1;
		} else {
			if (sfxImg != null) {
				sfxImg.sprite = soundOff;
			}
			sfxSource.volume = 0;
		}
	}

	public void PlayMusic(){
		audioManager.bgmSource1.Play ();
	}

	public void PlayMusic2(){
		audioManager.bgmSource1.Stop ();
		audioManager.bgmSource2.Play ();
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
			PlayerPrefs.SetFloat ("isBGM", 1);
			bgmImg.sprite = musicOn;
			bgmSource1.volume = 1;
			bgmSource2.volume = 1;
		} else {
			PlayerPrefs.SetFloat ("isBGM", 0);
			bgmImg.sprite = musicOff;
			bgmSource1.volume = 0;
			bgmSource2.volume = 0;
		}
	}

	public void ToggleSfx(){
		isSFX = !isSFX;
		if (isSFX) {
			PlayerPrefs.SetFloat ("isSFX", 1);
			sfxImg.sprite = soundOn;
			sfxSource.volume = 1;
		} else {
			PlayerPrefs.SetFloat ("isSFX", 0);
			sfxImg.sprite = soundOff;
			sfxSource.volume = 0;
		}
	}
}
