using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

	[Header("Audio Mixer")]
	public AudioMixer master;
	public AudioMixer music;
	public AudioMixer sounds;
	[Space(5)]
	public float maxVol;
	public float minVol;
	[Space(10)]
	[Header("Exposed Parameters")]
	public string masterVolume;
	public string musicVolume;
	public string soundsVolume;

	void Start () {}
	void Update () {}

	public void setMasterVol(float unit) {
		master.SetFloat (masterVolume, getDB (unit));
	}
	public void setmusicVol(float unit) {music.SetFloat (musicVolume, getDB (unit));}
	public void setsoundsVol(float unit) {sounds.SetFloat (soundsVolume, getDB (unit));}

	public float getDB(float unit) {
		return -10 * (maxVol - minVol) / 9 * Mathf.Pow (10, -unit) + 10 * (maxVol - minVol) / 9 + minVol;
	}

	public float getDefaultDB() {
		return Mathf.Log10 ((-10 * maxVol + minVol) / (-10 * maxVol + 10 * minVol));
	}
}