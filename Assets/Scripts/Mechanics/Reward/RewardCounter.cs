using UnityEngine;
using UnityEngine.UI;

public class RewardCounter : MonoBehaviour
{
    [Header("Reward Counter")]
    public Image[] rewardPieces;
    public Image[] emptyPieces;

    private void Update()
    {
        RewardPieces();
    }

    void RewardPieces()
    {
        int savedPieces = SaveManager.instance.rewardPieces;

        for (int i = 0; i < emptyPieces.Length; i++)
        {
            if (i < savedPieces)
            {
                rewardPieces[i].enabled = true;
                emptyPieces[i].enabled = false;
            }
            else
            {
                rewardPieces[i].enabled = false;
                emptyPieces[i].enabled = true;
            }
        }
    }
}