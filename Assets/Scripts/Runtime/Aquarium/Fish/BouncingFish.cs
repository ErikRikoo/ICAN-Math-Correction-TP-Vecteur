using System;
using UnityEngine;

namespace Runtime.Aquarium.Fish
{
    public class BouncingFish : AFish
    {
        private void Start()
        {
            Movement.OnCollisionWithBounds += OnCollisionWithBounds;
        }

        private void OnCollisionWithBounds(Vector2 _normal)
        {
            Movement.Speed = Vector2.Reflect(Movement.Speed, _normal);
        }
    }
}