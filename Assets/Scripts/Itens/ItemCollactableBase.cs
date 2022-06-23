using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollactableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public float timeToHide=3;
    public GameObject graphicItem;
    public ParticleSystem particleSystem;

    [Header("Sounds")]
    public AudioSource audioSource;


    private void Awake()
    {
        //if (particleSystem != null) particleSystem.transform.SetParent(null);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    protected virtual void HideItens()
    {
        if (graphicItem != null) graphicItem.SetActive(false);
        Invoke(nameof(HideObject), timeToHide);
    }
    protected virtual void Collect() 
    {
        HideItens();
        OnCollect();
    }
    private void HideObject()
    {
        gameObject.SetActive(false);
    }
    protected virtual void OnCollect() 
    {

        if (particleSystem != null) particleSystem.Play();
        if (audioSource != null) audioSource.Play();
        
    }
    
        
}
