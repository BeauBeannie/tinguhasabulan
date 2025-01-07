using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSwitch : MonoBehaviour {
    public GameObject switchOn, switchOff;

    public void OnChangeValue() {
        bool onoffSwitch = gameObject.GetComponent<Toggle>().isOn;
        if (onoffSwitch)
        {
            switchOn.SetActive(true);
            switchOff.SetActive(false);
        }
        if (!onoffSwitch)
        {
            switchOn.SetActive(false);
            switchOff.SetActive(true);

        }
    }
}

