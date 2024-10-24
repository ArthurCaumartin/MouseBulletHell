using UnityEngine;

public class ColorLoop : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField, Range(0, 1)] private float _colorS = 1;
    [SerializeField, Range(0, 1)] private float _colorV = 1;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _renderer.color = Color.HSVToRGB(Mathf.InverseLerp(-1, 1, Mathf.Sin(Time.time * _speed)), _colorS, _colorV);
    }
}
