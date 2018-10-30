using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Physics
{
    public interface IMoveable
    {
        Vector3 Move(ref Particle particle, float dt);
    }
}
