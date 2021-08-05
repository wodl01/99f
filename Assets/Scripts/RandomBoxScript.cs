using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBoxScript : MonoBehaviour
{
    [Header("Manager")]
    public ItemInfoManager itemInfoManager;

    [Header("BoxShape")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite onSprite;
    [SerializeField] Sprite offSprite;


    [Header("BoxInteract")]
    [SerializeField] bool canInteract;
    [SerializeField] bool isOpen;
    [SerializeField] bool itemOut;
    [SerializeField] GameObject buttonIconObject;
    [SerializeField] GameObject itemInfoPanel;
    [SerializeField] GameObject itemSellPanel;

    [Header("ItemOB")]
    [SerializeField] GameObject itemObject;
    [SerializeField] SpriteRenderer itemSpriteRenderer;

    [Header("ItemPercentage")]
    [SerializeField] int normalPer;
    [SerializeField] int rarePer;
    [SerializeField] int epicPer;
    [SerializeField] int legendPer;
    [SerializeField] Color normalColor;
    [SerializeField] Color rareColor;
    [SerializeField] Color epicColor;
    [SerializeField] Color legendColor;

    [Header("ItemCode")]
    [SerializeField] int selectedItemCode;
    [SerializeField] List<int> normalItemCodes;
    [SerializeField] List<int> rareItemCodes;
    [SerializeField] List<int> epicItemCodes;
    [SerializeField] List<int> legendItemCodes;

    [Header("ItemInfoPanel")]
    [SerializeField] Text itemNameText;
    [SerializeField] Text itemGradeText;
    [SerializeField] Text itemInfoText;
    [SerializeField] Text[] itemNormalOptionTexts;
    [SerializeField] Text[] itemSpecialOptionTexts;
    [SerializeField] Text itemSellPriceText;

    [Header("BoxAnimation")]
    [SerializeField] Animator boxAnimator;
    private void Start()
    {
        itemObject.SetActive(false);
        itemInfoPanel.SetActive(false);
        itemSellPanel.SetActive(false);
        buttonIconObject.SetActive(false);

        for (int i = 0; i < itemInfoManager.ItemInfos.Count; i++)
        {
            switch (itemInfoManager.ItemInfos[i].Grade)
            {
                case 0:
                    legendItemCodes.Add(i);
                    break;
                case 1:
                    epicItemCodes.Add(i);
                    break;
                case 2:
                    rareItemCodes.Add(i);
                    break;
                case 3:
                    normalItemCodes.Add(i);
                    break;
            }
        }

        isOpen = false;
        canInteract = false;
        itemOut = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canInteract = true;

            if (isOpen == false)
            {
                //spriteRenderer.sprite = onSprite;
                buttonIconObject.SetActive(true);
            }
            else if(itemOut)
            {
                ItemInfoUpdate();
                itemInfoPanel.SetActive(true);
                itemSellPanel.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canInteract = false;

            if (isOpen == false)
            {
                //spriteRenderer.sprite = offSprite;
                buttonIconObject.SetActive(false);
            }
            else if (itemOut)
            {
                itemInfoPanel.SetActive(false);
                itemSellPanel.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (canInteract && !itemOut && Input.GetKeyDown(KeyCode.F))
        {
            isOpen = true;
            //spriteRenderer.sprite = offSprite;

            buttonIconObject.SetActive(false);

            RandomItemPick();
        }
        if(canInteract && itemOut && Input.GetKey(KeyCode.E))
        {
            itemInfoManager.GetItem(selectedItemCode);
            Destroy(gameObject);
        }
        else if(canInteract && itemOut && Input.GetKey(KeyCode.Q))
        {
            itemInfoManager.SellItem(selectedItemCode);
            Destroy(gameObject);
        }
    }

    void RandomItemPick()
    {
        int randomRate = Random.Range(0, 101);
        if(0 <= randomRate && randomRate < normalPer)
        {
            int randomItemNum = Random.Range(0, normalItemCodes.Count);
            selectedItemCode = normalItemCodes[randomItemNum];
            Debug.Log("�Ϲ�");
        }
        else if(normalPer <= randomRate && randomRate < normalPer + rarePer)
        {
            int randomItemNum = Random.Range(0, rareItemCodes.Count);
            selectedItemCode = rareItemCodes[randomItemNum];
            Debug.Log("����");
        }
        else if(normalPer + rarePer <= randomRate && randomRate < normalPer + rarePer + epicPer)
        {
            int randomItemNum = Random.Range(0, epicItemCodes.Count);
            selectedItemCode = epicItemCodes[randomItemNum];
            Debug.Log("����");
        }
        else if (normalPer + rarePer + epicPer <= randomRate && randomRate < normalPer + rarePer + epicPer + legendPer)
        {
            int randomItemNum = Random.Range(0, legendItemCodes.Count);
            selectedItemCode = legendItemCodes[randomItemNum];
            Debug.Log("����");
        }

        //itemSpriteRenderer.sprite = itemInfoManager.ItemInfos[selectedItemCode].ItemShape;
        itemObject.SetActive(true);
        boxAnimator.SetBool("Open", true);
    }

    void ItemCameOut()
    {
        itemOut = true;

        if (canInteract)
        {
            ItemInfoUpdate();
            itemInfoPanel.SetActive(true);
            itemSellPanel.SetActive(true);
        }
        else
        {
            itemInfoPanel.SetActive(false);
            itemSellPanel.SetActive(false);
        }
    }

    void ItemInfoUpdate()
    {
        itemNameText.text = itemInfoManager.ItemInfos[selectedItemCode].Name;
        itemInfoText.text = itemInfoManager.ItemInfos[selectedItemCode].ItemInfo;
        switch (itemInfoManager.ItemInfos[selectedItemCode].Grade)
        {
            case 0:
                itemGradeText.text = "����";
                itemGradeText.color = legendColor;
                break;
            case 1:
                itemGradeText.text = "����";
                itemGradeText.color = epicColor;
                break;
            case 2:
                itemGradeText.text = "���";
                itemGradeText.color = rareColor;
                break;
            case 3:
                itemGradeText.text = "�Ϲ�";
                itemGradeText.color = normalColor;
                break;
        }

        for (int i = 0; i < itemNormalOptionTexts.Length; i++)
        {
            itemNormalOptionTexts[i].gameObject.SetActive(false);
        }

        if (itemInfoManager.ItemInfos[selectedItemCode].MoveSpeed != 0)
        {
            bool input = false;
            for (int i = 0; i < itemNormalOptionTexts.Length; i++)
            {
                if (!itemNormalOptionTexts[i].gameObject.activeSelf && !input)
                {
                    itemNormalOptionTexts[i].text = "�̵��ӵ� " + itemInfoManager.ItemInfos[selectedItemCode].MoveSpeed + "%";
                    itemNormalOptionTexts[i].gameObject.SetActive(true);
                    input = true;

                }
            }
        }
        if (itemInfoManager.ItemInfos[selectedItemCode].AttackSpeed != 0)
        {
            bool input = false;
            for (int i = 0; i < itemNormalOptionTexts.Length; i++)
            {
                if (!itemNormalOptionTexts[i].gameObject.activeSelf && !input)
                {
                    itemNormalOptionTexts[i].text = "���ݼӵ� " + itemInfoManager.ItemInfos[selectedItemCode].AttackSpeed + "%";
                    itemNormalOptionTexts[i].gameObject.SetActive(true);
                    input = true;

                }
            }
        }
        if (itemInfoManager.ItemInfos[selectedItemCode].BulletAmount != 0)
        {
            bool input = false;
            for (int i = 0; i < itemNormalOptionTexts.Length; i++)
            {
                if (!itemNormalOptionTexts[i].gameObject.activeSelf && !input)
                {
                    itemNormalOptionTexts[i].text = "�Ѿ˰��� " + itemInfoManager.ItemInfos[selectedItemCode].BulletAmount;
                    itemNormalOptionTexts[i].gameObject.SetActive(true);
                    input = true;

                }
            }
        }

        itemSellPriceText.text = "+" + itemInfoManager.ItemInfos[selectedItemCode].SellPrice.ToString() + "G";
    }
}
