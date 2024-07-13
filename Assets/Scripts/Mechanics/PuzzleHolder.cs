using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleHolder : MonoBehaviour, IDropHandler
{
    public int holderID; // Tambahkan ID pada object holder

    public void OnDrop(PointerEventData eventData)
    {
        PuzzlePiece piece = eventData.pointerDrag.GetComponent<PuzzlePiece>();
        if (piece != null && !piece.isPlaced)
        {
            if (piece.pieceID == holderID)
            {
                piece.PlacePiece(transform);
            }
            else
            {
                piece.ResetPosition(); // Kembali ke posisi awal jika ID tidak cocok
            }
        }
    }
}
