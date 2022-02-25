using System;
using Runtime.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Runtime.Aquarium.Fish
{
    [RequireComponent(typeof(Movement))]
    public class AFish : MonoBehaviour
    {
        
        private Movement m_Movement;

        protected Movement Movement => m_Movement;

        public Vector2 Position => transform.position.XZ();

        
        private void Awake()
        {
            m_Movement = GetComponent<Movement>();
        }
    }
}