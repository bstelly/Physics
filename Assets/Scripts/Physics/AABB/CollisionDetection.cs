using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Physics.AABB
{

    public class CollisionDetection : MonoBehaviour
    {
        public List<CollisionVolume2D> colliders;

        public Transform collisionVolumes;

        public Material collisionMaterial;

        private Renderer renderer = new Renderer();

        void Start()
        {
            foreach (Transform child in collisionVolumes)
            {
                colliders.Add(child.GetComponent<CollisionVolume2D>());
            }
            renderer.material = collisionMaterial;
        }

        void Update()
        {
            for (int i = 0; i < colliders.Count; i++)
            {
                for (int j = 0; j < colliders.Count; j++)
                {
                    if (colliders[i] != colliders[j])
                    {
                        if (colliders[i].min.x < colliders[j].max.x &&
                            colliders[i].max.x > colliders[j].min.x &&
                            colliders[i].min.y < colliders[j].max.y &&
                            colliders[i].max.y > colliders[j].min.y)
                        {
                            collisionVolumes.GetChild(i).GetComponent<Renderer>().material.color = Color.red;
                            collisionVolumes.GetChild(j).GetComponent<Renderer>().material.color = Color.red;
                            Debug.Log("collision detected");
                        }
                        else
                        {
                            collisionVolumes.GetChild(i).GetComponent<Renderer>().material.color = Color.white;
                        }
                    }
                }
            }
        }

    }
}

