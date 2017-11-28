using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IDestroyable {

    const float minPathUpdateTime = .2f;

    public float speed = 20;
    public int damage = 1;
    public float health = 10;

    public Rigidbody2D projectile;
    public float projectileSpeed;
    public float cooldown;

    public LayerMask layerMask;

    float maxHealth;

    internal Transform target;
    Vector2 oldTargetPosition;
    Vector2[] path;
    //Path path;
    int targetIndex;
    bool pathingActive = true;
    bool findingPath;
    Rigidbody2D rb2d;

    internal bool lineOfSight;

    RaycastHit2D leftHit;
    RaycastHit2D middleHit;
    RaycastHit2D rightHit;

    internal Rigidbody2D projectileInstance;

    internal float projectileWidth;

    internal float tempCooldown;

    internal float iFrames;
    internal float tempIFrames;

    internal void StartUp()
    {
        HealthManager.EndGame += EndGame;

        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb2d = GetComponent<Rigidbody2D>();

        //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), MovementManager.collider);

        maxHealth = health;

        StartCoroutine(UpdatePath());
	}
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (tempIFrames == 0)
        {
            if (collision.gameObject.tag == "Projectile")
            {
                ProjectileHit tempProjectile = collision.gameObject.GetComponent<ProjectileHit>();

                ChangeHealth(-tempProjectile.damage);

                tempIFrames = tempProjectile.iFrames;
            }
        }
    }
    internal bool CheckLoS(Vector2 origin, Vector2 target, Vector2 offset)
    {
        Debug.DrawRay(origin + offset, target - origin, Color.red);
        leftHit = Physics2D.Raycast(origin + offset, target - origin, Vector2.Distance(origin, target), layerMask);

        Debug.DrawRay(origin, target - origin, Color.red);
        middleHit = Physics2D.Raycast(origin, target - origin, Vector2.Distance(origin, target), layerMask);

        Debug.DrawRay(origin - offset, target - origin, Color.red);
        rightHit = Physics2D.Raycast(origin - offset, target - origin, Vector2.Distance(origin, target), layerMask);

        if ((!leftHit || leftHit.transform.gameObject.tag == "Player")
            && (!rightHit || rightHit.transform.gameObject.tag == "Player")
            && (middleHit && middleHit.transform.gameObject.tag == "Player"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnPathFound(Vector2[] newPath, bool pathSuccessful) {
		if (pathSuccessful && pathingActive) {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
        }

        findingPath = false;
    }

    IEnumerator UpdatePath()
    {
        if (Time.timeSinceLevelLoad < .3f)
        {
            yield return new WaitForSeconds(.3f);
        }
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        
        while (true)
        {
            yield return new WaitForSeconds(minPathUpdateTime);

            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        }
    }

    IEnumerator FollowPath()
    {
        if (path.Length != 0)
        {
            Vector2 currentWaypoint = path[0];
            while (true)
            {
                if ((Vector2)transform.position == currentWaypoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length)
                    {
                        yield break;
                    }
                    currentWaypoint = path[targetIndex];
                }

                transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                Vector2 temp = currentWaypoint - (Vector2)transform.position;
                
                rb2d.velocity += new Vector2(temp.x, temp.y).normalized * speed;

                yield return null;

            }
        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector2.one / 10);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }

    void ChangeHealth(float change)
    {
        health += change;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
        
        if (health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    void EndGame()
    {
        StopCoroutine(UpdatePath());
        StopCoroutine("FollowPath");

        rb2d.drag = 0;
    }
}
