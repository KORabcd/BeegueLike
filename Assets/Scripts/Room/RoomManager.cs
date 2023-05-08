using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

    private Room[,] map = new Room[10,10];
    [System.Serializable]
    public struct RoomPalette
    {
        public Room emptyRoom;
    }

    [SerializeField]
    public RoomPalette roomPalette;

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
        GenerateMap();

        currentRoom.room = Instantiate(map[0, 0], Vector3.zero, Quaternion.identity,currentRoom.transform);
    }

    void GenerateMap()
    {
        for (int i=0;i<10;i++)
        {
            for(int j=0;j<10;j++)
            {
                map[i, j] = roomPalette.emptyRoom;
            }
        }

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                for (int wallNumber=0; wallNumber<map[i,j].walls.Count;wallNumber++)
                {
                    map[i, j].nextRoomAvailable[wallNumber] = true;
                }
            }
        }
    }

    public void MoveRoom(Wall passWall)
    {
        Debug.Log("move room");
        StartCoroutine("MoveRoomIE", passWall);
    }

    public IEnumerator MoveRoomIE(Wall passWall)
    {
        //다음 룸 복제
        Vector2Int nextRoomCoord = currentRoom.roomCoord+passWall.nextCoord;
        Debug.Log(nextRoomCoord.ToString());
        Room nextRoom = map[nextRoomCoord.x, nextRoomCoord.y];
        Vector3 nextRoomPosition = Data.RoomPositionByCoord(passWall.nextCoord);

        Room prevRoom = currentRoom.room;
        currentRoom.room = Instantiate(nextRoom, nextRoomPosition, Quaternion.identity, currentRoom.transform);
        currentRoom.roomCoord = nextRoomCoord;

        //움직임 중 돌발상황 제어
        player.enabled = false;
        player.gameObject.layer = 7;

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
        player.gameObject.layer = 6;
        player.ResetMovement();

        //이전 룸 삭제
        Destroy(prevRoom.gameObject);
    }
}
