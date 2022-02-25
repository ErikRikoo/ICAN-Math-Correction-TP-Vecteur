using System;
using Runtime.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

namespace Runtime.Aquarium.Fish
{
    public class RandomFish : AFish
    {
        [SerializeField] private AquariumBound m_Bound;
        
        [Min(0f)]
        [SerializeField] private float m_PositionThreshold;
        [Min(0)]
        [SerializeField] private float m_Speed = 1f;
        
        private Vector2 m_Goal;

        public void Start()
        {
            ComputeNewGoal();
        }

        private void Update()
        {
            if (Vector2.Distance(transform.position.XZ(), m_Goal) < m_PositionThreshold)
            {
                ComputeNewGoal();
            }
        }

        private void ComputeNewGoal()
        {
            m_Goal = m_Bound.RandomPosition;
            Movement.Speed = (m_Goal - transform.position.XZ()).normalized * m_Speed;
        }

        private void OnDrawGizmos()
        {
            Handles.color = Color.blue;
            Handles.DrawLine(transform.position, m_Goal.X0Y());
            Handles.color = Color.red;
            Handles.DrawWireDisc(m_Goal.X0Y(), Vector3.up, 0.5f);
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, Vector3.up, m_PositionThreshold);
        }
    }
}