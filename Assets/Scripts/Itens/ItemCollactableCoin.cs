using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollactableCoin : ItemCollactableBase
{    
    public Collider collider;
    public bool collect = false;
    public float lerp = 5f;
    public float minDistance = 1f;
    

    private void Start()
    {
        CoinsAnimationManager.Instance.RegisterCoins(this);
    }
    protected override void OnCollect()
    {
        base.OnCollect();
        collider.enabled = false;
        collect = true;
        PlayerController.Instance.Bounce();
        CoinsCount.Instance.ChangeTextCoins(1);
    }

    protected override void Collect()
    {
        OnCollect();
    }


    private void Update()
    {
        if (collect)
        {
            transform.position = Vector3.Lerp(transform.position, PlayerController.Instance.transform.position, lerp * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < minDistance)
            {
                HideItens();
                Destroy(gameObject);
            }
;
        }
    }
}
