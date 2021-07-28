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
            SoundVolume(_sound, _maxVolume, _speed);
            _sound.Play();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            SoundVolume(_sound, -_maxVolume, _speed);
            _sound.Stop();
        }
    }

    public void SoundVolume(AudioSource sound, float maxVolume, float speed)
    {
        sound.volume = Mathf.MoveTowards(sound.volume, maxVolume, speed * Time.fixedDeltaTime);
    }
}
