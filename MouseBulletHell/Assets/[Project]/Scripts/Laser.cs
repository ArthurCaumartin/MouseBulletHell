using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField, Range(.01f, 1)] float _speedFactor = .5f;
    [SerializeField] private float _duration;
    [SerializeField, Range(0, 1)] private float _activationThresold;
    [SerializeField] private AnimationCurve _activationCurve;
    [SerializeField] private Gradient _offColorGradient;
    [SerializeField] private Gradient _onColorGradient;

    private Transform _target;
    private float _speed = 5;
    private LineRenderer _lineRenderer;
    private float _lifeTime = 0;
    private float _activationValue;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        transform.right = -transform.position.normalized;
        SetColor(0);
    }

    public Laser Initialize(Transform targetTransform, float speed)
    {
        _target = targetTransform;
        _speed = speed;
        return this;
    }


    private void Update()
    {
        RotateToTarget();

        _lifeTime += Time.deltaTime;
        _activationValue = _activationCurve.Evaluate(Mathf.InverseLerp(0, _duration, _lifeTime));
        SetColor(_activationValue);
        if (_activationValue > _activationThresold)
        {
            _lineRenderer.widthCurve.keys[0].value = Mathf.Lerp(.5f, 1, Mathf.InverseLerp(-1, 1, Mathf.Sin(_lifeTime * 10)));
            //! do damage and destroy
        }


        if(_lifeTime > _duration) Destroy(gameObject);
    }

    private void RotateToTarget()
    {
        transform.right = Vector2.Lerp(transform.right
                                    , (_target.position - transform.position).normalized
                                    , Time.deltaTime * _speed * _speedFactor);

        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, transform.position + (transform.right * 100));
    }

    private void SetColor(float value)
    {
        print("Set color/ value = " + value);
        GradientColorKey[] colorKeys = _lineRenderer.colorGradient.colorKeys;
        GradientAlphaKey[] alphaKeys = _lineRenderer.colorGradient.alphaKeys;
        for (int i = 0; i < colorKeys.Length; i++)
        {
            print("Set key : " + i);
            colorKeys[i].color = Color.Lerp(_offColorGradient.colorKeys[i].color
                                                    , _onColorGradient.colorKeys[i].color
                                                    , value);
            alphaKeys[i].alpha = Mathf.Lerp(_offColorGradient.alphaKeys[i].alpha
                                                    , _onColorGradient.alphaKeys[i].alpha
                                                    , value);
        }
        Gradient newGrad = new Gradient();
        newGrad.SetKeys(colorKeys, alphaKeys);
        _lineRenderer.colorGradient = newGrad;
        // _line.colorGradient.colorKeys = colorKeys;
        // _line.colorGradient.alphaKeys = alphaKeys;
    }
}