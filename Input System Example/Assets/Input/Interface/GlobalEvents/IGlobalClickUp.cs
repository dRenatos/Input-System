using UnityEngine;

public interface IGlobalClickUp
{
   void OnGlobalClickUp(int inputIndex, Vector2 screenPosition, Transform hitTransform, Vector3 hitPosition);
}
