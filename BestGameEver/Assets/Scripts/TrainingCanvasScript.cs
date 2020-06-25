using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingCanvasScript : MonoBehaviour
{
    public GameObject trainingUI;

    void Start()
    {
        Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Resume();
        }
    }

    public void Resume()
    {
        trainingUI.SetActive(false);
        Time.timeScale = 1f;
    }

    void Pause()
    {
        trainingUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
