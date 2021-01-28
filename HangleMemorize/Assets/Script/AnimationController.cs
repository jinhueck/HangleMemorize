using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public AnimationState state;
    public Animator animator;

    public string animName_TurnFront;
    public string animName_TurnBack;
    public string animName_Success;
    public string animName_Entry;
    public string animName_Fail;

    private bool bDelay = false;
    private bool bAnimPlay = false;
    private float delayTime = 0f;
    private float animPlayTime = 0f;
    private float presentTime = 0f;

    public void PlayAnimation(AnimationState _state)
    {
        state = _state;
        delayTime = GetDelayTime(_state);
        animPlayTime = GetAnimationLength(_state);
        presentTime = 0f;
        bDelay = true;
        bAnimPlay = true;
    }

    public float GetDelayTime(AnimationState _state)
    {
        float returnValue = 0f;
        switch(_state)
        {
            case AnimationState.Anim_TurnFront:
                break;
            case AnimationState.Anim_TurnBack:
                break;
            case AnimationState.Anim_Success:
                break;
            case AnimationState.Anim_Entry:
                returnValue = 2f;
                break;
            case AnimationState.Anim_Fail:
                break;
        }
        return returnValue;
    }

    public float GetAnimationLength(AnimationState _state)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        float animationLength = 0f;
        string animName = GetAnimationName(_state);
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == animName)
            {
                animationLength = clip.length;
            }
        }
        return animationLength;
    }

    public string GetAnimationName(AnimationState _state)
    {
        switch (_state)
        {
            case AnimationState.Anim_TurnFront:
                return animName_TurnFront;
            case AnimationState.Anim_TurnBack:
                return animName_TurnBack;
            case AnimationState.Anim_Success:
                return animName_Success;
            case AnimationState.Anim_Entry:
                return animName_Entry;
            case AnimationState.Anim_Fail:
                return animName_TurnFront;
            default:
                return null;
        }
    }

    private void Update()
    {
        if(bAnimPlay == false)
        {
            return;
        }

        if(bDelay == true && presentTime >= delayTime)
        {
            bDelay = false;
            presentTime = 0f;
            string animName = GetAnimationName(state);
            animator.Play(animName, 0, 0);
            return;
        }
        else if(bDelay == true && presentTime < delayTime)
        {
            presentTime += Time.deltaTime;
            return;
        }

        if(presentTime < animPlayTime)
        {
            presentTime += Time.deltaTime;
            return;
        }
        else
        {
            state = AnimationState.Anim_None;
            presentTime = 0f;
            bAnimPlay = false;
        }
    }
}

public enum AnimationState
{
    Anim_None,
    Anim_TurnFront,
    Anim_TurnBack,
    Anim_Success,
    Anim_Entry,
    Anim_Fail,
}
