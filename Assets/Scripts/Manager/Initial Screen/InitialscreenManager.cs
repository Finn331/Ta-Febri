using System.Collections;
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
    [SerializeField] private RectTransform logoEU;
    [SerializeField] private RectTransform logoDkv;
    [SerializeField] private RectTransform logoTripledot;

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
    private float endYMini = 273f;
    private float endYZoo = 112f;
    private float startXSponsor = -1700f;
    private float animationDuration = 0.9f; // Duration of the animation in seconds
    private float fadeDuration = 0.5f; // Duration of the fade out in seconds
    private float combinedLogoAnimationDuration = 1.5f; // Duration of the combined logo animation
    private float delayBeforeDeactivation = 1f; // Delay before deactivating "Mini" and "Zoo" logos

    // Initial X positions for sponsor logos
    private float startXLogoRagunan = -1700f; // Default start position for logoRagunan
    private float startXLogoEU = -373f; // Initial position for logoEU
    private float startXLogoDkv = 17f; // Initial position for logoDkv
    private float startXLogoTripledot = 234.3f; // Initial position for logoTripledot

    // Final X positions for sponsor logos
    private float endXLogoRagunan = -373.3f;
    private float endXLogoEU = -156f;
    private float endXLogoDkv = 234.3f;
    private float endXLogoTripledot = 476.7f;

    // Start is called before the first frame update
    void Start()
    {
        // Set initial positions for Mini and Zoo logos
        SetInitialPositionY(logoM, startYMini);
        SetInitialPositionY(logoI, startYMini);
        SetInitialPositionY(logoN, startYMini);
        SetInitialPositionY(logoI2, startYMini);
        SetInitialPositionY(logoZ, startYMini);
        SetInitialPositionY(logoO, startYMini);
        SetInitialPositionY(logoO2, startYMini);

        // Set initial positions for Sponsor logos and set alpha to 0
        SetInitialPositionX(logoRagunan, startXLogoRagunan);
        SetInitialPositionX(logoEU, startXLogoEU);
        SetInitialPositionX(logoDkv, startXLogoDkv);
        SetInitialPositionX(logoTripledot, startXLogoTripledot);

        // Start animation sequence
        AnimateLogos();
    }

    private void AnimateLogos()
    {
        // Create a sequence for the animation
        LTSeq sequence = LeanTween.sequence();

        // Animate "Mini" logos
        AnimateMiniLogos(sequence);

        // Animate "Zoo" logos
        AnimateZooLogos(sequence);

        // Animate Sponsor logos after "Mini" and "Zoo" logos
        sequence.append(() =>
        {
            AnimateSponsorLogos(sequence);
        });

        // Activate combined logo and fade out Sponsor logos
        sequence.append(() =>
        {
            ActivateCombinedLogo(sequence);
        });
    }

    private void AnimateMiniLogos(LTSeq sequence)
    {
        sequence.append(LeanTween.moveY(logoM, endYMini, animationDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => PlayLogoPop()));
        sequence.append(LeanTween.moveY(logoI, endYMini, animationDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => PlayLogoPop()));
        sequence.append(LeanTween.moveY(logoN, endYMini, animationDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => PlayLogoPop()));
        sequence.append(LeanTween.moveY(logoI2, endYMini, animationDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => PlayLogoPop()));
    }

    private void AnimateZooLogos(LTSeq sequence)
    {
        sequence.append(LeanTween.moveY(logoZ, endYZoo, animationDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => PlayLogoPop()));
        sequence.append(LeanTween.moveY(logoO, endYZoo, animationDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => PlayLogoPop()));
        sequence.append(LeanTween.moveY(logoO2, endYZoo, animationDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            PlayLogoPop();
            logoO2Anim.SetTrigger("isCracked");
        }));
    }

    private void AnimateSponsorLogos(LTSeq sequence)
    {
        // Animate logoRagunan
        LTDescr ragunanTween = LeanTween.moveX(logoRagunan, endXLogoRagunan, animationDuration)
            .setEase(LeanTweenType.easeInOutQuad)
            .setOnComplete(() =>
            {
                PlayLogoPop2();

                // After logoRagunan is done, start animating the rest of the sponsor logos
                AnimateNextSponsorLogo(logoEU, endXLogoEU, () =>
                {
                    AnimateNextSponsorLogo(logoDkv, endXLogoDkv, () =>
                    {
                        AnimateNextSponsorLogo(logoTripledot, endXLogoTripledot, PlayLogoPop2);
                    });
                });
            });
    }

    private void AnimateNextSponsorLogo(RectTransform logo, float endX, System.Action onComplete)
    {
        LTDescr tween = LeanTween.moveX(logo, endX, animationDuration)
            .setEase(LeanTweenType.easeInOutQuad)
            .setOnStart(() => FadeInLogo(logo))
            .setOnComplete(() =>
            {
                PlayLogoPop2();
                onComplete?.Invoke();
            });
    }

    private void FadeInLogo(RectTransform logo)
    {
        // Set alpha to 0 and then fade in
        logo.gameObject.SetActive(true);
        LeanTween.alpha(logo, 0f, 0f);
        LeanTween.alpha(logo, 1f, fadeDuration);
    }

    private void ActivateCombinedLogo(LTSeq sequence)
    {
        // Waiting for all sponsor logos to end
        sequence.append(() => StartCoroutine(WaitForSponsorLogosToEnd(() =>
        {
            // Fade out Sponsor logos
            FadeOutSponsorLogos();

            // Delay before deactivating "Mini" and "Zoo" logos
            LeanTween.delayedCall(delayBeforeDeactivation, () =>
            {
                // Fade out "Mini" and "Zoo" logos
                FadeOutMiniZooLogos();

                // Activate combined logo after fading out
                ActivateCombinedLogoAfterFade();
            });
        })));
    }

    private IEnumerator WaitForSponsorLogosToEnd(System.Action onComplete)
    {
        // Wait until all Sponsor logos' animations are completed
        yield return new WaitForSeconds(animationDuration); // Adjust the wait time as needed

        // Callback when all logos are finished
        onComplete?.Invoke();
    }

    private void FadeOutSponsorLogos()
    {
        LeanTween.alpha(logoRagunan, 0, fadeDuration).setOnComplete(() => logoRagunan.gameObject.SetActive(false));
        LeanTween.alpha(logoEU, 0, fadeDuration).setOnComplete(() => logoEU.gameObject.SetActive(false));
        LeanTween.alpha(logoDkv, 0, fadeDuration).setOnComplete(() => logoDkv.gameObject.SetActive(false));
        LeanTween.alpha(logoTripledot, 0, fadeDuration).setOnComplete(() => logoTripledot.gameObject.SetActive(false));
    }

    private void FadeOutMiniZooLogos()
    {
        LeanTween.alpha(logoM, 0, fadeDuration).setOnComplete(() => DeactivateLogo(logoM));
        LeanTween.alpha(logoI, 0, fadeDuration).setOnComplete(() => DeactivateLogo(logoI));
        LeanTween.alpha(logoN, 0, fadeDuration).setOnComplete(() => DeactivateLogo(logoN));
        LeanTween.alpha(logoI2, 0, fadeDuration).setOnComplete(() => DeactivateLogo(logoI2));
        LeanTween.alpha(logoZ, 0, fadeDuration).setOnComplete(() => DeactivateLogo(logoZ));
        LeanTween.alpha(logoO, 0, fadeDuration).setOnComplete(() => DeactivateLogo(logoO));
        LeanTween.alpha(logoO2, 0, fadeDuration).setOnComplete(() => DeactivateLogo(logoO2));
    }

    private void ActivateCombinedLogoAfterFade()
    {
        // Activate and animate combined logo
        logoMiniZoo.SetActive(true);
        RectTransform logoMiniZooRect = logoMiniZoo.GetComponent<RectTransform>();
        LeanTween.scale(logoMiniZoo, new Vector3(0.3f, 0.3f, 0.3f), combinedLogoAnimationDuration).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.move(logoMiniZooRect, new Vector3(677, 318.31f, 0), combinedLogoAnimationDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            // Activate header image with fade in
            headerImage.SetActive(true);
            SetAlpha(headerImage, 0f); // Ensure headerImage starts fully transparent
            LeanTween.alpha(headerImage, 1f, fadeDuration).setOnComplete(() =>
            {
                // Delay before deactivating header image
                LeanTween.delayedCall(1f, () =>
                {
                    LeanTween.alpha(headerImage, 0f, fadeDuration).setOnComplete(() =>
                    {
                        headerImage.SetActive(false);

                        // Change logoMiniZoo and its children color to black
                        ChangeColorToBlack(logoMiniZoo);

                        // Fade out Initial Background and activate Background
                        FadeOutInitialBackgroundAndActivate();
                    });
                });
            });
        });
    }

    private void FadeOutInitialBackgroundAndActivate()
    {
        LeanTween.alpha(initialBackground, 0f, fadeDuration).setOnComplete(() =>
        {
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
    }

    private void SetInitialPositionY(RectTransform logo, float startY)
    {
        Vector2 startPos = logo.anchoredPosition;
        startPos.y = startY;
        logo.anchoredPosition = startPos;
    }

    private void SetInitialPositionX(RectTransform logo, float startX)
    {
        Vector2 startPos = logo.anchoredPosition;
        startPos.x = startX;
        logo.anchoredPosition = startPos;
    }

    private void SetInitialAlpha(RectTransform logo)
    {
        // Set initial alpha to 0
        LeanTween.alpha(logo, 0f, 0f);
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
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = alpha;
        }
    }

    private void ChangeColorToBlack(GameObject obj)
    {
        // Change the main GameObject's color to black
        Image image = obj.GetComponent<Image>();
        if (image != null)
        {
            image.color = Color.black;
        }

        // Change color of all Image components in children GameObjects
        Image[] images = obj.GetComponentsInChildren<Image>();
        foreach (Image img in images)
        {
            img.color = Color.black;
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
