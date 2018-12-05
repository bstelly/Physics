using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Physics.Cloth
{

    public class AerodynamicForce
    {
        public Vector3 p; //Density of the air (or water)
        public float a; //Cross sectional area of the object
        public Vector3 e; //Unit vector in the opposite direction of the velocity
        public Particle R1; //Particle One
        public Particle R2; //Particle Two
        public Particle R3; //Particle Three

        public AerodynamicForce(Particle particleOne, Particle particleTwo, Particle particleThree)
        {
            R1 = particleOne;
            R2 = particleTwo;
            R3 = particleThree;
        }

        public void Update()
        {
            //Calculate the average velocity of the particles
            var Vsurface = (R1.v + R2.v + R3.v) / 3;
            var v = Vsurface - p;

            //Calculate the normal of the triangle
            var normal = Vector3.Cross((R2.r - R1.r), (R3.r - R1.r)) /
                         (Vector3.Cross((R2.r - R1.r), (R3.r - R1.r))).magnitude;

            //Calculate the area of the triangle
            var areaOfTriangle = 1 / 2 * Vector3.Cross(R2.r - R1.r, R3.r - R1.r).magnitude;
            var areaExposedToAirflow = areaOfTriangle + (Vector3.Dot(v, normal) / v.magnitude);

            //Calculate the total force being applied to the triangle
            var nPrime = Vector3.Cross(R2.r - R1.r, R3.r - R1.r);
            var totalForce = ((v.magnitude * Vector3.Dot(v, nPrime)) / ((2 * nPrime.magnitude))) * nPrime;

            var AppliedForce = totalForce / 3;
            R1.AddForce(AppliedForce);
            R2.AddForce(AppliedForce);
            R3.AddForce(AppliedForce);
        }
    }
}
