using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    private Vector2 _velocity;

    void Update()
    {
        _velocity = transform.right * _speed;
        transform.position += (Vector3)(_velocity * Time.deltaTime);
    }
}

