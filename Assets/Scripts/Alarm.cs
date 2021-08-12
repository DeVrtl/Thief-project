using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;
    [SerializeField] private float _maxVolume;
    [SerializeField] private float _speed;

    private IEnumerator _coroutineVolumeIncrease;
    private IEnumerator _coroutineVolumeReduction;

    private void Start()
    {
        _coroutineVolumeIncrease = SoundVolume(_sound, _maxVolume, _speed);
        _coroutineVolumeReduction = SoundVolume(_sound, -_maxVolume, _speed);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            _sound.Play();
            StopCoroutine(_coroutineVolumeReduction);
            StartCoroutine(_coroutineVolumeIncrease);
        }
    }
    
    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            StopCoroutine(_coroutineVolumeIncrease);
            StartCoroutine(_coroutineVolumeReduction);
        }
    }

    private IEnumerator SoundVolume(AudioSource sound, float maxVolume, float speed)
    {
        while (true)
        {
            _sound.volume = Mathf.MoveTowards(sound.volume, maxVolume, speed * Time.deltaTime);
            yield return null;
        }
    }
}
