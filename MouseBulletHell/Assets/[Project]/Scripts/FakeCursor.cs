using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCursor : MonoBehaviour
{
    public Vector2 _worldMouseDelta;
    private Vector2 _lastFrameWorldPos;

    void Start()
    {
        _lastFrameWorldPos = Camera.main.WorldToScreenPoint(Input.mousePosition);
    }

    public void Update()
    {
        Vector2 worldMousePos = Camera.main.WorldToScreenPoint(Input.mousePosition);
        _worldMouseDelta = worldMousePos - _lastFrameWorldPos;
        _lastFrameWorldPos = worldMousePos;
    }


    public Vector2 GetMouseDelta()
    {
        return _worldMouseDelta;
    }
}
