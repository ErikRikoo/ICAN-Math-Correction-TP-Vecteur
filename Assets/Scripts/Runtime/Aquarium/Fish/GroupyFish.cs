using System;
using Runtime.Utilities;
using Unity.Collections;
using UnityEditor;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR;

namespace Runtime.Aquarium.Fish
{
    public class GroupyFish : AFish
    {
        [Min(0)]
        [SerializeField] private float m_DetectionRadius;
        [Range(0, 180)]
        [SerializeField] private float m_HalfDetectionAngle;

        [SerializeField] private bool m_ShouldResetMovementOnLost;
        
        
        [ReadOnly]
        [SerializeField]
        private LeaderFish m_Followed;

        private float m_FollowingFactor = 1f;

        private void Start()
        {
            var collider = gameObject.GetComponent<SphereCollider>();
            collider.isTrigger = true;
            collider.radius = m_DetectionRadius;
        }

        private void Update()
        {
            SceneView.RepaintAll();
            if (m_Followed == null)
            {
                return;
            }

            if (!IsInSight(m_Followed.transform.position))
            {
                m_Followed = null;
                return;
            }

            var accelerationFactor = m_Followed.transform.position.XZ() - (transform.position.XZ() + Movement.Speed);
            Movement.Acceleration = accelerationFactor.normalized * m_FollowingFactor;
        }

        private void OnTriggerStay(Collider other)
        {
            GetFollowedIfPossible(other.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            GetFollowedIfPossible(other.gameObject);
        }

        public void GetFollowedIfPossible(GameObject other)
        {
            if (m_Followed == null && other.TryGetComponent(out LeaderFish _fish))
            {
                if (IsInSight(_fish.transform.position))
                {
                    m_Followed = _fish;
                }   
            }
        }

        private bool IsInSight(Vector3 _position)
        {
            Vector3 direction = (_position - transform.position).normalized;
            float dot = Vector3.Dot(direction, transform.forward);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
            return angle >= -m_HalfDetectionAngle && angle < m_HalfDetectionAngle;
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

        private void OnDrawGizmos()
        {
            Handles.color = Color.green;
            Vector3 directionDetection = transform.forward * m_DetectionRadius;
            Vector3 leftAngleLimitDirection = Quaternion.Euler(0, -m_HalfDetectionAngle, 0) * directionDetection;
            Vector3 rightAngleLimitDirection = Quaternion.Euler(0, m_HalfDetectionAngle, 0) * directionDetection;

            DrawAngle();
            

            Handles.DrawLine(transform.position, transform.position + directionDetection * 1.2f);
            Handles.DrawLine(transform.position, transform.position + leftAngleLimitDirection);
            Handles.DrawLine(transform.position, transform.position + rightAngleLimitDirection);
            
            if (m_Followed != null)
            {
                Handles.color = Color.red;
                Handles.DrawWireDisc(m_Followed.transform.position, transform.up, 0.5f);
            }
        }

        private void DrawAngle()
        {
            int count = (int) (m_HalfDetectionAngle / 9) + 1;
            Vector3[] points = new Vector3[count];
            Vector3 directionDetection = transform.forward * m_DetectionRadius;
            Vector3 directionDetectionFirst = Quaternion.Euler(0, -m_HalfDetectionAngle, 0) * directionDetection;
            
            Quaternion angle = Quaternion.Euler(0, m_HalfDetectionAngle * 2f / (count - 1), 0f);
            Vector3 pos = transform.position;
            for (int i = 0; i < count; ++i)
            {
                points[i] = pos + directionDetectionFirst;
                directionDetectionFirst = angle * directionDetectionFirst;
            }

            int[] indexes = new int[(count - 1) * 2];
            for (int i = 0; i < count - 1; ++i)
            {
                indexes[i * 2] = i;
                indexes[i * 2 + 1] = i + 1;
            }
            
            Handles.DrawLines(points, indexes);
        }
    }
}