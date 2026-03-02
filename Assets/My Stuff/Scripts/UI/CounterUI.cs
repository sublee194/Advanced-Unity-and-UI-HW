using TMPro;
using UnityEngine;

public class CounterUI : MonoBehaviour
{
    public TextMeshProUGUI totalSpiderCounterText;
    public TextMeshProUGUI killedSpiderCounterText;
    private int totalCount = 0;
    private int killedCount = 0;

    void Start()
    {
        UpdateUI();
    }

    public void AddTotalCount(int amount)
    {
        totalCount += amount;
        UpdateUI();
    }

    public void MinusTotalCount(int amount)
    {
        totalCount -= amount;
        UpdateUI();
    }

    public void AddKilledCount(int amount)
    {
        killedCount += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        totalSpiderCounterText.text = totalCount.ToString();
        killedSpiderCounterText.text= killedCount.ToString();
    }
}
