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

    private Slider verticalSlider;
    private Button stopButton;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();

        // If UI is not assigned, try to find it automatically (for testing in editor)
        if (fixedJoystick == null)
        {
            fixedJoystick = FindObjectOfType<FixedJoystick>();
        }
        if (verticalSlider == null)
        {
            verticalSlider = FindObjectOfType<Slider>();
            if (verticalSlider != null)
            {
                verticalSlider.minValue = -1f;
                verticalSlider.maxValue = 1f;
                verticalSlider.value = 0f;
                verticalSlider.onValueChanged.AddListener(OnVerticalSliderValueChanged);
            }
        }
        if (stopButton == null)
        {
            stopButton = FindObjectOfType<Button>();
            if (stopButton != null)
            {
                stopButton.onClick.AddListener(StopVerticalMovement);
            }
        }
    }

    // This method allows PrefabCreator to set UI references
    public void InitializeControls(FixedJoystick joystick, Slider slider, Button stopBtn)
    {
        fixedJoystick = joystick;
        verticalSlider = slider;
        stopButton = stopBtn;

        if (verticalSlider != null)
        {
            verticalSlider.minValue = -1f;
            verticalSlider.maxValue = 1f;
            verticalSlider.value = 0f;
            verticalSlider.onValueChanged.AddListener(OnVerticalSliderValueChanged);
        }

        if (stopButton != null)
        {
            stopButton.onClick.AddListener(StopVerticalMovement);
        }
    }

    private void FixedUpdate()
    {
        if (fixedJoystick == null) return; // Ensure joystick is assigned

        float xVal = fixedJoystick.Horizontal;
        float yVal = fixedJoystick.Vertical;

        Vector3 movement = new Vector3(xVal, 0, yVal);
        rigidBody.velocity = new Vector3(movement.x * speed, rigidBody.velocity.y, movement.z * speed);

        if (xVal != 0 || yVal != 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(xVal, yVal) * Mathf.Rad2Deg, transform.eulerAngles.z);
        }
    }

    private void OnVerticalSliderValueChanged(float value)
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, value * verticalSpeed, rigidBody.velocity.z);
    }

    private void StopVerticalMovement()
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);
        if (verticalSlider != null)
        {
            verticalSlider.value = 0;
        }
    }
}
