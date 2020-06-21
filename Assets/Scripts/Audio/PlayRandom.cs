using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayRandom : MonoBehaviour
{
	public AudioClip[] AudioClips;
	public float MinPitch = .9f;
	public float MaxPitch = 1.1f;
	public bool PlayOnAwake = true;

	private AudioSource _Source;

	private void Awake()
	{
		_Source = GetComponent<AudioSource>();
	}

	private void OnEnable()
	{
		if(PlayOnAwake)
		{
			PlayAudio(Random.Range(0, AudioClips.Length - 1));
		}
	}

	public void PlayAudio(int audioID)
	{
		_Source.pitch = Random.Range(MinPitch, MaxPitch);

		if (audioID >= 0 && audioID < AudioClips.Length)
		{
			_Source.PlayOneShot(AudioClips[audioID]);
		}
	}
}
