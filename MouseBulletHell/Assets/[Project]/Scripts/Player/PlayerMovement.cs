using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector2 _worldMousePos;
    private Vector2 _velocity;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position
                                            , _camera.ScreenToWorldPoint(_worldMousePos)
                                            , ref _velocity
                                            , 1 / _speed);
    }

    private void OnMousePosition(InputValue value)
    {
        _worldMousePos = value.Get<Vector2>();
    }
}
