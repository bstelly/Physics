using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Physics
{
    public class ParticleBehaviour : MonoBehaviour
    {

        //[SerializeField] public MovementScriptable movement;
        [SerializeField] Particle particle;

        void Start()
        {
            particle.Position = transform.position;

            switch (particle.movementType)
            {
                case Particle.MovementTypes.Linear:
                    particle.Moveable = new LinearMove();
                    break;
                case Particle.MovementTypes.Input:
                    particle.Moveable = new InputMove();
                    break;
            }

        }

        void Update()
        {
            transform.position = particle.Moveable.Move(ref particle, Time.deltaTime);
        }
    }
}