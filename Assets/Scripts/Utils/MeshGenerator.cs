using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;
    public Vector3[] vertices;
    public Vector3[] vertices3;
    public int[] triangles;

    public GameObject objReference;
    public List<Vector3> vertices2;
    // Start is called before the first frame update
    void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;


        vertices2.Add(objReference.transform.localPosition);
    }

    private void FixedUpdate()
    {
        Vector3 newVertices = objReference.transform.localPosition;
        if (Math.Round(newVertices.x,0) != Math.Round(vertices2[vertices2.Count - 1].x,0)
            && Math.Round(newVertices.z ,0) != Math.Round(vertices2[vertices2.Count - 1].z,0))
        {
            vertices2.Add(newVertices);
        }

        
            CreateShape();
            UpdateMesh();
        

        //Debug.Log("Novo x = " + Math.Round(newVertices.x, 1) + " Antigo x= " + Math.Round(vertices2[vertices2.Count - 1].x, 1));
        //Debug.Log("Novo y = " + Math.Round(newVertices.y, 1) + " Antigo y= " + Math.Round(vertices2[vertices2.Count - 1].y, 1));
    
    }
    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        //SetCollider();
    }

     //setting up the collider
    public void SetCollider()
    {
        MeshCollider meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        meshc.convex = true;
        meshc.sharedMesh = mesh;
    }
    private void CreateShape()
    {
        vertices = new Vector3[vertices2.Count];
        for (int y = 0; y < vertices2.Count; y++)
        {
            vertices[y] = vertices2[y];
        }
        int teste = vertices2.Count*3-12;
        triangles = new int[teste];
        int newVert = 0;
        for (int y = 0; y < vertices2.Count*3 - 12; y+=3)
        {
            if (y == 0)
            {
                triangles[y] = 0;
                triangles[y+1] = y+1;
                triangles[y+2] = y+2;
                newVert = 3;
            }
            else
            {
                triangles[y] = 0;
                triangles[y+1] = newVert-1;
                triangles[y + 2] = newVert;
                newVert++;
            }
        }

        //triangles = new int[]
        //{
        //    0,1,2,
        //    0,2,3,
        //    0,3,4,
        //    0,4,5,
        //    0,5,6,
        //    0,6,7,
        //    0,7,8,
        //    0,8,9
        //};
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        
            Debug.Log(collision.transform.name);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
            Debug.Log(other.transform.name);
        
    }
}
