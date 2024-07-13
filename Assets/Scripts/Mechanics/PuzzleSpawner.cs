using UnityEngine;

public class PuzzleSpawner : MonoBehaviour
{
    public GameObject[] prefabs; // Array prefab yang akan diinstansiasi
    public GameObject instantiateArea; // GameObject yang menentukan area instansiasi

    private void Start()
    {
        InstantiatePrefabs();
    }

    private void InstantiatePrefabs()
    {
        foreach (GameObject prefabToInstantiate in prefabs)
        {
            // Dapatkan ukuran area instansiasi
            Bounds bounds = instantiateArea.GetComponent<Renderer>().bounds;

            // Dapatkan posisi acak dalam area
            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomY = Random.Range(bounds.min.y, bounds.max.y);
            Vector3 randomPosition = new Vector3(randomX, randomY, 0);

            // Instantiate prefab pada posisi yang didapatkan, tanpa parent
            Instantiate(prefabToInstantiate, randomPosition, Quaternion.identity);
        }
    }
}
