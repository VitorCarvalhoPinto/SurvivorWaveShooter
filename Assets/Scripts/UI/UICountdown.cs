using TMPro;
using UnityEngine;

public class UICountDown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;

    public void UpdateCountDown(float secondsLeft)
    {
        countDownText.text = secondsLeft > 0f ? secondsLeft.ToString("F1") : "SOBREVIVA!";
    }
}
