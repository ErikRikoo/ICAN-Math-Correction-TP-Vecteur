using UnityEngine;

namespace Runtime.Aquarium
{
    [CreateAssetMenu(fileName = "AquariumBound", menuName = "Aquarium/Bound", order = 0)]
    public class AquariumBound : ScriptableObject
    {
        [SerializeField] private Vector2 m_Min;
        [SerializeField] private Vector2 m_Max;

        public Vector2 Min => m_Min;
        
        public Vector2 Max => m_Max;

        public Vector2 RandomPosition => new Vector2(
            Random.Range(m_Min.x, m_Max.x),
            Random.Range(m_Min.y, m_Max.y)
        );

        public bool IsOutOfBound(Vector2 _value, out Vector2 _collisionNormal)
        {
            _collisionNormal = Vector2.zero;

            if (_value.x < m_Min.x)
            {
                _collisionNormal = Vector2.right;
            } else if (_value.x > m_Max.x)
            {
                _collisionNormal = Vector2.left;
            }
            
            if (_value.y < m_Min.y)
            {
                _collisionNormal = Vector2.up;
            } else if (_value.y > m_Max.y)
            {
                _collisionNormal = Vector2.down;
            }
            
            return !IsIn(_value.x, m_Min.x, m_Max.x) || !IsIn(_value.y, m_Min.y, m_Max.y);
        }

        private bool IsIn(float _value, float _min, float _max)
        {
            return _value >= _min && _value < _max;
        }
        
        public Vector2 ClampPosition(Vector2 _value)
        {
            return new Vector2(
                Mathf.Clamp(_value.x, m_Min.x, m_Max.x),
                Mathf.Clamp(_value.y, m_Min.y, m_Max.y)
            );
        }
    }
}