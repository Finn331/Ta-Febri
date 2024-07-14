using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    private int totalPieces = 12; // Jumlah total puzzle pieces yang harus tersnap
    private int snappedPieces = 0; // Counter untuk puzzle pieces yang sudah tersnap
    public GameObject pict; // GameObject yang akan diaktifkan

    private void Awake()
    {
        if (pict == true)
        {
            pict.SetActive(false);
        }

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
            CanvasGroup canvasGroup = pict.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = pict.AddComponent<CanvasGroup>();
            }

            pict.SetActive(true);
            canvasGroup.alpha = 0f; // Set alpha ke 0 untuk memulai fade in
            LeanTween.alphaCanvas(canvasGroup, 1f, 2f).setEase(LeanTweenType.easeInOutQuad); // Fade in dalam 1 detik
        }
    }
}
