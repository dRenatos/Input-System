using UnityEngine;

[CreateAssetMenu(fileName = "Input Setup", menuName = "Input Setup", order = 1)]
public class InputSetup : ScriptableObject
{
    public float minSwipeLength => _minSwipeLength;
    public float minSwipeTime => _minSwipeTime;
    public float maxSwipeTime => _maxSwipeTime;
    
    [SerializeField] private float _minSwipeLength = 0.05f; 
    [SerializeField] private float _minSwipeTime = 0.1f;
    [SerializeField] private float _maxSwipeTime = 0.2f;
}
