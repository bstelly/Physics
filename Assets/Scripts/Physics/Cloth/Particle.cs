using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Physics.Cloth
{
    [Serializable]
    public class Particle
    {
        public Vector3 r; //position
        public Vector3 v; //Velocity
        public Vector3 a; //acceleration
        public Vector3 f; //force
        public float m; //mass

        Particle();
        Particle(Vector3 position)
        {
            r = position;
        }

        void Start()
        {
            a = f * m;
        }

        void Update()
        {
            a = f * m;
            v = v + (a * Time.deltaTime);
            r = r + (v * Time.deltaTime);
        }
    }
}