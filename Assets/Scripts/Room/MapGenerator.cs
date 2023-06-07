using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
public static class MapGenerator
{
    public static int[,] GenerateMapTypes(int height, int width, int inactiveRoomCnt, int specialRoomCnt)
    {
        int[,] dxy = new int[2, 6]
        {
            {1,0,-1,-1,0,1},
            {1,1,0,-1,-1,0}
        };
        //-1 : 맵 아닌 곳, -2 : 못 가는 룸,  0 : 갈 수 있는 룸, 1~ : 특수 룸
        int[,] mapType = new int[height, height];
        List<Coord>[] list = new List<Coord>[4];
        for (int i = 0; i < 4; i++)
            list[i] = new List<Coord>();
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < height; j++)
                if (i - j >= width || j - i >= width)
                    mapType[i, j] = -1; // 맵 아닌 곳 지정
                else
                {
                    if (i > width - 1 && j > width - 1)
                        list[0].Add(new Coord() { x = i, y = j });
                    if (i >= width - 1 && j <= width - 1)
                        list[1].Add(new Coord() { x = i, y = j });
                    if (i <= width - 1 && j >= width - 1)
                    {
                        //Debug.Log(i + "," + j);
                        list[2].Add(new Coord() { x = i, y = j });
                    }
                    if (i < width - 1 && j < width - 1)
                        list[3].Add(new Coord() { x = i, y = j });
                }
        }
        /*for (int i = 0; i < 4; i++)
        {
            Debug.Log(i + "번째 방그룹");
            Debug.Log("방 개수 : " + list[i].Count);
            for (int j = 0; j < list[i].Count; j++)
            {
                Debug.Log("(" + list[i][j].x + "," + list[i][j].y + ")");
            }
        }    디버깅 */
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < inactiveRoomCnt / 4; j++)
            {
                int rand = Random.Range(0, list[i].Count);
                mapType[list[i][rand].x, list[i][rand].y] = -2;
                list[i].RemoveAt(rand);
            }
        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < specialRoomCnt / 4; j++)
            { 
                int rand = Random.Range(0, list[i].Count); 
                int x1 = list[i][rand].x, y1 = list[i][rand].y;
                list[i].RemoveAt(rand);
                int k;
                for (k = 0; k < 6; k++)
                {
                    int x2 = x1 + dxy[0, k], y2 = y1 + dxy[1, k];
                    if (x2 >= 0 && y2 >= 0 && x2 < height && y2 < height)
                        if (mapType[x2, y2] >= 0)
                            break;
                }
                if (k == 6)
                {
                    j--;
                    continue;
                }
                mapType[x1, y1] = 1;
            }
        }
        //Debug.Log("MapGenarationEnd");
        return mapType;
    }
    public static bool BFS(int[,] mapType, int height, int width)
    {
        int i = 0, j = 0;
        Queue<Coord> que = new Queue<Coord>();
        que.Enqueue(new Coord() { x = i, y = j });
        while (que.Count != 0)
        {
            Coord num = que.Dequeue();
            if (num.x >= height || num.y >= height || mapType[num.x, num.y] < 0)
                continue;
            if (num.x == height - 1 && num.y == height - 1)
                return true;
            que.Enqueue(new Coord() { x = num.x + 1, y = num.y });
            que.Enqueue(new Coord() { x = num.x, y = num.y + 1 });
        }
        return false;
    }
}
