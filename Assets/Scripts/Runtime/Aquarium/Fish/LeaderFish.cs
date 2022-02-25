using System;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;

namespace Runtime.Aquarium.Fish
{
    public class LeaderFish : AFish
    {
        private void Start()
        {
            var collider = gameObject.AddComponent<SphereCollider>();
            collider.isTrigger = true;
            collider.radius = 1f;
        }
    }
}