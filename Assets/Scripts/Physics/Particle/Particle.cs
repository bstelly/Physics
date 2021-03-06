﻿using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Physics.Particle
{
    [Serializable]
    public class Particle
    {
        public Vector3 Position { get; set; }
        public Vector3 Displacement { get; set; }
        public Vector3 Velocity { get; set; }
        public Vector3 Acceleration { get; set; }
        public Vector3 Force;
        public float Mass;

        public IMoveable Moveable;

        public enum MovementTypes
        {
            Linear,
            Input
        }

        public MovementTypes movementType;
    }
}