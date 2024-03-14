using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UIManager))]
public class GameController : MonoBehaviour
{
    #region Dependencies
    public static GameController Instance { get; private set; }
    [SerializeField] private int knifeCount;
    [Header("Knife Spawning")]
    [SerializeField] private Vector2 knifeSpawnPosition;
    [SerializeField] private GameObject knifeObject;
    [SerializeField] private GameObject brokenWheelParent;
    [SerializeField] private GameObject wheel;
    [SerializeField] private Transform knifeParent;
    public UIManager uiManager { get; private set; }

    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        Instance = this;
        uiManager = GetComponent<UIManager>();
    }
    private void Start()
    {
        uiManager.SetInitialDisplayedKnifeCount(knifeCount);
        SpawnKnife();
    }

    #endregion

    #region Functionality Methods
    public void OnSuccessfulKnifeHit()
    {
        if (knifeCount > 0)
        {
            SpawnKnife();
        }
        else
        {
            StartGameOverSequence(true);
        }
    }


    private void SpawnKnife()
    {
        knifeCount--;
        Instantiate(knifeObject, knifeSpawnPosition, Quaternion.identity, knifeParent) ; 
    }
    public void StartGameOverSequence(bool win)
    {
       StartCoroutine(GameOverSequenceCoroutine(win));
    }
    private IEnumerator GameOverSequenceCoroutine(bool win)
    {
        if(win)
        {
            wheel.SetActive(false);
            SoundManager.Instance.PlaySFX("WoodenBreak");
            yield return new WaitForSeconds(0.1f);
            brokenWheelParent.SetActive(true);
            yield return new WaitForSecondsRealtime(2f);
            uiManager.ShowGameWinPanel();
        }
        else
        {
            yield return new WaitForSecondsRealtime(1f);
            uiManager.ShowGameOverPanel();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
   
    #endregion
}
