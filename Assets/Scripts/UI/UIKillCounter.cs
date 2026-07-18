using TMPro;
using UnityEngine;

public class UIKillCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textKillCount;

    public void UpdateKillCounter()
    {
        textKillCount.text = GameManager.Instance.GetKillAmount().ToString();
    }
}
