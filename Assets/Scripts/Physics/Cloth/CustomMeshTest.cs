﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Physics.Cloth
{



    public class CustomMeshTest : MonoBehaviour
    {

        List<Vector3> Vertices = new List<Vector3>();
        List<int> TrianglePoints = new List<int>();
        List<Vector3> SurfaceNormals = new List<Vector3>();
        List<Vector2> UVs = new List<Vector2>();
        public ClothBehaviour clothRef;
        MeshFilter InstanceMeshFilter;
        public Mesh InstanceMesh;

        public CustomMeshTest(List<AerodynamicForce> triangleList, MeshFilter meshFilter)
        {
            InstanceMesh = new Mesh();
            InstanceMesh.name = "Cloth";
            InstanceMeshFilter = meshFilter;

            for (int i = 0; i < Vertices.Count; i++)
            {
                
            }

            //for (int x = 0; x < 5; x++)
            //{
            //    for (int y = 0; y < 5; y++)
            //    {
            //        Vertices.Add(new Vector3(x, y, 0));
            //    }
            //}

            InstanceMesh.vertices = Vertices.ToArray();
        }

        public void Start()
        {
            foreach (var p in clothRef.particles)
            {
                Vertices.Add(p.r);
            }
            InstanceMesh.vertices = Vertices.ToArray();

            for (int i = 0; i < Vertices.Count; i++)
            {
                if (i % 5 != 5 - 1 && i < Vertices.Count - 5)
                {
                    //Bot Triangle
                    TrianglePoints.Add(i); //bot left
                    TrianglePoints.Add(i + 1); //bot right
                    TrianglePoints.Add(i + 5); //top left

                    //Top Triangle
                    TrianglePoints.Add(i + 1); //bot right
                    TrianglePoints.Add(i + 5 + 1); //top right
                    TrianglePoints.Add(i + 5); //top left
                }
            }

            InstanceMesh.triangles = TrianglePoints.ToArray();

            foreach (var vert in Vertices)
            {
                SurfaceNormals.Add(new Vector3(0, 0, 1));
            }

            InstanceMesh.normals = SurfaceNormals.ToArray();

            foreach (var vert in Vertices)
            {
                UVs.Add(new Vector2(vert.x / (5 - 1), vert.y / (5 - 1)));
            }

            InstanceMesh.uv = UVs.ToArray();

            InstanceMeshFilter.mesh = InstanceMesh;

        }

        public void Update()
        {

        }
    }
}
