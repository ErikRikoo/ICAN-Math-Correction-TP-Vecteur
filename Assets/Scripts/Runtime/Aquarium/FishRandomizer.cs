using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Aquarium
{
    [RequireComponent(typeof(Movement))]
    public class FishRandomizer : MonoBehaviour
    {
        [SerializeField] private bool m_ShouldRandomize;
        
        
        [Min(0)]
        [SerializeField] private float m_RandomizeFactor = 1;
        
        [SerializeField] private bool m_ShouldSetAcceleration;

        private void Awake()
        {
            if (!m_ShouldRandomize)
            {
                return;
            }
            
            var movement = GetComponent<Movement>();
            movement.Speed = Random.insideUnitCircle * m_RandomizeFactor;
            if (m_ShouldSetAcceleration)
            {
                movement.Acceleration = Random.insideUnitCircle * m_RandomizeFactor;
            }
        }
    }
}