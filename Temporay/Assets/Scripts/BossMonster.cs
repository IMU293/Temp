using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BossMonster : MonoBehaviour
{
    private NavMeshAgent b_Agent = null;
    private GameObject _target = null;

    [SerializeField] private int _damage = 3;
    public int HP = 10;
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            var playerCtrl = coll.gameObject.GetComponent<PlayerCtrl>();
            playerCtrl.playerHp -= _damage;
            Debug.Log("Hp: " + playerCtrl.playerHp);
        }
    }
        // Start is called before the first frame update
        void Start()
    {
        b_Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _target = GameObject.FindWithTag("Player");
        b_Agent.SetDestination(_target.transform.position);
        if (HP <= 0)
        {
            HP = 0;
            OnBossDefeated();
        }
    }
    private void OnBossDefeated()
    {
        // GameManager의 LoadNextScene 메서드를 호출하여 다음 씬으로 이동
        GameManager.Instance.LoadNextScene();

        // 보스 오브젝트 파괴
        Destroy(gameObject);
    }

}
