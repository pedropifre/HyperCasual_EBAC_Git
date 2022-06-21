using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollactableBase : MonoBehaviour
{

    [Header("Sounds")]
    public AudioSource audioSource;

    public string compareTag = "Player";
    public float timeToHide=3;
    public GameObject graphicItem;
    public ParticleSystem particleSystem;

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


    protected virtual void Collect() 
    {
        if (graphicItem != null) graphicItem.SetActive(false);
        Invoke(nameof(HideObject), timeToHide);
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
