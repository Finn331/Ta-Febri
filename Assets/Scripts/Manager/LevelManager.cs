using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [Header("Level Setting")]
    public int placedPuzzle;
    private bool isCompleted;

    [Header("Reward Setting")]
    public GameObject rewardBox;

    // Dipanggil sebelum Start
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        // Inisialisasi jika diperlukan
    }

    // Update is called once per frame
    void Update()
    {
        // Periksa kondisi atau logika lain yang perlu dijalankan setiap frame
    }

    // Untuk mengecek apakah Puzzle Piece sudah terpasang 12 atau belum
    public void PuzzleChecking()
    {
        if (placedPuzzle == 12)
        {
            isCompleted = true;
            if (isCompleted == true)
            {
                // Logika saat puzzle sudah lengkap
                // Misalnya menampilkan rewardBox atau memberi hadiah lainnya
                rewardBox.SetActive(true); // Contoh tindakan ketika puzzle lengkap
            }
        }
    }

    // Optional: Method to reset the level if needed
    public void ResetLevel()
    {
        placedPuzzle = 0;
        isCompleted = false;
        rewardBox.SetActive(false); // Contoh tindakan ketika level direset
    }
}
