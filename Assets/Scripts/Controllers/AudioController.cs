using Configs;
using UnityEngine;

namespace Controllers
{
    public abstract class AudioController : MonoBehaviour
    {
        [Header("Audio Initialization")]
        [SerializeField] private AudioSource audioSourcePrefab;

        protected void PlayAudio(AudioClip[] audioClip, float pitch = 1.0f, float volume = 1.0f)
        {
            int randomIndex = Random.Range(0, audioClip.Length);
            
            PlayAudio(audioClip[randomIndex], pitch, volume);
        }

        protected void PlayAudio(AudioClip audioClip, float pitch = 1.0f, float volume = 1.0f)
        {
            AudioSource instance = Instantiate(audioSourcePrefab, transform.position, Quaternion.identity);
            instance.pitch = pitch;
            instance.volume = volume;
            instance.clip = audioClip;
            instance.Play();
            
            Destroy(instance.gameObject, audioClip.length);
        }
    }
}