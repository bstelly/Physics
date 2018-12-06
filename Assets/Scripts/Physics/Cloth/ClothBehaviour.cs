using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Physics.Cloth
{
    public class ClothBehaviour : MonoBehaviour
    {
        public List<Particle> particles;
        public List<SpringDamper> springDampers;
        public List<AerodynamicForce> triangles;
        public Vector3 airDensity;

        public int width;
        public int height;

        private Particle grabbedParticle;
        private Vector3 worldMouse;

        void Start()
        {
            particles = new List<Particle>();
            springDampers = new List<SpringDamper>();
            triangles = new List<AerodynamicForce>();


            for (int y = 0; y < height; y++)
            {
                for (float x = 0; x < width; x++)
                {
                    particles.Add(new Particle(new Vector3(x, y, 0)));
                }
            }

            //Anchoring particles
            for (int i = 0; i < particles.Count; i++)
            {
                if (particles[i].r.y == height - 1)
                {
                    particles[i].IsAnchored = true;
                }
            }

            //Connecting the spring dampers
            for (int i = 0; i < particles.Count; i++)
            {
                //horizontal
                if (particles[i].r.x < width - 1)
                {
                    springDampers.Add(new SpringDamper(particles[i], particles[i + 1]));
                }

                if (particles[i].r.y < height - 1)
                {
                    springDampers.Add(new SpringDamper(particles[i], particles[i + width]));
                }

                //Cross dampers
                if (particles[i].r.x < width - 1 && particles[i].r.y < height - 1)
                {
                    springDampers.Add(new SpringDamper(particles[i], particles[i + width + 1]));

                    //Creating triangles for aerodynamics
                    triangles.Add(new AerodynamicForce(particles[i], particles[i + 1],
                        particles[i + width]));
                    triangles.Add(new AerodynamicForce(particles[i + 1], particles[i + width],
                        particles[i + width + 1]));
                }

                if (particles[i].r.x > 0 && particles[i].r.y != height - 1)
                {
                    springDampers.Add(new SpringDamper(particles[i], particles[i + width - 1]));
                }


            }
        }

        void Update()
        {
            var mousePos = Input.mousePosition;
            worldMouse = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x,
                mousePos.y, -Camera.main.transform.position.z));

            if (Input.GetMouseButtonDown(0))
            {
                foreach (var p in particles)
                {
                    var scaledPPositoin = new Vector3(p.r.x * transform.localScale.x,
                        p.r.y * transform.localScale.y,
                        p.r.z * transform.localScale.z);
                    var checkPos = new Vector3(worldMouse.x, worldMouse.y, p.r.z);
                    if (Vector3.Distance(checkPos, scaledPPositoin) <= 1f)
                    {
                        grabbedParticle = p;
                    }
                }
            }

            if (Input.GetMouseButton(0) && grabbedParticle != null)
            {
                grabbedParticle.r = worldMouse;
                if (grabbedParticle.f.magnitude >= 1 || Input.GetKeyDown(KeyCode.R))
                {
                    grabbedParticle.IsActive = false;
                    for (var i = 0; i < springDampers.Count; i++)
                    {
                        if (springDampers[i].CheckParticles(grabbedParticle))
                        {
                            springDampers.RemoveAt(i);
                        }
                    }

                    for (var i = 0; i < triangles.Count; i++)
                    {
                        if ()
                        {
                            triangles.RemoveAt(i);
                        }
                    }
                }

                if (Input.GetKeyDown(KeyCode.A))
                    grabbedParticle.IsAnchored = !grabbedParticle.IsAnchored;
            }

            if (Input.GetMouseButtonUp(0))
            {
                grabbedParticle = null;
            }




            foreach (var spring in springDampers)
            {
                spring.Update();
            }
            
            //Add gravity force to each particle
            foreach(var particle in particles)
            {
                if (!particle.IsAnchored)
                {
                    particle.Update();
                    particle.AddForce(new Vector3(0, -9.81f, 0));
                }
            }

            foreach(var triangle in triangles)
            {
                triangle.p = airDensity;
                triangle.Update();
            }
        }

        void OnDrawGizmos()
        {
            for (int i = 0; i < particles.Count; i++)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(particles[i].r, .5f);
            }

            for (int i = 0; i < springDampers.Count; i++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(springDampers[i].P1.r, springDampers[i].P2.r);
            }

            ////Checking if triangles were created properly
            //for (int i = 0; i < triangles.Count; i++)
            //{
            //    Gizmos.DrawLine(triangles[i].R1.r, triangles[i].R2.r);
            //    Gizmos.DrawLine(triangles[i].R2.r, triangles[i].R3.r);
            //    Gizmos.DrawLine(triangles[i].R3.r, triangles[i].R1.r);
            //}
        }
    }
}
