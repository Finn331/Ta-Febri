using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    private int totalPieces = 12; // Jumlah total puzzle pieces yang harus tersnap
    private int snappedPieces = 0; // Counter untuk puzzle pieces yang sudah tersnap
    public GameObject pict; // GameObject yang akan diaktifkan
    public GameObject deskripsiHolder;
    public GameObject titleHolder;
    public GameObject buttonHolder;

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

    public void CheckAllSnapped()
    {
        snappedPieces++;
        if (snappedPieces >= totalPieces)
        {
            ActivatePict();
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
                LeanTween.move(rectTransform, new Vector2(398f, 435f), 2f).setEase(LeanTweenType.easeInOutQuad);
                LeanTween.size(rectTransform, new Vector2(580f, 470f), 2f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
                {
                    // Menunggu 2 detik setelah animasi selesai
                    LeanTween.delayedCall(2f, () =>
                    {
                        // Melakukan fade in pada deskripsi holder
                        if (deskripsiHolder != null)
                        {
                            deskripsiHolder.SetActive(true);
                            CanvasGroup canvasGroup = deskripsiHolder.GetComponent<CanvasGroup>();
                            if (canvasGroup == null)
                            {
                                canvasGroup = deskripsiHolder.AddComponent<CanvasGroup>();
                            }
                            canvasGroup.alpha = 0f; // Set alpha ke 0 untuk memulai fade in
                            LeanTween.alphaCanvas(canvasGroup, 1f, 1f).setEase(LeanTweenType.easeInOutQuad); // Fade in dalam 1 detik

                            FadeOutObject(titleHolder);
                            FadeOutObject(buttonHolder);
                            FadeOutObject(pict);
                        }
                    });
                });
            }
        }
    }

    private void FadeOutObject(GameObject obj)
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
            });
        }
    }
}
