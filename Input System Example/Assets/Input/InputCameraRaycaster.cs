using System.Collections.Generic;
using UnityEngine;

public class InputCameraRaycaster
{
    private readonly Camera _camera;
    private readonly InputManager _inputManager;
    private readonly Dictionary<int, int> _inputObjs;
    
    public InputCameraRaycaster(Camera camera, InputManager inputManager)
    {
        _camera = camera;
        _inputManager = inputManager;
        _inputObjs = new Dictionary<int, int>();
        
        _inputManager.OnInputDown += (inputIndex, screenPosition) =>
        {
            OnReceiveInput(InputType.ClickDown, inputIndex, screenPosition);
        };
        
        _inputManager.OnInputUp += (inputIndex, screenPosition) =>
        {
            OnReceiveInput(InputType.ClickUp, inputIndex, screenPosition);
        };
        
        _inputManager.OnClick += (inputIndex, screenPosition) =>
        {
            OnReceiveInput(InputType.Click, inputIndex, screenPosition);
        };
        
        _inputManager.OnHoldInput += (inputIndex, screenPosition) =>
        {
            OnReceiveInput(InputType.ClickHold, inputIndex, screenPosition);
        };
        
        _inputManager.OnDragInput += (inputIndex, screenPosition) =>
        {
            OnReceiveInput(InputType.ClickDrag, inputIndex, screenPosition);
        };
        
        _inputManager.OnSwipe += OnReceiveInput;
    }

    private void OnReceiveInput(InputType inputType, int inputIndex, Vector2 screenPosition)
    {
        RaycastHit raycastHit;
        var hasHit = Raycast(screenPosition, out raycastHit);

        GlobalInputEvents(inputType, inputIndex, screenPosition, hasHit, raycastHit);
        LocalInputEvents(inputType, inputIndex, hasHit, raycastHit);
    }
    
    private void OnReceiveInput(int inputIndex, SwipeDirection swipeDirection)
    {
        OnGlobalSwipe(inputIndex, swipeDirection);
        OnLocalSwipe(inputIndex, swipeDirection);
    }

    private void LocalInputEvents(InputType inputType, int inputIndex, bool hasHit, RaycastHit raycastHit)
    {
        if (_inputManager.registeredLocalObjs == null)
            return;
        if(_inputObjs.ContainsKey(inputIndex) && !_inputManager.registeredLocalObjs.ContainsKey(_inputObjs[inputIndex]))
            return;
        
        Vector3 hitPosition;
        
        if (hasHit)
        {
            hitPosition = raycastHit.point;

            if (inputType == InputType.ClickDown)
            {
                if(!_inputManager.registeredLocalObjs.ContainsKey(raycastHit.transform.GetInstanceID()))
                    return;
                
                _inputObjs.Add(inputIndex, raycastHit.transform.GetInstanceID());
                OnLocalInputDown(inputIndex, hitPosition);
                return;
            }
        }
        else
        {
            hitPosition = Vector3.negativeInfinity;
        }
        
        if (!_inputObjs.ContainsKey(inputIndex))
        {
            return;
        }
        
        switch (inputType)
        {
            case InputType.Click:
                OnLocalClick(inputIndex, hitPosition);
                break;
            case InputType.ClickUp:
                OnLocalInputUp(inputIndex, hitPosition);
                _inputObjs.Remove(inputIndex);
                break;
            case InputType.ClickHold:
                OnLocalHoldInput(inputIndex, hitPosition);
                break;
            case InputType.ClickDrag:
                OnLocalDragInput(inputIndex, hitPosition);
                break;
        }
    }

    private void GlobalInputEvents(InputType inputType, int inputIndex, Vector2 screenPosition, bool hasHit,
        RaycastHit raycastHit)
    {
        Vector3 hitPosition;
        Transform hitObject = null;
        
        if (hasHit)
        {
            hitPosition = raycastHit.point;
            hitObject = raycastHit.transform;
        }
        else
        {
            hitPosition = Vector3.negativeInfinity;
        }

        switch (inputType)
        {
            case InputType.Click:
                OnGlobalClick(inputIndex, screenPosition, hitObject, hitPosition);
                break;
            case InputType.ClickDown:
                OnGlobalInputDown(inputIndex, screenPosition, hitObject, hitPosition);
                break;
            case InputType.ClickUp:
                OnGlobalInputUp(inputIndex, screenPosition, hitObject, hitPosition);
                break;
            case InputType.ClickHold:
                OnGlobalHoldInput(inputIndex, screenPosition, hitObject, hitPosition);
                break;
            case InputType.ClickDrag:
                OnGlobalDragInput(inputIndex, screenPosition, hitObject, hitPosition);
                break;
        }
    }
  
    #region OnClick

