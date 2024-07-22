using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeControl : MonoBehaviour
{
    public GameObject scrollBar;
    float scrollPos = 0;
    float[] pos;
    int posisi = 0;
    [SerializeField] private AudioClip clickSFX;

    public void Next()
    {

        if (posisi < pos.Length - 1)
        {
            posisi += 1;
            scrollPos = pos[posisi];
        }
        if (posisi == 5)
        {

            if (PuzzleManager.Instance != null)
            {
                PuzzleManager.Instance.CloseDeskripsiHolder();
                Debug.Log("PuzzleManager ditemukan dan CloseDeskripsiHolder() dipanggil.");
            }
            else
            {
                Debug.LogWarning("PuzzleManager tidak ditemukan.");
            }

            if (PuzzleManager2.Instance != null)
            {
                PuzzleManager2.Instance.CloseDeskripsiHolder();
                Debug.Log("PuzzleManager2 ditemukan dan CloseDeskripsiHolder() dipanggil.");
            }
            else
            {
                Debug.LogWarning("PuzzleManager2 tidak ditemukan.");
            }

            if (PuzzleManager3.Instance != null)
            {
                PuzzleManager3.Instance.CloseDeskripsiHolder();
                Debug.Log("PuzzleManager3 ditemukan dan CloseDeskripsiHolder() dipanggil.");
            }
            else
            {
                Debug.LogWarning("PuzzleManager3 tidak ditemukan.");
            }

            if (PuzzleManager4.Instance != null)
            {
                PuzzleManager4.Instance.CloseDeskripsiHolder();
                Debug.Log("PuzzleManager4 ditemukan dan CloseDeskripsiHolder() dipanggil.");
            }
            else
            {
                Debug.LogWarning("PuzzleManager4 tidak ditemukan.");
            }

            if (PuzzleManager5.Instance != null)
            {
                PuzzleManager5.Instance.CloseDeskripsiHolder();
                Debug.Log("PuzzleManager5 ditemukan dan CloseDeskripsiHolder() dipanggil.");
            }
            else
            {
                Debug.LogWarning("PuzzleManager5 tidak ditemukan.");
            }

            if (PuzzleManager6.Instance != null)
            {
                PuzzleManager6.Instance.CloseDeskripsiHolder();
                Debug.Log("PuzzleManager6 ditemukan dan CloseDeskripsiHolder() dipanggil.");
            }
            else
            {
                Debug.LogWarning("PuzzleManager6 tidak ditemukan.");
            }

            if (PuzzleManager7.Instance != null)
            {
                PuzzleManager7.Instance.CloseDeskripsiHolder();
                Debug.Log("PuzzleManager7 ditemukan dan CloseDeskripsiHolder() dipanggil.");
            }
            else
            {
                Debug.LogWarning("PuzzleManager7 tidak ditemukan.");
            }

            if (PuzzleManager8.Instance != null)
            {
                PuzzleManager8.Instance.CloseDeskripsiHolder();
                Debug.Log("PuzzleManager8 ditemukan dan CloseDeskripsiHolder() dipanggil.");
            }
            else
            {
                Debug.LogWarning("PuzzleManager8 tidak ditemukan.");
            }

            if (PuzzleManager9.Instance != null)
            {
                PuzzleManager9.Instance.CloseDeskripsiHolder();
                Debug.Log("PuzzleManager9 ditemukan dan CloseDeskripsiHolder() dipanggil.");
            }
            else
            {
                Debug.LogWarning("PuzzleManager9 tidak ditemukan.");
            }

            if (PuzzleManager10.Instance != null)
            {
                PuzzleManager10.Instance.CloseDeskripsiHolder();
                Debug.Log("PuzzleManager10 ditemukan dan CloseDeskripsiHolder() dipanggil.");
            }
            else
            {
                Debug.LogWarning("PuzzleManager10 tidak ditemukan.");
            }

            if (PuzzleManager11.Instance != null)
            {
                PuzzleManager11.Instance.CloseDeskripsiHolder();
                Debug.Log("PuzzleManager11 ditemukan dan CloseDeskripsiHolder() dipanggil.");
            }
            else
            {
                Debug.LogWarning("PuzzleManager11 tidak ditemukan.");
            }

            if (PuzzleManager12.Instance != null)
            {
                PuzzleManager12.Instance.CloseDeskripsiHolder();
                Debug.Log("PuzzleManager12 ditemukan dan CloseDeskripsiHolder() dipanggil.");
            }
            else
            {
                Debug.LogWarning("PuzzleManager12 tidak ditemukan.");
            }
        }
        AudioManager.instance.PlaySound(clickSFX);
    }

    public void Prev()
    {
        if (posisi > 0)
        {
            posisi -= 1;
            scrollPos = pos[posisi];
        }
        AudioManager.instance.PlaySound(clickSFX);
    }

    private void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        if (Input.GetMouseButtonDown(0))
        {
            scrollPos = scrollBar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
                {
                    scrollBar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollBar.GetComponent<Scrollbar>().value, pos[i], 0.15f);
                    posisi = i;
                }
            }
        }
    }
}