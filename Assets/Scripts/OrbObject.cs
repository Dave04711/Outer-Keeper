using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbObject : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float rotateSpeed = 5f;
    [SerializeField] float radius = 2f;
    [SerializeField] float duration = 40;
    float angle;
    [SerializeField] GameObject particles;

    private void Update()
    {
        angle += rotateSpeed * Time.deltaTime;

        var offset = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
        transform.position = target.position + offset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyFreeze enemyFreeze = collision.gameObject.GetComponent<EnemyFreeze>();
        if(enemyFreeze == null)
        {
            return;
        }
        enemyFreeze.Freeze();
    }

    private void OnEnable()
    {
        StartCoroutine(Duration());
        particles.SetActive(true);
        particles.GetComponent<ParticleSystem>().Play();
    }

    IEnumerator Duration()
    {
        yield return new WaitForSeconds(duration);
        Destroy(Instantiate(particles, transform.position, Quaternion.identity), 2);
        gameObject.SetActive(false);
    }
}
