
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public EnemyBulletPooler enemyBulletPooler;
    public CoverBox coverBoxPrefab;

    public bool ShowTileText;

    public Text timeText;
    public Text scoreText;
    public Text lifes;

    int sideBound = 24;
    int topBound = 23;
    public int score = 0;

    public int coverBoxBottom = 4;

    public GameTile gameTilePrefab;
    public int gridXCount = 12;
    public int gridZCount = 12;

    public GameTile[][] tiles;
    GameObject tileHolder;
    public Ufo[] ufos;

    public PlayerController player;

    public LivingEntity enemy1;
    public LivingEntity enemy2;


    private void Update()
    {
        timeText.text = Time.time.ToString("0.00");
        scoreText.text = $"score: {score}";
        lifes.text = $"lifes: {player.health}";
    }

    private void Awake()
    {
        //InstantiatePlayField();
        InstantiateGrid();
        //InstantiateInRow(11, 8, enemy1, true);
        InstantiateUfoRow(11, 4, 5, enemy1,Direction.left);
        InstantiateUfoRow(10, 4, 5, enemy2,Direction.right);
        //InstantiateUfoRow(9, 4, 5, enemy1,Direction.left);
        InstantiateCoverBlockInGrid(1, 1);
        InstantiateCoverBlockInGrid(2, 1);

        InstantiateCoverBlockInGrid(5, 1);
        InstantiateCoverBlockInGrid(6, 1);

        InstantiateCoverBlockInGrid(9, 1);
        InstantiateCoverBlockInGrid(10, 1);


        //StartCoroutine(ufos[0].MoveToTile(4, 11, 500));
    }

    void InstantiatePlayField()
    {
        float offsetFromCenter = sideBound / 2f;

        GameObject coverBoxHolder = new GameObject("coverBoxHolder");
        coverBoxHolder.transform.parent = transform;

        for (int y = 0; y < 2; y++)
        {
            for (int x = -sideBound; x <= sideBound; x += sideBound / 2)
            {
                InstantiateBoxRow(5, x, 4 + y * 1.2f, coverBoxHolder.transform);
            }
        }




        void InstantiateBoxRow(int count, float offset, float y, Transform parentTransform)
        {
            GameObject rowHolder = new GameObject($"row {offset}");
            rowHolder.transform.parent = parentTransform;

            float sideOffset = 1.2f * count / 2f - .6f;

            for (int x = 0; x < count; x++)
            {
                CoverBox cB = Instantiate(coverBoxPrefab);
                cB.transform.position = new Vector3(x * 1.2f + offset - sideOffset, y, 0);
                cB.transform.parent = rowHolder.transform;
            }
        }


    }

    void InstantiateGrid()
    {
        tileHolder = new GameObject("Tile Holder");
        tileHolder.transform.parent = transform;

        float offsetX = 5f;
        float offsetZ = 2.5f;

        float offsetFromGround = 2f;

        float offsetFromCenter = (offsetX * gridXCount / 2f) - offsetX / 2f;


        if (tiles == null)
        {
            tiles = new GameTile[gridZCount][];
            for (int z = 0; z < gridZCount; z++)
            {


                tiles[z] = new GameTile[gridXCount];
            }
        }
        for (int z = 0, i = 0; z < gridZCount; z++)
        {
            GameObject rowHolder = new GameObject($"row {z}");
            rowHolder.transform.parent = tileHolder.transform;
            for (int x = 0; x < gridXCount; x++, i++)
            {
                GameTile t = tiles[z][x] = Instantiate(gameTilePrefab);
                t.InitializeTile(x, z, ShowTileText);
                t.transform.position = new Vector3(x * offsetX - offsetFromCenter, offsetFromGround + z * offsetZ, 0);
                t.transform.parent = rowHolder.transform;
            }
        }
    }

    //void InstantiateInRow(int row, int count, LivingEntity entity, bool fromLeft)
    //{
    //    if (count > gridXCount) return;

    //    ufos = new Ufo[count];

    //    for (int x = gridXCount - 1, i = 0; x > gridXCount - count; x--, i++)
    //    {
    //        Ufo lE = ufos[i] = (Ufo)Instantiate(entity);
    //        entity.transform.position = tiles[row][x].transform.position;
    //        tiles[row][x].isTaken = true;
    //        lE.gm = this;
    //        lE.currentTile = tiles[row][x];
    //        lE.SetNextTile();
    //        lE.gameObject.name = $"ufo {i} {x}/{row}";
    //    }
    //}

    void InstantiateUfoRow(int row, int startTile, int count, LivingEntity entity, Direction direction)
    {
        int endTile = startTile + count;

        if (endTile > gridXCount) endTile = gridXCount;

        if (row > gridZCount) row = gridZCount;

        ufos = new Ufo[count];


        for (int x = startTile, i = 0; x < endTile; x++, i++)
        {
            Ufo ufo = ufos[i] = (Ufo)Instantiate(entity);
            ufo.transform.position = tiles[row][x].transform.position;
            ufo.name = $"ufo {x} {row}/{x}";
            ufo.currentTile = tiles[row][x];
            ufo.currentTile.isTaken = true;
            ufo.gm = this;
            ufo.direction = direction;
            ufo.state = EnemyState.atPosition;
            ufo.enemyBulletPooler = enemyBulletPooler;
            
            
        }
    }

    void InstantiateCoverBlockInGrid(int xCoord, int zCoord)
    {
        float offsetX = 1.1f + .55f;
        float offsetZ = .55f;
        for (int z = 0; z < 2; z++)
        {
            for (int x = 0; x < 4; x++)
            {
                CoverBox cB = Instantiate(coverBoxPrefab);
                cB.transform.position = tiles[zCoord][xCoord].transform.position + new Vector3(x * 1.1f - offsetX, z * 1.1f - offsetZ, 0);
                tiles[zCoord][xCoord].isTaken = true;
            }
        }
    }


}
