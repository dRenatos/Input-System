using UnityEngine;

public class InputData
{
    public float inputDownTime => _inputDownTime;
    public Vector2 inputScreenPosition;
    
    private readonly float _inputDownTime;
    private readonly Vector2 _inputDownPosition;

    private Vector2 _lastInputPosition;

    public InputData(Vector2 inputDownPosition)
    {
        _inputDownPosition = inputDownPosition;
        _inputDownTime = Time.time;
    }
    
    public SwipeDirection CalculateSwipe(float minSwipeLength, float minSwipeTime, float maxSwipeTime)
    {
        var swipeVector = (inputScreenPosition / Screen.dpi) - (_inputDownPosition / Screen.dpi);

        if (Time.time - _inputDownTime < minSwipeTime || Time.time - _inputDownTime > maxSwipeTime ||
            swipeVector.magnitude < minSwipeLength)
        {
            return SwipeDirection.None;
        }

        if(Mathf.Abs(swipeVector.x) > Mathf.Abs(swipeVector.y))
        {
            return swipeVector.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
        }

        return swipeVector.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
    }

    public Vector2 GetDeltaPosition()
    {
        return _lastInputPosition - inputScreenPosition;
    }

    public void Update()
    {
        _lastInputPosition = inputScreenPosition;
    }
}
