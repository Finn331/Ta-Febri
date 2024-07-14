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
            SpriteRenderer spriteRenderer = pict.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                pict.SetActive(true);
                Color color = spriteRenderer.color;
                color.a = 0f; // Set alpha ke 0 untuk memulai fade in
                spriteRenderer.color = color;
                LeanTween.alpha(pict, 1f, 2f).setEase(LeanTweenType.easeInOutQuad); // Fade in dalam 1 detik
            }
        }
    }
}
