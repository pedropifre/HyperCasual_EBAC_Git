using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BaseScaleHelper : MonoBehaviour
{
    [Header("Animation")]
    public float scaleDuration = 2f;
    public float scaleBounce = 1.2f;
    public Ease ease = Ease.OutBack;

    public void Start()
    {

        Vector3 scaleInicial = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(scaleInicial, scaleDuration).SetEase(ease);

    }


}
