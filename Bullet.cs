using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed;
    private IDamageable victim;
    private float damage;


    public void SeekTower( Tower tower, float _damage )
    {
        target = tower.towerTransform;
        damage = _damage;
        victim = tower;
    }

    public void SeekEmpty( Transform _transform )
    {
        target = _transform;
        damage = 0;
        victim = null;
    }

    public void SeekEnemy( Creep creep, float _damage )
    {
        target = creep.creepTransform;
        damage = _damage;
        victim = creep;
    }

    private void Update()
    {
        if ( target == null )
        {
            Destroy (gameObject);
            return;
        }

        Vector2 dir = target.position - transform.position;
        float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
        float distThisFrame = speed * Time.deltaTime;

        if ( dir.magnitude <= distThisFrame )
        {
            HitTarget ();
            return;
        }        
        transform.Translate (dir.normalized * distThisFrame, Space.World);
        
    }
    private void HitTarget()
    {
        if(victim != null)
            victim.CalcDamage (damage);
        Destroy (gameObject);
    }
}
