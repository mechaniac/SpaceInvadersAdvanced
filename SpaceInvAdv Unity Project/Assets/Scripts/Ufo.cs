using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : LivingEntity
{

    public GameTile nextTile;
    public GameTile currentTile;
    public Direction direction;
    public EnemyState state;

    public EnemyBulletPooler enemyBulletPooler;

    public float moveStep = 2f;
    public Floatvariable myMoveStep;

    public float currentMove = 0f;
    
    public bool isMoving = false;

    float shootTime = 0f;

    AudioSource audioSource;

    private void Awake()
    {
        moveStep = myMoveStep.Value;
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        shootTime += Time.deltaTime;
        Movement();
        
    }
    private void FixedUpdate()
    {
        if (isPlayerUnderneath() && shootTime > 1f)
        {
            Debug.Log("Shooting");
            shootTime = 0f;
            Shoot();
        }
    }

    void Movement()
    {
        currentMove += Time.deltaTime;
        if (!isMoving && nextTile == null)
        {
            SetNextTile();
        }

        if (currentMove >= myMoveStep.Value && nextTile != null && !isMoving)
        {
            StartCoroutine(MoveToTile(nextTile, 1f));
        }

    }


    public void SetNextTile()
    {

        if (direction == Direction.left && currentTile.xCoord > 0)
        {
            //Debug.Log("stting");
            SetNextTile(gm.tiles[currentTile.zCoord][currentTile.xCoord - 1], Direction.left);
            return;
        }

        if (direction == Direction.left && currentTile.xCoord == 0 && currentTile.zCoord > 0)
        {
            SetNextTile(gm.tiles[currentTile.zCoord - 1][currentTile.xCoord], Direction.right);
            return;
        }

        if (direction == Direction.right && currentTile.xCoord < gm.gridXCount - 1)
        {
            SetNextTile(gm.tiles[currentTile.zCoord][currentTile.xCoord + 1], Direction.right);
            return;
        }

        if (direction == Direction.right && currentTile.xCoord == gm.gridXCount - 1)
        {
            SetNextTile(gm.tiles[currentTile.zCoord - 1][currentTile.xCoord], Direction.left);
            return;
        }

        void SetNextTile(GameTile tile, Direction nextDirection)
        {
            //Debug.Log($"tile setting: {tile.name}");
            

            if (!tile.isTaken)
            {
                direction = nextDirection;
                nextTile = tile;

                nextTile.isTaken = true;
            }
            
            
        }
    }



    public IEnumerator MoveToTile(GameTile goal, float time)
    {
        float elapsedTime = 0;
        isMoving = true;

        Vector3 endPosition = goal.transform.position;
        Vector3 startPosition = transform.position;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        currentTile.isTaken = false;
        currentTile = goal;
        nextTile = null;
        currentMove = 0;
        isMoving = false;
        
    }

    public override void Die()
    {
        currentTile.isTaken = false;
        if(nextTile) nextTile.isTaken = false;

        base.Die();
    }

    bool isPlayerUnderneath()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                //Debug.Log($"i hit player");
                return true;
            }
        }
        return false;
    }

    void Shoot()
    {
        EnemyProjectile p = enemyBulletPooler.ProjectileFromPool();
        p.transform.position = transform.position + new Vector3(0, -1, 0);
        audioSource.Play();    }

    private void OnDisable()
    {
        gm.score++;
    }

}

public enum Direction
{
    left,
    right
}

public enum EnemyState
{
    atPosition,
    moving
}