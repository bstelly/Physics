using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Physics.Cloth
{
    public class ClothBehaviour : MonoBehaviour
    {
        public List<Particle> particles;
        public List<SpringDamper> springDampers;
        public List<AerodynamicForce> triangles;
        private Vector3 airDensity;

        public int width;
        public int height;

        private Particle grabbedParticle;
        private Vector3 worldMouse;

        public Slider SliderSpringConstant;
        public Slider SliderDampingFactor;
        public Slider SliderWindX;
        public Slider SliderWindY;
        public Slider SliderWindZ;

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
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            var mousePos = Input.mousePosition;
            worldMouse = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x,
                mousePos.y, -Camera.main.transform.position.z));

            if (Input.GetMouseButtonDown(0))
            {
                foreach (var particle in particles)
                {
                    var scaledPPositoin = new Vector3(particle.r.x * transform.localScale.x,
                        particle.r.y * transform.localScale.y,
                        particle.r.z * transform.localScale.z);
                    var checkPos = new Vector3(worldMouse.x, worldMouse.y, particle.r.z);
                    if (Vector3.Distance(checkPos, scaledPPositoin) <= 1f)
                    {
                        grabbedParticle = particle;
                    }
                }
            }

            if (Input.GetMouseButton(0) && grabbedParticle != null)
            {
                grabbedParticle.r = worldMouse;
                if (Input.GetKeyDown(KeyCode.R))
                {
                    grabbedParticle.IsActive = false;
                    for (var i = 0; i < springDampers.Count; i++)
                    {
                        if (grabbedParticle == springDampers[i].P1 || 
                            grabbedParticle == springDampers[i].P2)
                        {
                            springDampers.RemoveAt(i);
                        }
                    }

                    for (var i = 0; i < triangles.Count; i++)
                    {
                        if (grabbedParticle == triangles[i].R1 || 
                            grabbedParticle == triangles[i].R2 || 
                            grabbedParticle == triangles[i].R3)
                        {
                            triangles.RemoveAt(i);
                        }
                    }

                }

                if (Input.GetKeyDown(KeyCode.A))
                {
                    grabbedParticle.IsAnchored = !grabbedParticle.IsAnchored;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                grabbedParticle = null;
            }


            foreach (var spring in springDampers)
            {
                spring.Ks = SliderSpringConstant.value;
                spring.Kd = SliderDampingFactor.value;
                spring.Update();
            }
            
            //Add gravity force to each particle and update particles
            foreach(var particle in particles)
            {
                if (!particle.IsAnchored && particle != grabbedParticle)
                {
                    particle.AddForce(new Vector3(0, -9.81f, 0));
                    particle.Update();
                }
            }

            //Add aerodynamic force
            foreach(var triangle in triangles)
            {
                airDensity = new Vector3(SliderWindX.value, SliderWindY.value, SliderWindZ.value);
                triangle.p = airDensity;
                triangle.Update();
            }

        }

        void OnDrawGizmos()
        {
            //for (int i = 0; i < particles.Count; i++)
            //{
            //    Gizmos.color = Color.blue;
            //    Gizmos.DrawSphere(particles[i].r, .5f);
            //}

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
