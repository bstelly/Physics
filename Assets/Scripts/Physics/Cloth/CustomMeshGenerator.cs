using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Policy;
using UnityEngine;

namespace Assets.Scripts.Physics.Cloth
{
    public class CustomMeshGenerator : MonoBehaviour
    {
        List<Vector3> Vertices = new List<Vector3>();
        List<int> TrianglePoints = new List<int>();
        List<Vector3> SurfaceNormals = new List<Vector3>();
        List<Vector2> UVs = new List<Vector2>();

        public MeshFilter InstanceMeshFilter;
        public Mesh InstanceMesh;

        public ClothBehaviour clothRef;

        public int rippedParticles = 0;

        void Start()
        {
            InstanceMesh = new Mesh();
            InstanceMesh.name = "Mesh";
            InstanceMeshFilter.mesh = InstanceMesh;

        }

        void Update()
        {
            //CheckForRippedParticles();
            AddVertices();
            AddTrianglePoints();
            RecalculateTriangles();
            FindSurfaceNormals();
            FindUVs();
            
        }

        void AddVertices()
        {
            Vertices.Clear();
            foreach (var p in clothRef.particles)
            {
                    Vertices.Add(p.r);
            }
            InstanceMesh.vertices = Vertices.ToArray();
        }

        void AddTrianglePoints()
        {
            if (TrianglePoints.Count == 0)
            {
                for (int i = 0; i < Vertices.Count; i++)
                {
                    if (i % clothRef.width != clothRef.width - 1 && i < Vertices.Count - clothRef.width)
                    {
                        //Bot Triangle
                        TrianglePoints.Add(i); //bot left
                        TrianglePoints.Add(i + 1); //bot right
                        TrianglePoints.Add(i + clothRef.width); //top left

                        //Top Triangle
                        TrianglePoints.Add(i + 1); //bot right
                        TrianglePoints.Add(i + clothRef.width + 1); //top right
                        TrianglePoints.Add(i + clothRef.width); //top left
                    }
                }
            }

            InstanceMesh.triangles = TrianglePoints.ToArray();
        }

        void FindSurfaceNormals()
        {
            SurfaceNormals.Clear();
            foreach (var vert in Vertices)
            {
                SurfaceNormals.Add(new Vector3(0, 0, 1));
            }
            InstanceMesh.normals = SurfaceNormals.ToArray();
        }

        void FindUVs()
        {
            UVs.Clear();
            foreach (var vert in Vertices)
            {
                UVs.Add(new Vector2(vert.x / (clothRef.width - 1), vert.y / (clothRef.width - 1)));
            }
            InstanceMesh.uv = UVs.ToArray();
        }

        void RecalculateTriangles()
        {
            var indices = new List<int>(InstanceMesh.triangles);
            for (var j = 0; j < indices.Count / 3 - 1; j++)
            {
                if (!clothRef.particles[indices[j * 3]].IsActive ||
                    !clothRef.particles[indices[j * 3 + 2]].IsActive ||
                    !clothRef.particles[indices[j * 3 + 1]].IsActive)
                {
                    indices.RemoveRange(j * 3, 3);
                }
            }

            InstanceMesh.triangles = indices.ToArray();
        }
    }
}
