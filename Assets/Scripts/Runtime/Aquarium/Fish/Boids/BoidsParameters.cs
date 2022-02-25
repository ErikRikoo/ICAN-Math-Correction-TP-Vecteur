using UnityEngine;

namespace Runtime.Aquarium.Fish.Boids
{
    [CreateAssetMenu(fileName = "BoidsParameters", menuName = "Aquarium/Boids/Parameters", order = 0)]
    public class BoidsParameters : ScriptableObject
    {
        [Range(0, 1)]
        [SerializeField] private float m_CoherenceRatio;
        
        [Range(0, 1)]
        [SerializeField] private float m_SeparationRatio;
        
        [Range(0, 1)]
        [SerializeField] private float m_AlignmentRatio;

        public float CoherenceRatio => m_CoherenceRatio;
        public float SeparationRatio => m_SeparationRatio;
        public float AlignmentRatio => m_AlignmentRatio;
    }
}