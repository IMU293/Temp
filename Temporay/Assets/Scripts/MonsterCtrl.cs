using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    private NavMeshAgent m_Agent = null;
    private GameObject _target = null;

    private Animator m_Animator = null;

    public GameObject expGem;

    public int HP = 5;

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            var playerCtrl = coll.gameObject.GetComponent<PlayerCtrl>();
            playerCtrl.playerHp--;
            Debug.Log("Hp: "+playerCtrl.playerHp);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            m_Animator.SetBool("getHit", true);
        }
    }
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _target = GameObject.FindWithTag("Player");
        m_Agent.SetDestination(_target.transform.position);

        m_Animator.SetBool("getHit", false);

        if (HP <= 0)
        {
            dieMonster();
        }

    }
    void dieMonster()
    {
        Instantiate(expGem, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
   
}
