using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public GameObject pauseMenu;
    public string sceneToLoad;
    public string sceneToBack;
    private Vector3 pauseMenuOriginalPosition;
    public GameObject dimmer;
    // public GameObject penutup;


    void Start()
    {
        // Menyimpan posisi asli dari pauseMenu
        pauseMenuOriginalPosition = pauseMenu.transform.localPosition;
    }

    public void OnClickPauseMenu()
    {
        LeanTween.moveLocalX(pauseMenu, -260f, 0.5f).setEase(LeanTweenType.easeInOutSine);
        dimmer.SetActive(true); // Mengaktifkan dimmer
        // penutup.SetActive(false);

        // Mengatur alpha dari 255 (fully visible) menjadi 200 menggunakan LeanTween
        LeanTween.alpha(dimmer.GetComponent<RectTransform>(), 0.78f, 0.5f).setEase(LeanTweenType.easeInOutSine); // 200 / 255 = 0.78f
    }

    public void OnClickCloseButton()
    {
        LeanTween.moveLocalX(pauseMenu, pauseMenuOriginalPosition.x, 0.5f).setEase(LeanTweenType.easeInOutSine);
        dimmer.SetActive(false);
        // penutup.SetActive(true);
    }

    public void HomeScene()
    {
       SceneManager.LoadScene("Mainmenu");
    }

    public void NextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void BackScene()
    {
        SceneManager.LoadScene(sceneToBack);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
