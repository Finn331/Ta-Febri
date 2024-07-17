using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainmenuManager : MonoBehaviour
{
    [Header("Setting Setup")]
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject settingHolder;
    [SerializeField] private float delaySetting;

    [Header("Information Setup")]
    [SerializeField] private GameObject informationPanel;
    [SerializeField] private GameObject informationHolder;
    [SerializeField] private float delayInformation;

    [Header("Mainmenu Audio")]
    [SerializeField] private AudioClip mainmenuSong;
    [SerializeField] private AudioClip clickSFX;

    [Header("UI Elements")]
    [SerializeField] private Image fadeOutPanel;
    [SerializeField] private float fadeDuration = 1.0f;

    private AudioSource audioSource; // AudioSource untuk memainkan mainmenuSong

    // Start is called before the first frame update
    void Start()
    {
        // Optionally, make sure the fadeOutPanel is fully transparent at the start
        if (fadeOutPanel != null)
        {
            Color color = fadeOutPanel.color;
            color.a = 0;
            fadeOutPanel.color = color;
            fadeOutPanel.gameObject.SetActive(false);
        }
        AudioManager.instance.PlayMusic(mainmenuSong, true);
    }

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Setup AudioSource and play mainmenuSong
        //audioSource = gameObject.AddComponent<AudioSource>();
        //audioSource.loop = true;
        //audioSource.clip = mainmenuSong;
        //audioSource.Play();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play()
    {
        AudioManager.instance.PlaySound(clickSFX);
        StartCoroutine(FadeOutAndLoadScene("SampleScene"));
    }

    public void Setting()
    {
        AudioManager.instance.PlaySound(clickSFX);
        settingPanel.SetActive(true);
        settingHolder.SetActive(true);
        LeanTween.scale(settingHolder, new Vector3(1, 1, 1), delaySetting).setEase(LeanTweenType.easeOutBounce);
    }

    public void SettingBack()
    {
        AudioManager.instance.PlaySound(clickSFX);
        settingHolder.SetActive(false);
        settingPanel.SetActive(false);
        settingHolder.transform.localScale = new Vector3(0, 0, 0);
    }

    public void Information()
    {
        AudioManager.instance.PlaySound(clickSFX);
        informationHolder.SetActive(true);
        informationPanel.SetActive(true);
        LeanTween.scale(informationHolder, new Vector3(1, 1, 1), delaySetting).setEase(LeanTweenType.easeOutBounce);
    }

    public void InformationBack()
    {
        AudioManager.instance.PlaySound(clickSFX);
        informationHolder.SetActive(false);
        informationPanel.SetActive(false);
        informationHolder.transform.localScale = new Vector3(0, 0, 0);
    }

    public void Quit()
    {
        AudioManager.instance.PlaySound(clickSFX);
        Application.Quit();
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        if (fadeOutPanel != null)
        {
            fadeOutPanel.gameObject.SetActive(true);

            float elapsedTime = 0f;
            Color color = fadeOutPanel.color;

            while (elapsedTime < fadeDuration)
            {
                color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
                fadeOutPanel.color = color;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            color.a = 1;
            fadeOutPanel.color = color;
        }

        SceneManager.LoadScene(sceneName);
    }

    // Clean up AudioSource
    private void OnDestroy()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            Destroy(audioSource);
        }
    }
}
