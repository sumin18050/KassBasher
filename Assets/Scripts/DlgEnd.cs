using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DlgEnd : MonoBehaviour {
    Button btnRestartGame;
    Button btnBackToMenu;
    Button btnSendScore;
    void Start () {
        btnRestartGame = transform.Find("BtnRestartGame").GetComponent<Button>();
        btnBackToMenu = transform.Find("BtnBackToMenu").GetComponent<Button>();
        btnSendScore = transform.Find("BtnSendScore").GetComponent<Button>();

        btnRestartGame.onClick.AddListener(()=> {
            SceneManager.LoadScene("Play");
        });
        btnBackToMenu.onClick.AddListener(() => {
            SceneManager.LoadScene("Menu");
        });

        gameObject.SetActive(false);
    }
}
