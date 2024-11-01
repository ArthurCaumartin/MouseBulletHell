using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _playerTransform;
    private float _speed = 10f;

    public Bullet Initialize(Transform targetTransform, float speed)
    {
        _playerTransform = targetTransform;
        _speed = speed;
        transform.right = (_playerTransform.position - transform.position).normalized;
        return this;
    }

    private void Update()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GameManager.instance.OnPlayerHit();
            Destroy(gameObject);
        }
    }
}