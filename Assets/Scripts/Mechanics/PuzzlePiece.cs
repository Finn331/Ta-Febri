using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class PuzzlePiece : MonoBehaviour
{
    public int objectID; // ID untuk GameObject yang di-drag
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

                    // Animasi scaling menggunakan LeanTween
                    LeanTween.scale(gameObject, new Vector3(1.5f, 1.5f, 1.5f), 0.5f).setEase(LeanTweenType.easeOutBack);

                    // Menonaktifkan gambar holder jika ada
                    holderScript.DisableHolderImage();
                    PuzzleManager.Instance.CheckAllSnapped();

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
