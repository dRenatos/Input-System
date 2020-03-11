using UnityEngine;

public interface IGlobalClickDrag
{
   void OnGlobalClickDrag(int inputIndex, Vector2 screenPosition, Transform hitObject, Vector3 hitPosition);
}
