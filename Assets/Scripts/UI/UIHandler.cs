using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI distanceTraveledText;
    [SerializeField] TextMeshProUGUI gasolineText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] CanvasGroup gameOverCanvasGroup;
    [SerializeField] string carSelectionSceneName = "Main menu";

    [Header("Tutorial")]
    [SerializeField] CanvasGroup tutorialCanvasGroup;
    [SerializeField] float tutorialDuration = 4f;
    [SerializeField] float tutorialFadeOutSpeed = 2f;

    //Reference
    CarHandler playerCarHandler;

    void Start()
    {
        gameOverCanvasGroup.interactable = false;
        gameOverCanvasGroup.alpha = 0;

        playerCarHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<CarHandler>();
        playerCarHandler.OnPlayerCrashed += PlayerCarHandler_OnPlayerCrashed;
        playerCarHandler.OnGasolineChanged += UpdateGasolineUI;
        UpdateGasolineUI(playerCarHandler.CurrentGasoline);

        if (tutorialCanvasGroup != null)
        {
            tutorialCanvasGroup.alpha = 1f;
            tutorialCanvasGroup.interactable = false;
            tutorialCanvasGroup.blocksRaycasts = false;
            StartCoroutine(TutorialSequenceCO());
        }
    }

    void Update()
    {
        distanceTraveledText.text = playerCarHandler.DistanceTraveled.ToString("000000");
    }

    void UpdateGasolineUI(float gasoline)
    {
        if (gasolineText != null)
            gasolineText.text = $"FUEL: {gasoline:0}";
    }

    IEnumerator TutorialSequenceCO()
    {
        yield return new WaitForSeconds(tutorialDuration);

        while (tutorialCanvasGroup.alpha > 0f)
        {
            tutorialCanvasGroup.alpha = Mathf.MoveTowards(tutorialCanvasGroup.alpha, 0f, Time.deltaTime * tutorialFadeOutSpeed);
            yield return null;
        }

        tutorialCanvasGroup.gameObject.SetActive(false);
    }

    IEnumerator StartGameOverAnimationCO()
    {
        yield return new WaitForSecondsRealtime(3.0f);

        gameOverCanvasGroup.interactable = true;

        while (gameOverCanvasGroup.alpha < 0.8f)
        {
            gameOverCanvasGroup.alpha = Mathf.MoveTowards(gameOverCanvasGroup.alpha, 1.0f, Time.deltaTime * 2);
            yield return null;
        }
    }

    void PlayerCarHandler_OnPlayerCrashed(CarHandler obj)
    {
        gameOverText.text = $"DISTANCE: {distanceTraveledText.text}";
        StartCoroutine(StartGameOverAnimationCO());
    }

    public void onRestartClicked()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void onCarSelectionClicked()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(carSelectionSceneName);
    }
}
