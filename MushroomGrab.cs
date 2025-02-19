using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MushroomGrab : MonoBehaviour
{
    public AudioClip grabSound; // 抓取音效
    private AudioSource audioSource; // 音效播放器
    private bool hasPlayedSound = false; // 标记音效是否已播放

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // 监听抓取事件
    public void OnGrabbed(SelectEnterEventArgs args)
    {
        if (!hasPlayedSound)
        {
            PlayGrabSound();
            hasPlayedSound = true;
        }
    }

    private void PlayGrabSound()
    {
        if (grabSound != null)
        {
            audioSource.PlayOneShot(grabSound);
        }
    }
}
