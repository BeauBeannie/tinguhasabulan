using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class PrefabCreator : MonoBehaviour
{
    [SerializeField] private GameObject dragonPrefab;
    [SerializeField] private Vector3 prefabOffset;

    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Slider verticalSlider;
    [SerializeField] private Button stopButton;

    private GameObject dragon;
    private ARTrackedImageManager aRTrackedImageManager;

    private void OnEnable()
    {
        aRTrackedImageManager = GetComponent<ARTrackedImageManager>();
        aRTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs obj)
    {
        foreach (ARTrackedImage image in obj.added)
        {
            if (dragon == null) // Prevent multiple dragons from spawning
            {
                dragon = Instantiate(dragonPrefab, image.transform);
                dragon.transform.localPosition += prefabOffset;

                // Assign UI controls to the dragon
                HDragonController dragonController = dragon.GetComponent<HDragonController>();
                if (dragonController != null)
                {
                    dragonController.InitializeControls(joystick, verticalSlider, stopButton);
                }
                else
                {
                    Debug.LogError("HDragonController not found on dragon prefab!");
                }
            }
        }
    }

    private void OnDisable()
    {
        if (aRTrackedImageManager != null) // ✅ Prevent errors if null
        {
            aRTrackedImageManager.trackedImagesChanged -= OnImageChanged;
        }

        if (dragon != null)
        {
            Destroy(dragon);
            dragon = null;
        }

        // ✅ Ensure AR properly resets when transitioning levels
        StartCoroutine(ResetARTracking());
    }

    private IEnumerator ResetARTracking()
    {
        if (aRTrackedImageManager != null) // ✅ Prevent errors
        {
            aRTrackedImageManager.enabled = false;
        }

        yield return new WaitForSeconds(0.5f); // Short delay to ensure proper reset

        if (aRTrackedImageManager != null)
        {
            aRTrackedImageManager.enabled = true;
        }

        // ✅ Destroy the dragon in case tracking reset fails
        if (dragon != null)
        {
            Destroy(dragon);
            dragon = null;
        }
    }

}
