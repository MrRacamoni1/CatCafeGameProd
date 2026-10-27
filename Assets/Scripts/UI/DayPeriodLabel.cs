using UnityEngine;
using TMPro;

public class DayPeriodLabel : MonoBehaviour
{
    [SerializeField] UniversalTimer universalTimer;
    [SerializeField] TMP_Text timeLabel;

    void Update()
    {
        UniversalTimer.DayPeriod period = universalTimer.GetCurrentPeriod();

        timeLabel.text = period.ToString();
        timeLabel.color = universalTimer.GetPeriodColor(period);
    }
}