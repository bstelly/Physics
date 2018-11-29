using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Comparers;


namespace Assets.Scripts.Physics.AABB
{
    public class CollisionVolume2D : MonoBehaviour
    {
        public Vector2 min;
        public Vector2 max;

        public bool isColliding;

        void Start()
        {
            min = new Vector2(transform.position.x - (transform.localScale.x / 2),
                transform.position.y - (transform.localScale.y / 2));
            max = new Vector2(transform.position.x + (transform.localScale.x / 2),
                transform.position.y + (transform.localScale.y / 2));
        }

        void Update()
        {
            min = new Vector2(transform.position.x - (transform.localScale.x / 2),
                transform.position.y - (transform.localScale.y / 2));
            max = new Vector2(transform.position.x + (transform.localScale.x / 2),
                transform.position.y + (transform.localScale.y / 2));
        }
    }
}
