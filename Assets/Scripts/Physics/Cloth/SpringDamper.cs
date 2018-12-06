using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Physics.Cloth
{
    [Serializable]
    public class SpringDamper
    {
        public float Ks; //Spring Constant
        public float Kd; //Damping Factor
        public float Lo; //Rest Length
        public Particle P1; //Particle One
        public Particle P2; //Particle Two

        public SpringDamper(Particle particleOne, Particle particleTwo)
        {
            P1 = particleOne;
            P2 = particleTwo;
            Ks = 10;
            Kd = 2;
            Lo = Vector3.Distance(P1.r, P2.r);
        }

        public void Update()
        {
            //Calculate the unit length vector between the two particles
            var ePrime = P2.r - P1.r;
            var mag = ePrime.magnitude;
            var unitLength = ePrime / mag;

            //Calculate the 1D velocities
            var V1 = Vector3.Dot(unitLength, P1.v);
            var V2 = Vector3.Dot(unitLength, P2.v);

            //Convert from 1D to 3D
            var Fsd = (-Ks * (Lo - mag)) - (Kd * (V1 - V2));
            var F1 = Fsd * unitLength;
            var F2 = -F1;
            P1.AddForce(F1);
            P2.AddForce(F2);
        }
    }
}