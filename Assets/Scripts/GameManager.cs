using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Assistant assistant;
    public Monster monster;
    public Player player;
    public DlgEnd dlgEnd;

    static GameManager instanse;
    public static GameManager Instanse {
        get {
            return instanse;
        }
    }

    private void Awake()
    {
        instanse = this;

        assistant = FindObjectOfType<Assistant>();
        monster = FindObjectOfType<Monster>();
        player = FindObjectOfType<Player>();
        dlgEnd = FindObjectOfType<DlgEnd>();
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CallAssistant(Vector3 callerPos)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Vector3.zero);
        assistant.Alive(new Vector3(pos.x + 1, callerPos.y, 0));
    }
}
