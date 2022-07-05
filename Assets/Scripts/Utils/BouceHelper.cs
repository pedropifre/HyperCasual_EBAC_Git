using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BouceHelper : MonoBehaviour
{
    [Header("Animation")]
    public float scaleDuration = .2f;
    public float scaleBounce = 1.2f;
    public Ease ease = Ease.OutBack;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Bounce();
        }
    }

    public void Bounce()
    {
        

        var sequence = DOTween.Sequence()
           .Append(transform.DOScale(scaleBounce, scaleDuration).SetEase(ease));
        sequence.SetLoops(2, LoopType.Yoyo);
    }
    
}
