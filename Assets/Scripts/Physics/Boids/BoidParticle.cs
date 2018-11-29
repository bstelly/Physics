using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Physics.Boids
{

    [Serializable]
    public class BoidParticle
    {
        public Vector3 Position;
        public Vector3 Velocity;
        public bool isPerching;
        public float perchTimer;

    }
}
