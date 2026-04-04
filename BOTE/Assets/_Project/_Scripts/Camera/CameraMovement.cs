using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    #region Variables

    private Vector3 _origin;
    private Vector3 _difference;

    private Camera _mainCamera;

    private bool _isDragging;

    private Bounds _cameraBounds;
    private Vector3 _targetPosition;
    [SerializeField] private GameInput gameInput;

    #endregion

    private void Awake() 
    {
        _mainCamera = Camera.main; 
    }

    private void Start()
    {
        gameInput.OnDragAction += GameInput_OnDragAction;
        gameInput.EndDragAction += GameInput_EndDragAction;
        Calculate();
    }
    public void Calculate()
    {
        if (Globals.bound == null) return;
            var height = _mainCamera.orthographicSize;
        var width = height * _mainCamera.aspect;

        var minX = Globals.WorldBounds.min.x + width;
        var maxX = Globals.WorldBounds.max.x - width;

        var minY = Globals.WorldBounds.min.y + height;
        var maxY = Globals.WorldBounds.max.y - height;

        _cameraBounds = new Bounds();
        _cameraBounds.SetMinMax(
            new Vector3(minX, minY, 0.0f),
            new Vector3(maxX, maxY, 0.0f)
            );
    }
    public float CalcutaleMaxOrtho()
    {
        if (Globals.bound == null) return float.MaxValue;
        var maxY = Globals.WorldBounds.size.y/2;
        return maxY;
    }
    public void FixCamera()
    {
        _difference = transform.position - transform.position;
        _targetPosition = transform.position - _difference;
        _targetPosition = GetCameraBounds();
        transform.position = _targetPosition;
    }
    public void GameInput_OnDragAction(object sender, System.EventArgs e)
    {
        _origin = GetMousePosition;
        _isDragging = true;
    }
    public void GameInput_EndDragAction(object sender, System.EventArgs e)
    {
        _isDragging = false;
    }
    private void LateUpdate()
    {
        if (!_isDragging) return;
        _difference = GetMousePosition - transform.position;
        _targetPosition = _origin-_difference;
        _targetPosition = GetCameraBounds();
        transform.position = _targetPosition;
    }

    private Vector3 GetCameraBounds()
    {
        if (Globals.bound == null) return _targetPosition;
        return new Vector3(
            Mathf.Clamp(_targetPosition.x, _cameraBounds.min.x, _cameraBounds.max.x),
            Mathf.Clamp(_targetPosition.y, _cameraBounds.min.y, _cameraBounds.max.y),
            transform.position.z
        );
    }

    private Vector3 GetMousePosition => _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
}