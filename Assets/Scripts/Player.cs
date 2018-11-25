using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    Animator animator;
    State state;

    public enum AnimatorTrigerPlayer
    {
        Hit
    }

    enum State
    {
        Idle,
        Ready,
        Done
    }
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                if (Input.GetMouseButtonDown(0))
                {
                    state = State.Ready;
                }
                break;
            case State.Ready:
                if (Input.GetMouseButtonDown(0))
                {
                    PlayAnimation(AnimatorTrigerPlayer.Hit);
                    Invoke("HitMonster", 0.02f);
                    state = State.Done;
                }
                break;
            case State.Done:
                if (Input.GetMouseButtonDown(0))
                {
                    PlayAnimation(AnimatorTrigerPlayer.Hit);
                    state = State.Done;
                }
                break;
            default:
                break;
        }
    }

    void HitMonster()
    {
        GameManager.Instanse.monster.GetHit();
    }

    public void PlayAnimation(AnimatorTrigerPlayer animatorTrigerPlayer)
    {
        animator.SetTrigger(animatorTrigerPlayer.ToString());
    }
}
