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
            // Memastikan komponen RectTransform terpasang
            RectTransform rectTransform = pict.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                // Atur posisi dan ukuran
                rectTransform.anchoredPosition = new Vector2(400, 4.7f);
                rectTransform.sizeDelta = new Vector2(593, 460);

                // Memainkan animasi scaling menggunakan LeanTween
                LeanTween.scale(rectTransform, new Vector3(593, 460, 1), 2f).setEase(LeanTweenType.easeInOutQuad); // Scale in dalam 2 detik
            }

            // Aktifkan GameObject pict
            pict.SetActive(true);
        }
    }
}
