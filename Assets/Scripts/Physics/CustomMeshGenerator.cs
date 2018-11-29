using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class CustomMeshGenerator {
    List<Vector3> vertices = new List<Vector3>();
    List<int> TrianglePoints = new List<int>();
    List<Vector3> SurfaceNormals = new List<Vector3>();
    List<Vector2> UVs = new List<Vector2>();
    public MeshFilter InstanceMeshFilter;
    public Mesh InstanceMesh;

    void start()
    {
        InstanceMesh = new InstanceMesh();
        InstanceMesh.name = "Mesh";
    }
}
