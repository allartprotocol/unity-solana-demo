using UnityEngine;

public class Projectile : MonoBehaviour, iPoolable<Projectile>
{
    public float speed = 3f;
    private Pool<Projectile> pool;

    public void Init(Pool<Projectile> pool)
    {
        this.pool = pool;
    }

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemies>())
        {
            if (pool != null)
            {
                collision.gameObject.GetComponent<Enemies>().DestroyEnemy();
                
                pool.ReturnToPool(this);
                this.gameObject.SetActive(false);
                App.instance.RewardPoints(100);
            }
        }
        else if (collision.gameObject.GetComponent<Barrier>())
        {
            if (pool != null)
            {
                pool.ReturnToPool(this);
                this.gameObject.SetActive(false);
            }
        }
    }
}
