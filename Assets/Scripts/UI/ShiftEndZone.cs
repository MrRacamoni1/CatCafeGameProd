using UnityEngine;

public class ShiftEndZone : MonoBehaviour
{
    Collider[] zoneColliders;
    bool shiftEnded = false;

    void Awake()
    {
        zoneColliders = GetComponentsInChildren<Collider>();
        SetZonesEnabled(false);
    }

    public void Activate()
    {
        SetZonesEnabled(true);
    }

    void SetZonesEnabled(bool isEnabled)
    {
        foreach (Collider zone in zoneColliders)
        {
            zone.enabled = isEnabled;
        }
    }

    public void PlayerReachedExit()
    {
        if (shiftEnded) return;
        shiftEnded = true;

        TaskManager.Instance.CompleteStep(TaskStep.Exit);
        GameManager.Instance.EndGame();
    }
}