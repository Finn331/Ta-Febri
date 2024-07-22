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
    [SerializeField] private PuzzleManager puzzleManager; // Referensi ke skrip PuzzleManager

    public void Next()
    {
        if (posisi < pos.Length - 1)
        {
            posisi += 1;
            scrollPos = pos[posisi];
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

        // Tambahkan pengecekan posisi dan input untuk navigasi
        if ((posisi == 4 && Input.GetMouseButtonDown(0)) || (posisi == 4 && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            puzzleManager.CloseDeskripsiHolder();
        }
    }
}
