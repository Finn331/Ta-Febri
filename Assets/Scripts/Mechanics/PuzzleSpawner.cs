using System.Collections;
using UnityEngine;

public class PuzzleSpawner : MonoBehaviour
{
    public GameObject[] prefabs; // Array prefab yang akan diinstansiasi
    public GameObject instantiateArea; // GameObject yang menentukan area instansiasi
    public float delayBetweenInstantiates = 0.5f; // Waktu tunda antara instansiasi
    public float animationTime = 1f; // Waktu animasi LeanTween
    public float checkInterval; // Interval waktu untuk memeriksa posisi prefab
    public AudioClip popSfx; // Suara efek yang akan diputar
    
    private void Start()
    {
        StartCoroutine(InstantiatePrefabs());
    }

    private IEnumerator InstantiatePrefabs()
    {
        foreach (GameObject prefabToInstantiate in prefabs)
        {
            // Mainkan efek suara pop
            PlayPopSfx();

            // Dapatkan ukuran area instansiasi
            Bounds bounds = instantiateArea.GetComponent<Renderer>().bounds;

            // Dapatkan posisi acak dalam area
            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomY = Random.Range(bounds.min.y, bounds.max.y);
            Vector3 randomPosition = new Vector3(randomX, randomY, 0);

            // Instantiate prefab pada posisi di atas area, tanpa parent
            GameObject instantiatedPrefab = Instantiate(prefabToInstantiate, new Vector3(randomX, bounds.max.y + 1, 0), Quaternion.identity);

            // Tambahkan animasi menggunakan LeanTween
            LeanTween.move(instantiatedPrefab, randomPosition, animationTime).setEase(LeanTweenType.easeInOutQuad);

            // Mulai coroutine untuk memeriksa posisi prefab
            StartCoroutine(CheckPrefabPosition(instantiatedPrefab, randomPosition));

            // Tunggu sebelum menginstansiasi prefab berikutnya
            yield return new WaitForSeconds(delayBetweenInstantiates);
        }
    }

    private IEnumerator CheckPrefabPosition(GameObject prefab, Vector3 targetPosition)
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);

            if (prefab != null && prefab.transform.position.y > 1.9f)
            {
                // Mainkan animasi kembali jika posisi y di atas 1.9 dan tidak tersnapped
                LeanTween.move(prefab, targetPosition, animationTime).setEase(LeanTweenType.easeInOutQuad);
            }
        }
    }

    private void PlayPopSfx()
    {
        AudioManager.instance.PlaySound(popSfx);
    }
}
