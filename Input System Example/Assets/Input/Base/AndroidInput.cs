﻿using UnityEngine;

public class AndroidInput : BaseInput
{
	public AndroidInput(float minSwipeLength, float minSwipeTime, float maxSwipeTime) : base(minSwipeLength, minSwipeTime, maxSwipeTime)
	{
	}

	public override void Update(float deltaTime)
	{
		if (Input.touches.Length == 0)
		{
			return;
		}

		base.Update(deltaTime);

		foreach (var screenTouch in Input.touches)
		{
			if (screenTouch.phase == TouchPhase.Began && !inputDatas.ContainsKey(screenTouch.fingerId))
			{
				var androidInputData = new InputData(screenTouch.position);
				inputDatas.Add(screenTouch.fingerId, androidInputData);
				onInputDown?.Invoke(screenTouch.fingerId, screenTouch.position);
			}
			else if (screenTouch.phase == TouchPhase.Ended || screenTouch.phase == TouchPhase.Canceled)
			{
				onInputUp?.Invoke(screenTouch.fingerId);
			}
		}
	}

	protected override void UpdateInputPosition()
	{
		foreach (var screenTouch in Input.touches)
		{
			if (screenTouch.phase == TouchPhase.Began)
			{
				continue;
			}
			inputDatas[screenTouch.fingerId].inputScreenPosition = screenTouch.position;
		}
	}
}

	