using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [SerializeField] private float _duration = .5f;
    [SerializeField] private float _intensity = 2f;
    [SerializeField] private int _vibrato = 5;
    [SerializeField] private float _randomness = 90f;

    public void Shake()
    {
        transform.DOShakePosition(_duration, _intensity, _vibrato, _randomness)
        .OnComplete(() => transform.DOMove(Vector3.zero, _duration / 2));
    }
}
