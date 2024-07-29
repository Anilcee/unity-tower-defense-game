using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject target;

    private Enemy enemy;

    public float speed=10f;

    public int damage;

    public float attackRadius;

    public GameObject BulletHitEffect;


    public void Seek(GameObject _target)
    {
        target=_target;
    }

    // Update is called once per frame
    void Update()
    {
        if(target==null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir=target.transform.position-transform.position;
        float distanceThisFrame=speed*Time.deltaTime;

        if(dir.magnitude<=distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized*distanceThisFrame,Space.World);
    }

    void HitTarget()
{
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(target.transform.position, enemy.transform.position);
            if (distance <= attackRadius)
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage / 2);
            }
        }

    
        target.GetComponent<Enemy>().TakeDamage(damage);
    
    GameObject bulletHitEffectİns=(GameObject) Instantiate(BulletHitEffect,transform.position,transform.rotation);
    Destroy(bulletHitEffectİns,0.2f);
    Destroy(gameObject);
}



}
