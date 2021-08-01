using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;
    [SerializeField] private float _maxVolume;
    [SerializeField] private float _speed;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            _sound.Play();
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            SoundVolume(_sound, _maxVolume, _speed);
        }
    }
    
    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            StartCoroutine(AttenuationSoundVolume(_sound, _maxVolume, _speed));
        }
    }

    private IEnumerator AttenuationSoundVolume(AudioSource sound, float maxVolume, float speed)
    {
        while (true)
        {
            _sound.volume = Mathf.MoveTowards(sound.volume, -maxVolume, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void SoundVolume(AudioSource sound, float maxVolume, float speed)
    {
        sound.volume = Mathf.MoveTowards(sound.volume, maxVolume, speed * Time.deltaTime);
    }
}
