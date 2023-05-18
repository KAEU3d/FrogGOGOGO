using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance;
    private CanvasGroup canvasGroup;
    public float scaler;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this);
    }

    public void Transition(string sceneName)
    {
        Debug.Log("Transition to" + sceneName);
        Time.timeScale = 1;
        StartCoroutine(TransitionToScene(sceneName));
    }

    private IEnumerator TransitionToScene(string sceneName)
    {
        yield return FadeTo(1);
        yield return SceneManager.LoadSceneAsync(sceneName);
        yield return FadeTo(0);
    }

    private IEnumerator FadeTo(int amount)
    {
        canvasGroup.blocksRaycasts = true;
        while(canvasGroup.alpha != amount)
        {
            switch(amount)
            {
                case 0:
                    canvasGroup.alpha -= Time.deltaTime * scaler;
                    break;
                case 1:
                    canvasGroup.alpha += Time.deltaTime * scaler;
                    break;
            }
            yield return null;
        }
        canvasGroup.blocksRaycasts = false;
    }
}
