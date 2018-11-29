using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Physics.Cloth
{
    public class ClothBehaviour : MonoBehaviour
    {
        public SpringDamper springDamper;
        public List<Particle> particles;

        public GameObject particleOne;
        public GameObject particleTwo;


        // Use this for initialization
        void Start()
        {

            springDamper.P1 = new Particle();
            springDamper.P2 = particleTwo.GetComponent<Particle>();
        }

        // Update is called once per frame
        void Update()
        {
            springDamper.Update();
            
        }
    }
}
