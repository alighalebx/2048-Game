using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBoard : MonoBehaviour
{
    public Tile tilePrefab;
    public TileState[] tileStates;

    private TileGrid grid;

    private List<Tile> tiles;

    private void Awake()
    {
        grid = GetComponentInChildren<TileGrid>();
        tiles = new List<Tile>(16);
    }

    private void Start()
    {
        CreateTile();
        CreateTile();
    }

    private void CreateTile()
    {
        Tile tile = Instantiate(tilePrefab, grid.transform);
        tile.SetState(tileStates[0], 2);
        tile.Spwan(grid.GetRandomEmptyCell());
        tiles.Add(tile);
    }


    private Vector2 startPos;
    public int pixelDistToDetect = 20;
    private bool fingerDown;

    private void Update()
    {
        if(fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            startPos = Input.touches[0].position;
            fingerDown = true;
        }

        if(fingerDown)
        {
            // did we swipe up?
            if(Input.touches[0].position.y >= startPos.y + pixelDistToDetect)
            {
                fingerDown = false;
                Debug.Log("Swipe up");
                MoveTiles(Vector2Int.up, 0, 1, 1, 1);
            }

            else if(Input.touches[0].position.x <= startPos.x - pixelDistToDetect)
            {
                fingerDown = false;
                Debug.Log("Swipe left");
                MoveTiles(Vector2Int.left, 1, 1, 0, 1);
            }

            else if(Input.touches[0].position.x >= startPos.x + pixelDistToDetect)
            {
                fingerDown = false;
                Debug.Log("Swipe right");
                MoveTiles(Vector2Int.right, grid.width - 2 , -1, 0, 1);
            }

            else if(Input.touches[0].position.y <= startPos.y - pixelDistToDetect)
            {
                fingerDown = false;
                Debug.Log("swipe down");
                MoveTiles(Vector2Int.down, 0, 1, grid.height - 2, -1);
            }
        }

        if(fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            fingerDown = false;
        }

        // Testing

        if(fingerDown == false && Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            fingerDown = true;
        }

        if(fingerDown)
        {
            if(Input.mousePosition.y >= startPos.y + pixelDistToDetect)
            {
                fingerDown = false;
                Debug.Log("Swipe up");
                MoveTiles(Vector2Int.up, 0, 1, 1, 1);

            }
            
            else if(Input.mousePosition.x <= startPos.x - pixelDistToDetect)
            {
                fingerDown = false;
                Debug.Log("Swipe left");
                MoveTiles(Vector2Int.left, 1, 1, 0, 1);
            }

            else if(Input.mousePosition.x >= startPos.x + pixelDistToDetect)
            {
                fingerDown = false;
                Debug.Log("Swipe right");
                MoveTiles(Vector2Int.right, grid.width - 2 , -1, 0, 1);
            }

            else if(Input.mousePosition.y <= startPos.y - pixelDistToDetect)
            {
                fingerDown = false;
                Debug.Log("swipe down");
                MoveTiles(Vector2Int.down, 0, 1, grid.height - 2, -1);
            }

        }

        if(fingerDown && Input.GetMouseButtonUp(0))
        {
            fingerDown = false;
        }
    }

    private void MoveTiles(Vector2Int direction, int startX, int incrementX, int startY, int incrementY){
        for (int x = startX; x >= 0 && x < grid.width; x += incrementX)
        {
            for (int y = startY; y >=0 && y < grid.height; y += incrementY)
            {
                TileCell cell = grid.GetCell(x,y);

                if(cell.occupied){
                    MoveTile(cell.tile, direction);
                }
            }
        }
    }

    private void MoveTile(Tile tile, Vector2Int direction)
    {
        TileCell newCell = null;
        TileCell adjacent = grid.GetAdjacentCell(tile.cell, direction);
        
        while ( adjacent != null){
            if (adjacent.occupied)
            {
                break;
            }

            newCell = adjacent;
            adjacent = grid.GetAdjacentCell(adjacent, direction);
        }

        if(newCell != null)
        {
            tile.MoveTo(newCell);
        }

    }
}
