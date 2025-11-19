using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    public RectTransform heightTarget;
    public float increaseHeight = 20f;
    private MainMoneyShow mainMoneyShow;
    public int price = 2400;

    // Thay vì sprite → dùng GameObject
    public GameObject iconNormal;
    public GameObject iconNotEnough;

    public TextMeshProUGUI priceText;
    public GameObject notEnoughText; // cái text “Not enough money”

    private void OnEnable()
    {
        mainMoneyShow = GameObject.FindAnyObjectByType<MainMoneyShow>();
        Check();
    }

    public void OnClickUpgrade()
    {
        int currentMoney = DataManager.currentData.Gold;

        if (currentMoney < price)
        {
            NotEnoughMoney();
            AdsManager.Instance.ShowRewarded(DoUpgrade);
            return;
        }
        // Đủ tiền → upgrade
        DoUpgrade();
    }
    public void Check()
    {
        int currentMoney = DataManager.currentData.Gold;
        if (currentMoney < price)
        {
            NotEnoughMoney();
            return;
        }
        // Đủ tiền → upgrade
        UpdateDisplay();
    }
    public void NotEnoughMoney()
    {
        // Không đủ tiền → bật các object thông báo
        iconNormal.SetActive(false);
        iconNotEnough.SetActive(true);
        priceText.gameObject.SetActive(false);
        notEnoughText.SetActive(true);
    }
    private void DoUpgrade()
    {
        // Trừ tiền
        DataManager.currentData.Gold -= price;
        DataManager.SaveAll();
        mainMoneyShow.ShowInJson();//show money after upgrade

        // Tăng chiều cao
        heightTarget.sizeDelta = new Vector2(
            heightTarget.sizeDelta.x,
            heightTarget.sizeDelta.y + increaseHeight
        );

        // Tăng giá
        price += 1200;

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        // Khi đủ tiền → bật lại trạng thái bình thường
        iconNormal.SetActive(true);
        iconNotEnough.SetActive(false);

        priceText.gameObject.SetActive(true);
        notEnoughText.SetActive(false);

        priceText.text = price + "$";
    }
}
