using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundOnContactFinish : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Audio")]
    public AudioClip sound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlaySound();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Player"))
            return;

        PlaySound();
    }

    private void PlaySound()
    {
        if (sound != null)
        {
            audioSource.PlayOneShot(sound);
        }
    }
}
