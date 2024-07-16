using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    private int totalPieces = 12; // Jumlah total puzzle pieces yang harus tersnap
    private int snappedPieces = 0; // Counter untuk puzzle pieces yang sudah tersnap
    public bool rewardClaimed = false; // Status apakah reward sudah di-claimed
    public bool levelFinished = false; // Status apakah level sudah selesai
    public GameObject pict; // GameObject yang akan diaktifkan
    public GameObject deskripsiHolder;
    public GameObject titleHolder;
    public GameObject buttonPrev;
    public GameObject buttonNext;
    public GameObject rewardButton; // GameObject untuk reward button
    public GameObject level;
    public Button closeButton; // Tombol untuk menutup deskripsiHolder
    public GameObject rewardHolder;

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

        // Pastikan closeButton terhubung dan menambahkan listener untuk menutup deskripsiHolder
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseDeskripsiHolder);
        }
    }

    public void CheckAllSnapped()
    {
        snappedPieces++;
        if (snappedPieces >= totalPieces)
        {
            ActivatePict();
            rewardClaimed = true;  // Set reward sudah di-claimed menjadi true
            levelFinished = true;
        }
    }

    private void ActivatePict()
    {
        if (pict != null)
        {
            // Mengaktifkan GameObject pict
            pict.SetActive(true);

            // Mengatur RectTransform
            RectTransform rectTransform = pict.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                // Memainkan animasi menggunakan LeanTween
                LeanTween.move(rectTransform, new Vector2(398f, -435f), 2f).setEase(LeanTweenType.easeInOutQuad);
                FadeOutChildren(level);
                LeanTween.size(rectTransform, new Vector2(580f, 470f), 2f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
                {
                    // Menunggu 2 detik setelah animasi selesai
                    LeanTween.delayedCall(2f, () =>
                    {
                        // Melakukan fade in pada deskripsi holder
                        if (deskripsiHolder != null)
                        {
                            deskripsiHolder.SetActive(true);
                            rewardHolder.SetActive(true);
                            CanvasGroup canvasGroup = deskripsiHolder.GetComponent<CanvasGroup>();
                            CanvasGroup canvasGroup1 = rewardHolder.GetComponent<CanvasGroup>();
                            if (canvasGroup == null)
                            {
                                canvasGroup = deskripsiHolder.AddComponent<CanvasGroup>();
                                canvasGroup1 = rewardHolder.AddComponent<CanvasGroup>();
                            }
                            canvasGroup.alpha = 0f; // Set alpha ke 0 untuk memulai fade in
                            LeanTween.alphaCanvas(canvasGroup, 1f, 1f).setEase(LeanTweenType.easeInOutQuad); // Fade in dalam 1 detik
                            LeanTween.alphaCanvas(canvasGroup1, 1f, 1f).setEase(LeanTweenType.easeInOutQuad); // Fade in dalam 1 detik

                            FadeOutObject(titleHolder);
                            FadeOutObject(buttonNext);
                            FadeOutObject(buttonPrev);
                            FadeOutObject(pict);
                        }
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

    public void CloseDeskripsiHolder()
    {
        // Melakukan fade out pada deskripsiHolder
        if (deskripsiHolder != null)
        {
            FadeOutObject(deskripsiHolder, MoveRewardButton);
        }
    }

    private void MoveRewardButton()
    {
        if (rewardButton != null)
        {
            RectTransform rectTransform = rewardButton.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                LeanTween.move(rectTransform, new Vector3(-24.89f, 0f, 0f), 1f).setEase(LeanTweenType.easeInOutQuad);
            }
        }
    }

    public void PulseRewardButton()
    {
        if (rewardButton != null)
        {
            // Animasi pulse dengan menggunakan LeanTween
            LeanTween.scale(rewardButton, new Vector3(0.3f, 0.3f, 0.3f), 0.8f)
            .setEasePunch()
            .setLoopPingPong(1);
        }
    }

    // Metode untuk mengatur reward claimed menjadi true
    public void SetRewardClaimed(bool claimed)
    {
        rewardClaimed = claimed;
    }

    // Metode untuk mengatur level finished menjadi true
    public void SetLevelFinished(bool finished)
    {
        levelFinished = finished;
    }

    // Metode untuk restart level
    public void RestartLevel()
    {
        // Reset snappedPieces
        snappedPieces = 0;

        // Cek dan reset rewardClaimed dan levelFinished
        if (rewardClaimed && levelFinished)
        {
            rewardClaimed = true;  // Set reward sudah di-claimed menjadi true
            levelFinished = false; // Set level sudah selesai menjadi false
        }

        // Load ulang scene saat ini
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
