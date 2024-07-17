using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    [Header("Level Setting")]
    public int placedPuzzle;
    private bool isCompleted;
    public bool isrewardClaimed = false; // Status apakah reward sudah di-claimed
    // public bool levelFinished = false;

    [Header("Reward Setting")]
    public GameObject rewardButton;
    public GameObject pieces;

    [Header("GameObject")]
    public GameObject targetGameObjectPosition;

    public void OnClickReward()
    {

        CanvasGroup canvasGroup = rewardButton.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = rewardButton.AddComponent<CanvasGroup>();
        }
        // Fade out rewardButton
        LeanTween.alphaCanvas(canvasGroup, 0, 0.8f).setOnComplete(() =>
        {
            rewardButton.SetActive(false);

            // Set active pieces and fade in
            pieces.SetActive(true);
            CanvasGroup piecesCanvasGroup = pieces.GetComponent<CanvasGroup>();
            if (piecesCanvasGroup == null)
            {
                piecesCanvasGroup = pieces.AddComponent<CanvasGroup>();
            }
            piecesCanvasGroup.alpha = 0;
            LeanTween.alphaCanvas(piecesCanvasGroup, 1, 0.4f);
            LeanTween.scale(pieces, new Vector3(0.5f, 0.5f, 0.5f), 0.5f).setEasePunch().setOnComplete(() =>
            {
                LeanTween.delayedCall(1f, () =>
                {
                    RectTransform targetRect = targetGameObjectPosition.GetComponent<RectTransform>();
                    Vector2 targetPosition = targetRect.anchoredPosition;
                    Vector2 targetSize = targetRect.sizeDelta;
                    RectTransform rectTransform = pieces.GetComponent<RectTransform>();
                    LeanTween.move(pieces, targetPosition, 2f).setEase(LeanTweenType.easeInOutQuad);
                    LeanTween.size(rectTransform, new Vector2(174f, 121f), 2f).setEase(LeanTweenType.easeInOutQuad);
                });
            });
        });
    }
    // Untuk mengecek apakah Puzzle Piece sudah terpasang 12 atau belum
    public void PuzzleChecking()
    {
        // if (placedPuzzle == 12)
        // {
        //     isCompleted = true;
        //     if (isCompleted == true)
        //     {
        //         // Logika saat puzzle sudah lengkap
        //         // Misalnya menampilkan rewardBox atau memberi hadiah lainnya
        //         rewardBox.SetActive(true); // Contoh tindakan ketika puzzle lengkap
        //     }
        // }
    }

    // Optional: Method to reset the level if needed
    public void ResetLevel()
    {
        placedPuzzle = 0;
        isCompleted = false;
        rewardButton.SetActive(false); // Contoh tindakan ketika level direset
    }

}
