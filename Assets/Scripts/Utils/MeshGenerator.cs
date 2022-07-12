using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;
    public Vector3[] vertices;
    public List<Vector3> verticesList;
    public int[] triangles;

    public GameObject objReference;

    void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        //para não colidir com o objeto inicial
        //Adicionar o ponto inicial
        verticesList.Add(objReference.transform.localPosition);
    }

   

    private void Update()
    {
        Vector3 newVertices = objReference.transform.localPosition;
        if (Math.Round(newVertices.x,0) != Math.Round(verticesList[verticesList.Count - 1].x,0)
            && Math.Round(newVertices.z ,0) != Math.Round(verticesList[verticesList.Count - 1].z,0))
        {
            verticesList.Add(newVertices);
            CreateShape();
            UpdateMesh();
        }

    
    }
    private void CreateShape()
    {
        
        vertices = new Vector3[verticesList.Count];
        for (int y = 0; y < verticesList.Count; y++)
        {
            vertices[y] = verticesList[y];
        }
        int teste = verticesList.Count*3-12;
        if(teste>0)triangles = new int[teste];
        int newVert = 0;
        for (int y = 0; y < verticesList.Count*3 - 12; y+=3)
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

  
    }

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

     //setting up the collider
    public void SetCollider()
    {
        Debug.Log("Criando shape");
        MeshCollider meshc= new MeshCollider();
        meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        meshc.convex = true;
        verticesList.Clear();
        triangles = new int[0];
        vertices = new Vector3[0];
        mesh.Clear();
        verticesList.Add(objReference.transform.localPosition);
    }
    
    IEnumerator Esperar(int sec)
    {
        yield return new WaitForSeconds(sec);
        Destroy(gameObject.GetComponent<MeshCollider>());
    }

    public void Destruir()
    {
        StartCoroutine( Esperar(1));
       

    }
    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log(collision.transform.name);

    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log(other.transform.name);

    }
}
