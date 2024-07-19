using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuzzleManager13 : MonoBehaviour
{
    public static PuzzleManager13 Instance;

    [Header("Integer & Boolean Checking")]
    public int totalPieces = 12; // Jumlah total puzzle pieces yang harus tersnap
    public int snappedPieces = 0; // Counter untuk puzzle pieces yang sudah tersnap
    public bool levelFinished; // Status apakah level sudah selesai

    [Header("GameObject")]
    public GameObject pict; // GameObject yang akan diaktifkan
    public GameObject titleHolder;
    public GameObject buttonPrev;
    public GameObject buttonNext;
    public GameObject level;

    [Header("Level Manager")]
    public GameObject containerTextSelamatHolder;
    public GameObject NextButton;
    public Button NextButtons;
    public GameObject PrevButton;

    [Header("Audio Setting")]
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject settingHolder;
    [SerializeField] private float delaySetting;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void OpenSettingPanel()
    {
        settingPanel.SetActive(true);
        settingHolder.SetActive(true);
        LeanTween.scale(settingHolder, new Vector3(1, 1, 1), delaySetting).setEase(LeanTweenType.easeOutBounce);
    }

    public void CloseSettingPanel()
    {
        LeanTween.scale(settingHolder, new Vector3(0, 0, 0), delaySetting).setEase(LeanTweenType.easeOutBounce).setOnComplete(() =>
        {
            settingPanel.SetActive(false);
            settingHolder.SetActive(false);
        });
    }

    public void CheckAllSnapped()
    {
        snappedPieces++;
        if (snappedPieces >= totalPieces)
        {
            ActivatePict();
            // rewardClaimed = true;  // Set reward sudah di-claimed menjadi true
            // levelFinished = true;

        }
    }

    private void ActivatePict()
    {
        if (pict != null)
        {
            // Mengaktifkan GameObject pict
            pict.SetActive(true);

            // Mengatur RectTransform pict
            RectTransform rectTransform = pict.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                // Memainkan animasi pict menggunakan LeanTween
                LeanTween.move(rectTransform, new Vector2(398f, -435f), 2f).setEase(LeanTweenType.easeInOutQuad);
                FadeOutChildren(level);
                LeanTween.size(rectTransform, new Vector2(580f, 470f), 2f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
                {
                    // Menunggu 2 detik setelah animasi selesai
                    LeanTween.delayedCall(2f, () =>
                    {
                        ToTextSelamat();
                        FadeOutObject(titleHolder);
                        FadeOutObject(pict);
                    });
                });
            }
        }
    }

    private void FadeOutObject(GameObject obj, System.Action onComplete = null)
    {
        if (obj != null)
        {
            CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = obj.AddComponent<CanvasGroup>();
            }
            canvasGroup.alpha = 1f; // Set alpha ke 1 untuk memulai fade out
            LeanTween.alphaCanvas(canvasGroup, 0f, 1f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
            {
                obj.SetActive(false); // Menonaktifkan objek setelah fade out selesai
                onComplete?.Invoke();
            });
        }
    }

    private void FadeOutChildren(GameObject parentObject, System.Action onComplete = null)
    {
        if (parentObject != null)
        {
            // Ambil semua child dari parentObject
            foreach (Transform child in parentObject.transform)
            {
                SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    // Fading out child menggunakan LeanTween
                    Color color = spriteRenderer.color;
                    LeanTween.value(child.gameObject, color.a, 0f, 2f).setEase(LeanTweenType.easeInOutQuad).setOnUpdate((float val) =>
                    {
                        color.a = val;
                        spriteRenderer.color = color;
                    }).setOnComplete(() =>
                    {
                        child.gameObject.SetActive(false); // Menonaktifkan child setelah fade out selesai
                        onComplete?.Invoke();
                    });
                }
            }
        }
    }


    public void ToTextSelamat()
    {
        containerTextSelamatHolder.SetActive(true);
        RectTransform transformSelamat = containerTextSelamatHolder.GetComponent<RectTransform>();
        if (transformSelamat == null)
        {
            transformSelamat = containerTextSelamatHolder.AddComponent<RectTransform>();
        }
        LeanTween.move(transformSelamat, new Vector3(-24.89f, 0f, 0f), 1f).setEase(LeanTweenType.easeInOutQuad);
        NextButton.SetActive(true);
        PrevButton.SetActive(true);
        CanvasGroup canvasNextButton = NextButton.GetComponent<CanvasGroup>();
        CanvasGroup canvasPrevButton = PrevButton.GetComponent<CanvasGroup>();
        if (canvasNextButton == null)
        {
            canvasNextButton = NextButton.AddComponent<CanvasGroup>();
        }
        if (canvasPrevButton == null)
        {
            canvasPrevButton = PrevButton.AddComponent<CanvasGroup>();
        }
        LeanTween.alphaCanvas(canvasNextButton, 1, 0.4f);
        LeanTween.alphaCanvas(canvasPrevButton, 1, 0.4f);
    }


    void SetAlpha(GameObject obj, float alpha)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = obj.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = alpha;
    }

    // Metode untuk restart level
    public void RestartLevel()
    {
        // Reset snappedPieces
        snappedPieces = 0;

        // Cek dan reset rewardClaimed dan levelFinished
        if (levelFinished)
        {
            levelFinished = false; // Set level sudah selesai menjadi false
        }

        // Load ulang scene saat ini
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
