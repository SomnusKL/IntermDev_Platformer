using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Die : MonoBehaviour
{
    private Scene previousScene;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            // Code to execute when space is pressed
            Debug.Log("Space key was pressed");
            string previousScene = PlayerPrefs.GetString("PreviousScene");
            SceneManager.LoadScene(previousScene);
        }
    }
}
