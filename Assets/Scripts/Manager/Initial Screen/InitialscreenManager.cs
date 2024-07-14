using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialscreenManager : MonoBehaviour
{
    [Header("Logo Mini")]
    [SerializeField] private RectTransform logoM;
    [SerializeField] private RectTransform logoI;
    [SerializeField] private RectTransform logoN;
    [SerializeField] private RectTransform logoI2;

    [Header("Logo Zoo")]
    [SerializeField] private RectTransform logoZ;
    [SerializeField] private RectTransform logoO;
    [SerializeField] private RectTransform logoO2;
    [SerializeField] private Animator logoO2Anim;

    [Header("Logo Sponsor")]
    [SerializeField] private RectTransform logoRagunan;
    [SerializeField] private RectTransform logoTripledot;
    [SerializeField] private RectTransform logoDkv;

    [Header("Combined Logo")]
    [SerializeField] private GameObject logoMiniZoo;
    [SerializeField] private Image[] logoSatuan; // Array of Images to be set to black

    [Header("Header Before Loading")]
    [SerializeField] private GameObject headerImage;

    [Header("Backgrounds")]
    [SerializeField] private GameObject initialBackground;
    [SerializeField] private GameObject background;

    [Header("Loading UI")]
    [SerializeField] private Slider loadingSlider; // Reference to your loading slider UI

    [Header("Scene Name")]
    [SerializeField] private string sceneToLoad = "YourNextSceneNameHere"; // The name of the scene to load

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip logoPop;
    [SerializeField] private AudioClip logoPop2;

    private float startYMini = 900f;
    private float endYMini = 247.5f;
    private float endYZoo = 50f;
    private float startYSponsor = -900f;
    private float endYSponsor = -183f;
    private float animationDuration = 1f; // Durasi animasi dalam detik
    private float fadeDuration = 0.5f; // Durasi fade out dalam detik
    private float combinedLogoAnimationDuration = 1.5f; // Durasi animasi untuk logoMiniZoo
    private float delayBeforeDeactivation = 1f; // Delay sebelum menonaktifkan logo "Mini" dan "Zoo"

    // Start is called before the first frame update
    void Start()
    {
        AnimateLogos();
    }

    private void AnimateLogos()
    {
        // Set initial positions for Mini and Zoo logos
        SetInitialPosition(logoM, startYMini);
        SetInitialPosition(logoI, startYMini);
        SetInitialPosition(logoN, startYMini);
        SetInitialPosition(logoI2, startYMini);
        SetInitialPosition(logoZ, startYMini);
        SetInitialPosition(logoO, startYMini);
        SetInitialPosition(logoO2, startYMini);

        // Set initial positions for Sponsor logos
        SetInitialPosition(logoRagunan, startYSponsor);
        SetInitialPosition(logoTripledot, startYSponsor);
        SetInitialPosition(logoDkv, startYSponsor);

        // Create a sequence for the animation
        LTSeq sequence = LeanTween.sequence();

        // Animate "Mini"
        sequence.append(LeanTween.moveY(logoM, endYMini, animationDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => PlayLogoPop()));
        sequence.append(LeanTween.moveY(logoI, endYMini, animationDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => PlayLogoPop()));
        sequence.append(LeanTween.moveY(logoN, endYMini, animationDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => PlayLogoPop()));
        sequence.append(LeanTween.moveY(logoI2, endYMini, animationDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => PlayLogoPop()));

        // Animate "Zoo"
        sequence.append(LeanTween.moveY(logoZ, endYZoo, animationDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => PlayLogoPop()));
        sequence.append(LeanTween.moveY(logoO, endYZoo, animationDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => PlayLogoPop()));
        sequence.append(LeanTween.moveY(logoO2, endYZoo, animationDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => {
            PlayLogoPop();
            logoO2Anim.SetTrigger("isCracked");
        }));

        // Animate Sponsor logos
        sequence.append(LeanTween.moveY(logoRagunan, endYSponsor, animationDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => PlayLogoPop2()));
        sequence.append(LeanTween.moveY(logoTripledot, endYSponsor, animationDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => PlayLogoPop2()));
        sequence.append(LeanTween.moveY(logoDkv, endYSponsor, animationDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => PlayLogoPop2()));

        // Activate combined logo and fade out Sponsor logos
        sequence.append(() =>
        {
            LeanTween.alpha(logoRagunan, 0, fadeDuration).setOnComplete(() => logoRagunan.gameObject.SetActive(false));
            LeanTween.alpha(logoTripledot, 0, fadeDuration).setOnComplete(() => logoTripledot.gameObject.SetActive(false));
            LeanTween.alpha(logoDkv, 0, fadeDuration).setOnComplete(() => logoDkv.gameObject.SetActive(false));

            // Delay before deactivating "Mini" and "Zoo" logos
            LeanTween.delayedCall(delayBeforeDeactivation, () => {
                // Fade out "Mini" and "Zoo" logos
                FadeOutLogo(logoM);
                FadeOutLogo(logoI);
                FadeOutLogo(logoN);
                FadeOutLogo(logoI2);
                FadeOutLogo(logoZ);
                FadeOutLogo(logoO);
                FadeOutLogo(logoO2);

                // Activate combined logo after fading out
                LeanTween.delayedCall(fadeDuration, () => {
                    // Deactivate "Mini" and "Zoo" logos
                    DeactivateLogo(logoM);
                    DeactivateLogo(logoI);
                    DeactivateLogo(logoN);
                    DeactivateLogo(logoI2);
                    DeactivateLogo(logoZ);
                    DeactivateLogo(logoO);
                    DeactivateLogo(logoO2);

                    // Activate and animate combined logo
                    logoMiniZoo.SetActive(true);

                    RectTransform logoMiniZooRect = logoMiniZoo.GetComponent<RectTransform>();
                    LeanTween.scale(logoMiniZoo, new Vector3(0.3f, 0.3f, 0.3f), combinedLogoAnimationDuration).setEase(LeanTweenType.easeInOutQuad);
                    LeanTween.move(logoMiniZooRect, new Vector3(677, 318.31f, 0), combinedLogoAnimationDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => {
                        // Activate header image with fade in
                        headerImage.SetActive(true);
                        SetAlpha(headerImage, 0f); // Ensure headerImage starts fully transparent
                        LeanTween.alpha(headerImage, 1f, fadeDuration).setOnComplete(() =>
                        {
                            // Delay before deactivating header image
                            LeanTween.delayedCall(1f, () =>
                            {
                                LeanTween.alpha(headerImage, 0f, fadeDuration).setOnComplete(() => {
                                    headerImage.SetActive(false);

                                    // Change logoMiniZoo and its children color to black
                                    ChangeColorToBlack(logoMiniZoo);

                                    // Fade out Initial Background and activate Background
                                    LeanTween.alpha(initialBackground, 0f, fadeDuration).setOnComplete(() => {
                                        initialBackground.SetActive(false);
                                        background.SetActive(true);
                                        SetAlpha(background, 0f);
                                        LeanTween.alpha(background, 1f, fadeDuration).setOnComplete(() =>
                                        {
                                            // Start loading the next scene
                                            loadingSlider.gameObject.SetActive(true);
                                            StartCoroutine(LoadLevel(sceneToLoad));
                                        });
                                    });
                                });
                            });
                        });
                    });
                });
            });
        });
    }

    private void SetInitialPosition(RectTransform logo, float startY)
    {
        Vector2 startPos = logo.anchoredPosition;
        startPos.y = startY;
        logo.anchoredPosition = startPos;
    }

    private void FadeOutLogo(RectTransform logo)
    {
        LeanTween.alpha(logo, 0, fadeDuration).setOnComplete(() => logo.gameObject.SetActive(false));
    }

    private void DeactivateLogo(RectTransform logo)
    {
        logo.gameObject.SetActive(false);
    }

    private void SetAlpha(GameObject obj, float alpha)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Color color = sr.color;
            color.a = alpha;
            sr.color = color;
        }
    }

    private void ChangeColorToBlack(GameObject obj)
    {
        // Change the main GameObject's color to black
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = Color.black;
        }

        // Change color of all Image components in children GameObjects
        Image[] images = obj.GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            image.color = Color.black;
        }
    }

    private IEnumerator LoadLevel(string levelToLoad)
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

    private void PlayLogoPop()
    {
        if (audioSource != null && logoPop != null)
        {
            audioSource.PlayOneShot(logoPop);
        }
    }

    private void PlayLogoPop2()
    {
        if (audioSource != null && logoPop2 != null)
        {
            audioSource.PlayOneShot(logoPop2);
        }
    }
}
