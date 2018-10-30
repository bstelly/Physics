using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Physics
{
    [CreateAssetMenu]
    public class MovementScriptable : ScriptableObject
    {
        [SerializeField]
        public enum MovementTypes
        {
            Linear
        };

        public MovementTypes movementType;
    }
}