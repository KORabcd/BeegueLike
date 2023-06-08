using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InvenItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Sprite ItemSprite;

    Vector2 curPos;
    GraphicRaycaster gr;
    Vector2 originPos;


    InventoryManager inventory;
    public int currentIndex;

    bool isQuick = false;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
        GetComponent<Image>().sprite = ItemSprite;
        gr = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<GraphicRaycaster>();
        transform.position = inventory.ItemGrids[currentIndex].transform.position;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originPos = transform.position;
         transform.SetParent(isQuick == false ? inventory.ItemUIParent : inventory.QuickUIParent);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var ped = new PointerEventData(null);
        ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(ped, results);

        if (results.Count <= 0) return;

        if (results.Count == 1)
        {
            transform.position = originPos;
            return;
        }
        // 이벤트 처리부분
        if (results[1].gameObject.tag == "ItemGrid" || results[2].gameObject.tag == "ItemGrid")
        {
            for (int i = 0; i < inventory.ItemGridAvailable.Length; i++)
            {
                if (inventory.ItemGrids[i].transform.position == results[1].gameObject.transform.position && inventory.ItemGridAvailable[i] == 0)
                {
                    if (isQuick)
                    {
                        transform.parent = inventory.ItemUIParent;
                        if (currentIndex != -1)
                            inventory.QuickGridAvailable[currentIndex] = 0;
                        else
                            inventory.TrashGridAvailable = 0;
                        isQuick = false;
                    }
                    if (currentIndex != -1)
                        inventory.ItemGridAvailable[currentIndex] = 0;
                    else
                        inventory.TrashGridAvailable = 0;
                    currentIndex = i;
                    transform.position = results[1].gameObject.transform.position;
                    inventory.ItemGridAvailable[i] = 1;
                    break;
                }
                else if (inventory.ItemGrids[i].transform.position == results[1].gameObject.transform.position || inventory.ItemGrids[i].transform.position == results[2].gameObject.transform.position)
                {
                    if (isQuick == false)
                    {
                        for (int j = 0; j < inventory.ItemUIParent.transform.childCount; j++)
                        {

                            if (Mathf.FloorToInt(inventory.ItemGrids[i].transform.position.x) == Mathf.FloorToInt(inventory.ItemUIParent.transform.GetChild(j).position.x) && Mathf.FloorToInt(inventory.ItemGrids[i].transform.position.y) == Mathf.FloorToInt(inventory.ItemUIParent.transform.GetChild(j).position.y))
                            {
                                transform.position = inventory.ItemUIParent.transform.GetChild(j).position;
                                if(currentIndex != -1)
                                    inventory.ItemUIParent.transform.GetChild(j).position = inventory.ItemGrids[currentIndex].transform.position;
                                else inventory.ItemUIParent.transform.GetChild(j).position = inventory.TrashGrid.transform.position;
                                inventory.ItemUIParent.transform.GetChild(j).GetComponent<InvenItem>().currentIndex = currentIndex;
                                currentIndex = i;
                                break;

                            }
                            else transform.position = originPos;
                        }
                    }
                    else
                    {
                        for (int j = 0; j < inventory.ItemUIParent.transform.childCount; j++)
                        {

                            if (Mathf.FloorToInt(inventory.ItemGrids[i].transform.position.x) == Mathf.FloorToInt(inventory.ItemUIParent.transform.GetChild(j).position.x) && Mathf.FloorToInt(inventory.ItemGrids[i].transform.position.y) == Mathf.FloorToInt(inventory.ItemUIParent.transform.GetChild(j).position.y))
                            {
                                transform.position = inventory.ItemUIParent.transform.GetChild(j).position;
                                inventory.ItemUIParent.transform.GetChild(j).position = inventory.QuickGrids[currentIndex].transform.position;
                                inventory.ItemUIParent.transform.GetChild(j).GetComponent<InvenItem>().currentIndex = currentIndex;
                                inventory.ItemUIParent.transform.GetChild(j).GetComponent<InvenItem>().isQuick = true;
                                isQuick = false;
                                inventory.ItemUIParent.transform.GetChild(j).SetParent(inventory.QuickUIParent);
                                transform.SetParent(inventory.ItemUIParent);
                                currentIndex = i;
                                break;

                            }
                        }
                    }
                    break;
                }
            }
        }
        else if (results[1].gameObject.tag == "QuickGrid" || results[2].gameObject.tag == "QuickGrid")
        {
            for (int i = 0; i < inventory.QuickGridAvailable.Length; i++)
            {
                if (inventory.QuickGrids[i].transform.position == results[1].gameObject.transform.position && inventory.QuickGridAvailable[i] == 0)
                {
                    if (isQuick == false)
                    {
                        if (currentIndex != -1)
                            inventory.ItemGridAvailable[currentIndex] = 0;
                        else
                            inventory.TrashGridAvailable = 0;
                        currentIndex = i;
                        transform.position = results[1].gameObject.transform.position;
                        inventory.QuickGridAvailable[i] = 1;
                        transform.SetParent(inventory.QuickUIParent);
                    }
                    else
                    {
                        if(currentIndex != -1)
                            inventory.QuickGridAvailable[currentIndex] = 0;
                        else
                            inventory.TrashGridAvailable = 0;
                        currentIndex = i;
                        transform.position = results[1].gameObject.transform.position;
                        inventory.QuickGridAvailable[i] = 1;
                    }
                    isQuick = true;
                    break;
                }
                else if (inventory.QuickGrids[i].transform.position == results[1].gameObject.transform.position || inventory.QuickGrids[i].transform.position == results[2].gameObject.transform.position)
                {
                    if (isQuick)
                    {
                        for (int j = 0; j < inventory.QuickUIParent.transform.childCount; j++)
                        {

                            if (Mathf.FloorToInt(inventory.QuickGrids[i].transform.position.x) == Mathf.FloorToInt(inventory.QuickUIParent.transform.GetChild(j).position.x) && Mathf.FloorToInt(inventory.QuickGrids[i].transform.position.y) == Mathf.FloorToInt(inventory.QuickUIParent.transform.GetChild(j).position.y))
                            {
                                transform.position = inventory.QuickUIParent.transform.GetChild(j).position;
                                inventory.QuickUIParent.transform.GetChild(j).position = inventory.QuickGrids[currentIndex].transform.position;
                                inventory.QuickUIParent.transform.GetChild(j).GetComponent<InvenItem>().currentIndex = currentIndex;
                                currentIndex = i;

                                break;

                            }
                            else transform.position = originPos;
                        }
                    }
                    else
                    {
                        for (int j = 0; j < inventory.QuickUIParent.transform.childCount; j++)
                        {

                            if (Mathf.FloorToInt(inventory.QuickGrids[i].transform.position.x) == Mathf.FloorToInt(inventory.QuickUIParent.transform.GetChild(j).position.x) && Mathf.FloorToInt(inventory.QuickGrids[i].transform.position.y) == Mathf.FloorToInt(inventory.QuickUIParent.transform.GetChild(j).position.y))
                            {
                                transform.position = inventory.QuickUIParent.transform.GetChild(j).position;
                                if(currentIndex != -1)
                                    inventory.QuickUIParent.transform.GetChild(j).position = inventory.ItemGrids[currentIndex].transform.position;
                                else inventory.QuickUIParent.transform.GetChild(j).position = inventory.TrashGrid.transform.position;
                                inventory.QuickUIParent.transform.GetChild(j).GetComponent<InvenItem>().currentIndex = currentIndex;
                                inventory.QuickUIParent.transform.GetChild(j).GetComponent<InvenItem>().isQuick = false;
                                isQuick = true;
                                inventory.QuickUIParent.transform.GetChild(j).SetParent(inventory.ItemUIParent);
                                transform.SetParent(inventory.QuickUIParent);
                                currentIndex = i;
                                break;

                            }
                        }
                    }
                    break;
                }
            }
        }
        else if(results[1].gameObject.tag == "TrashGrid" || results[2].gameObject.tag == "TrashGrid")
        {
            int resultNum = 0;
            if (results[1].gameObject.tag == "TrashGrid")
                resultNum = 1;
            else
                resultNum = 2;
            if(isQuick && currentIndex != -1)
                inventory.QuickGridAvailable[currentIndex] = 0;
            else if(currentIndex != -1)
                inventory.ItemGridAvailable[currentIndex] = 0;

            transform.SetParent(inventory.TrashUIParent);

            transform.position = results[resultNum].gameObject.transform.position;
            isQuick = false;
            if (inventory.TrashGrid.transform.position == results[resultNum].gameObject.transform.position && inventory.TrashGridAvailable == 1 && currentIndex != -1)
            {
                Destroy(inventory.TrashUIParent.GetChild(0).gameObject);
            }
            else
            {
                inventory.TrashGridAvailable = 1;
            }
            currentIndex = -1;

        }
        else transform.position = originPos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        curPos = Input.mousePosition;
        transform.position = curPos;
    }
}
