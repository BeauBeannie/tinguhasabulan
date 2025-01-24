using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HDragonController : MonoBehaviour
{
    [SerializeField] private float speed; // Horizontal and forward/backward speed
    [SerializeField] private float verticalSpeed; // Vertical movement speed
    [SerializeField] private Slider verticalSlider; // Reference to the vertical slider

    private FixedJoystick fixedJoystick;
    private Rigidbody rigidBody;

    private bool isDraggingSlider = false; // Tracks whether the slider is being held

    private void OnEnable()
    {
        // Initialize joystick and Rigidbody
        fixedJoystick = FindObjectOfType<FixedJoystick>();
        rigidBody = GetComponent<Rigidbody>();

        verticalSlider = GameObject.Find("Slider").GetComponent<Slider>();


        // Set slider range and default value
        verticalSlider.minValue = -1f;
        verticalSlider.maxValue = 1f;
        verticalSlider.value = 0f;

        // Add listener for value changes
        verticalSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void FixedUpdate()
    {
        // Handle horizontal movement with joystick
        float xVal = fixedJoystick.Horizontal;
        float yVal = fixedJoystick.Vertical;

        Vector3 movement = new Vector3(xVal, 0, yVal);
        rigidBody.velocity = new Vector3(movement.x * speed, rigidBody.velocity.y, movement.z * speed);

        // Rotate the dragon to face movement direction
        if (xVal != 0 || yVal != 0)
        {
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                Mathf.Atan2(xVal, yVal) * Mathf.Rad2Deg,
                transform.eulerAngles.z
            );
        }

        // Reset slider value to 0 smoothly when not dragging
        if (!isDraggingSlider && Mathf.Abs(verticalSlider.value) > 0.01f)
        {
            verticalSlider.value = Mathf.Lerp(verticalSlider.value, 0f, Time.deltaTime * 5f);
        }
    }

    // Called when the slider value changes (while being dragged)
    private void OnSliderValueChanged(float value)
    {
        isDraggingSlider = true; // Slider is being interacted with
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, value * verticalSpeed, rigidBody.velocity.z);
    }

    private void LateUpdate()
    {
        // Detect when the slider interaction is released
        if (Input.GetMouseButtonUp(0) || Input.touchCount == 0)
        {
            isDraggingSlider = false; // User has released the slider
        }
    }
}
