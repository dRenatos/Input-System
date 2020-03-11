using UnityEngine;

public interface IGlobalClick
{
    void OnGlobalClick(int inputIndex, Vector2 screenPosition, Transform hitTransform, Vector3 hitPosition);
}
