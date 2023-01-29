using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] 
    private CanvasGroup loadingOverlay;
    
    [SerializeField]
    [Range(0.01f, 3.0f)]
    private float FadeTime = 0.5f;

    private AsyncOperation asyncOperation;
    public static SceneLoader Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void LoadSceneAsync(string sceneName)
    {
        gameObject.SetActive(true);
        StartCoroutine(PerformLoadSceneAsync(sceneName));
        
    }
    private IEnumerator PerformLoadSceneAsync(string sceneName)
    {
        asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        yield return StartCoroutine(FadeIn());

        while (asyncOperation.isDone == false)
        {
            yield return null;
        }

        yield return StartCoroutine(FadeOut());
        gameObject.SetActive(false);
        //Debug.Log("Destroy");
        //Destroy(this.gameObject);
    }
    private IEnumerator FadeIn()
    {
        float start = 0;
        float end = 1;
        float speed = (end - start) / FadeTime;

        loadingOverlay.alpha = start;
        while (loadingOverlay.alpha < end)
        {
            loadingOverlay.alpha += asyncOperation.progress;
            yield return null;
        }
        loadingOverlay.alpha = end;
    }
    private IEnumerator FadeOut()
    {
        float start = 1;
        float end = 0;
        float speed = (end - start) / FadeTime;

        loadingOverlay.alpha = start;
        while (loadingOverlay.alpha > end)
        {
            loadingOverlay.alpha -= asyncOperation.progress;
            yield return null;
        }
        loadingOverlay.alpha = end;
    }
}
