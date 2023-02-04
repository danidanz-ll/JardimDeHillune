using UnityEngine;
using UnityEngine.Events;


public class CameraShake : MonoBehaviour
{
    public UnityEvent shock;

    private void Start()
    {
        
        InvokeRepeating("ShockwaveEvent", 3.0f, 4.0f);
    }
    private void ShockwaveEvent()
    {
        shock.Invoke();
    }
}