using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Physics.Cloth
{
    public class ClothBehaviour : MonoBehaviour
    {
        public List<SpringDamper> springDampers;
        public List<Particle> particles;

        // Use this for initialization
        void Start()
        {
            float width = 5;
            float height = 5;

            for (int x = 0; x < width; x++)
            {
                for (float y = 0; y < height; y++)
                {
                    particles.Add(new Particle(new Vector3(x, y, 0)));
                }
            }

            for (int i = 0; i < particles.Count; i++)
            {

                springDampers.Add(new SpringDamper(particles[i], particles[i + 1]));
            }
        }

        // Update is called once per frame
        void Update()
        {
            foreach(var spring in springDampers)
            {
                spring.Update();
            }
            
            //Add gravity force to each particle
            foreach(var particle in particles)
            {
                particle.AddForce(new Vector3(0, -9.81, 0));
            }
        }
    }
}
