using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Physics;
using UnityEngine;

namespace Assets.Scripts.Physics
{
    public class InputMove : IMoveable
    {
        public Vector3 Move(ref Particle particle, float dt)
        {
            if (Input.GetKey(KeyCode.W))
            {
                particle.Position += Vector3.up;
            }

            if (Input.GetKey(KeyCode.A))
            {
                particle.Position += Vector3.left;
            }

            if (Input.GetKey(KeyCode.S))
            {
                particle.Position += Vector3.down;
            }

            if (Input.GetKey(KeyCode.D))
            {
                particle.Position += Vector3.right;
            }

            return particle.Position;
        }
    }
}