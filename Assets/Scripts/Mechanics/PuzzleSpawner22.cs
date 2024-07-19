using UnityEngine;
using System.Collections;

public class PuzzleSpawner22 : MonoBehaviour
{
    public GameObject[] prefabs; // Array prefab yang akan diinstansiasi
    public GameObject[] puzzlePieces; // Array puzzle pieces yang akan di fade out
    public GameObject instantiateArea; // GameObject yang menentukan area instansiasi
    public float delayBetweenSpawns = 0.5f; // Delay antara instansiasi prefab
    public float fadeOutDuration = 1.0f; // Durasi fade out untuk setiap puzzle piece

    private void Start()
    {
        StartCoroutine(FadeOutAndInstantiate());
    }

    private IEnumerator FadeOutAndInstantiate()
    {
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            GameObject puzzlePiece = puzzlePieces[i];
            GameObject prefabToInstantiate = prefabs[i];

            // Tambahkan CanvasGroup jika belum ada
            CanvasGroup canvasGroup = puzzlePiece.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = puzzlePiece.AddComponent<CanvasGroup>();
            }

            // Lakukan fade out dengan LeanTween
            LeanTween.alphaCanvas(canvasGroup, 0, fadeOutDuration).setEase(LeanTweenType.easeInOutQuad);

            // Tunggu sampai fade out selesai sebelum melanjutkan
            yield return new WaitForSeconds(fadeOutDuration);

            // Dapatkan ukuran area instansiasi
            Bounds bounds = instantiateArea.GetComponent<Renderer>().bounds;

            // Dapatkan posisi acak dalam area
            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomY = Random.Range(bounds.min.y, bounds.max.y);
            Vector3 randomPosition = new Vector3(randomX, randomY, 0);

            // Instantiate prefab pada posisi yang didapatkan, tanpa parent
            GameObject instantiatedPrefab = Instantiate(prefabToInstantiate, randomPosition, Quaternion.identity);

            // Set initial scale to 0
            instantiatedPrefab.transform.localScale = Vector3.zero;

            // Scale from 0 to 1
            LeanTween.scale(instantiatedPrefab, Vector3.one, 1f).setEase(LeanTweenType.easeInOutBack);
            // AudioManager.instance.PlaySound(popSfx);

            // Wait for the delay before spawning the next prefab
            yield return new WaitForSeconds(delayBetweenSpawns);
        }
    }
}
