using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject powerUpButton = null;
    [SerializeField] private GameObject retryButton = null;
    [SerializeField] private GameObject successfulFinishPanel = null;
    [SerializeField] private GameObject finishText = null;
    [SerializeField] private GameObject nextLevelButton = null;

    //private void Awake()
    //{
    //    DroppedCube.init();

    //}
    private void OnEnable()
    {
        DroppedCube.init();
    }

    public void successfulFinish(float height)
    {
        successfulFinishPanel.SetActive(true);
        nextLevelButton.SetActive(true);

        int totalCoins = (int)(height * 10 * Tower.SCALE);

        string finishText = $"Coins this run -> {totalCoins}\n";

        if (PlayerPrefs.HasKey("total_coins"))
        {
            int coinsInDb = PlayerPrefs.GetInt("total_coins");
            totalCoins += coinsInDb;
        }

        finishText += $"Coins total -> {totalCoins}";
        this.finishText.GetComponent<Text>().text = finishText;

        PlayerPrefs.SetInt("total_coins", totalCoins);

    }

    public void gameOver()
    {
        powerUpButton.SetActive(false);
        retryButton.SetActive(true);
    }

    public void onRetryButtonClick()
    {
        retryButton.SetActive(false);
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
