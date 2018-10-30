using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Physics
{
    [Serializable]
    public class Particle
    {
        public IMoveable Moveable;
        public Vector3 Position { get; set; }
        public Vector3 Displacement { get; set; }
        public Vector3 Velocity;
        public Vector3 Acceleration { get; set; }
        public Vector3 Force;
        public float Mass;
    }
}