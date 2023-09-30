using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class SettingsMenuUI : MonoBehaviour
{
    public static SettingsMenuUI Instance { get; private set; }

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private Button exitSettingsButton;

    [SerializeField] private GameObject pressToRebindObject;

    [SerializeField] private TextMeshProUGUI upText;
    [SerializeField] private TextMeshProUGUI leftText;
    [SerializeField] private TextMeshProUGUI downText;
    [SerializeField] private TextMeshProUGUI rightText;
    [SerializeField] private TextMeshProUGUI jumpText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI toggleHWText;
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI useItemText; 
    [SerializeField] private Button upButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button downButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button jumpButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button toggleHWButton;
    [SerializeField] private Button attackButton;
    [SerializeField] private Button useItemButton;

    private bool isRebinding;




    private void Awake()
    {
        Instance = this;
        isRebinding = true;
        ToggleRebindUI();

        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundController.Instance.SetVolume();
            UpdateUI();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicController.Instance.SetVolume();
            UpdateUI();
        });
        exitSettingsButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        }); 
        upButton.onClick.AddListener(() =>
        {
            RebindControl(InputBindings.Up);
        });
        leftButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        downButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        rightButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        jumpButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        interactButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        toggleHWButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        attackButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        useItemButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }

    private void Start()
    {
        UpdateUI();
    }


    private void UpdateUI()
    {
        soundEffectsText.text = CalculateVolume("Sound Effects", SoundController.Instance.GetVolume());
        musicText.text = CalculateVolume("Music", MusicController.Instance.GetVolume());

        upText.text = InputController.Instance.GetBindingText(InputBindings.Up);
        leftText.text = InputController.Instance.GetBindingText(InputBindings.Left);
        downText.text = InputController.Instance.GetBindingText(InputBindings.Down);
        rightText.text = InputController.Instance.GetBindingText(InputBindings.Right);
        jumpText.text = InputController.Instance.GetBindingText(InputBindings.Jump);
        interactText.text = InputController.Instance.GetBindingText(InputBindings.Interact);
        toggleHWText.text = InputController.Instance.GetBindingText(InputBindings.ToggleHW);
        attackText.text = InputController.Instance.GetBindingText(InputBindings.Attack);
        useItemText.text = InputController.Instance.GetBindingText(InputBindings.UseItem);
    }
    private string CalculateVolume(string volumeType, float volume)
    {
        return volumeType + ": " + (int)(volume * 100) + "%";
    }



    private void RebindControl(InputBindings binding)
    {
        ToggleRebindUI();
        InputController.Instance.RebindBinding(binding, ToggleRebindUI);
        UpdateUI();
    }
    private void ToggleRebindUI()
    {
        isRebinding = !isRebinding;
        pressToRebindObject.SetActive(isRebinding);
    }
    
}
