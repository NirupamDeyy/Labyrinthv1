using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class ActionUIconrol : MonoBehaviour
{
    public GameObject pauseMenuUI;
    [SerializeField] private GameObject player;
    [SerializeField]
    private Button resume, restart, exit;
    [SerializeField] private TMP_Text winText;
    public Timer timer;
    private bool isPaused = false;
    private PlayerShootControl playerShootControl;
    private MissileShootControl missileShootControl;
    private GunSwitchControl gunSwitchControl;


    public void Start()
    {
        pauseMenuUI.SetActive(false);
        playerShootControl = player.GetComponent<PlayerShootControl>();
        missileShootControl = player.GetComponent<MissileShootControl>();
        gunSwitchControl = GetComponent<GunSwitchControl>();
        resume.onClick.AddListener(() => Resume());
        restart.onClick.AddListener(() => Restart()); 
        exit.onClick.AddListener(() => ExitGame());
    }

    private void ExitGame()
    {
        Application.Quit();

    }

    private void Restart()
    {
        Resume();
        SceneManager.LoadScene("Scene1");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f; // Pause the game
        pauseMenuUI.SetActive(true); // Show pause menu UI
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerShootControl.enabled = false;
        missileShootControl.enabled = false; 
        gunSwitchControl.enabled = false;
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f; // Resume normal time
        pauseMenuUI.SetActive(false); // Hide pause menu UI
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerShootControl.enabled = true;
        missileShootControl.enabled = true;
        gunSwitchControl.enabled = true;
    }

    public void ShowWinText(int totalscore)
    {
        float time = timer.GetTimePassed();
        winText.text = "MISSION PASSED, " + totalscore + " COLLECTED IN: " + time + " mins. ";
    }

    public void ShowTimeUpText()
    {
       winText.text = "MISSION FAILED, TIME IS UP.";
    }

}
