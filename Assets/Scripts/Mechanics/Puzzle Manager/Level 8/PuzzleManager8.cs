using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuzzleManager8 : MonoBehaviour
{
    public static PuzzleManager8 Instance;

    [Header("Integer & Boolean Checking")]
    public int totalPieces = 12; // Jumlah total puzzle pieces yang harus tersnap
    public int snappedPieces = 0; // Counter untuk puzzle pieces yang sudah tersnap
    public bool rewardClaimed; // Status apakah reward sudah di-claimed
    public bool levelFinished; // Status apakah level sudah selesai

    [Header("GameObject")]
    public GameObject pict; // GameObject yang akan diaktifkan
    public GameObject deskripsiHolder;
    public GameObject titleHolder;
    public GameObject buttonPrev;
    public GameObject buttonNext;
    public GameObject rewardButton; // GameObject untuk reward button
    public GameObject level;
    public Button closeButton; // Tombol untuk menutup deskripsiHolder
    public GameObject containerRewardHolder;
    public GameObject rewardHolder;
    public GameObject rewardBox;
    public Button rewardButtons;

    [Header("Level Manager")]
    public GameObject containerTextSelamatHolder;
    [SerializeField] private GameObject containerTextSelamatHolder2;
    public GameObject containerPiecesReward;
    public GameObject targetGameObjectPosition;
    public GameObject NextButton;
    public Button NextButtons;
    public GameObject pieces;
    public GameObject PrevButton;

    [Header("Audio Setting")]
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject settingHolder;
    [SerializeField] private float delaySetting;
    [SerializeField] private AudioClip bgmSong;
    [SerializeField] private AudioClip clickSFX;

    [Header("Object to disable")]
    [SerializeField] private GameObject[] disabledObject;

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
        SaveManager.instance.currentLevel = 8;
        SaveManager.instance.Save();
    }

    void Start()
    {
        AudioManager.instance.PlayMusic(bgmSong, true);
        if (rewardClaimed)
        {
            containerRewardHolder.SetActive(false);
            rewardButton.SetActive(false);
            containerRewardHolder.SetActive(false);

        }

        LevelFinishedCheck();
        // if (rewardClaimed == true)
        // {
        //     NextButtons.interactable = true;

        //     containerRewardHolder.SetActive(false);
        //     rewardButton.SetActive(false);
        //     containerRewardHolder.SetActive(false);
        // }
        // else
        // {
        //     NextButtons.interactable = false;
        // }

    }

    void Update()
    {
        Checking();
        if (rewardClaimed == true)
        {
            NextButtons.interactable = true;
        }
        else
        {
            NextButtons.interactable = false;
        }
    }

    public void OpenSettingPanel()
    {
        settingPanel.SetActive(true);
        settingHolder.SetActive(true);
        LeanTween.scale(settingHolder, new Vector3(1, 1, 1), delaySetting).setEase(LeanTweenType.easeOutBounce);
        AudioManager.instance.PlaySound(clickSFX);
    }

    public void CloseSettingPanel()
    {
        LeanTween.scale(settingHolder, new Vector3(0, 0, 0), delaySetting).setEase(LeanTweenType.easeOutBounce).setOnComplete(() =>
        {
            settingPanel.SetActive(false);
            settingHolder.SetActive(false);
        });
        AudioManager.instance.PlaySound(clickSFX);
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
                        // Melakukan fade in pada deskripsi holder
                        if (deskripsiHolder != null)
                        {
                            //check jika rewardclaimed == false maka containerRewardHolder akan di fade in
                            if (rewardClaimed == false)
                            {
                                // containerRewardHolder.SetActive(true);
                                rewardHolder.SetActive(true);
                                CanvasGroup canvasGroup1 = rewardHolder.GetComponent<CanvasGroup>();
                                canvasGroup1 = rewardHolder.AddComponent<CanvasGroup>();
                                canvasGroup1.alpha = 0f;
                                LeanTween.alphaCanvas(canvasGroup1, 1f, 1f).setEase(LeanTweenType.easeInOutQuad); // Fade in dalam 1 detik
                            }
                            //jika rewardClaimed nya == true maka containerRewardHolder akan di destroyWithDelay 5f (5 detik)
                            else
                            {
                                Debug.Log("Reward Claimed!");
                                Checking();

                            }
                            deskripsiHolder.SetActive(true);
                            CanvasGroup canvasGroup = deskripsiHolder.GetComponent<CanvasGroup>();
                            if (canvasGroup == null)
                            {
                                canvasGroup = deskripsiHolder.AddComponent<CanvasGroup>();
                            }
                            canvasGroup.alpha = 0f; // Set alpha ke 0 untuk memulai fade in
                            LeanTween.alphaCanvas(canvasGroup, 1f, 1f).setEase(LeanTweenType.easeInOutQuad); // Fade in dalam 1 detik

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
        AudioManager.instance.PlaySound(clickSFX);
        SaveManager.instance.level_8_completed = true;
        SaveManager.instance.Save();
        // Melakukan fade out pada deskripsiHolder
        if (deskripsiHolder != null)
        {
            if (rewardClaimed)
            {
                FadeOutObject(deskripsiHolder, ToTextSelamat);
            }
            FadeOutObject(deskripsiHolder, MoveRewardButton);
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
        LeanTween.alphaCanvas(canvasNextButton, 1, 0.4f);
        LeanTween.alphaCanvas(canvasPrevButton, 1, 0.4f);
    }

    private void MoveRewardButton()
    {
        if (rewardClaimed == false)
        {
            if (containerRewardHolder != null)
            {
                RectTransform rectTransform = rewardButton.GetComponent<RectTransform>();
                RectTransform rectTransform1 = containerPiecesReward.GetComponent<RectTransform>();
                RectTransform rectTransform2 = containerTextSelamatHolder.GetComponent<RectTransform>();
                if (rectTransform && rectTransform1 && rectTransform2 != null)
                {
                    LeanTween.move(rectTransform, new Vector3(-24.89f, 0f, 0f), 1f).setEase(LeanTweenType.easeInOutQuad);
                    LeanTween.move(rectTransform1, new Vector3(-24.89f, 0f, 0f), 1f).setEase(LeanTweenType.easeInOutQuad);
                    LeanTween.move(rectTransform2, new Vector3(-24.89f, 0f, 0f), 1f).setEase(LeanTweenType.easeInOutQuad);
                }
                Checking();
            }
        }
    }

    public void Checking()
    {
        if (rewardClaimed == true)
        {

            SaveManager.instance.SetRewardClaimed(8, true);

            //SaveManager.instance.Save();
            StartCoroutine(DestroyRewardWithDelay(5f));
            if (containerRewardHolder && pieces == null)
            {
                Debug.Log("Reward Claimed");
                containerTextSelamatHolder.SetActive(true);
                NextButton.SetActive(true);
                PrevButton.SetActive(true);
                containerRewardHolder.SetActive(false);
                // Setelah animasi perpindahan selesai, fade out containerRewardHolder
                CanvasGroup containerRewardBoxCanvasGroup = rewardBox.GetComponent<CanvasGroup>();
                if (containerRewardBoxCanvasGroup == null)
                {
                    containerRewardBoxCanvasGroup = rewardBox.AddComponent<CanvasGroup>();
                }

                LeanTween.alphaCanvas(containerRewardBoxCanvasGroup, 0, 0.4f).setOnComplete(() =>
                {
                    rewardBox.SetActive(false);

                    // containerTextSelamatHolder.SetActive(true);
                    // NextButton.SetActive(true);
                    // PrevButton.SetActive(true);
                    CanvasGroup canvasSelamat = containerTextSelamatHolder.GetComponent<CanvasGroup>();
                    CanvasGroup canvasNextButton = NextButton.GetComponent<CanvasGroup>();
                    CanvasGroup canvasPrevButton = PrevButton.GetComponent<CanvasGroup>();
                    if (canvasSelamat == null)
                    {
                        canvasSelamat = containerTextSelamatHolder.AddComponent<CanvasGroup>();
                    }
                    canvasSelamat.alpha = 0;
                    LeanTween.alphaCanvas(canvasSelamat, 1, 0.4f);
                    LeanTween.alphaCanvas(canvasNextButton, 1, 0.4f);
                    LeanTween.alphaCanvas(canvasPrevButton, 1, 0.4f);
                });
            }
        }

        if (SaveManager.instance.level_8_RewardClaimed == true)
        {
            rewardClaimed = true;

        }
    }

    public void PulseRewardButton()
    {
        AudioManager.instance.PlaySound(clickSFX);
        levelFinished = true;
        if (rewardButton != null)
        {
            rewardButtons.interactable = false;
            // Animasi pulse dengan menggunakan LeanTween
            LeanTween.scale(rewardButton, new Vector3(0.3f, 0.3f, 0.3f), 0.4f).setEasePunch().setLoopPingPong(1);
            rewardClaimed = true;
            NextButtons.interactable = true;
            // LevelManager.Instance.SetRewardClaimed(true);
            if (SaveManager.instance.level_8_RewardClaimed == true)
            {
                rewardClaimed = true;
            }

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

                                    containerTextSelamatHolder.SetActive(true);
                                    NextButton.SetActive(true);
                                    PrevButton.SetActive(true);
                                    CanvasGroup canvasSelamat = containerTextSelamatHolder.GetComponent<CanvasGroup>();
                                    CanvasGroup canvasNextButton = NextButton.GetComponent<CanvasGroup>();
                                    CanvasGroup canvasPrevButton = PrevButton.GetComponent<CanvasGroup>();
                                    if (canvasSelamat == null)
                                    {
                                        canvasSelamat = containerTextSelamatHolder.AddComponent<CanvasGroup>();
                                    }
                                    canvasSelamat.alpha = 0;
                                    LeanTween.alphaCanvas(canvasSelamat, 1, 0.4f);
                                    LeanTween.alphaCanvas(canvasNextButton, 1, 0.4f);
                                    LeanTween.alphaCanvas(canvasPrevButton, 1, 0.4f);
                                });
                            });

                        LeanTween.scale(pieces, new Vector2(0.20f, 0.20f), 1.5f).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
                        {
                            LeanTween.delayedCall(0.1f, () =>
                            {
                                SaveManager.instance.rewardPieces++;
                                SaveManager.instance.Save();
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
        AudioManager.instance.PlaySound(clickSFX);
        // Reset snappedPieces
        snappedPieces = 0;

        // Cek dan reset rewardClaimed dan levelFinished
        if (rewardClaimed && levelFinished)
        {
            rewardClaimed = true;  // Set reward sudah di-claimed menjadi true
            levelFinished = false; // Set level sudah selesai menjadi false
            SaveManager.instance.level_8_completed = false;
            SaveManager.instance.Save();
        }
        for (int i = 0; i < disabledObject.Length; i++)
        {
            if (disabledObject[i] != null)
            {
                disabledObject[i].SetActive(true);
            }
        }
        // Load ulang scene saat ini
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator DestroyRewardWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(rewardButton);
        // Destroy(pieces);
        Destroy(rewardHolder);
    }

    public void LevelFinishedCheck()
    {
        if (SaveManager.instance.level_8_completed == true)
        {
            levelFinished = true;
            //Mematikan semua Object yang akan di Disable
            for (int i = 0; i < disabledObject.Length; i++)
            {
                if (disabledObject[i] != null)
                {
                    disabledObject[i].SetActive(false);
                }
            }
            containerTextSelamatHolder2.SetActive(true);
            LeanTween.scale(containerTextSelamatHolder2, new Vector3(1, 1, 1), 1f).setEase(LeanTweenType.easeInOutQuad);
        }
    }
}