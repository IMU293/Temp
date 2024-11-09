using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            // 발사 방향으로 힘을 주어 총알을 발사
            rb.AddForce(transform.forward * 2f, ForceMode.Impulse);
        }
        else
        {
            Debug.LogError("Rigidbody not found on bullet!");
        }

        // 일정 시간이 지나면 총알이 사라지도록 설정
        Destroy(gameObject, 3f);
    }

    // 충돌 처리
    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Monster"))
        {
            var monster = coll.gameObject.GetComponent<MonsterCtrl>();
            monster.HP--;
            Destroy(gameObject);       // 총알을 파괴
        }
        else if (coll.CompareTag("Boss"))
        {
            var b_monster = coll.gameObject.GetComponent<BossMonster>();
            b_monster.HP--;
            Destroy(gameObject);       // 총알을 파괴
        }
    }
}


