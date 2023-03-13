using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    AudioSource audiosource;

    int delay;

    bool canPlayMusic = true;
    bool canPauseMusic = true;

    private UI_PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = FindObjectOfType<UI_PauseMenu>();

        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audiosource.isPlaying && canPlayMusic)
        {
            canPlayMusic = false;

            StartCoroutine(DelayBetweenMusic());
        }

        if (pauseMenu.isPaused && canPauseMusic)
        {
            audiosource.volume -= 0.1f;

            canPauseMusic = false;
        }

        if (!pauseMenu.isPaused && !canPauseMusic)
        {
            audiosource.volume += 0.1f;

            canPauseMusic = true;
        }
    }

    IEnumerator DelayBetweenMusic()
    {
        delay = Random.Range(3, 10);

        yield return new WaitForSeconds(delay);

        audiosource.Play();

        canPlayMusic = true;
    }
}
