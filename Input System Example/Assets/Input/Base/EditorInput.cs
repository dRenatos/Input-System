﻿using UnityEngine;

public class EditorInput: BaseInput
{
    public EditorInput(float minSwipeLength, float minSwipeTime, float maxSwipeTime) : base(minSwipeLength, minSwipeTime, maxSwipeTime)
    {
    }
    
    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        
        if (Input.GetMouseButtonDown(0))
        {
            var editorInputData = new InputData(Input.mousePosition);
            inputDatas.Add(0, editorInputData);
            
            onInputDown?.Invoke(0, Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            onInputUp?.Invoke(0);
        }
    }

    protected override void UpdateInputPosition()
    {
        inputDatas[0].inputScreenPosition = Input.mousePosition;
    }
}
