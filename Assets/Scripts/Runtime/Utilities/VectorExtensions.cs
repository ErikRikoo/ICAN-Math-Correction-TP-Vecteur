using UnityEngine;

namespace Runtime.Utilities
{
    public static class VectorExtensions
    {
        public static Vector3 X0Y(this Vector2 _instance)
        {
            return new Vector3(_instance.x, 0, _instance.y);
        }

        public static Vector2 XZ(this Vector3 _instance)
        {
            return new Vector2(_instance.x, _instance.z);
        }
    }
}