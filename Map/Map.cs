using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// uv: Screen space (1170 x 2532)
// xy: World space (2.31*2 x 5*2)
// rc: Row Column

public enum Color
{
    Empty,
    Red,
    Yellow,
    Blue
};

public class Map
{
    // [Public]
    public Vector2 screenWH { get; private set; }
    public Vector2 tileWH { get; private set; }
    public List<MapObject> mapObjects { get; private set; }

    public const int rows = 8;

    // [Private]
    private List<Tile> tiles;
    private const float marginN = 0f;
    private const float marginS = 0f;
    private const float marginW = 0f;
    private const float marginE = 0f;

    private Vector2 xyN;
    private Vector2 xyS;
    private Vector2 xyW;
    private Vector2 xyE;

    // Singleton
    private static Map _instance;
    public static Map Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Map();
            }
            return _instance;
        }
    }

    private Map()
    {
        mapObjects = new List<MapObject>();
        tiles = new List<Tile>();

        // This gets the Main Camera from the Scene
        Camera mainCam = Camera.main;
        Assert.IsNotNull(mainCam);

        float screenHeight = mainCam.orthographicSize * 2f;
        float screenWidth = mainCam.aspect * screenHeight;
        this.screenWH = new Vector2(screenWidth, screenHeight);

        float tileHeight = screenHeight * (1f - marginN - marginS) / rows;
        float tileWidth = screenWidth * (1f - marginW - marginE) / rows;
        this.tileWH = new Vector2(tileWidth, tileHeight);

        float xW = -screenWH.x / 2f + screenWH.x * marginW;
        float xE = screenWH.x / 2f - screenWH.x * marginE;
        float xC = (xW + xE) / 2f;
        float yS = -screenWH.y / 2f + screenWH.y * marginS;
        float yN = screenWH.y / 2f - screenWH.y * marginN;
        float yC = (yS + yN) / 2f;
        xyN = new Vector2(xC, yN);
        xyS = new Vector2(xC, yS);
        xyW = new Vector2(xW, yC);
        xyE = new Vector2(xE, yC);
    }

    public void Create()
    {
        for (int r = 0; r < rows; ++r)
        {
            for (int c = 0; c < rows; ++c)
            {
                tiles.Add(new Tile(new Vector2Int(r, c), tileWH));
            }
        }
    }

    public Tile GetTile(Vector2Int rc)
    {
        return tiles[rc.x * rows + rc.y];
    }

    public bool InsideMap(Vector2 xy)
    {
        Func<Vector2, Vector2, Vector2, bool> lineSign = (xy, xy1, xy2) =>
            (xy2.y - xy1.y) * xy.x - (xy2.x - xy1.x) * xy.y +
            (xy2.x - xy1.x) * xy1.y - (xy2.y - xy1.y) * xy1.x > 0f;
        bool sign1 = lineSign(xy, xyW, xyN);
        bool sign2 = lineSign(xy, xyS, xyE);
        bool sign3 = lineSign(xy, xyS, xyW);
        bool sign4 = lineSign(xy, xyE, xyN);
        return sign1 != sign2 && sign3 != sign4;
    }

    public bool InsideMap(Vector2Int rc)
    {
        return rc.x >= 0 &&
            rc.x < rows &&
            rc.y >= 0 &&
            rc.y < rows;
    }

    public Vector2Int XYtoRC(Vector2 xy)
    {
        Assert.IsTrue(InsideMap(xy));

        Func<Vector2, Vector2, Vector2, bool> lineSign = (xy, xy1, xy2) =>
            (xy2.y - xy1.y) * xy.x - (xy2.x - xy1.x) * xy.y +
            (xy2.x - xy1.x) * xy1.y - (xy2.y - xy1.y) * xy1.x > 0f;

        for (int r = 0; r < rows; ++r)
        {
            for (int c = 0; c < rows; ++c)
            {
                Vector2 xyL = xyW + new Vector2(                                // left
                    (r + c) * tileWH.x / 2f,
                    (c - r) * tileWH.y / 2f);
                Vector2 xyR = xyL + new Vector2(tileWH.x, 0f);                  // right
                Vector2 xyT = xyL + tileWH / 2f;                                // top
                Vector2 xyB = xyL + new Vector2(tileWH.x / 2f, -tileWH.y / 2f); // bottom

                bool sign1 = lineSign(xy, xyL, xyT);
                bool sign2 = lineSign(xy, xyB, xyR);

                bool sign3 = lineSign(xy, xyB, xyL);
                bool sign4 = lineSign(xy, xyR, xyT);

                if (sign1 != sign2 && sign3 != sign4)
                {
                    return new Vector2Int(r, c);
                }
            }
        }
        return new Vector2Int(-1, -1);
    }

    public Vector2 RCtoXY(Vector2Int rc)
    {
        Assert.IsTrue(InsideMap(rc), $"rc = {rc}");

        float x = (rc.x + rc.y) * tileWH.x / 2f;
        float y = (rc.y - rc.x) * tileWH.y / 2f;
        return xyW + new Vector2(x, y) + new Vector2(tileWH.x / 2f, 0f);
    }

    public void InitializeTowers()
    {
        Tower tower1 = AddObject<Tower>(new Vector2Int(2, 2));
        mapObjects.Add(tower1);

        Tower tower2 = AddObject<Tower>(new Vector2Int(2, 5));
        mapObjects.Add(tower2);

        Tower tower3 = AddObject<Tower>(new Vector2Int(5, 2));
        mapObjects.Add(tower3);

        Tower tower4 = AddObject<Tower>(new Vector2Int(5, 5));
        mapObjects.Add(tower4);
    }

    public T AddObject<T>(Vector2Int rc) where T : MapObject, new()
    {
        return GetTile(rc).AddObject<T>() as T;
    }

    public void DestroyObject<T>(Vector2Int rc) where T : MapObject, new()
    {
        GetTile(rc).DestroyObject<T>();
    }

    public void MoveObject<T>(Vector2Int rcFrom, Vector2Int rcTo) where T : MapObject, new()
    {
        MapObject obj = (MapObject)GetTile(rcFrom).RemoveObject<T>();
        Assert.IsNotNull(obj);
        GetTile(rcTo).InsertObject(obj);
        obj.SetPosition(rcTo);
    }

    public bool IsEmpty(Vector2Int rc)
    {
        return GetTile(rc).IsEmpty();
    }

    public void TowerCollapse(Tower tower)
    {
        mapObjects.Remove(tower);
    }
}
