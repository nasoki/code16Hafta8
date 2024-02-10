using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorControl : MonoBehaviour
{
    public GameObject ExitTile, EndOfLevelPanel, finalText, failPanel;
    public ParticleSystem particles;
    public float totalTime = 300;
    public TMP_Text timeText, endOfLevelPanelTimeText;
    public AudioClip WinnerClip, FailClip;
    public AudioSource MazeSource;
    public bool GameOn = true;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        particles.Pause();
        GameOn = true;
        failPanel.SetActive(false);
        EndOfLevelPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOn)
        {
            totalTime -= Time.deltaTime;
        }
        timeText.text = totalTime.ToString("0");
        if (totalTime < 20)
        {
            animator.Play("TimerTextUpbeat");
            timeText.color = Color.red;
        }
        if(totalTime <= 0)
        {
            StartCoroutine(LevelFail());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Green") || other.CompareTag("Blue") || other.CompareTag("Red"))
        {
            if (GameObject.Find(other.tag + "Door") != null)
            {
                GameObject.Find(other.tag + "Door").SetActive(false);
                GameObject.Find(other.tag + "Text").SetActive(false);
            }
        }
        if (other.CompareTag("Key"))
        {
            ExitTile.SetActive(true);
            finalText.SetActive(true);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Exit"))
        {
            StartCoroutine(LevelEnd());
        }
    }
    IEnumerator LevelEnd()
    {
        particles.Play();
        GameOn = false;
        MazeSource.PlayOneShot(WinnerClip);
        yield return new WaitForSeconds(2);
        endOfLevelPanelTimeText.text = "And you still got " + totalTime.ToString("0") + " seconds left!";
        EndOfLevelPanel.SetActive(true);
        Time.timeScale = .0f;
        Cursor.lockState = CursorLockMode.None;
    }
    IEnumerator LevelFail()
    {
        GameOn = false;
        MazeSource.PlayOneShot(FailClip);
        yield return new WaitForSeconds(4.5f);
        failPanel.SetActive(true);
        Time.timeScale = .0f;
        Cursor.lockState = CursorLockMode.None;

    }
    public void RestartButton()
    {
        SceneManager.LoadScene(0);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
