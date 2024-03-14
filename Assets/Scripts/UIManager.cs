using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameWinPanel;
    [Header("Knife Count Display")]
    [SerializeField] private GameObject panelKnives;
    [SerializeField] private GameObject iconKnife;
    [SerializeField] private Color usedKnifeIconColor;
    private int knifeIconIndexToChange = 0;

    public void ShowGameOverPanel()
    {
        SoundManager.Instance.PlaySFX("GameOver");
        gameOverPanel.SetActive(true);
    }
    public void ShowGameWinPanel()
    {
        gameWinPanel.SetActive(true);
    }
    public void SetInitialDisplayedKnifeCount(int count)
    {
        for(int i=0; i<count; i++)
        {
            Instantiate(iconKnife, panelKnives.transform);
        }
    }
    public void DecreamentDisplayKnifeCount()
    {
        panelKnives.transform.GetChild(knifeIconIndexToChange++).GetComponent<Image>().color=usedKnifeIconColor;
    }
   
}
