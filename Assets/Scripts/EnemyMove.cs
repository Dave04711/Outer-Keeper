using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    List<Transform> points;
    bool moving;
    [SerializeField] float speed = 1;
    float defSpeed;
    [SerializeField] Animator animator;
    int index = 1;
    [SerializeField] GameObject VanishFX;

    private void Awake()
    {
        defSpeed = speed;
    }

    public void Init(Path _path)
    {
        GetComponent<Health>().Respawn();
        points = _path.points;
        transform.position = points[0].position;
        index = 1;
    }

    private void Update()
    {
        if(index == points.Count)
        { 
            PathsManager.instance.GetComponent<EnemySpawn>().TurnOff(gameObject);
            Destroy(Instantiate(VanishFX, transform.position, Quaternion.identity), 2f);
        }
        else
        {
            if (Vector2.Distance(transform.position, points[index].position) <= .02f) { index++; }
            if (index < points.Count)
            {
                animator.SetBool("move", true);
                float step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, points[index].position, step);
                Rotate(points[index].position.x < transform.position.x);
            }
            else
            {
                animator.SetBool("move", false);
            } 
        }
    }

    void Rotate(bool _p)
    {
        if (_p)
        {
            transform.eulerAngles = Vector2.up * 180;
        }
        else
        {
            transform.eulerAngles = Vector2.zero;
        }
    }

    public void Slowness(float _p)
    {
        StopAllCoroutines();
        StartCoroutine(Slow(_p));
    }

    IEnumerator Slow(float _percentage, float _time = 3f)
    {
        speed = defSpeed * _percentage;
        yield return new WaitForSeconds(_time);
        speed = defSpeed;
    }
}