﻿using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInput
{
    public Action<int, Vector2> OnDown, OnHold, OnUp, OnClick;
    public Action<int, SwipeDirection> OnSwipe;
    public Action<int, Vector2> OnDrag;

    protected Action<int, Vector2> onInputDown;
    protected Action<int> onInputUp;
    protected Dictionary<int, InputData> inputDatas = null;
    
    private float _minSwipeLength;
    private float _minSwipeTime;
    private float _maxSwipeTime;

    protected BaseInput(float minSwipeLength, float minSwipeTime, float maxSwipeTime)
    {
        _minSwipeLength = minSwipeLength;
        _minSwipeTime = minSwipeTime;
        _maxSwipeTime = maxSwipeTime;
        
        inputDatas = new Dictionary<int, InputData>();
        
        onInputDown += OnInputDown;
        onInputUp += OnInputUp;
    }

    public virtual void Update(float deltaTime)
    {
        if (inputDatas.Count == 0)
        {
            return;
        }

        UpdateInputPosition();
        
        foreach (var inputData in inputDatas)
        {
//			 start drag only after the max swipe time threshold
			if (Time.time - inputData.Value.inputDownTime > _maxSwipeTime)
            {
                OnDrag?.Invoke(inputData.Key, inputData.Value.GetDeltaPosition());
            }

            OnHold?.Invoke(inputData.Key, inputData.Value.inputScreenPosition);
            inputData.Value.Update();
        }
    }

    protected virtual void UpdateInputPosition()
    {
    }
    
    private void OnInputDown(int index, Vector2 screenPosition)
    {
        OnDown?.Invoke(index, screenPosition);
    }

    private void OnInputUp(int index)
    {
        if (!inputDatas.ContainsKey(index))
        {
            return;
        }

        var inputData = inputDatas[index];
        
        if (Time.time - inputData.inputDownTime < _minSwipeTime)
        {
            OnClick?.Invoke(index, inputData.inputScreenPosition);
        }
        else
        {
            var swipe = inputData.CalculateSwipe(_minSwipeLength, _minSwipeTime, _maxSwipeTime);

            if (swipe != SwipeDirection.None)
            {
                OnSwipe?.Invoke(index, swipe);
            }
            else if (Time.time - inputData.inputDownTime < _maxSwipeTime)
            {
                OnClick?.Invoke(index, inputData.inputScreenPosition);
            }
        }

        inputDatas.Remove(index);
        OnUp?.Invoke(index, inputData.inputScreenPosition);
    }
}
    
public enum SwipeDirection
{
    Up, Down, Left, Right, None
}