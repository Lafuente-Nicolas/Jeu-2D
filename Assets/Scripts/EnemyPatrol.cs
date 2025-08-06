using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public SpriteRenderer graphics;
    public float speed;
    public Transform[] waypoints;

    public int damageOnCollision = 20;
    private Transform target;
    private int destPoint = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {    // syst�me de d�placement du serpant
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        // si l'ennemi est quasiment arriv� � sa destination
        if(Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];
            graphics.flipX = !graphics.flipX;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // si l'ennemi touche le joueur
        if (collision.transform.CompareTag("Player"))
        {
            // inflige des d�g�ts au joueur
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageOnCollision);
            
        }
    }
}
