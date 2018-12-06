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
        public float m = 1; //mass
        public bool IsAnchored;
        public bool IsActive = true;

        public Particle(Vector3 position)
        {
            r = position;
            a = f * m;
        }

        public void Update()
        {
            a = f * m;
            v = v + (a * Time.deltaTime);
            r = r + (v * Time.deltaTime);
            f = Vector3.zero;
        }

        public void AddForce(Vector3 force)
        {
            f += force;
        }
    }
}