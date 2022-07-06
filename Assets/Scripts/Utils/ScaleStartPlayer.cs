using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleStartPlayer : MonoBehaviour
{
    [Header("Animation")]
    public float scaleDuration = 2f;
    public float scaleBounce = 1.2f;
    public Ease ease = Ease.OutBack;


    public void ScalePlayerInit(Transform transform)
    {
        //o Vector3.zero estava dando um bug onde ao animar, ele contava um hit de powerup
        //esse foi o unico jeito que encontrei de contornar isso, colocando 0.1f 
        transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        transform.DOScale(1, scaleDuration).SetEase(ease);
        
    }
}
