using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Moon")) // Check if the dragon collides with a moon
        {
            Debug.Log("Moon collected!"); // Debugging: Check if this prints
            MoonManager.instance.CollectMoon(); // Call MoonManager to update count
            Destroy(other.gameObject); // Remove the collected moon
        }
    }
}
