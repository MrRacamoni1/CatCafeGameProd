using UnityEngine;
using UnityEngine.UI;

public class TimeProgressBar : MonoBehaviour
{
    [SerializeField] UniversalTimer universalTimer;
    [SerializeField] Slider slider;
    [SerializeField] Image fillImage;

    void Update()
    {
        UniversalTimer.DayPeriod period = universalTimer.GetCurrentPeriod();

        slider.value = universalTimer.timePassed / universalTimer.dayLengthInSeconds;
        fillImage.color = universalTimer.GetPeriodColor(period);
    }
}