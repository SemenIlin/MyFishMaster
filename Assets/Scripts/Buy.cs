using UnityEngine;

public class Buy : MonoBehaviour
{
    private Price price = new Price();

    public void BuyLength()
    {
        var price = this.price.NextLengthPrice;
        PlayerProgress.Money -= price;
        UpdateButtonsInteractable();
        UIManager.Instance.UpdateCurrentMoney();

        this.price.IncrementeIndexOfLength();
        UIManager.Instance.UpdateLength(this.price.NextLength, this.price.NextLengthPrice);

        SaveManager.Instance.SaveGame();
    }
    public void BuyStrength()
    {
        var price = this.price.NextStrengthPrice;
        PlayerProgress.Money -= price;
        UpdateButtonsInteractable();
        UIManager.Instance.UpdateCurrentMoney();

        this.price.IncrementeIndexOfStrength();
        UIManager.Instance.UpdateStrength(this.price.NextStrength, this.price.NextStrengthPrice);

        SaveManager.Instance.SaveGame();
    }

    private void UpdateButtonsInteractable()
    {
        UIManager.Instance.SetInteractableStrengthButton(this.price.HasBuyStrength());
        UIManager.Instance.SetInteractableLengthButton(this.price.HasBuyLength());
    }
}