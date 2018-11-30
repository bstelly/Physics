using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

namespace Assets.Scripts.Physics.Cloth
{
    public class ClothBehaviour : MonoBehaviour
    {
        public List<Particle> particles;
        public List<SpringDamper> springDampers;

        public int width;
        public int height;

        void Start()
        {
            particles = new List<Particle>();
            springDampers = new List<SpringDamper>();

            for (int x = 0; x < width; x++)
            {
                for (float y = 0; y < height; y++)
                {
                    particles.Add(new Particle(new Vector3(x, y, 0)));
                }
            }

            for (int i = 0; i < particles.Count; i++)
            {
                if (particles[i].r.y == height - 1)
                {
                    particles[i].isAnchored = true;
                }
            }

            //for (int i = 0; i < particles.Count; i++)
            //{
            //    if (particles[i].r.x < width)
            //    {
            //        springDampers.Add(new SpringDamper(particles[i], particles[i + 1]));
            //    }
            //}


            for (int i = 0; i < particles.Count; i++)
            {
                if (i < width)
                {
                    springDampers.Add(new SpringDamper(particles[i], particles[i + 1]));
                }

                if (i != height)
                {
                    springDampers.Add(new SpringDamper(particles[i], particles[i + width]));
                }
            }
        }

        void Update()
        {
            foreach(var spring in springDampers)
            {
                spring.Update();
            }
            
            //Add gravity force to each particle
            foreach(var particle in particles)
            {
                if (!particle.isAnchored)
                {
                    particle.AddForce(new Vector3(0, -9.81f, 0));
                    particle.Update();
                }
            }
        }

        void OnDrawGizmos()
        {
            for (int i = 0; i < particles.Count; i++)
            {
                Gizmos.DrawSphere(particles[i].r, .5f);
            }

            for (int i = 0; i < springDampers.Count; i++)
            {
                Gizmos.DrawLine(springDampers[i].P1.r, springDampers[i].P2.r);
            }
        }
    }
}
