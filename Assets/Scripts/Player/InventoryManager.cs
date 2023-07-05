using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    int OpenIndex=0;
    public GameObject inventoryObj;

    public List<GameObject> collectAbleObjs;

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

    [System.NonSerialized]
    public int usingWeaponIndex;
    public SpriteRenderer swardSprite;
    public InvenItem[] WeaponBar;
    float mouseWheelOffset;
    void Start()
    {
        ItemGrids = GameObject.FindGameObjectsWithTag("ItemGrid");
        ItemGridAvailable = new int[ItemGrids.Length];
    }

    void Update()
    {
        InvenOpen();
        WeaponSwitch();
        Collect();
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
    void InvenOpen()
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
    void WeaponSwitch()
    {
        mouseWheelOffset += Input.GetAxis("Mouse ScrollWheel");
        if(Mathf.Abs(mouseWheelOffset) > 0f)
        {
            usingWeaponIndex = mouseWheelOffset > 0 ? usingWeaponIndex - 1 : usingWeaponIndex + 1;
            if (usingWeaponIndex > 3) usingWeaponIndex = 0;
            else if (usingWeaponIndex < 0) usingWeaponIndex = 3;
            mouseWheelOffset = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
            usingWeaponIndex = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            usingWeaponIndex = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            usingWeaponIndex = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            usingWeaponIndex = 3;

        if (QuickGridAvailable[usingWeaponIndex] == 1)
        {
            swardSprite.sprite = WeaponBar[usingWeaponIndex].ItemSprite;
        }
        else
        {
            swardSprite.sprite = null;
        }
    }
    void Collect()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (collectAbleObjs.Count == 0) return;
            float minDis = 100;
            int minObj = 0;
            for(int i = 0; i < collectAbleObjs.Count; i++)
            {
                if(minDis >= Vector2.Distance(this.gameObject.transform.position, collectAbleObjs[i].transform.position))
                {
                    minDis = Vector2.Distance(this.gameObject.transform.position, collectAbleObjs[i].transform.position);
                    minObj = i;
                }
            }
            ItemToInven(collectAbleObjs[minObj]);
            Destroy(collectAbleObjs[minObj]);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            collectAbleObjs.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            collectAbleObjs.Remove(collision.gameObject);
        }
    }
}
