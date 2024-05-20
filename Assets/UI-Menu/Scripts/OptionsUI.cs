using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{

    public static OptionsUI Instance {  get; private set; }

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;

    private void Awake()
    {
        Instance = this;
        soundEffectsButton.onClick.AddListener(() => //When this button is pressed, do this!
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() => 
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() =>
        {
            GamePauseUI.Instance.Show();
            Hide();
        });
    }

    private void Start()
    {
        GameInput.Instance.OnGameUnpaused += GameInput_OnGameUnpaused;
        UpdateVisual();
        Hide();
    }

    private void GameInput_OnGameUnpaused(object sender, System.EventArgs e) //Listening to this event lets you exit the options via ESC
    {
        Hide();
    }

    private void UpdateVisual() //Make sure the right digit is show on the button
    {
        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

    }


    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
