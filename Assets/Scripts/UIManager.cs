using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _fishScreen;
    [SerializeField] private GameObject _mainScreen;
    [SerializeField] private Text _currentMoney;
    [Header("Length button")]
    [SerializeField] private Button _lengthButton;
    [SerializeField] private Text _length;
    [SerializeField] private Text _priceLength;

    [Header("Strength button")]
    [SerializeField] private Button _strengthButton;
    [SerializeField] private Text _strength;
    [SerializeField] private Text _priceStrength;

    [Header("Fish")]
    [SerializeField] private Text _currentFish;
    [SerializeField] private Text _maxFish;

    public static UIManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SetInteractableLengthButton(bool isInteractable)
    {
        _lengthButton.interactable = isInteractable;
    }

    public void SetInteractableStrengthButton(bool isInteractable)
    {
        _strengthButton.interactable = isInteractable;
    }
    public void ShowMainScreen(bool isShow)
    {
        _mainScreen.SetActive(isShow);
        _fishScreen.SetActive(!isShow);
    }
    public void UpdateCurrentMoney()
    {
        _currentMoney.text = PlayerProgress.Money.ToString("C0");
    }
    public void UpdateLength(int length, int price)
    {
        _length.text = length.ToString() + " m";
        _priceLength.text = price.ToString("C0");
    }
    public void UpdateStrength(int strength, int price)
    {
        _strength.text = strength.ToString() + " Fish";
        _priceStrength.text = price.ToString("C0");
    }

    public void UpdateCurrentFish(int quantity)
    {
        _currentFish.text = quantity.ToString();
    }

    public void UpdateMaxFish(int quantity)
    {
        _maxFish.text = quantity.ToString();
    }
}
