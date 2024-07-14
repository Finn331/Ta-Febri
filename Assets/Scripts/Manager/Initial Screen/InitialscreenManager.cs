using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float startYMini = 900f;
    private float endYMini = 247.5f;
    private float endYZoo = 50f;
    private float startYSponsor = -590f;
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
        sequence.append(LeanTween.moveY(logoM, endYMini, animationDuration).setEase(LeanTweenType.easeInOutQuad));
        sequence.append(LeanTween.moveY(logoI, endYMini, animationDuration).setEase(LeanTweenType.easeInOutQuad));
        sequence.append(LeanTween.moveY(logoN, endYMini, animationDuration).setEase(LeanTweenType.easeInOutQuad));
        sequence.append(LeanTween.moveY(logoI2, endYMini, animationDuration).setEase(LeanTweenType.easeInOutQuad));

        // Animate "Zoo"
        sequence.append(LeanTween.moveY(logoZ, endYZoo, animationDuration).setEase(LeanTweenType.easeInOutQuad));
        sequence.append(LeanTween.moveY(logoO, endYZoo, animationDuration).setEase(LeanTweenType.easeInOutQuad));
        sequence.append(LeanTween.moveY(logoO2, endYZoo, animationDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => {
            logoO2Anim.SetTrigger("isCracked");
        }));

        // Animate Sponsor logos
        sequence.append(LeanTween.moveY(logoRagunan, endYSponsor, animationDuration).setEase(LeanTweenType.easeInOutQuad));
        sequence.append(LeanTween.moveY(logoTripledot, endYSponsor, animationDuration).setEase(LeanTweenType.easeInOutQuad));
        sequence.append(LeanTween.moveY(logoDkv, endYSponsor, animationDuration).setEase(LeanTweenType.easeInOutQuad));

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
                    LeanTween.move(logoMiniZooRect, new Vector3(677, 318.31f, 0), combinedLogoAnimationDuration).setEase(LeanTweenType.easeInOutQuad);
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
        CanvasGroup canvasGroup = logo.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = logo.gameObject.AddComponent<CanvasGroup>();
        }
        LeanTween.alphaCanvas(canvasGroup, 0, fadeDuration);
    }

    private void DeactivateLogo(RectTransform logo)
    {
        logo.gameObject.SetActive(false);
    }
}
