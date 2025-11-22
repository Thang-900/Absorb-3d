using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    public RectTransform heightTarget;
    public float increaseHeight = 20f;
    private MainMoneyShow mainMoneyShow;
    public int price = 2400;
    private IndexShow indexShow;

    public bool isSpeedUpgrade;
    public bool isIncomeUpgrade;
    public bool isVacuumUpgrade;
    // Thay vì sprite → dùng GameObject
    public GameObject iconNormal;
    public GameObject iconNotEnough;

    public TextMeshProUGUI priceText;
    public GameObject notEnoughText; // cái text “Not enough money”
    public TextMeshProUGUI levelTab;
    private void OnEnable()
    {
        mainMoneyShow = GameObject.FindAnyObjectByType<MainMoneyShow>();
        indexShow = GameObject.FindAnyObjectByType<IndexShow>();
        SetOnStart();
        Check();
    }

    public void OnClickUpgrade()
    {
        int currentMoney = DataManager.currentData.Gold;

        if (currentMoney < price)
        {
            NotEnoughMoney();

            if (AdsManager.Instance.rewardedReady)
            {
                AdsManager.Instance.ShowRewarded(DoUpgrade);
            }
            else
            {
                Debug.Log("Rewarded NOT READY YET");
            }
            return;
        }

        // Đủ tiền → upgrade
        DoUpgrade();
        SetOnStart();
        Check();
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
        if(DataManager.currentData.Gold>price)
        {
            DataManager.currentData.Gold -= price;
        }
        
        if (isSpeedUpgrade)
        {
            DataManager.currentData.TabIncomeLevel += 1;
            DataManager.currentData.SpeedRateOnStart += 0.1f;
            levelTab.text = DataManager.currentData.TabIncomeLevel.ToString() + " Level";
        }
        else if (isIncomeUpgrade)
        {
            DataManager.currentData.TabVacuumLevel += 1;
            DataManager.currentData.IncomeRateOnStart += 0.1f;

            levelTab.text = DataManager.currentData.TabVacuumLevel.ToString() + " Level";

        }
        else if (isVacuumUpgrade)
        {
            DataManager.currentData.TabSpeedLevel += 1;
            DataManager.currentData.VacuumRateOnStart += 0.1f;

            levelTab.text = DataManager.currentData.TabSpeedLevel.ToString() + " Level";
        }
        DataManager.SaveAll();
        indexShow.SetAllIndexShow();
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

        priceText.text = price.ToString();
    }
    private void SetOnStart()
    {
        if (isSpeedUpgrade)
        {
            float index = DataManager.currentData.TabIncomeLevel;
            heightTarget.sizeDelta = new Vector2(
                heightTarget.sizeDelta.x, index * increaseHeight
            );
            levelTab.text = DataManager.currentData.TabIncomeLevel.ToString() + " Level";
            price = 2400 + (int)index * 1200; 
            priceText.text = price.ToString();
        }
        else if (isIncomeUpgrade)
        {
            float index = DataManager.currentData.TabVacuumLevel;
            heightTarget.sizeDelta = new Vector2(
                heightTarget.sizeDelta.x,  index * increaseHeight
            );
            levelTab.text = DataManager.currentData.TabVacuumLevel.ToString() + " Level";
            price = 2400 + (int)index * 1200;
            priceText.text = price.ToString();

        }
        else if (isVacuumUpgrade)
        {
            float index = DataManager.currentData.TabSpeedLevel;

            heightTarget.sizeDelta = new Vector2(
                heightTarget.sizeDelta.x,index * increaseHeight
            );
            levelTab.text = DataManager.currentData.TabVacuumLevel.ToString() + " Level";
            price = 2400 + (int)index * 1200;
            priceText.text = price.ToString();
        }
        indexShow.SetAllIndexShow();
    }
}
