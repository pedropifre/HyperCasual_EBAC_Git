using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeedUp : PowerUpBase
{
    [Header("Power up speed up")]
    public float amountSpeed;
    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.PowerUpSpeedUp(amountSpeed);
        PlayerController.Instance.SetPowerUpText("Speed Boost");
    }
    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.Instance.ResetSpeed();
        PlayerController.Instance.SetPowerUpText("");
    }
}
