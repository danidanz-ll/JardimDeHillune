using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameEvents))]
public class GameManager : MonoBehaviour
{
    [Header("Level stats")]
    [SerializeField] 
    private int Level;
    
    [SerializeField] 
    private int NextLevel;
    
    [SerializeField] 
    private float TimeToNextLevel;

    [SerializeField]
    private bool IsReloadLevel = false;

    private GameEvents gameEvents;

    private void Start()
    {
        gameEvents = GetComponent<GameEvents>();

        gameEvents.TimeOver += PerformPlayerWin;
        gameEvents.MatchWin += PerformPlayerWin;
        gameEvents.GameOver += PerformGameOver;

        if (!PlayerPrefs.HasKey("save_level"))
        {
            PlayerPrefs.SetInt("save_level", 1);
        }
    }
    private void PerformPlayerWin()
    {
        SaveProgress(NextLevel);
        if (IsReloadLevel)
        {
            StartCoroutine(ReloadLevel());
        }
        else
        {
            StartCoroutine(ReloadLevel());
        }
    }
    private void PerformGameOver()
    {
        SaveProgress(Level);
        StartCoroutine(ReloadLevel());
    }
    private void SaveProgress(int level)
    {
        PlayerPrefs.SetInt("save_level", level);
    }
    public IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(TimeToNextLevel);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(TimeToNextLevel);
        SceneManager.LoadScene("Level_" + NextLevel);
    }
}
