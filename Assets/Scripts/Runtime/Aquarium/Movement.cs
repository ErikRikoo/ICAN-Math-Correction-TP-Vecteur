using System;
using System.Transactions;
using Runtime.Utilities;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Runtime.Aquarium
{
    public class Movement : MonoBehaviour
    {
        [Min(0f)]
        [SerializeField] private float m_MaxSpeed;
        
        [SerializeField] private AquariumBound m_Bound;
        
        [SerializeField] public Vector2 m_Speed;
        [SerializeField] public Vector2 Acceleration;

        public Vector2 Speed
        {
            get => m_Speed;
            set
            {
                m_Speed = value;
                transform.LookAt(transform.position + m_Speed.X0Y());
            }
        }
        
        public Action<Vector2> OnCollisionWithBounds;

        private void Update()
        {
            Speed += Acceleration * Time.deltaTime;
            Speed = Mathf.Clamp(Speed.magnitude, 0, m_MaxSpeed) * Speed.normalized;
            transform.localPosition += (Speed * Time.deltaTime).X0Y();

            CheckAquariumCollision();
        }

        private void CheckAquariumCollision()
        {
            if (m_Bound.IsOutOfBound(transform.localPosition.XZ(), out Vector2 collisionNormal))
            {
                OnCollisionWithBounds?.Invoke(collisionNormal);
                transform.localPosition = m_Bound.ClampPosition(transform.localPosition.XZ()).X0Y();
            }
        }

        public void Reset()
        {
            Speed = Vector2.zero;
            Acceleration = Vector2.zero;
        }
    }
}