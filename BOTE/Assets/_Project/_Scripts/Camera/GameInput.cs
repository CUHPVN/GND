using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput instance { get; private set; }
    public event EventHandler OnDragAction;
    public event EventHandler EndDragAction;
    private PlayerAction playerInputActions;



    public void Awake()
    {
        instance = this;
        playerInputActions = new PlayerAction();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Drag.canceled += Drag_canceled;
        playerInputActions.Player.Drag.performed += Drag_performed;
    }
    private void Drag_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (IsPointerOverUI()) return;
        if (OnDragAction != null)
        {
            OnDragAction(this, EventArgs.Empty);
        }
    }
    private void Drag_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (EndDragAction != null)
        {
            EndDragAction(this, EventArgs.Empty);
        }
    }


    public float GetScrollY()
    {
        Vector2 inputVector = playerInputActions.Player.Zoom.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector.y;
    }
    public static bool IsPointerOverUI()
    {
        if (EventSystem.current == null) return false;

#if UNITY_ANDROID || UNITY_IOS
    if (Input.touchCount > 0)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.GetTouch(0).position;

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
    return false;
#else
        return EventSystem.current.IsPointerOverGameObject();
#endif
    }
}
