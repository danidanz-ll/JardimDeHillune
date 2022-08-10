using System;
using UnityEngine;

public class SpawnerEvents : MonoBehaviour
{
    public event Action AllUnitsAreDead;
    public void WarnAllUnitsDied()
    {
        AllUnitsAreDead.Invoke();
    }
}
