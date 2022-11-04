using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour {

    public static UiManager Instance { get; private set; }
    
    [SerializeField] UiAnimationGenerator[] actions = null;

    [SerializeField] Image[] panels;

    public void ClosePanel(int i)
    {
        panels[i].gameObject.SetActive(false);

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
