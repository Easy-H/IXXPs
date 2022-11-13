using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMethod : MonoBehaviour {

    public void OpenPanel(int i)
    {
        UiManager.Instance.OpenPanel(i);
    }
    public void ClosePanel(int i) {
        UiManager.Instance.ClosePanel(i);
    }

    public void PrintMessage(string message) {
        UiManager.Instance.PrintMessage(message);
    }


    public void NotOnMain()
    {
        GameManager.Instance._onMain = false;
    }
    public void OnMain()
    {
        GameManager.Instance._onMain = true;
    }

    public void UndoBuy()
    {
        GameManager.Instance.UndoBuy();

    }

    public void EndEdit() {
        GameManager.Instance.EndEdit();
    }

    public void ResetScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoNextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoScene(int i) {
        SceneManager.LoadScene(i);
    }

    public void OpenWeb(string link) {
        Application.OpenURL(link);
    }


}
