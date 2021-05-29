using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏音效
/// </summary>
public class AudioManager : MonoBehaviour {

	public static AudioManager instance { get; private set; }

	private AudioSource audioS;

	// Use this for initialization
	void Start () {
		instance = this;
		audioS = GetComponent<AudioSource>();
	}
	
	public void AudioPlay(AudioClip clip)
    {
		audioS.PlayOneShot(clip);
    }
}
