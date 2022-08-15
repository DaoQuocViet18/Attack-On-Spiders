using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject playScreen;
    public bool paused;

    private float score;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the user has pressed the P key
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ChangePaused();
        }
    }


    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            playScreen.SetActive(false);
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            playScreen.SetActive(true);
            Time.timeScale = 1;
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
}
