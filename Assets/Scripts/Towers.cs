    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Towers : MonoBehaviour
    {
        // Start is called before the first frame update

        private GameObject targetEnemy;
        public float range = 1.0f;
        public float rotationSpeed = 1.0f;

        public float fireRate=1f;
        private float fireCountDown=0f;

        public GameObject bulletPrefab;
        public Transform firePoint;
        public bool useLaser=false;
        public LineRenderer lineRenderer;
        public ParticleSystem laserEffect;
        void Update()
        {
            // Find all the enemies in the scene
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            // Create a list of pairs (distance, enemy)
            List<KeyValuePair<float, GameObject>> enemiesByDistance = new List<KeyValuePair<float, GameObject>>();

            // Calculate the distance to each enemy and store it in the list
            foreach (GameObject enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                enemiesByDistance.Add(new KeyValuePair<float, GameObject>(distance, enemy));
            }

            // Sort the list by distance
            enemiesByDistance.Sort((x, y) => x.Key.CompareTo(y.Key));

            if (enemiesByDistance.Count > 0)
            {
                targetEnemy = enemiesByDistance[0].Value;
            }
            else
            {
                targetEnemy = null;
                
            }
            
            if (targetEnemy != null && Vector3.Distance(transform.position, targetEnemy.transform.position) <= range)
            {
                // Make the tower look at the target enemy
                float t = Time.deltaTime * rotationSpeed;
                Quaternion targetRotation = Quaternion.LookRotation(targetEnemy.transform.position - transform.position);
                targetRotation.x = 0;
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, t);

            }
            else
            {
                // Make the tower look at the center of its range
                transform.LookAt(transform.position + transform.forward * range);
                if(useLaser)
                {
                    lineRenderer.enabled=false;
                    laserEffect.Stop();
                }
                
            }

            // Check if the enemy is inside the tower's firing range
            if (targetEnemy != null && Vector3.Distance(transform.position, targetEnemy.transform.position) <= range)
            {
                if(useLaser)
                {
                    Laser();
                }
                else
                {
                       // Decrease the countdown timer to the next shot
                    fireCountDown -= Time.deltaTime;
                    // If the countdown timer is zero or less, shoot the enemy
                if (fireCountDown <= 0)
                {
                    Shoot();
                    fireCountDown = 1f / fireRate;
                }
                }
             
            }
        }

        void Laser()
        {
            if(!lineRenderer.enabled)
            {
                lineRenderer.enabled=true;
            }
            lineRenderer.SetPosition(0,firePoint.position);
            lineRenderer.SetPosition(1,targetEnemy.transform.position);
            targetEnemy.GetComponent<Enemy>().TakeDamage(0.025f);
            laserEffect.Play();
            laserEffect.transform.position=targetEnemy.transform.position;
        }
        void Shoot()
        {
            GameObject bulletGO=(GameObject)Instantiate(bulletPrefab,firePoint.position,firePoint.rotation);
                Bullet bullet =bulletGO.GetComponent<Bullet>();
            if(bullet!=null)
            {
                bullet.Seek(targetEnemy);
            }
            
        }
    }

