using UnityEngine;

public class Enemies : MonoBehaviour, iPoolable<Enemies>
{
    public float speed = 3f;
    private Pool<Enemies> projPool;
    public GameObject explosion;

    public void Init(Pool<Enemies> pool)
    {
        projPool = pool;
    }

    void Update()
    {
        transform.position -= Vector3.up * speed * Time.deltaTime;
    }

    public void DestroyEnemy()
    {
        projPool.ReturnToPool(this);
        this.gameObject.SetActive(false);
        Instantiate(explosion, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.GetComponent<Player>().TakeDamage();
            projPool.ReturnToPool(this);
            this.gameObject.SetActive(false);
        }
        else if (collision.gameObject.GetComponent<Barrier>()) {
            projPool.ReturnToPool(this);
            this.gameObject.SetActive(false);
        }
    }
}
