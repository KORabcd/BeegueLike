using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

    public Room[,] rooms = new Room[10,10];

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
        map.MaterializeMap();

        rooms[0, 0].gameObject.SetActive(true);
        rooms[0, 0].animator.SetBool("Show", true);
        currentRoom.room = rooms[0, 0];

        player.transform.position = Data.playerInitPos;
    }

    public void MoveRoom(Wall passWall)
    {
        StartCoroutine("MoveRoomIE", passWall);
    }

    private IEnumerator MoveRoomIE(Wall passWall)
    {
        //���� �� ����
        Vector2Int nextRoomCoord = currentRoom.roomCoord + passWall.nextCoord;
        Vector3 nextRoomPosition = Data.RoomPositionByCoord(passWall.nextCoord);

        Room prevRoom = currentRoom.room;
        Room nextRoom = rooms[nextRoomCoord.x, nextRoomCoord.y];
        nextRoom.transform.position = nextRoomPosition;
        nextRoom.gameObject.SetActive(true);


        currentRoom.room = nextRoom;
        currentRoom.roomCoord = nextRoomCoord;

        //�� ���߱�
        EnemyManager.Instance.enemyEnabled = false;

        //������ �� ���߻�Ȳ ����
        player.enabled = false;
        player.ResetMovement();
        player.DisablePhysics();
        nextRoom.enabled = false;
        nextRoom.DisablePhysics();
        prevRoom.enabled = false;
        prevRoom.DisablePhysics();

        //�� ����� + ���̱�
        nextRoom.animator.SetBool("Show", true);
        nextRoom.Open();
        prevRoom.animator.SetBool("Show", false);

        //������
        Vector3 nextRoomStart = nextRoomPosition;
        Vector3 nextRoomDest = prevRoom.transform.position;

        Debug.Log("next room dest" + nextRoomDest);

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

        // - �� ���߱�
        EnemyManager.Instance.enemyEnabled = true;

        // - ������ �� ���߻�Ȳ ����
        player.enabled = true;
        player.EnablePhysics();
        nextRoom.enabled = true;
        nextRoom.EnablePhysics();

        //�� ����
        prevRoom.gameObject.SetActive(false);
    }
}
