using UnityEngine;

public class ObeliscController : MonoBehaviour
{
    [HideInInspector]
    public static GameObject Instance { get; private set; }

    private LifeSystem lifeSystem;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }

        lifeSystem = GetComponent<LifeSystem>();

        if (Settings.GetUserSettings())
        {
            lifeSystem.maxLife = SettingsAllies.GetLife("Obelisc");
            lifeSystem.currentLife = SettingsAllies.GetLife("Obelisc");
        }
    }
}
