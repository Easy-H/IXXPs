using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoSingleton<UIManager> {
    
    [SerializeField] UiAnimationGenerator[] actions = null;

    [SerializeField] Image[] panels;

    public void ClosePanel(int i)
    {
        panels[i].gameObject.SetActive(false);

    }

    public void PrintMessage(string key)
    {
        GUIMessageBox messageBox = AssetOpenManager.Import<GUIMessageBox>("Assets/Prefabs/MessageBox.prefab", "Canvas");
        messageBox.SetMessage(key);

    }

    public void OpenPanel(int i)
    {
        panels[i].gameObject.SetActive(true);
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
