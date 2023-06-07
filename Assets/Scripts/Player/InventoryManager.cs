using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    int OpenIndex=1;
    public GameObject inventoryObj;

    public GameObject[] ItemGrids;
    public int[] ItemGridAvailable;

    public Transform ItemUIParent;
    public GameObject ItemUIHusks;

    public Transform QuickUIParent;

    public GameObject[] QuickGrids;
    public int[] QuickGridAvailable;

    public Transform TrashUIParent;

    public GameObject TrashGrid;
    public int TrashGridAvailable;
    void Start()
    {
        ItemGrids = GameObject.FindGameObjectsWithTag("ItemGrid");
        ItemGridAvailable = new int[ItemGrids.Length];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {   
            OpenIndex = OpenIndex == 0 ? 1 : 0;
        }
        if (OpenIndex == 0)
        {
            inventoryObj.SetActive(false);
        }
        else
        {
            inventoryObj.SetActive(true);
        }
    }
    public void ItemToInven(GameObject itemObj)
    {
        GameObject itemUI = Instantiate(ItemUIHusks, ItemUIParent);
        InvenItem _invenItem = itemUI.GetComponent<InvenItem>();
        if (itemObj.GetComponent<SpriteRenderer>() != null)
            _invenItem.ItemSprite = itemObj.GetComponent<SpriteRenderer>().sprite;
        else if (itemObj.GetComponent<Image>() != null)
            _invenItem.ItemSprite = itemObj.GetComponent<Image>().sprite;
        for (int i = 0; i < ItemGridAvailable.Length; i++)
        {
            if (ItemGridAvailable[i] == 0)
            {
                itemUI.transform.position = ItemGrids[i].transform.position;
                ItemGridAvailable[i] = 1;
                _invenItem.currentIndex = i;
                break;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemToInven(collision.gameObject);
        Destroy(collision.gameObject);
    }
}
