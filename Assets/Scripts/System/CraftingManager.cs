using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    public string recipe;
    public Sprite completedItem;
}
public class CraftingManager : MonoBehaviour
{
    public Recipe[] recipe;
    public GameObject craftTable;
    public InventoryManager inventoryManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Craft()
    {
        int SourceItemCnt = 0;
        for(int i = 0; i < 6; i++) 
        {
            if(craftTable.transform.GetChild(i).transform.childCount != 0)
            {
                SourceItemCnt++;
            }
        }
        if (SourceItemCnt == 0) return;
        string[] SourceItem = new string[SourceItemCnt];
        int _sourceItemCnt = 0;
        for (int i = 0; _sourceItemCnt < SourceItemCnt; i++)
        {
            if (craftTable.transform.GetChild(i).transform.childCount != 0)
            {
                SourceItem[_sourceItemCnt] = craftTable.transform.GetChild(i).transform.GetChild(0).GetComponent<InvenItem>().ItemSprite.name;
                _sourceItemCnt++;
            }
        }
        string[] split_recipe;

        
        for (int i = 0;i < recipe.Length; i++)
        {
            split_recipe = recipe[i].recipe.Split(',');
            string temp_recipe = recipe[i].recipe;
            if (temp_recipe.Contains(SourceItem[0]))
            {
                int completeSource = 0;
                if(split_recipe.Length == SourceItemCnt)
                {
                    for (int j = 0; j < split_recipe.Length; j++)
                    {
                        if (temp_recipe.Contains(SourceItem[j]))
                        {
                            string subStringValue = SourceItem[j];

                            int firstFindIndex = temp_recipe.IndexOf(subStringValue);
                            temp_recipe = temp_recipe.Remove(firstFindIndex, subStringValue.Length);
                            completeSource++;
                        }
                        if (split_recipe.Length == completeSource)
                        {
                            GameObject itemUI = Instantiate(inventoryManager.ItemUIHusks, craftTable.transform.GetChild(6).transform);
                            InvenItem _invenItem = itemUI.GetComponent<InvenItem>();
                            _invenItem.currentIndex = -2;
                            _invenItem.ItemSprite = recipe[i].completedItem;
                            itemUI.transform.position = craftTable.transform.GetChild(6).transform.position;
                            for (int k = 0; k < 6; k++)
                            {
                                if (craftTable.transform.GetChild(k).transform.childCount != 0)
                                {
                                    Destroy(craftTable.transform.GetChild(k).transform.GetChild(0).gameObject);
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}
