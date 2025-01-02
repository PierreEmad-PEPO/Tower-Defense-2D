using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform[] checkpoints;
    [SerializeField] float speed;

    private int target = 0;
    EnemyHealth health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!health.IsDead)
            transform.position = Vector3.MoveTowards(
                transform.position, checkpoints[target].position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            target++;
            if (target >= checkpoints.Length)
            {
                EnemySpawner.Instance.RemoveEnemyFromScreen(this);
                Destroy(gameObject);
            }
        }
    }
}
