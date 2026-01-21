using System;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class MountainGenerator : MonoBehaviour
{
    public float meshSize = 4;
    public int meshResolution = 2;
    public float heightScale = 1;
    public float widthScale = 1;

    Vector3[] verts;
    int[] tris;

    void Update()
    {
        float distBetweenVertices = meshSize / meshResolution;
        int verticesPerRow = meshResolution + 1;
        verts = new Vector3[verticesPerRow * verticesPerRow];
        int vertexIndex = 0;
        for (int z = 0; z < verticesPerRow; z++)
        {
            for (int x = 0; x < verticesPerRow; x++)
            {
                double randHeight = Math.Exp(-(Math.Pow((x * distBetweenVertices - meshSize / 2) / widthScale, 2) + Math.Pow((z * distBetweenVertices - meshSize / 2) / widthScale, 2)));
                verts[vertexIndex] = new Vector3(x * distBetweenVertices, (float)randHeight * heightScale, z * distBetweenVertices);
                vertexIndex++;
            }
        }

        tris = new int[(meshResolution * meshResolution) * 6];
        int startingVert = verticesPerRow;
        int triangleIndex = 0;
        for (int i = 0; i < meshResolution; i++)
        {
            for (int j = 0; j < meshResolution; j++)
            {
                tris[triangleIndex + 2] = startingVert;
                tris[triangleIndex + 1] = startingVert - verticesPerRow;
                tris[triangleIndex] = startingVert - verticesPerRow + 1;

                tris[triangleIndex + 5] = startingVert;
                tris[triangleIndex + 4] = startingVert - verticesPerRow + 1;
                tris[triangleIndex + 3] = startingVert + 1;
                startingVert += 1;
                triangleIndex += 6;
            }
            startingVert += 1;
        }

        Mesh mesh = new Mesh
        {
            vertices = verts,
            triangles = tris
        };

        mesh.RecalculateNormals();
        
        GetComponent<MeshFilter>().mesh = mesh;
    }
}
