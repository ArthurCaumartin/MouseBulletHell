using UnityEngine;
using UnityEngine.InputSystem;

public class BallControler : MonoBehaviour
{
    [SerializeField] private Transform _positifBall;
    [SerializeField] private Transform _negatifBall;
    [Space]
    [SerializeField] private float _dashCooldown = 1.5f;
    [SerializeField] private float _positifSpeed = 15;
    [SerializeField] private float _negatifSpeed = 15;
    private Vector2 _worldMousePos;
    private Vector2 _velocity;
    private Camera _camera;
    private float _dashCooldownTime;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        _dashCooldownTime += Time.deltaTime;

        _positifBall.position = Vector2.SmoothDamp(_positifBall.position
                                            , _camera.ScreenToWorldPoint(_worldMousePos)
                                            , ref _velocity
                                            , 1 / _positifSpeed);

        _negatifBall.position = Vector2.Lerp(_negatifBall.position, (-_positifBall.position).normalized * 15, Time.deltaTime * _negatifSpeed);

    }

    private void OnMousePosition(InputValue value)
    {
        _worldMousePos = value.Get<Vector2>();
    }

    private void OnRightClic(InputValue value)
    {
        // Collider2D[] colliderArray = Physics2D.OverlapCircleAll(_negatifBall.position, 10);
        // for (int i = 0; i < colliderArray.Length; i++)
        // {
        //     Bullet b = colliderArray[i].GetComponent<Bullet>();
        //     if (b) Destroy(b.gameObject);
        // }

        if (_dashCooldownTime >= _dashCooldown)
        {
            _dashCooldownTime = 0;
            Mouse.current.WarpCursorPosition(_camera.WorldToScreenPoint(_negatifBall.position));
            GameManager.instance.Score += 10;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, .2f);
        Gizmos.DrawSphere(_negatifBall.position, 10);
    }

}
