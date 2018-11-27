using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Physics
{

    public class CollisionDetection : MonoBehaviour
    {

        [NonSerialized] public List<float> xValues;

        [NonSerialized] public List<float> yValues;

        public List<CollisionVolume2D> colliders;
        public List<CollisionVolume2D> OpenList;
        public List<CollisionVolume2D> ClosedList;

        public Transform collisionVolumes;

        void Start()
        {
            foreach (Transform child in collisionVolumes)
            {
                colliders.Add(child.GetComponent<CollisionVolume2D>());
            }
        }

    //    void Update()
    //    {
    //        for (int i = 0; i < colliders.Count; i++)
    //        {
    //            for (int j = 0; j < colliders.Count; j++)
    //            {
    //                if (colliders[i] != colliders[j])
    //                {
    //                    if (colliders[i].min.x < colliders[j].max.x &&
    //                        colliders[i].max.x > colliders[j].min.x &&
    //                        colliders[i].min.y < colliders[j].max.y &&
    //                        colliders[i].max.y > colliders[j].min.y)
    //                    {
    //                        Debug.Log("collision detected");
    //                    }
    //                }
    //            }
    //        }
    //    }

    }
}

