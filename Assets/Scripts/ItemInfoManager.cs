using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfoManager : MonoBehaviour
{
    public PlayerState playerState;
    [SerializeField] InventoryManager inventory;

    [System.Serializable]
    public class Items
    {
        public string Name;
        public int Grade;
        public string ItemInfo;
        public Sprite ItemShape;
        public int AttackSpeed;
        public int BulletAmount;
        public int SumAngle;
        public int MoveSpeed;
        public int BuyPrice;
        public int SellPrice;
    }

    public List<Items> ItemInfos;

    public void GetItem(int ItemCode)
    {
        Debug.Log("∏‘¿∫ æ∆¿Ã≈€ :" + ItemInfos[ItemCode].Name);

        if (playerState.attackSpeedPer + ItemInfos[ItemCode].AttackSpeed < 50)
            playerState.attackSpeedPer = 50;
        else
            playerState.attackSpeedPer += ItemInfos[ItemCode].AttackSpeed;

        if (playerState.moveSpeedPer + ItemInfos[ItemCode].MoveSpeed < 100)
            playerState.moveSpeedPer = 100;
        else
            playerState.moveSpeedPer += ItemInfos[ItemCode].MoveSpeed;

        if (playerState.bulletAmount + ItemInfos[ItemCode].BulletAmount < 1)
            playerState.bulletAmount = 1;
        else
            playerState.bulletAmount += ItemInfos[ItemCode].BulletAmount;
    }

    public void SellItem(int ItemCode)
    {
        playerState.gold += ItemInfos[ItemCode].SellPrice;
        inventory.goldAmountText.text = playerState.gold.ToString() + "G";
    }
}
