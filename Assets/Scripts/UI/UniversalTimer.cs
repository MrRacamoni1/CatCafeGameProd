using UnityEngine;

public class UniversalTimer : MonoBehaviour
{
    public enum DayPeriod
    {
        Morning,
        Noon,
        Afternoon,
        Evening,
        Closed
    }

    public float dayLengthInSeconds = 300f;
    public float timePassed = 0f;

    void Update()
    {
        timePassed += Time.deltaTime;

        if (timePassed > dayLengthInSeconds)
        {
            timePassed = dayLengthInSeconds;
        }
    }

     public DayPeriod GetCurrentPeriod()
    {
        float progress = timePassed / dayLengthInSeconds;

        if (progress < 0.2f) return DayPeriod.Morning;
        if (progress < 0.4f) return DayPeriod.Noon;
        if (progress < 0.6f) return DayPeriod.Afternoon;
        if (progress < 0.8f) return DayPeriod.Evening;
        return DayPeriod.Closed;
    }

    public Color GetPeriodColor(DayPeriod period)
    {
        switch (period)
        {
            case DayPeriod.Morning: return new Color(1f, 0.9f, 0.6f); //Softish Yellow 
            case DayPeriod.Noon: return new Color(1f, 1f, 0.85f); //Bright Orange
            case DayPeriod.Afternoon: return new Color(1f, 0.7f, 0.4f); //Warm, Calm Orange
            case DayPeriod.Evening: return new Color(0.6f, 0.45f, 0.8f); //Cool Purple
            case DayPeriod.Closed: return new Color(0.7f, 0.07f, 0.07f); //Red - Meant to mimic a closed/stop sign
            default: return Color.white;
        }
    }
}
