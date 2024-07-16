using UnityEngine;

public class InputChecker : MonoBehaviour
{
    public GameObject targetObject; // GameObject yang akan ditutup
    public GameObject detectionAreaObject; // GameObject yang digunakan sebagai area deteksi
    public GameObject pauseMenu; // Pause menu yang akan ditutup
    public GameObject dimmer; // Dimmer yang akan dinonaktifkan

    private Collider2D detectionCollider;
    private Vector3 pauseMenuOriginalPosition;

    private void Start()
    {
        pauseMenuOriginalPosition = pauseMenu.transform.localPosition;
        if (detectionAreaObject != null)
        {
            detectionCollider = detectionAreaObject.GetComponent<Collider2D>();
            if (detectionCollider == null)
            {
                Debug.LogError("Detection Area Object harus memiliki Collider2D");
            }
        }
        else
        {
            Debug.LogError("Detection Area Object tidak ditetapkan");
        }
    }

    private void Update()
    {
        if (detectionCollider != null)
        {
            CheckInput();
        }
    }

    private void CheckInput()
    {
        // Periksa input mouse
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            if (IsInputWithinArea(mousePosition))
            {
                CloseTargetObject();
            }
        }

        // Periksa input touch
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
                {
                    Vector3 touchPosition = touch.position;
                    if (IsInputWithinArea(touchPosition))
                    {
                        CloseTargetObject();
                    }
                }
            }
        }
    }

    private bool IsInputWithinArea(Vector3 inputPosition)
    {
        // Ubah posisi input dari layar ke world
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(inputPosition);
        worldPosition.z = 0f; // Set z ke 0 untuk 2D

        // Periksa apakah posisi world berada dalam collider deteksi
        return detectionCollider.OverlapPoint(worldPosition);
    }

    private void CloseTargetObject()
    {
        if (pauseMenu != null && dimmer != null)
        {
            LeanTween.moveLocalX(pauseMenu, pauseMenuOriginalPosition.x, 0.5f).setEase(LeanTweenType.easeInOutSine);
            dimmer.SetActive(false);
        }
        else
        {
            Debug.LogError("Pause menu atau dimmer tidak ditetapkan");
        }
    }
}
