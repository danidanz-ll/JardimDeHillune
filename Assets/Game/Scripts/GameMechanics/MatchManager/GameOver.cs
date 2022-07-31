using UnityEngine;

public class GameOver : MonoBehaviour
{
    private GameObject Player;
    private GameObject Objective;
    private LifeSystem PlayerLife;
    private LifeSystem ObjectiveLife;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Objective = GameObject.FindGameObjectWithTag("Objective");
        PlayerLife = Player.GetComponent<LifeSystem>();
        ObjectiveLife = Objective.GetComponent<LifeSystem>();
    }
    private void Update()
    {
        if (PlayerLife.currentLife <= 0 || ObjectiveLife.currentLife <= 0)
        {
            Debug.Log("Game Over!");
        }
    }
}
