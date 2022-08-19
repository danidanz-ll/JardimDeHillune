using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TMP_Text gameOverText;
    public bool IsGameOver { get; private set; } = false;
    
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
            if (gameOverText != null)
            {
                gameOverText.text = "Você perdeu!";
            }
            Debug.Log("Game Over!");
            IsGameOver = true;
        }
    }
}
