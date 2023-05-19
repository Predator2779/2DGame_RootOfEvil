using Newtonsoft.Json.Linq;
using UnityEngine;

public class PlayerAudioHandler : MonoBehaviour
{
    [SerializeField] private AudioClip[] aClips;
    [SerializeField] private float _stepDelay;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeStep()
    {
        int value = Random.Range(0, aClips.Length);

        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.clip = aClips[value];
            audioSource.PlayDelayed(_stepDelay);
        }
    }

    public void StopPlaying()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}