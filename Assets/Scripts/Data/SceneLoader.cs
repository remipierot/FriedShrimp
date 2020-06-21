using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    public Transform ProgressBar;
    public GameObject Panel;

    private Vector3 _BarScale = Vector3.one;

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

        DisablePanel();
    }

    void DisablePanel()
	{
        Panel.SetActive(false);
	}

    void UpdateBar(float value)
	{
        _BarScale.x = value;
        ProgressBar.localScale = _BarScale;
	}

    IEnumerator LoadScene(string sceneName)
	{
        Panel.SetActive(true);

        AsyncOperation asyncLoading = SceneManager.LoadSceneAsync(sceneName);

        while(!asyncLoading.isDone)
		{
            UpdateBar(Mathf.Clamp01(asyncLoading.progress / .9f));

            yield return null;
		}

        DisablePanel();
	}

    public void ChangeScene(string sceneName)
	{
        StartCoroutine(LoadScene(sceneName));
	}
}
