using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Level Setting")]
    public int placedPuzzle;
    public bool isCompleted = false;
    public bool isRewardClaimed = false; // Status apakah reward sudah di-claimed
    // public bool levelFinished = false;

    [Header("Reward Setting")]
    public GameObject rewardButton;
    public GameObject pieces;

    [Header("GameObject")]
    public GameObject targetGameObjectPosition;
    public GameObject textSelamatHolder;
    public GameObject rewardHolder;
    public GameObject NextButton;
    public GameObject PrevButton;

    void Awake()
    {
        // Inisialisasi singleton instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Hanya satu instance yang diperbolehkan
        }
    }

    void Update()
    {
        if (isRewardClaimed == true)
        {
            SaveManager.instance.SetRewardClaimed_Level_1(true);
        }
    }

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
            LeanTween.scale(pieces, new Vector3(0.5f, 0.5f, 0.5f), 1f).setEasePunch().setOnComplete(() =>
            {
                LeanTween.delayedCall(1f, () =>
                {
                    // Mendapatkan posisi dan ukuran dari targetObject
                    Vector3 targetPosition = targetGameObjectPosition.transform.position;

                    // Menggunakan LeanTween untuk animasi perpindahan
                    LeanTween.move(pieces, targetPosition, 1.5f).setEase(LeanTweenType.easeOutQuad)
                        .setOnUpdate((float t) =>
                        {
                            // Menghitung alpha berdasarkan waktu t
                            float alpha = 1.0f - t;
                            SetAlpha(rewardHolder, alpha);
                        })
                        .setOnComplete(() =>
                        {
                            // Setelah animasi perpindahan selesai, fade out rewardHolder
                            CanvasGroup rewardHolderCanvasGroup = rewardHolder.GetComponent<CanvasGroup>();
                            if (rewardHolderCanvasGroup == null)
                            {
                                rewardHolderCanvasGroup = rewardHolder.AddComponent<CanvasGroup>();
                            }
                            LeanTween.alphaCanvas(rewardHolderCanvasGroup, 0, 0.4f).setOnComplete(() =>
                            {
                                rewardHolder.SetActive(false);

                                textSelamatHolder.SetActive(true);
                                NextButton.SetActive(true);
                                PrevButton.SetActive(true);
                                CanvasGroup canvasSelamat = textSelamatHolder.GetComponent<CanvasGroup>();
                                CanvasGroup canvasNextButton = NextButton.GetComponent<CanvasGroup>();
                                CanvasGroup canvasPrevButton = PrevButton.GetComponent<CanvasGroup>();
                                if (canvasSelamat == null)
                                {
                                    canvasSelamat = textSelamatHolder.AddComponent<CanvasGroup>();
                                }
                                canvasSelamat.alpha = 0;
                                LeanTween.alphaCanvas(canvasSelamat, 1, 0.4f);
                                LeanTween.alphaCanvas(canvasNextButton, 1, 0.4f);
                                LeanTween.alphaCanvas(canvasPrevButton, 1, 0.4f);
                            });
                        });

                    LeanTween.scale(pieces, new Vector3(1f, 1f, 1f), 1.5f).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
                        {
                            LeanTween.delayedCall(0.1f, () =>
                            {
                                CanvasGroup canvasTargetObject = targetGameObjectPosition.GetComponent<CanvasGroup>();
                                if (canvasTargetObject == null)
                                {
                                    canvasTargetObject = targetGameObjectPosition.AddComponent<CanvasGroup>();
                                }
                                canvasTargetObject.alpha = 1f;
                                LeanTween.alphaCanvas(canvasTargetObject, 0, 0.4f);
                            });
                        });
                });
            });
        });
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

    // Untuk mengecek apakah Puzzle Piece sudah terpasang 12 atau belum
    public void PuzzleChecking()
    {
        // Lakukan pengecekan kondisi puzzle sesuai kebutuhan
        if (PuzzleManager.Instance != null)
        {
            if (allPuzzlePiecesSnapped())
            {
                // Panggil fungsi CheckAllSnapped dari PuzzleManager
                PuzzleManager.Instance.CheckAllSnapped();
            }
        }
        else
        {
            Debug.LogWarning("PuzzleManager instance is not set!");
        }
    }

    public void SetRewardClaimed(bool claimed)
    {
        isRewardClaimed = claimed;

        if (isRewardClaimed)
        {
            Debug.Log("Reward claimed!");
            // Lakukan tindakan lain yang sesuai setelah klaim reward
        }
    }

    bool allPuzzlePiecesSnapped()
    {
        // Implementasi logika pengecekan apakah semua puzzle sudah terpasang
        // Contoh sederhana, bisa berbeda tergantung pada logika game Anda
        return PuzzleManager.Instance.snappedPieces >= PuzzleManager.Instance.totalPieces;
    }

    // Fungsi untuk menetapkan status level selesai
    public void SetLevelFinished(bool finished)
    {
        isCompleted = finished;

        if (isCompleted)
        {
            Debug.Log("Level finished!");
            // Lakukan tindakan lain yang sesuai dengan level selesai
        }
    }

    // Optional: Method to reset the level if needed
    public void ResetLevel()
    {
        placedPuzzle = 0;
        isCompleted = false;
        rewardButton.SetActive(false); // Contoh tindakan ketika level direset
    }

}
