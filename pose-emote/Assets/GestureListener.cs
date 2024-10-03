using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class GestureListener : MonoBehaviour
{
    // Reference to the action-based controllers
    public ActionBasedController leftController;  // Assign in Inspector
    public ActionBasedController rightController; // Assign in Inspector
    public GameObject imageReaction;              // The image that reacts to gestures

    private bool isWaving = false;
    private float waveTimer = 0f;
    private float waveThreshold = 2.0f;           // Time threshold for wave gesture

    void Update()
    {
        // Get the current position of the right hand controller
        Vector3 rightControllerPosition = rightController.positionAction.action.ReadValue<Vector3>();
        Vector3 leftControllerPosition = leftController.positionAction.action.ReadValue<Vector3>();

        // Detecting a simple wave gesture (raising the right hand)
        if (rightControllerPosition.y > 1.5f && !isWaving)
        {
            isWaving = true;
            waveTimer = 0f; // Start timing the wave gesture
        }

        // Increment wave timer when waving
        if (isWaving)
        {
            waveTimer += Time.deltaTime;

            // Gesture is considered complete if the timer exceeds threshold
            if (waveTimer >= waveThreshold)
            {
                ShowReactionImage(); // Trigger image reaction
                isWaving = false;    // Reset waving state
            }
        }

        // Reset wave state if hand is lowered
        if (rightControllerPosition.y < 1.5f)
        {
            isWaving = false;
        }
    }

    // This function shows the image when a gesture is detected
    void ShowReactionImage()
    {
        imageReaction.SetActive(true);
        // You can add more logic to change the image, animate it, etc.
    }
}