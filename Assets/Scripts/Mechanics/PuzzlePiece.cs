using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Image image;
    public bool isPlaced = false; // Deklarasi boolean isPlaced
    private Transform originalParent;
    private Vector3 originalPosition; // Menyimpan posisi awal
    public int pieceID; // Tambahkan ID pada puzzle piece
    public RectTransform panelRectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        originalParent = transform.parent;
        originalPosition = transform.position; // Simpan posisi awal
        panelRectTransform = originalParent.GetComponent<RectTransform>(); // Mengambil RectTransform dari panel parent
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isPlaced)
        {
            // Mendapatkan warna saat ini
            Color currentColor = image.color;

            // Mengubah nilai alpha saja menjadi 170
            currentColor.a = 170 / 255f;

            // Mengatur kembali warna dengan nilai alpha yang baru
            image.color = currentColor;

            // Memindahkan parent ke root untuk menghindari masalah sorting
            transform.SetParent(transform.root);
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isPlaced)
        {
            // Memperbarui posisi objek mengikuti posisi mouse
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isPlaced)
        {
            // Mendapatkan warna saat ini
            Color currentColor = image.color;

            // Mengubah nilai alpha saja menjadi 255
            currentColor.a = 255 / 255f;

            // Mengatur kembali warna dengan nilai alpha yang baru
            image.color = currentColor;

            // Mengecek apakah puzzle piece berada di dalam area panel
            if (RectTransformUtility.RectangleContainsScreenPoint(panelRectTransform, Input.mousePosition))
            {
                // Tetap di posisi akhir jika berada di dalam panel
                transform.SetParent(originalParent);
            }
            else
            {
                // Kembali ke posisi awal jika berada di luar panel
                transform.SetParent(originalParent);
                transform.position = originalPosition;
            }

            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }

    public void PlacePiece(Transform slot)
    {
        isPlaced = true;
        transform.position = slot.position;
        transform.SetParent(slot);

        // Mengubah nilai alpha menjadi 255 jika isPlaced bernilai true
        Color currentColor = image.color;
        currentColor.a = 255 / 255f;
        image.color = currentColor;

        // Pastikan untuk memblokir raycast setelah ter-place
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void ResetPosition()
    {
        isPlaced = false;
        transform.position = originalPosition;
        transform.SetParent(originalParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
