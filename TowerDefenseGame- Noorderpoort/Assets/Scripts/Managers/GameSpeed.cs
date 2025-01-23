using UnityEngine;
using UnityEngine.UI;

public class GameSpeed : MonoBehaviour
{
    [SerializeField] Image[] buttons;
    [SerializeField] Color activeColor;
    [SerializeField] Color inactiveColor;
    void Start()
    {
        //Defaults to 1x speed
        foreach(var button in buttons)
        {
            button.color = inactiveColor;
        }
        buttons[0].color = activeColor;
    }

    public void ChangeSpeed(float amount)
    {
        Time.timeScale = amount;

        //Makes all buttons gray
        foreach (var button in buttons)
        {
            button.color = inactiveColor;
        }

        //Horrible way of making selected button blueishgreen
        switch (amount)
        {
            case 1:
                buttons[0].color = activeColor;
                break;
            case 2:
                buttons[1].color = activeColor;
                break;
            case 3:
                buttons[2].color = activeColor;
                break;
        }
    }
}
