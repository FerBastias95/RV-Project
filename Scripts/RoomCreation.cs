using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Handles the creation of new rooms.</summary>
public class RoomCreation : MonoBehaviour
{
    private const int MIN_ROOM_SIZE = 8;
    private const int MAX_ROOM_SIZE = 1024;
    private const int ROOM_Y_OFFSET = 50;

    [SerializeField] private int roomWidth;
    [SerializeField] private int roomLength;
    
    public Material floorMeshMaterial;

    // Unity events
    void Awake()
    {
        // Check constraints
        roomWidth = Mathf.Clamp(roomWidth, MIN_ROOM_SIZE, MAX_ROOM_SIZE);
        roomLength = Mathf.Clamp(roomLength, MIN_ROOM_SIZE, MAX_ROOM_SIZE);
    }

    // Functions
    public void CreateRoom()
    {
        // Check constraints
        roomWidth = Mathf.Clamp(roomWidth, MIN_ROOM_SIZE, MAX_ROOM_SIZE);
        roomLength = Mathf.Clamp(roomLength, MIN_ROOM_SIZE, MAX_ROOM_SIZE);

        Debug.Log("Creating new room, W:"+roomWidth+" L:"+roomLength);

        Vector3 roomPosition = Vector3.zero;
        roomPosition.y = ROOM_Y_OFFSET;
        
        GameObject room = new GameObject("Room");
        room.transform.position = roomPosition;

        GameObject roomFloor = CreateFloorMesh(Vector2.zero, new Vector2(roomWidth,roomLength));
        roomFloor.transform.position = Vector3.zero;
        
        roomFloor.transform.SetParent(room.transform, false);
        
    }

    private GameObject CreateFloorMesh(Vector2 blRoomCorner, Vector2 trRoomCorner){
        //b=bottom, t=top, l=left, r=rigth

        Vector3 blVertice = new Vector3(blRoomCorner.x,0,blRoomCorner.y);
        Vector3 brVertice = new Vector3(trRoomCorner.x,0,blRoomCorner.y);
        Vector3 tlVertice = new Vector3(blRoomCorner.x,0,trRoomCorner.y);
        Vector3 trVertice = new Vector3(trRoomCorner.x,0,trRoomCorner.y);
        Vector3[] vertices = new Vector3[]{tlVertice,trVertice,blVertice,brVertice};

        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
        }

        int[] triangles = new int[]{0,1,2,2,1,3};

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        GameObject roomFloor = new GameObject("RoomFloor", typeof(MeshFilter),typeof(MeshRenderer));

        roomFloor.transform.localScale = Vector3.one;
        roomFloor.GetComponent<MeshFilter>().mesh = mesh;
        roomFloor.GetComponent<MeshRenderer>().material = floorMeshMaterial;

        return roomFloor;
    }

    // Properties
    public int RoomWidth
    {
        get { return roomWidth; }
        set { roomWidth = Mathf.Clamp(value, MIN_ROOM_SIZE, MAX_ROOM_SIZE); }
    }

    public int RoomLength
    {
        get { return roomLength; }
        set { roomLength = Mathf.Clamp(value, MIN_ROOM_SIZE, MAX_ROOM_SIZE); }
    }
}
