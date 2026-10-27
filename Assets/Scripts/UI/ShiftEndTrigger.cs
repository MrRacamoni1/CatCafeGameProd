using UnityEngine;

public class ShiftEndTrigger : MonoBehaviour
{
    ShiftEndZone parentZone;

    void Awake()
    {
        parentZone = GetComponentInParent<ShiftEndZone>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            parentZone.PlayerReachedExit();
        }
    }
}