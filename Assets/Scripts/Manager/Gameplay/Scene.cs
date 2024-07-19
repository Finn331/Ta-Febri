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
    [SerializeField] private AudioClip clickSFX;

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
        AudioManager.instance.PlaySound(clickSFX);
    }

    public void OnClickCloseButton()
    {
        LeanTween.moveLocalX(pauseMenu, pauseMenuOriginalPosition.x, 0.5f).setEase(LeanTweenType.easeInOutSine);
        dimmer.SetActive(false);
        AudioManager.instance.PlaySound(clickSFX);
        // penutup.SetActive(true);
    }

    public void HomeScene()
    {
       SceneManager.LoadScene("Mainmenu");
        AudioManager.instance.PlaySound(clickSFX);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
        AudioManager.instance.PlaySound(clickSFX);
    }

    public void BackScene()
    {
        SceneManager.LoadScene(sceneToBack);
        AudioManager.instance.PlaySound(clickSFX);
    }

    public void QuitGame()
    {
        Application.Quit();
        AudioManager.instance.PlaySound(clickSFX);
    }
}
