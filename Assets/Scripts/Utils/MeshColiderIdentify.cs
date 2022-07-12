using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshColiderIdentify : MonoBehaviour
{

    public bool hit = false;
    public MeshGenerator meshGenerator;

    private void Start()
    {
        hit = false;
    }
    //enquanto est� no come�o e ainda n�o iniciou a curva

    private void OnTriggerStay(Collider other)
    {
        hit = false;
    }
    private void OnTriggerExit(Collider other)
    {
        hit = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (hit)
        {
            Debug.Log("ahhhhhhhhhhhhh");
            meshGenerator.SetCollider();
            meshGenerator.Destruir();
        }
    }

}
