using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HDragonController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float verticalSpeed;

    private FixedJoystick fixedJoystick;
    private Rigidbody rigidBody;

    //controls the up down movement
    private Slider verticalSlider;

    //Reference the stop button
    private Button stopButton;

    private void OnEnable()
    {
        fixedJoystick = FindObjectOfType<FixedJoystick>();
        rigidBody = gameObject.GetComponent<Rigidbody>();

        verticalSlider = GameObject.Find("Slider").GetComponent<Slider>();
        stopButton = GameObject.Find("Stopper Button").GetComponent<Button>();

        // Set up the slider
        verticalSlider.minValue = -1f; // Minimum value for downward movement
        verticalSlider.maxValue = 1f;  // Maximum value for upward movement
        verticalSlider.value = 0f;     // Default value to the middle (no movement)

        // Add listener to the slider for value changes
        verticalSlider.onValueChanged.AddListener(OnVerticalSliderValueChanged);

        if(stopButton != null)
        {
            stopButton.onClick.AddListener(StopVerticalMovement);
        }
        else
        {
            Debug.LogError("Stopper Button not found!");
        }
    }


    private void FixedUpdate()
    {
        float xVal = fixedJoystick.Horizontal;
        float yVal = fixedJoystick.Vertical;

        Debug.Log($"Joystick Input - Horizontal: {xVal}, Vertical: {yVal}"); // Log joystick input


        Vector3 movement = new Vector3(xVal, 0, yVal);
        rigidBody.velocity = new Vector3(movement.x * speed, rigidBody.velocity.y, movement.z * speed); // Update only x and z, keep y as is for vertical movement


        if(xVal != 0 && yVal != 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(xVal, yVal)*Mathf.Rad2Deg, transform.eulerAngles.z);

            Debug.Log($"Joystick Input - Horizontal: {xVal}, Vertical: {yVal}"); // Log joystick input
        }
    }

    // Handle vertical movement based on slider value
    private void OnVerticalSliderValueChanged(float value)
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, value * verticalSpeed, rigidBody.velocity.z);
    }

    // Function to handle up movement using the button
    private void MoveUp()
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, verticalSpeed, rigidBody.velocity.z);
    }

    // Function to handle down movement using the button
    private void MoveDown()
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, -verticalSpeed, rigidBody.velocity.z);
    }

    // Function to stop vertical movement
    private void StopVerticalMovement()
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);
        verticalSlider.value = 0; // Reset the slider value to 0
        Debug.Log("Vertical movement stopped!");
    }

}
