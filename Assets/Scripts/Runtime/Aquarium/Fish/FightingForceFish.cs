using System;
using System.Collections;
using UnityEngine;

namespace Runtime.Aquarium.Fish
{
    public class FightingForceFish : AFish
    {
        [SerializeField] private Vector2 m_Force = new Vector2(0, -9.8f);

        [Min(0)]
        [SerializeField] private float m_PushDelay;
        [SerializeField] private float m_PushIntensity;

        private void Start()
        {
            Movement.Acceleration = m_Force;
            StartCoroutine(c_PushRoutine());
        }

        private IEnumerator c_PushRoutine()
        {
            while (true)
            {
                Push();
                yield return new WaitForSeconds(m_PushDelay);
            }
        }

        private void Push()
        {
            Movement.Speed += (-m_Force).normalized * m_PushIntensity;
        }
    }
}