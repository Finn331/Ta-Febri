using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainmenuManager : MonoBehaviour
{
    [Header("Mainmenu Audio")]
    [SerializeField] private AudioClip mainmenuSong;
    [SerializeField] private AudioClip clickSFX;

    [Header("Menu Screen")]
    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject menuCanvas;

    [Header("Slider")]
    [SerializeField] Slider loadingSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        AudioManager.instance.PlaySound(clickSFX);
    }

    public void Setting()
    {
        AudioManager.instance.PlaySound(clickSFX);
    }

    public void Information()
    {
        AudioManager.instance.PlaySound(clickSFX);
    }

    public void Quit()
    {
        AudioManager.instance.PlaySound(clickSFX);
        Application.Quit();
    }

    // Loading Mechanics
    public void LoadLevelBtn(string levelToLoad)
    {
        menuCanvas.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadLevel(levelToLoad));
    }

    IEnumerator LoadLevel(string levelToLoad)
    {
        AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(levelToLoad);
        Time.timeScale = 1;
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progress;

            yield return null;
        }
    }
}
