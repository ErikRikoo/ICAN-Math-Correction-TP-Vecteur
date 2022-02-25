using System.Collections.Generic;
using Runtime.Utilities;
using UnityEngine;

namespace Runtime.Aquarium.Fish.Boids
{
    public class BoidFish : AFish
    {
        [SerializeField] private BoidsParameters m_Parameters;

        [Min(0)]
        [SerializeField] private float m_DetectionRadius;

        [SerializeField] private float m_SeparationThreshold;
        

        [SerializeField] private bool m_ShouldResetMovementOnLost;
        
        
        [SerializeField]
        private List<BoidFish> m_Followed = new List<BoidFish>();

        [SerializeField]
        private float m_ForceFactor = 1f;

        private float m_SquaredSeparetionThreshold;

        private void Start()
        {
            var collider = gameObject.GetComponent<SphereCollider>();
            collider.isTrigger = true;
            collider.radius = m_DetectionRadius;
            m_SquaredSeparetionThreshold = m_SeparationThreshold * m_SeparationThreshold;
        }

        private void Update()
        {
            if (m_Followed.Count == 0)
            {
                return;
            }

            Vector2 cohesionForce = ComputeCohesionForce();
            Vector2 separationForce = ComputeSeparationForce();
            Vector2 alignementForce = ComputeAlignmentForce();
            var accelerationFactor =
                cohesionForce * m_Parameters.CoherenceRatio +
                separationForce * m_Parameters.SeparationRatio +
                alignementForce * m_Parameters.AlignmentRatio;
            Movement.Acceleration = accelerationFactor.normalized * m_ForceFactor;
        }
        
        private Vector2 ComputeCohesionForce()
        {
            Vector2 ret = Vector2.zero;
            foreach (var fish in m_Followed)
            {
                ret += (fish.Position - Position).normalized;
            }

            ret /= m_Followed.Count;
            return ret;
        }
        
        private Vector2 ComputeSeparationForce()
        {
            Vector2 ret = Vector2.zero;
            foreach (var fish in m_Followed)
            {
                var offset = fish.Position - Position;
                if (offset.SqrMagnitude() < m_SquaredSeparetionThreshold)
                {
                    ret += (-offset).normalized;
                }
            }

            ret /= m_Followed.Count;
            return ret;
        }
        
        private Vector2 ComputeAlignmentForce()
        {
            Vector2 ret = Vector2.zero;
            foreach (var fish in m_Followed)
            {
                ret += fish.Movement.Speed;
            }

            ret /= m_Followed.Count;
            return ret;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            GetFollowedIfPossible(other.gameObject);
        }

        public void GetFollowedIfPossible(GameObject other)
        {
            if (other.TryGetComponent(out BoidFish _fish))
            {
                m_Followed.Add(_fish);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out BoidFish fish))
            {
                m_Followed.Remove(fish);
            }
        }
    }
}