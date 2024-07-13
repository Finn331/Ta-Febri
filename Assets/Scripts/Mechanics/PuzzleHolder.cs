using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleHolder : MonoBehaviour
{
    public int holderID; // ID untuk holder

    private SpriteRenderer holderImage; // Komponen SpriteRenderer dari holder

    private void Start()
    {
        holderImage = GetComponent<SpriteRenderer>(); // Mengambil komponen SpriteRenderer dari holder
    }

    public void SetActiveState(bool isActive)
    {
        holderImage.enabled = isActive; // Mengatur status aktif/nonaktif dari SpriteRenderer holder
    }

    public void DisableHolderImage()
    {
        if (holderImage != null)
        {
            // Fade out the image over 0.8 seconds
            LeanTween.alpha(gameObject, 0f, 0.8f).setOnComplete(() => {
                holderImage.enabled = false; // Menonaktifkan gambar holder setelah delay
                // Reset the alpha to 1 for potential future use
                Color color = holderImage.color;
                color.a = 1f;
                holderImage.color = color;
            });
        }
    }
}
