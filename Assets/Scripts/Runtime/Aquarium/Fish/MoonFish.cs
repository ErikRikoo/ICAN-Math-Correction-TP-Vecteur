using Runtime.Utilities;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

namespace Runtime.Aquarium.Fish
{
    public class MoonFish : AFish
    {
        [Min(0)]
        [SerializeField] private float m_DetectionRadius;

        [SerializeField] private bool m_ShouldResetMovementOnLost;
        
        
        [ReadOnly]
        [SerializeField]
        private PlanetFish m_Followed;

        [SerializeField]
        private float m_FollowingFactor = 1f;

        private void Start()
        {
            var collider = gameObject.GetComponent<SphereCollider>();
            collider.isTrigger = true;
            collider.radius = m_DetectionRadius;
        }

        private void Update()
        {
            if (m_Followed == null)
            {
                return;
            }

            var accelerationFactor = m_Followed.transform.position.XZ() - transform.position.XZ();
            Movement.Acceleration = transform.forward.XZ() + accelerationFactor.normalized * m_FollowingFactor;
        }

        private void OnTriggerEnter(Collider other)
        {
            GetFollowedIfPossible(other.gameObject);
        }

        public void GetFollowedIfPossible(GameObject other)
        {
            if (m_Followed == null && other.TryGetComponent(out PlanetFish _fish))
            {
                m_Followed = _fish;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (m_Followed?.gameObject == other.gameObject)
            {
                m_Followed = null;
                if (m_ShouldResetMovementOnLost)
                {
                    Movement.Reset();
                }
            }
        }
    }
}