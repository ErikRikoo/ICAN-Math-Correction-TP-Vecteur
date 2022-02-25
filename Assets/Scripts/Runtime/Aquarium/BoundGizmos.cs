using System;
using Runtime.Utilities;
using UnityEditor;
using UnityEngine;

namespace Runtime.Aquarium
{
    public class BoundGizmos : MonoBehaviour
    {
        [SerializeField] private AquariumBound m_Bound;

        private void OnDrawGizmos()
        {
            if (m_Bound == null)
            {
                return;
            }
            
            Vector2 center = Vector2.Lerp(m_Bound.Min, m_Bound.Max, 0.5f);
            Vector2 size = m_Bound.Max - m_Bound.Min;
            Handles.DrawWireCube(center.X0Y(), size.X0Y());
        }
    }
}