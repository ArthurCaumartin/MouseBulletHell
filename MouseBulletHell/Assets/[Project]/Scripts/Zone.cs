using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField, Range(.01f, 1)] float _speedFactor = .5f;
    [SerializeField] private Color _startColor = Color.green;
    [SerializeField] private Color _endColor = Color.red;
    private float _speed = 5;
    private float _lifeTime = 0;
    private SpriteRenderer _sprite;

    private void Start()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        transform.right = -transform.position.normalized;
        SetColor(0);
    }

    public Zone Initialize(float speed)
    {
        _speed = speed;
        return this;
    }

    private void Update()
    {
        _lifeTime += Time.deltaTime * _speed * _speedFactor;
        SetColor(_lifeTime);
        SetScale(_lifeTime);
        if (_lifeTime > 1)
        {
            Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, _range);
            for (int i = 0; i < col.Length; i++)
            {
                if (col[i].tag == "Player")
                {
                    GameManager.instance.OnPlayerHit();
                    break;
                }
            }
            Destroy(gameObject);
        }
    }

    private void SetColor(float value)
    {
        _sprite.color = Color.Lerp(_startColor, _endColor, value);
    }

    private void SetScale(float value)
    {
        print("Value : " + value);
        _sprite.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * _range * 2, value);
    }

    // public void OnValidate()
    // {
    //     _sprite = GetComponentInChildren<SpriteRenderer>();
    //     _sprite.transform.localScale = Vector3.one * _range * 2;
    // }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, .4f);
        Gizmos.DrawSphere(transform.position, _range);
    }
}