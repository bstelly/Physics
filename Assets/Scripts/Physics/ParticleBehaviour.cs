using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Physics
{
    public class ParticleBehaviour : MonoBehaviour
    {

        [SerializeField] public MovementScriptable movement;
        [SerializeField] Particle particle;

        void Start()
        {
            transform.position = particle.Position;

            if (movement.movementType == MovementScriptable.MovementTypes.Linear)
            {
                particle.Moveable = new LinearMove();
            }
            //switch (movement.movementType)
            //{
            //    case MovementScriptable.MovementTypes.Linear:
            //        particle.Moveable = new LinearMove();
            //        break;
            //}

        }

        void Update()
        {
            transform.position = particle.Moveable.Move(ref particle, Time.deltaTime);
        }
    }
}