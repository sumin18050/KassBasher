using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assistant : MonoBehaviour
{
    enum State
    {
        Idle,
        Move,
        Arrive,
        Done
    }

    State state;

    public float speed;
    public float speedFlagAngle;
    public float distance;


    Transform flag;
    TextMesh textMesh;

    private void Start()
    {
        flag = transform.Find("Flag");
        flag.rotation = Quaternion.Euler(0, 0, 25);
        textMesh = flag.GetComponentInChildren<TextMesh>();
    }

    void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Move:
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                Monster monster = GameManager.Instanse.monster;
                if (transform.position.x >= monster.transform.position.x - distance)
                {
                    state = State.Arrive;
                }
                break;
            case State.Arrive:
                textMesh.text = ((int)(-GameManager.Instanse.monster.transform.position.x * 6.25f)).ToString();
                if (flag.eulerAngles.z < 90)
                {
                    flag.Rotate(0, 0, -speedFlagAngle * Time.deltaTime);
                }
                else
                {
                    GameManager.Instanse.dlgEnd.gameObject.SetActive(true);
                    state = State.Done;
                }
                break;
            case State.Done:
                break;
            default:
                break;
        }
    }

    internal void Alive(Vector3 pos)
    {
        state = State.Move;
        transform.position = pos;
    }
}
