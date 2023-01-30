using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public enum Theme {
    Green, Yellow, White
}

public class GameManager : MonoSingleton<GameManager> {

    public static float TimeScale { get; set; }

    public static int money = 1000;

    public static bool touch;
    public Vector3 Axis;

    public Theme _planetTheme;

    [SerializeField] Text _moneyText;

    private void Start()
    {
        TimeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        _moneyText.text = money.ToString();

        touch = false;
    }

}
