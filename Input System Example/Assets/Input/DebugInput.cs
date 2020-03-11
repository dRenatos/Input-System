using UnityEngine;

public class DebugInput : MonoBehaviour, ILocalClickUp, ILocalClickDown, ILocalClick, ILocalClickHold, ILocalSwipe
{
    void Awake()
    {
        InputManager.RegisterForLocalInput(this);
    }
    
    public void OnLocalClickUp(int index, Vector3 position)
    {
        Debug.Log("OnLocalClickUp");
    }

    public void OnLocalClickDown(int index, Vector3 position)
    {
        Debug.Log("OnLocalClickDown");
    }

    public void OnLocalClick(int index, Vector3 position)
    {
        Debug.Log("OnLocalClick");
    }

    public void OnLocalClickHold(int index, Vector3 position)
    {
        Debug.Log("OnLocalClickHold");
    }

    public void OnLocalSwipe(int inputIndex, SwipeDirection swipeDirection)
    {
        Debug.Log(swipeDirection);
    }
}
