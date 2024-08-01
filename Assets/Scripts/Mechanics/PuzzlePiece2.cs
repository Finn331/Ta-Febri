using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class PuzzlePiece2 : MonoBehaviour
{
    [Header("Puzzle Piece Setting")]
    public int objectID; // ID untuk GameObject yang di-drag
    [SerializeField] AudioClip snappedClip;
    private bool isDragging = false;
    private bool isSnapped = false; // Menandai apakah GameObject sudah tersnap atau belum
    private Vector3 offset;
    private Vector3 initialPosition; // Posisi awal GameObject saat di-instantiate
    private Camera mainCamera;
    private float snapDistance = 0.5f; // Jarak untuk deteksi collider
    private Collider2D spawnAreaCollider; // Collider untuk area spawn
    private Vector3 lastValidPosition; // Posisi terakhir sebelum diangkat



    private void Start()
    {
        mainCamera = Camera.main;
        initialPosition = transform.position; // Simpan posisi awal saat di-instantiate

        // Cari Collider dari GameObject "Spawn Area"
        GameObject spawnArea = GameObject.Find("Spawn Area");
        if (spawnArea != null)
        {
            spawnAreaCollider = spawnArea.GetComponent<Collider2D>();
        }
        else
        {
            Debug.LogError("Spawn Area not found or Collider2D component is missing.");
        }

        // Pindahkan objek ke atas (di atas semua objek lain)
        transform.SetAsLastSibling();

        // Cari GameObject "level"
        GameObject levelObject = GameObject.Find("HolderPuzzleClone");
        if (levelObject != null)
        {
            // Set game object ini menjadi child dari "level"
            transform.SetParent(levelObject.transform);
        }
        else
        {
            Debug.LogError("Level GameObject not found.");
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = GetTouchWorldPosition(touch.position);

            HandleInput(touch.phase, touchPosition);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            HandleInput(TouchPhase.Began, mousePosition);
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            transform.position = mousePosition + offset;
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            HandleInput(TouchPhase.Ended, Vector3.zero);
        }

        // Pengecekan jika GameObject berada di luar Spawn Area
        if (!isSnapped && spawnAreaCollider != null && !spawnAreaCollider.bounds.Contains(transform.position))
        {
            if (!isDragging)
            {
                ResetToInitialPosition();
            }
        }
    }

    private void HandleInput(TouchPhase touchPhase, Vector3 inputPosition)
    {
        int puzzlePieceLayerMask = LayerMask.GetMask("PuzzlePiece"); // Sesuaikan dengan nama layer

        switch (touchPhase)
        {
            case TouchPhase.Began:
                RaycastHit2D hit = Physics2D.Raycast(inputPosition, Vector2.zero, Mathf.Infinity, puzzlePieceLayerMask);
                if (hit.collider != null && hit.collider == GetComponent<Collider2D>())
                {
                    isDragging = true;
                    offset = transform.position - inputPosition;
                    lastValidPosition = transform.position; // Simpan posisi terakhir sebelum diangkat
                    Debug.Log("Dragging started");
                    GetComponent<SortingGroup>().sortingOrder = 5;

                    // Pindahkan objek ke atas (di atas semua objek lain)
                    transform.SetAsLastSibling();
                }
                break;

            case TouchPhase.Moved:
                if (isDragging)
                {
                    transform.position = inputPosition + offset;
                }
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                if (isDragging)
                {
                    Debug.Log("Dragging ended");
                    CheckForHolderMatch();
                    if (!isSnapped && spawnAreaCollider != null && !spawnAreaCollider.bounds.Contains(transform.position))
                    {
                        ResetToInitialPosition();
                    }
                    isDragging = false;
                    GetComponent<SortingGroup>().sortingOrder = 2;
                }
                break;
        }
    }

    private Vector3 GetTouchWorldPosition(Vector3 touchPosition)
    {
        touchPosition.z = mainCamera.nearClipPlane;
        return mainCamera.ScreenToWorldPoint(touchPosition);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.nearClipPlane;
        return mainCamera.ScreenToWorldPoint(mousePosition);
    }

    private void CheckForHolderMatch()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, snapDistance); // Menggunakan OverlapCircleAll untuk mendeteksi collider di sekitar

        foreach (Collider2D collider in colliders)
        {
            PuzzleHolder holderScript = collider.GetComponent<PuzzleHolder>();
            if (holderScript != null && holderScript.holderID == objectID)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < snapDistance)
                {
                    transform.position = collider.transform.position; // Snap ke posisi holder
                    isSnapped = true; // Menandai bahwa GameObject sudah tersnap
                    GetComponent<Collider2D>().enabled = false; // Menonaktifkan collider agar tidak bisa diambil lagi
                    lastValidPosition = transform.position; // Update posisi terakhir yang valid setelah tersnap

                    Debug.Log("Snapped to holder: " + holderScript.holderID);

                    // memainkan audio ketika snapped
                    AudioManager.instance.PlaySound(snappedClip);
                    // Animasi scaling menggunakan LeanTween
                    LeanTween.scale(gameObject, new Vector3(1.60f, 1.60f, 1.14f), 0.5f).setEase(LeanTweenType.easeOutBack);

                    // Menonaktifkan gambar holder jika ada
                    holderScript.DisableHolderImage();
                    if (PuzzleManager.Instance != null)
                    {
                        PuzzleManager.Instance.CheckAllSnapped();
                        Debug.Log("PuzzleManager ditemukan dan CheckAllSnapped() dipanggil.");
                    }
                    else
                    {
                        Debug.LogWarning("PuzzleManager tidak ditemukan.");
                    }

                    if (PuzzleManager2.Instance != null)
                    {
                        PuzzleManager2.Instance.CheckAllSnapped();
                        Debug.Log("PuzzleManager2 ditemukan dan CheckAllSnapped() dipanggil.");
                    }
                    else
                    {
                        Debug.LogWarning("PuzzleManager2 tidak ditemukan.");
                    }

                    if (PuzzleManager3.Instance != null)
                    {
                        PuzzleManager3.Instance.CheckAllSnapped();
                        Debug.Log("PuzzleManager3 ditemukan dan CheckAllSnapped() dipanggil.");
                    }
                    else
                    {
                        Debug.LogWarning("PuzzleManager3 tidak ditemukan.");
                    }

                    if (PuzzleManager4.Instance != null)
                    {
                        PuzzleManager4.Instance.CheckAllSnapped();
                        Debug.Log("PuzzleManager4 ditemukan dan CheckAllSnapped() dipanggil.");
                    }
                    else
                    {
                        Debug.LogWarning("PuzzleManager4 tidak ditemukan.");
                    }

                    if (PuzzleManager5.Instance != null)
                    {
                        PuzzleManager5.Instance.CheckAllSnapped();
                        Debug.Log("PuzzleManager5 ditemukan dan CheckAllSnapped() dipanggil.");
                    }
                    else
                    {
                        Debug.LogWarning("PuzzleManager5 tidak ditemukan.");
                    }

                    if (PuzzleManager6.Instance != null)
                    {
                        PuzzleManager6.Instance.CheckAllSnapped();
                        Debug.Log("PuzzleManager6 ditemukan dan CheckAllSnapped() dipanggil.");
                    }
                    else
                    {
                        Debug.LogWarning("PuzzleManager6 tidak ditemukan.");
                    }

                    if (PuzzleManager7.Instance != null)
                    {
                        PuzzleManager7.Instance.CheckAllSnapped();
                        Debug.Log("PuzzleManager7 ditemukan dan CheckAllSnapped() dipanggil.");
                    }
                    else
                    {
                        Debug.LogWarning("PuzzleManager7 tidak ditemukan.");
                    }

                    if (PuzzleManager8.Instance != null)
                    {
                        PuzzleManager8.Instance.CheckAllSnapped();
                        Debug.Log("PuzzleManager8 ditemukan dan CheckAllSnapped() dipanggil.");
                    }
                    else
                    {
                        Debug.LogWarning("PuzzleManager8 tidak ditemukan.");
                    }

                    if (PuzzleManager9.Instance != null)
                    {
                        PuzzleManager9.Instance.CheckAllSnapped();
                        Debug.Log("PuzzleManager9 ditemukan dan CheckAllSnapped() dipanggil.");
                    }
                    else
                    {
                        Debug.LogWarning("PuzzleManager9 tidak ditemukan.");
                    }

                    if (PuzzleManager10.Instance != null)
                    {
                        PuzzleManager10.Instance.CheckAllSnapped();
                        Debug.Log("PuzzleManager10 ditemukan dan CheckAllSnapped() dipanggil.");
                    }
                    else
                    {
                        Debug.LogWarning("PuzzleManager10 tidak ditemukan.");
                    }

                    if (PuzzleManager11.Instance != null)
                    {
                        PuzzleManager11.Instance.CheckAllSnapped();
                        Debug.Log("PuzzleManager11 ditemukan dan CheckAllSnapped() dipanggil.");
                    }
                    else
                    {
                        Debug.LogWarning("PuzzleManager11 tidak ditemukan.");
                    }

                    if (PuzzleManager12.Instance != null)
                    {
                        PuzzleManager12.Instance.CheckAllSnapped();
                        Debug.Log("PuzzleManager12 ditemukan dan CheckAllSnapped() dipanggil.");
                    }
                    else
                    {
                        Debug.LogWarning("PuzzleManager12 tidak ditemukan.");
                    }

                    if (PuzzleManager13.Instance != null)
                    {
                        PuzzleManager13.Instance.CheckAllSnapped();
                        Debug.Log("PuzzleManager13 ditemukan dan CheckAllSnapped() dipanggil.");
                    }
                    else
                    {
                        Debug.LogWarning("PuzzleManager13 tidak ditemukan.");
                    }

                    break;
                }
            }
        }
    }

    private void ResetToLastValidPosition()
    {
        if (!isSnapped) // Hanya reset jika belum tersnap
        {
            transform.position = lastValidPosition; // Kembalikan ke posisi terakhir sebelum diangkat
            Debug.Log("Reset to last valid position");
        }
    }

    private void ResetToInitialPosition()
    {
        transform.position = initialPosition; // Kembalikan ke posisi awal saat di-instantiate
        Debug.Log("Reset to initial position");
    }
}
