using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

    private Room[,] rooms = new Room[10,10];

    public Map map;
    public Player player;
    public CurrentRoom currentRoom;

    [System.Serializable]
    public struct TransitionInfo
    {
        public float time;
        public AnimationCurve easing;
    }

    [SerializeField]
    private TransitionInfo transitionInfo;

    private void Awake()
    {
        Instance = this;
        map.GenerateMap();
        CopyMap();

        rooms[0, 0].gameObject.SetActive(true);
        rooms[0, 0].animator.SetBool("Show", true);
        currentRoom.room = rooms[0, 0];
    }

    public void CopyMap()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                rooms[i, j] = Instantiate(map.map[i,j],map.transform);
                rooms[i, j].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                foreach (Wall wall in rooms[i, j].walls)
                {
                    int x = i + wall.nextCoord.x;
                    int y = j + wall.nextCoord.y;
                    if (x < 0 || y < 0)
                    {
                        rooms[i, j].nextRoomAvailable[wall.wallNumber] = false;
                    }
                    else
                    {
                        rooms[i, j].nextRoomAvailable[wall.wallNumber] = true;
                    }
                }

                rooms[i, j].Open();
            }
        }
    }

    public void MoveRoom(Wall passWall)
    {
        StartCoroutine("MoveRoomIE", passWall);
    }

    public IEnumerator MoveRoomIE(Wall passWall)
    {
        //다음 룸 복제
        Vector2Int nextRoomCoord = currentRoom.roomCoord + passWall.nextCoord;
        Vector3 nextRoomPosition = Data.RoomPositionByCoord(passWall.nextCoord);

        Room prevRoom = currentRoom.room;
        Room nextRoom = rooms[nextRoomCoord.x, nextRoomCoord.y];
        nextRoom.transform.position = nextRoomPosition;
        nextRoom.gameObject.SetActive(true);


        currentRoom.room = nextRoom;
        currentRoom.roomCoord = nextRoomCoord;

        //움직임 중 돌발상황 제어
        player.enabled = false;
        player.ResetMovement();
        player.DisablePhysics();
        nextRoom.DisablePhysics();
        prevRoom.DisablePhysics();

        //룸 숨기기 + 보이기
        nextRoom.animator.SetBool("Show", true);
        prevRoom.animator.SetBool("Show", false);

        //움직임
        Vector3 nextRoomStart = nextRoomPosition;
        Vector3 nextRoomDest = prevRoom.transform.position;

        Vector3 prevRoomStart = prevRoom.transform.position;
        Vector3 prevRoomDest = nextRoomDest - nextRoomStart;

        Vector3 playerStart = player.transform.position;
        Wall inWall = nextRoom.walls[(passWall.wallNumber + 3) % 6];
        Vector3 playerDest = inWall.playerInitPos;

        float t = 0;
        while(t<transitionInfo.time)
        {
            t += Time.deltaTime;
            float ease_t = transitionInfo.easing.Evaluate(t/transitionInfo.time);
            currentRoom.room.transform.position = Vector3.Lerp(nextRoomStart, nextRoomDest, ease_t);
            prevRoom.transform.position = Vector3.Lerp(prevRoomStart, prevRoomDest, ease_t);
            player.transform.position = Vector3.Lerp(playerStart, playerDest, ease_t);
            yield return null;
        }

        // - 움직임 중 돌발상황 제어
        player.enabled = true;
        player.EnablePhysics();
        nextRoom.EnablePhysics();

        //룸 제어
        prevRoom.gameObject.SetActive(false);
    }
}
