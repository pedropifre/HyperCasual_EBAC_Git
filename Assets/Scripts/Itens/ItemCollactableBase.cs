using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollactableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public float timeToHide=3;
    public GameObject graphicItem;
    public TextMeshPro graphicText;
    public ParticleSystem particleSystem2;

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
            //Debug.Log(collision.name);
        }
    }

    protected virtual void HideItens()
    {
        if (graphicItem != null) graphicItem.SetActive(false);
        if (graphicText != null) graphicText.text = "";
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

        if (particleSystem2 != null)
        {
            particleSystem2.transform.SetParent(null);   
            particleSystem2.Play();
        }
        if (audioSource != null) audioSource.Play();
        
    }
    
        
}
