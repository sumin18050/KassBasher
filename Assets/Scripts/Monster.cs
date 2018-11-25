using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    enum State
    {
        Idle,
        Drop,
        Fly,
        Die
    }

    enum AnimTrigger
    {
        Jump,
        Around,
        Fly,
        Die
    }

    Rigidbody2D _rigidbody2D;
    Animator animator;
    State state;
    UnityEngine.Object original;
    AudioSource audioSource;
    Dictionary<string, AudioClip> audioDict = new Dictionary<string, AudioClip>() ;

    public Vector2 angle;
    public float force;


    // Use this for initialization
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = 0;
        animator = GetComponent<Animator>();
        original = Resources.Load("Prefabs/Dirt");
        audioSource = GetComponent<AudioSource>();

        
        audioDict["bounce"] = Resources.Load<AudioClip>("Audios/bounce");
        audioDict["whistleUp"] = Resources.Load<AudioClip>("Audios/whistleUp");
        audioDict["swing"] = Resources.Load<AudioClip>("Audios/swing");
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                if (Input.GetMouseButtonDown(0))
                {
                    animator.SetTrigger(AnimTrigger.Jump.ToString());
                    Invoke("Jump",0.2f);
                }
                break;
            case State.Drop:
                break;
            case State.Fly:
                if (_rigidbody2D.velocity == Vector2.zero)
                {
                    state = State.Die;
                    GameManager.Instanse.CallAssistant(transform.position);
                    animator.SetTrigger(AnimTrigger.Die.ToString());
                }
                break;
            case State.Die:
                break;
            default:
                break;
        }
    }

    public void GetHit()
    {
        if (state == State.Drop)
        {
            if (transform.position.y < 1f && transform.position.y > -0.2f)
            {
                Player player = GameManager.Instanse.player;
                player.PlayAnimation(Player.AnimatorTrigerPlayer.Hit);
                GetForce(new Vector2(-angle.x, transform.position.y + angle.y), force);
                animator.SetTrigger(AnimTrigger.Fly.ToString());
                
                audioSource.clip = audioDict["swing"];
                audioSource.Play();

                Invoke("PlayWhistleUp",0.2f);
            }
            state = State.Fly;
        }
    }

    public void PlayWhistleUp()
    {
        audioSource.clip = audioDict["whistleUp"];
        audioSource.Play();
    }

    void GetForce(Vector2 direct, float n)
    {
        direct.Normalize();
        Vector2 force = direct * n;
        _rigidbody2D.AddForce(force);
    }

    void Jump()
    {
        _rigidbody2D.AddForce(Vector3.up * 200);
        state = State.Drop;
        _rigidbody2D.gravityScale = 1f;
        audioSource.clip = audioDict["bounce"];
        audioSource.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.clip = audioDict["bounce"];
        audioSource.Play();
        HitGround(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        HitGround(collision);
    }

    private void HitGround(Collision2D collision)
    {
        if (state == State.Fly)
        {
            

            if (collision.transform.name == "Ground")
            {
                Vector3 birthPos = new Vector3(transform.position.x, collision.contacts[0].point.y, 0);
                
                Instantiate(original, birthPos, Quaternion.identity);
            }
        }
    }
}
