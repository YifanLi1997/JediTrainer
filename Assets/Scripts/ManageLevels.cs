using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ManageLevels : MonoBehaviour
{
    [SerializeField] GameObject title;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject trainingMode;
    [SerializeField] GameObject combatMode;
    [SerializeField] GameObject bossMode;
    [SerializeField] GameObject TutoMode;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject customizationPanel;
    [SerializeField] GameObject infoPanel;
    [SerializeField] GameObject gameoverWinPanel;
    [SerializeField] GameObject gameoverLosePanel;

    public AudioClip waiting;

    public Image lifebar;
    private int counter;

    private bool trainigdone, combatdone, bossdone, tutorial;

    public void OnTraningMode()
    {
        title.SetActive(false);
        menuPanel.SetActive(false);
        trainingMode.SetActive(true);
        combatMode.SetActive(false);
    }

    public void OnCombatMode()
    {
        title.SetActive(false);
        menuPanel.SetActive(false);
        trainingMode.SetActive(false);
        combatMode.SetActive(true);
    }

    public void OnPause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void OnResume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        customizationPanel.SetActive(false);
    }

    public void OnCustomization()
    {
        pausePanel.SetActive(false);
        customizationPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        title.SetActive(true);
        menuPanel.SetActive(true);
        trainingMode.SetActive(false);
        combatMode.SetActive(false);
    }

    public void Start()
    {
        title.SetActive(true);
        menuPanel.SetActive(true);
        counter = 0;
        trainigdone = false;
        combatdone = false;
        bossdone = false;
        tutorial = false;
        this.gameObject.GetComponent<AudioSource>().clip = waiting;
        this.gameObject.GetComponent<AudioSource>().Play();
    }

    public void Update()
    {
        counter++;
        if (lifebar.rectTransform.offsetMax.x < -150)
        {
            title.SetActive(true);
            gameoverLosePanel.SetActive(true);
            trainingMode.SetActive(false);
            bossMode.SetActive(false);
            combatMode.SetActive(false);
            if (counter > 1000)
                counter = 0;
            if (counter > 990)
            {
                EditorApplication.isPlaying = false;
            }

        }
        else
        {
            if (!tutorial && counter > 1000)
            {
                this.gameObject.GetComponent<AudioSource>().Stop();
                menuPanel.SetActive(false);
                TutoMode.SetActive(true);
                customizationPanel.SetActive(true);
                tutorial = true;
            }


            if (tutorial && !TutoMode.activeInHierarchy)
            {

                if (counter > 300)
                {
                    infoPanel.SetActive(true);
                    counter = 0;
                }
                if (counter > 290)
                {
                    infoPanel.SetActive(false);
                    trainingMode.SetActive(true);
                    trainigdone = true;
                    counter = 0;
                }
            }

            if (!combatdone && trainigdone && !trainingMode.activeInHierarchy)
            {
                if (counter > 200)
                    counter = 0;
                if (counter > 190)
                {
                    combatMode.SetActive(true);
                    combatdone = true;
                }
            }
            if (!bossdone && combatdone && !combatMode.activeInHierarchy)
            {
                if (counter > 200)
                    counter = 0;
                if (counter > 190)
                {
                    bossMode.SetActive(true);
                    bossdone = true;
                }
            }
            if (bossMode == null)
            {
                gameoverWinPanel.SetActive(true);
                if (counter > 1000)
                    counter = 0;
                if (counter > 990)
                {
                    EditorApplication.isPlaying = false;
                }
            }
        }
        
    }

}
