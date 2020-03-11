using UnityEngine;

public interface IGlobalClickDown
{
   void OnGlobalClickDown(int inputIndex, Vector2 screenPosition, Transform hitTransform, Vector3 hitPosition);
}