    private void OnGlobalClick(int inputIndex, Vector2 screenPosition, Transform hitObject, Vector3 hitPosition)
    {
        if (_inputManager.registeredGlobalObjs == null)
        {
            return;
        }
        foreach (var globalEventDispatcher in _inputManager.registeredGlobalObjs.Values)
        {
            globalEventDispatcher.click?.OnGlobalClick(inputIndex, screenPosition, hitObject, hitPosition);
        }
    }
    
    private void OnLocalClick(int inputIndex, Vector3 hitPosition)
    {
        _inputManager.registeredLocalObjs[_inputObjs[inputIndex]].click?
            .OnLocalClick(inputIndex, hitPosition);
    }
    
    #endregion
    
    #region OnInputUp
  
    private void OnGlobalInputUp(int inputIndex, Vector2 screenPosition, Transform hitObject, Vector3 hitPosition)
    {
        if (_inputManager.registeredGlobalObjs == null)
        {
            return;
        }
        foreach (var globalEventDispatcher in _inputManager.registeredGlobalObjs.Values)
        {
            globalEventDispatcher.clickUp?.OnGlobalClickUp(inputIndex, screenPosition, hitObject, hitPosition);
        }
    }
    
    private void OnLocalInputUp(int inputIndex, Vector3 hitPosition)
    {
        _inputManager.registeredLocalObjs[_inputObjs[inputIndex]].clickUp?
            .OnLocalClickUp(inputIndex, hitPosition);
    }
    
    #endregion
    
    #region OnHold

    private void OnGlobalHoldInput(int inputIndex, Vector2 screenPosition, Transform hitObject, Vector3 hitPosition)
    {
        if (_inputManager.registeredGlobalObjs == null)
        {
            return;
        }
        foreach (var globalEventDispatcher in _inputManager.registeredGlobalObjs.Values)
        {
            globalEventDispatcher.clickHold?.OnGlobalClickHold(inputIndex, screenPosition, hitObject, hitPosition);
        }
    }
    
    private void OnLocalHoldInput(int inputIndex, Vector3 hitPosition)
    {
        _inputManager.registeredLocalObjs[_inputObjs[inputIndex]].
            clickHold?.OnLocalClickHold(inputIndex, hitPosition);
    }
    
    #endregion
    
    #region OnDrag

    private void OnGlobalDragInput(int inputIndex, Vector2 screenPosition, Transform hitObject, Vector3 hitPosition)
    {
        if (_inputManager.registeredGlobalObjs == null)
        {
            return;
        }
        foreach (var globalEventDispatcher in _inputManager.registeredGlobalObjs.Values)
        {
            globalEventDispatcher.clickDrag?.OnGlobalClickDrag(inputIndex, screenPosition, hitObject, hitPosition);
        }
    }
    
    private void OnLocalDragInput(int inputIndex, Vector3 hitPosition)
    {
        _inputManager.registeredLocalObjs[_inputObjs[inputIndex]].
            clickDrag?.OnLocalClickDrag(inputIndex, hitPosition);
    }
    
    #endregion
    
    #region OnInputDown
    
    private void OnGlobalInputDown(int inputIndex, Vector2 screenPosition, Transform hitTransform, Vector3 hitPosition)
    {
        if (_inputManager.registeredGlobalObjs == null)
        {
            return;
        }
        foreach (var globalEventDispatcher in _inputManager.registeredGlobalObjs.Values)
        {
            globalEventDispatcher.clickDown?.OnGlobalClickDown(inputIndex, screenPosition, hitTransform, hitPosition);
        }
    }

    private void OnLocalInputDown(int inputIndex, Vector3 hitPosition)
    {
        _inputManager.registeredLocalObjs[_inputObjs[inputIndex]].clickDown?
            .OnLocalClickDown(inputIndex, hitPosition);
    }
    
    #endregion

    #region Swipe
    
    private void OnGlobalSwipe(int inputIndex, SwipeDirection swipeDirection)
    {
        if (_inputManager.registeredGlobalObjs == null)
        {
            return;
        }
        foreach (var globalEventDispatcher in _inputManager.registeredGlobalObjs.Values)
        {
            globalEventDispatcher.swipe?.OnGlobalSwipe(inputIndex, swipeDirection);
        }
    }
    
    private void OnLocalSwipe(int inputIndex, SwipeDirection swipeDirection)
    {
        if(!_inputObjs.ContainsKey(inputIndex))
            return;
        
        _inputManager.registeredLocalObjs[_inputObjs[inputIndex]].swipe?
            .OnLocalSwipe(inputIndex, swipeDirection);
    }
    
    #endregion
    
    private bool Raycast(Vector2 screenPoint, out RaycastHit raycastHit)
    {
        var rayOriginPoint = new Vector3(screenPoint.x, screenPoint.y, _camera.nearClipPlane);
        var cameraRay = _camera.ScreenPointToRay(rayOriginPoint);
        return Physics.Raycast(cameraRay, out raycastHit, Mathf.Infinity);
    }

    private enum InputType
    {
        Swipe,
        Click,
        ClickDown,
        ClickUp,
        ClickHold,
        ClickDrag,
    }
    
}

