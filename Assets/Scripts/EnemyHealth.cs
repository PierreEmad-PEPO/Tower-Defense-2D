using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health;

    private Animator animator;
    private bool isDead = false;

    public bool IsDead {  get { return isDead; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int _damage)
    {
        health -= _damage;
        animator.Play("Hurt");
        if (health < 0)
            Die();

    }

    private void Die()
    {
        animator.SetTrigger("DidDie");
        isDead = true;
        GetComponent<BoxCollider2D>().enabled = false;
        EnemySpawner.Instance.OnEnemyDestroy.Invoke(this.transform);
    }
}
