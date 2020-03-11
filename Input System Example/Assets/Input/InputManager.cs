using System;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera _camera = default(Camera);
    
    public Action<int, Vector2> OnInputDown { get => _input.OnDown; set => _input.OnDown = value; }
    public Action<int, Vector2> OnInputUp { get => _input.OnUp; set => _input.OnUp = value; }
    public Action<int,Vector2> OnHoldInput { get => _input.OnHold; set => _input.OnHold = value; }
    public Action<int,Vector2> OnDragInput { get => _input.OnDrag; set => _input.OnDrag = value; }
    public Action<int,Vector2> OnClick { get => _input.OnClick; set => _input.OnClick = value; }
    public Action<int,SwipeDirection> OnSwipe { get => _input.OnSwipe; set => _input.OnSwipe = value; }

    public Dictionary<int, ImplementedGlobalInputEvents> registeredGlobalObjs => _registeredGlobalObjs;
    public Dictionary<int, ImplementedLocalInputEvents> registeredLocalObjs => _registeredLocalObjs;

    private BaseInput _input;
    private InputCameraRaycaster _inputCameraRaycaster;
    private static Dictionary<int, ImplementedGlobalInputEvents> _registeredGlobalObjs;
    private static Dictionary<int, ImplementedLocalInputEvents> _registeredLocalObjs;
    
    public void Awake()
    {
        _input = InputFactory.Create();
        _inputCameraRaycaster = new InputCameraRaycaster(_camera, this);
    }

    private void Update()
    {
        _input?.Update(Time.deltaTime);
    }
  
    public static void RegisterForGlobalInput(Component obj)
    {
        if (_registeredGlobalObjs == null)
        {
            _registeredGlobalObjs = new Dictionary<int, ImplementedGlobalInputEvents>();
        }
        if (_registeredGlobalObjs.ContainsKey(obj.transform.GetInstanceID()))
        {
            Debug.LogWarning("Trying to register object twice to receive input events!");
            return;
        }

        if (!(obj is IGlobalClick) && !(obj is IGlobalClickDown) && !(obj is IGlobalClickUp) &&
            !(obj is IGlobalClickHold) && !(obj is IGlobalClickDrag) && !(obj is IGlobalSwipe))
        {
            return;
        }
        
        _registeredGlobalObjs.Add(obj.transform.GetInstanceID(), new ImplementedGlobalInputEvents(obj as IGlobalClick, 
            obj as IGlobalClickDown, obj as IGlobalClickUp, obj as IGlobalClickHold, obj as IGlobalClickDrag, 
            obj as IGlobalSwipe));
    }
    
    public static void RegisterForLocalInput(Component obj)
    {
        if (_registeredLocalObjs == null)
        {
            _registeredLocalObjs = new Dictionary<int, ImplementedLocalInputEvents>();
        }
        
        if (_registeredLocalObjs.ContainsKey(obj.transform.GetInstanceID()))
        {
            Debug.LogWarning("Trying to register object twice to receive input events!");
            return;
        }
        
        if (!(obj is ILocalClick) && !(obj is ILocalClickDown) && !(obj is ILocalClickUp) &&
            !(obj is ILocalClickHold) && !(obj is ILocalClickDrag) && !(obj is ILocalSwipe))
        {
            return;
        }
        
        _registeredLocalObjs.Add(obj.transform.GetInstanceID(), new ImplementedLocalInputEvents(obj as ILocalClick, 
            obj as ILocalClickDown, obj as ILocalClickUp, obj as ILocalClickHold, obj as ILocalClickDrag, 
            obj as ILocalSwipe));
    }

    public static void UnregisterForGlobalInput(Component obj)
    {
        if (!_registeredGlobalObjs.ContainsKey(obj.transform.GetInstanceID()))
        {
            return;
        }
        
        _registeredGlobalObjs.Remove(obj.transform.GetInstanceID());
    }
    
    public static void UnregisterForLocalInput(Component obj)
    {
        if (!_registeredLocalObjs.ContainsKey(obj.transform.GetInstanceID()))
        {
            return;
        }
        
        _registeredLocalObjs.Remove(obj.transform.GetInstanceID());
    }
}
