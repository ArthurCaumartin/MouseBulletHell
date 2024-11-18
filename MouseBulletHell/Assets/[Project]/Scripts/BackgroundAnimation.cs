using UnityEngine;

public class BackgroundAnimation : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _colorSpeed;
    [SerializeField] private float _scaleSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField, Range(0, 1)] private float _colorS = 1;
    [SerializeField, Range(0, 1)] private float _colorV = 1;
    private SpriteRenderer _renderer;
    Vector3 _startScale;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _startScale = transform.localScale;
    }

    private void Update()
    {
        float colorTime = Mathf.InverseLerp(-1, 1, Mathf.Sin(Time.time * _speed * _colorSpeed));
        _renderer.color = Color.HSVToRGB(colorTime, _colorS, _colorV);

        float scaleTime = Mathf.InverseLerp(-1, 1, Mathf.Sin(Time.time * _speed * _scaleSpeed));
        Vector3 newScale = Vector3.Lerp(_startScale * 0.95f, _startScale * 1.2f, scaleTime);
        transform.localScale = newScale;

        float rotateTime = Mathf.Sin(Time.time * _speed * _rotateSpeed);
        transform.up = new Vector3(rotateTime, 1, 0);
    }
}
