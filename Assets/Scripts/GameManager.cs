using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private UIKillCounter uiKillCounter;

    public int killCount { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void AddKill()
    {
        killCount++;
        uiKillCounter.UpdateKillCounter();
    }

    public int GetKillAmount()
    {
        return killCount;
    }
}
