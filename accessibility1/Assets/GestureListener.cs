using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class GestureListener : MonoBehaviour
{
    public ActionBasedController leftController;
    public ActionBasedController rightController;
    public GameObject waveReactionImage;
    public GameObject facePalmReactionImage;
    public GameObject xArmsReactionImage;
    public GameObject thumbsUpReactionImage;
    private bool isWaving = false;
    private bool isFacePalming = false;
    private bool isXArms = false;
    private bool isThumbsUp = false;

    private float waveThreshold = 2.0f;
    private float waveTimer = 0f;

    void Update()
    {
        DetectWaveGesture();
        DetectFacePalmEmote();
        DetectXArmsPose();
        DetectThumbsUpGesture();
    }

    // Waving detector
    void DetectWaveGesture()
    {
        Vector3 rightHandPos = rightController.positionAction.action.ReadValue<Vector3>();

        if (rightHandPos.y > 1.5f && !isWaving)
        {
            isWaving = true;
            waveTimer = 0f;
        }

        if (isWaving)
        {
            waveTimer += Time.deltaTime;

            if (waveTimer >= waveThreshold)
            {
                SwapImage(waveReactionImage);
                isWaving = false;
            }
        }

        if (rightHandPos.y < 1.5f)
        {
            isWaving = false;
        }
    }

    // Facepalm Detector
    void DetectFacePalmEmote()
    {
        Vector3 rightHandPos = rightController.positionAction.action.ReadValue<Vector3>();

        // Checks if hand near head height and in front of face
        if (rightHandPos.y > 1.3f && Mathf.Abs(rightHandPos.z) < 0.3f && !isFacePalming)
        {
            SwapImage(facePalmReactionImage);
            isFacePalming = true;
        }

        if (rightHandPos.y < 1.0f)
        {
            isFacePalming = false;
        }
    }

    // X arms detector
    void DetectXArmsPose()
    {
        Vector3 rightHandPos = rightController.positionAction.action.ReadValue<Vector3>();
        Vector3 leftHandPos = leftController.positionAction.action.ReadValue<Vector3>();

        // Example condition: Hands crossing each other at chest height
        if (Mathf.Abs(rightHandPos.x - leftHandPos.x) < 0.2f && rightHandPos.y > 1.0f && leftHandPos.y > 1.0f && !isXArms)
        {
            SwapImage(xArmsReactionImage);
            isXArms = true;
        }

        if (Mathf.Abs(rightHandPos.x - leftHandPos.x) > 0.5f || rightHandPos.y < 1.0f || leftHandPos.y < 1.0f)
        {
            isXArms = false;
        }
    }

    // Thumbs Up Detector, using hand tracking via XR Hands (or simulated via trigger press)
    void DetectThumbsUpGesture()
    {
        // Simulated thumbs-up using the right controller's trigger press (you can replace this with XR Hands data)
        if (rightController.selectAction.action.ReadValue<float>() > 0.9f && !isThumbsUp)
        {
            SwapImage(thumbsUpReactionImage);
            isThumbsUp = true;
        }

        if (rightController.selectAction.action.ReadValue<float>() < 0.1f)
        {
            isThumbsUp = false;
        }
    }

    // Swaps the image based on gesture
    void SwapImage(GameObject reactionImage)
    {
        // Deactivate all reaction images first
        waveReactionImage.SetActive(false);
        facePalmReactionImage.SetActive(false);
        xArmsReactionImage.SetActive(false);
        thumbsUpReactionImage.SetActive(false);

        // Activate response image
        reactionImage.SetActive(true);
    }
}
