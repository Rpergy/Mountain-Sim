using System;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class MountainGenerator : MonoBehaviour
{
    [Header("Mesh Settings")]
    public float meshSize = 4;
    public int meshResolution = 2;

    [Header("Noise Settings")]
    public float amplitude = 1;
    public float noiseScale = 1;
    public int iterations = 1;

    public Vector3[] verts;
    int[] tris;

    float[,] noise;

    void Start()
    {
        noise = new float[meshResolution + 1, meshResolution + 1];
        GenerateNoise();
    }

    void Update()
    {
        noise = new float[meshResolution + 1, meshResolution + 1];
        GenerateNoise();
        GenerateVertices();
        GenerateTriangles();

        Mesh mesh = new Mesh
        {
            vertices = verts,
            triangles = tris
        };

        mesh.RecalculateNormals();
        
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void GenerateNoise()
    {
        for (int i = 0; i < iterations; i++)
        {
            float currentScale = noiseScale / (float)Math.Pow(2, i + 1);
            float currentAmplitude = amplitude / (float)Math.Pow(3, i + 1);
            for (int y = 0; y < noise.GetLength(0); y++)
            {
                for (int x = 0; x < noise.GetLength(1); x++)
                {
                    float sample = Mathf.PerlinNoise(x / currentScale, y / currentScale);
                    noise[y, x] += sample * currentAmplitude;
                }
            }
        }
    }

    void GenerateVertices()
    {
        float distBetweenVertices = meshSize / meshResolution;
        int verticesPerRow = meshResolution + 1;
        verts = new Vector3[verticesPerRow * verticesPerRow];
        int vertexIndex = 0;
        for (int z = 0; z < verticesPerRow; z++)
        {
            for (int x = 0; x < verticesPerRow; x++)
            {
                float height = noise[z, x];
                verts[vertexIndex] = new Vector3(x * distBetweenVertices, height, z * distBetweenVertices);
                vertexIndex++;
            }
        }
    }

    void GenerateTriangles()
    {
        int verticesPerRow = meshResolution + 1;

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
    }
}
