using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Physics
{
    public class Boids : MonoBehaviour
    {
        public List<BoidParticle> boids = new List<BoidParticle>();
        public List<GameObject> gameObjects;


        void Start()
        {
            for (int i = 0; i < 400; i++)
            {
                gameObjects.Add(GameObject.CreatePrimitive(PrimitiveType.Sphere));
                boids.Add(new BoidParticle());
            }

            int iter = 0;
            foreach (var OBJECT in gameObjects)
            {
                boids[iter].Position = OBJECT.transform.position;
                iter += 1;
            }
        }

        void Update()
        {
            MoveAllBoidsToNewPositions();
        }

        public void InitializePositions()
        {
            
        }

        public void MoveAllBoidsToNewPositions()
        {
            Vector3 v1, v2, v3, v4;

            int iter = 0;
            foreach (var boid in boids)
            {
                //if (boid.isPerching)
                //{
                //    if (boid.perchTimer > 0)
                //    {
                //        boid.perchTimer = boid.perchTimer - 1;
                //    }
                //    else
                //    {
                //        boid.isPerching = false;
                //    }
                //}

                v1 = Rule1(boid);
                v2 = Rule2(boid);
                v3 = Rule3(boid);
                v4 = BoundPosition(boid);


                boid.Velocity = boid.Velocity + v1 + v2 + v3 + v4;
                LimitVelocity(boid);
                boid.Position = boid.Position + boid.Velocity;

                gameObjects[iter].transform.position = boid.Position;
                iter += 1;
            }
        }

        public Vector3 Rule1(BoidParticle b)
        {
            Vector3 centerOfMass = new Vector3();

            foreach (var boid in boids)
            {
                if (boid != b)
                {
                    centerOfMass = centerOfMass + boid.Position;
                }
            }

            centerOfMass = centerOfMass / (boids.Count - 1);
            return (centerOfMass - b.Position) / 100;
        }

        public Vector3 Rule2(BoidParticle b)
        {
            Vector3 c = new Vector3();

            foreach (var boid in boids)
            {
                if (boid != b)
                {
                    if ((boid.Position - b.Position).magnitude <= 1)
                    {
                        c = c - (boid.Position - b.Position);
                    }
                }
            }

            return c;
        }

        public Vector3 Rule3(BoidParticle b)
        {
            Vector3 perceivedVelocity = new Vector3();

            foreach (var boid in boids)
            {
                if (boid != b)
                {
                    perceivedVelocity = perceivedVelocity + boid.Velocity;
                }
            }

            perceivedVelocity = perceivedVelocity / (boids.Count - 1);

            return (perceivedVelocity - b.Velocity) / 8;
        }

        public Vector3 BoundPosition(BoidParticle b)
        {
            int xMin = -30, xMax = 30, yMin = -30, yMax = 30, zMin = -30, zMax = 30;
            int GroundLevel = 0;

            Vector3 v = new Vector3();

            if (b.Position.x < xMin)
            {
                v.x = 30;
            }
            else if (b.Position.x > xMax)
            {
                v.x = -30;
            }

            if (b.Position.y < yMin)
            {
                v.y = 30;
            }
            else if (b.Position.y > yMax)
            {
                v.y = -30;
            }

            if (b.Position.z < zMin)
            {
                v.z = 30;
            }
            else if (b.Position.z > zMax)
            {
                v.z = -30;
            }

            //if (b.Position.y < GroundLevel)
            //{
            //    b.Position.y = GroundLevel;
            //    b.isPerching = true;
            //}

            return v;
        }

        public void LimitVelocity(BoidParticle b)
        {
            int vlim = 2;
            Vector3 v;
            if (b.Velocity.magnitude > vlim)
            {
                b.Velocity = (b.Velocity / (b.Velocity.magnitude) * vlim);
            }
        }
    }
}
