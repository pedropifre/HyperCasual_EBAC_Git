using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{

    public List<AnimatorSetup> animatorSetups;
    public Animator animator;
    public enum AnimationType
    {
        IDLE,
        RUN,
        DEAD
    }

    public void Play(AnimationType type, float currentSpeedFactor=1f)
    {
        foreach (var animation in animatorSetups)
        {
            if (animation.type == type)
            {
                animator.SetTrigger(animation.trigger);
                animator.speed = animation.speed*currentSpeedFactor;
                break;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Play(AnimationType.RUN);
        }else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Play(AnimationType.IDLE);
        }else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Play(AnimationType.DEAD);
        }
    }


}

[System.Serializable]
public class AnimatorSetup
{
    public AnimatorManager.AnimationType type;
    public string trigger;
    public float speed = 1f;
}