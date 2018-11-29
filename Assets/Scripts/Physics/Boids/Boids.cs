using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Physics.Boids
{
    public class Boids : MonoBehaviour
    {
        public List<BoidParticle> boids = new List<BoidParticle>();
        public List<GameObject> gameObjects;
        public GameObject prefab;
        public Slider CohesionSlider;
        public Slider DispersionSlider;
        public Slider AlignmentSlider;
        public float cohesion = 100;
        public float dispersion = 1;
        public float alignment = 8;

        void Start()
        {
            CohesionSlider.value = cohesion;
            AlignmentSlider.value = alignment;
            DispersionSlider.value = dispersion;

            for (int i = 0; i < 300; i++)
            {
                //var temp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                var tempPos = new Vector3();
                tempPos.x = Random.Range(-500, 500);
                tempPos.y = Random.Range(1, 500);
                tempPos.z = Random.Range(-500, 500);
                var temp = Instantiate(prefab, tempPos, Quaternion.identity);

                gameObjects.Add(temp);
                boids.Add(new BoidParticle());
            }

            int iter = 0;
            foreach (var OBJECT in gameObjects)
            {
                boids[iter].Position = OBJECT.transform.position;
                iter += 1;
            }

            


            Initialize();
        }

        void Update()
        {
            cohesion = CohesionSlider.value;
            dispersion = DispersionSlider.value;
            alignment = AlignmentSlider.value;

            MoveAllBoidsToNewPositions();
        }

        public void Initialize()
        {
            foreach (var boid in boids)
            {
                boid.isPerching = false;
                boid.perchTimer = 100;
            }
        }

        public void MoveAllBoidsToNewPositions()
        {
            Vector3 v1, v2, v3, v4;

            int iter = 0;
            foreach (var boid in boids)
            {
                if (boid.isPerching)
                {
                    if (boid.perchTimer > 0)
                    {
                        boid.perchTimer = boid.perchTimer - 1;
                    }
                    else
                    {
                        boid.isPerching = false;
                        boid.perchTimer = 100;
                    }
                }

                if (boid.perchTimer == 100)
                {
                    v1 = Rule1(boid);
                    v2 = Rule2(boid);
                    v3 = Rule3(boid);
                    v4 = BoundPosition(boid);

                    boid.Velocity = boid.Velocity + v1 + v2 + v3 + v4;
                    LimitVelocity(boid);
                    float x = boid.Position.x;
                    boid.Position = boid.Position + boid.Velocity;

                    gameObjects[iter].transform.position = boid.Position;
                }

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
            return (centerOfMass - b.Position) / cohesion;
        }

        public Vector3 Rule2(BoidParticle b)
        {
            Vector3 c = new Vector3();

            foreach (var boid in boids)
            {
                if (boid != b)
                {
                    if ((boid.Position - b.Position).magnitude <= dispersion)
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

            return (perceivedVelocity - b.Velocity) / alignment;
        }

        public Vector3 BoundPosition(BoidParticle b)
        {
            int xMin = -30, xMax = 30, yMin = 1, yMax = 30, zMin = -30, zMax = 30;
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
                v.y = 1;
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

            if (b.Position.y <= GroundLevel)
            {
                b.Position.y = GroundLevel;
                b.isPerching = true;
            }

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

        public void AddMoreBoids()
        {
            List<GameObject> newGameObjects = new List<GameObject>();
            List<BoidParticle> newBoidParticles = new List<BoidParticle>();
            for (int i = 0; i <= 10; i++)
            {
                var tempPos = new Vector3();
                tempPos.x = Random.Range(-500, 500);
                tempPos.y = Random.Range(1, 500);
                tempPos.z = Random.Range(-500, 500);
                var temp = Instantiate(prefab, tempPos, Quaternion.identity);

                newGameObjects.Add(temp);
                newBoidParticles.Add(new BoidParticle());

                int iter = 0;
                foreach (var OBJECT in newGameObjects)
                {
                    boids[iter].Position = OBJECT.transform.position;
                    iter += 1;
                }

                foreach (var particle in newBoidParticles)
                {
                    particle.isPerching = false;
                    particle.perchTimer = 100;
                }

                foreach (var OBJECT in newGameObjects)
                {
                    gameObjects.Add(OBJECT);
                }

                foreach (var particle in newBoidParticles)
                {
                    boids.Add(particle);
                }

            }
        }
    }
}