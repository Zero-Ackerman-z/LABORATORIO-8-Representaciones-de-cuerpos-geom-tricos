using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereGenerator : MonoBehaviour
{
    public int numDivisions = 10;
    public float radius = 1f;
    public float vertexSize = 0.1f;
    public Vector3 translation = new Vector3(0f, 2f, 0f);
    public Vector3 scale = new Vector3(1.5f, 1.5f, 1.5f);
    public Vector3 rotation = new Vector3(45f, 30f, 60f);
    void Update()
    {
        // Aplicar transformación de translación
        transform.position = translation;

        // Aplicar transformación de escalado
        transform.localScale = scale;

        // Aplicar transformación de rotación
        transform.rotation = Quaternion.Euler(rotation);
    }
    void OnDrawGizmos()
    {
        DrawSphere();
    }
    void DrawSphere()
    {
        Vector3[] vertices = new Vector3[(numDivisions + 1) * (numDivisions + 1)];
        int[] triangles = new int[numDivisions * numDivisions * 6];

        for (int i = 0; i <= numDivisions; i++)
        {
            for (int j = 0; j <= numDivisions; j++)
            {
                float phi = (float)i / numDivisions * Mathf.PI;
                float theta = (float)j / numDivisions * Mathf.PI * 2;

                float x = Mathf.Sin(theta) * Mathf.Sin(phi);
                float y = Mathf.Cos(phi);
                float z = Mathf.Cos(theta) * Mathf.Sin(phi);

                Vector3 vertex = new Vector3(x, y, z) * radius;
                // Aplicar traslación
                vertex += translation;

                vertices[i * (numDivisions + 1) + j] = vertex;
                // Escalar el vértice basado en su dirección
                vertex = Vector3.Scale(vertex, scale);

                vertices[i * (numDivisions + 1) + j] = vertex;
                // Rotar el vértice
                vertex = Quaternion.Euler(rotation) * vertex;

                vertices[i * (numDivisions + 1) + j] = vertex;
            }
        }
        for (int i = 0; i < numDivisions; i++)
        {
            for (int j = 0; j < numDivisions; j++)
            {
                int vertexIndex = i * (numDivisions + 1) + j;

                DrawGizmoLine(vertices[vertexIndex], vertices[vertexIndex + 1]);
                DrawGizmoLine(vertices[vertexIndex], vertices[vertexIndex + numDivisions + 1]);
                DrawGizmoLine(vertices[vertexIndex + 1], vertices[vertexIndex + numDivisions + 2]);
                DrawGizmoLine(vertices[vertexIndex + numDivisions + 1], vertices[vertexIndex + numDivisions + 2]);
            }
        }

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + vertices[i], vertexSize);
        }
    }
    void DrawGizmoLine(Vector3 start, Vector3 end)
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + start, transform.position + end);
    }

}
