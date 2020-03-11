using UnityEngine;

public interface IGlobalClickHold
{
    void OnGlobalClickHold(int inputIndex, Vector2 screenPosition, Transform hitTransform, Vector3 hitPosition);
}
