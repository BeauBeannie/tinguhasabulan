using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject scanTutorial;
    public GameObject htpTutorial;
    // Start is called before the first frame update
    void Start()
    {
        scanTutorial.SetActive(true);
        htpTutorial.SetActive(false);

    }

}
