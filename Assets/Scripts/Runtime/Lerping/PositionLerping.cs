using UnityEngine;

namespace Runtime.Lerping
{
    public class PositionLerping : ALerping
    {
        [SerializeField] private Vector3 m_StartPosition;
        [SerializeField] private Vector3 m_EndPosition;
        
        
        protected override void UpdateState(float _time)
        {
            transform.localPosition = Vector3.LerpUnclamped(m_StartPosition, m_EndPosition, _time);
        }
    }
}