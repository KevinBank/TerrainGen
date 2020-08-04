using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]

public class MeshGeneraor : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;
    private int xSize = 100;
    private int zSize = 100;
    private float randomN1;

    private void Start()
    {
        randomN1 = Random.Range(0, 10000);

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for(int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float zoom1 = 0.1f; //smoothness of terrain
                float zoom2 = 0.075f;
                float zoom3 = 0.05f;
                float heightVarietion = 1;
                float y = Mathf.PerlinNoise(x * zoom1, z * zoom1) * heightVarietion + 5;//smaller zoom = bigger blobs
                float y2 = y + Mathf.PerlinNoise(x * zoom2, z * zoom2) * heightVarietion * 10; //This is how you stack noise?
                float y3 = y2 + Mathf.PerlinNoise(x * zoom3, z * zoom3) * heightVarietion * 20;
                vertices[i] = new Vector3(x, y3, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    //private void OnDrawGizmos()
    //{
    //    if (vertices == null)
    //    {
    //        return;
    //    }
    //    for (int i = 0; i < vertices.Length; i++)
    //    {
    //        Gizmos.DrawSphere(vertices[i], .1f);
    //    }
    //}
}
