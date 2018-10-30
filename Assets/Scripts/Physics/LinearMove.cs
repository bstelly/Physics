using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Physics
{
    public class LinearMove : IMoveable
    {

        public Vector3 Move(ref Particle particle, float dt)
        {
            particle.Displacement =  
            //particle.Velocity = (particle.Force / particle.Mass);
            //return particle.Position = particle.Position + particle.Velocity * dt;
        }
    }
}