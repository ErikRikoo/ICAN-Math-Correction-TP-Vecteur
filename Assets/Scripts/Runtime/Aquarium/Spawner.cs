using System;
using Runtime.Aquarium.Fish;
using Runtime.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Aquarium
{
    [Serializable]
    struct FishSpawner
    {
        public AFish Fish;
        
        [Min(0)]
        public int SpawnCount;
    }
    
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private bool m_ShouldRandomizeColor;
        
        
        [SerializeField] private AquariumBound m_Bound;
        
        [SerializeField] private FishSpawner[] m_FishToSpawn;

        private void Awake()
        {
            foreach (var fishSpawner in m_FishToSpawn)
            {
                for (int i = 0; i < fishSpawner.SpawnCount; i++)
                {
                    var instantiated = Instantiate(fishSpawner.Fish, m_Bound.RandomPosition.X0Y(), Quaternion.identity);
                    if (m_ShouldRandomizeColor)
                    {
                        instantiated.GetComponent<Renderer>().material.color = Random.ColorHSV();
                    }
                }
            }
        }
    }
}