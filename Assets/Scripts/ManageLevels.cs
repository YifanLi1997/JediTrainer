using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageLevels : MonoBehaviour
{
    [SerializeField] GameObject title;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject trainingMode;
    [SerializeField] GameObject combatMode;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject customizationPanel;
    [SerializeField] GameObject gameoverWinPanel;
    [SerializeField] GameObject gameoverLosePanel;

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

}
