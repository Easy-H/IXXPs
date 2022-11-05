using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour {

    public static UiManager Instance { get; private set; }
    
    [SerializeField] UiAnimationGenerator[] actions = null;

    [SerializeField] Image[] panels;

    [SerializeField] Image _messageBox;
    [SerializeField] Text _message;

    public void ClosePanel(int i)
    {
        panels[i].gameObject.SetActive(false);

    }

    public void PrintMessage(string message)
    {
        _messageBox.gameObject.SetActive(true);
        _message.text = message;

        Invoke("PopMessage", 1f);

    }


    void PopMessage()
    {
        _messageBox.gameObject.SetActive(false);

    }

    public void OpenPanel(int i)
    {
        panels[i].gameObject.SetActive(true);
    }

    private void Start() {
        Instance = this;
    }

    public void StartAnimation(int i) {
        actions[i].Action();
    }

    public void StartAnimation(string aniName) {

        for (int i = 0; i < actions.Length; i++) {

            if (actions[i].actionName == aniName) {
                actions[i].Action();
                break;
            }

        }

    }

}
