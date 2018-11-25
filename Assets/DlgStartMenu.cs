using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DlgStartMenu : MonoBehaviour {

    Button btnStartGameEasy;
    void Start()
    {
        btnStartGameEasy = transform.Find("BtnStartGameEasy").GetComponent<Button>();

        btnStartGameEasy.onClick.AddListener(() => {
            SceneManager.LoadScene("Play");
        });
    }
}
