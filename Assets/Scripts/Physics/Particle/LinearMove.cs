using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Physics.Particle
{
    public class LinearMove : IMoveable
    {

        public Vector3 Move(ref Particle particle, float dt)
        {
            if (particle.Mass > 0)
            {
                particle.Velocity = (particle.Force / particle.Mass);
                return particle.Position += particle.Velocity * dt;
            }

            return particle.Position;
        }
    }
}