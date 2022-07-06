using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : ItemCollactableBase
{
    public float duration;

    protected override void OnCollect()
    {
        base.OnCollect();
        PlayerController.Instance.Bounce();
        StartPowerUp();
    }

    protected virtual void StartPowerUp()
    {
        Debug.Log("start power up");
        Invoke(nameof(EndPowerUp), duration);
    }

    protected virtual void EndPowerUp()
    {
        Debug.Log("end power up");                            
    }


}
