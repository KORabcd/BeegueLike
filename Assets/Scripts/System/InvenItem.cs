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
        transform.parent = inventory.ItemUIParent;
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
        if (results[1].gameObject.tag == "ItemGrid")
        {
            for (int i = 0; i < inventory.ItemGridAvailable.Length; i++)
            {
                if (inventory.ItemGrids[i].transform.position == results[1].gameObject.transform.position && inventory.ItemGridAvailable[i] == 0)
                {
                    inventory.ItemGridAvailable[currentIndex] = 0;
                    currentIndex = i;
                    transform.position = results[1].gameObject.transform.position;
                    inventory.ItemGridAvailable[i] = 1;
                    break;
                }
                else if (inventory.ItemGrids[i].transform.position == results[1].gameObject.transform.position)
                {
                    transform.position = originPos;
                    break;
                }
            }
        }
        else transform.position = originPos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        curPos = Input.mousePosition;
        transform.position = curPos;
    }
}
