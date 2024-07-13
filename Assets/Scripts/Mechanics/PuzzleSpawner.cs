using UnityEngine;

public class PuzzleSpawner : MonoBehaviour
{
    public GameObject[] prefabs; // Array prefab yang akan diinstansiasi
    [SerializeField] private RectTransform panel; // Panel yang menentukan area instansiasi

    private void Start()
    {
        InstantiatePrefabs();
    }

    private void InstantiatePrefabs()
    {
        foreach (GameObject prefabToInstantiate in prefabs)
        {
            // Dapatkan ukuran panel
            Vector2 panelSize = panel.rect.size;

            // Dapatkan posisi acak dalam panel
            float randomX = Random.Range(-panelSize.x / 2, panelSize.x / 2);
            float randomY = Random.Range(-panelSize.y / 2, panelSize.y / 2);
            Vector3 randomPosition = new Vector3(randomX, randomY, 0);

            // Konversi posisi lokal ke posisi world
            Vector3 worldPosition = panel.TransformPoint(randomPosition);

            // Instantiate prefab pada posisi world yang didapatkan
            Instantiate(prefabToInstantiate, worldPosition, Quaternion.identity, panel);
        }
    }
}
