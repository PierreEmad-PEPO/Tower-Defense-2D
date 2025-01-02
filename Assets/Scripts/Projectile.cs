using UnityEngine;

public enum ProjectileTypes
{
    Arrow,
    Rock,
    Fireball,
}

public class Projectile : MonoBehaviour
{
    [SerializeField] int attackStrength;
    [SerializeField] ProjectileTypes projectileType;

    private float range;
    private float speed;
    private Vector3 dir;
    private Vector3 target;


    public int AttackStrength { get { return attackStrength; } }
    public ProjectileTypes ProjectileType { get { return projectileType; } }

    public void Initialize(float _range, float _speed, Vector3 _dir)
    {
        range = _range;
        speed = _speed;
        dir = _dir;

        float _angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
        target = transform.position + dir.normalized * range;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(
            transform.position, target, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<EnemyHealth>().TakeDamage(attackStrength);
        Destroy(gameObject);
    }
}
