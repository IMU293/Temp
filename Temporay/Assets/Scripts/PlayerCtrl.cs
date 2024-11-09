using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private float v = 0;     //수직 입력을 저장할 변수 (앞뒤 이동)
    private float h = 0;     //수평 입력을 저장할 변수 (좌우 이동)

    private float v1 = 0;    //보정된 수직 이동 값을 저장하는 변수
    private float h1 = 0;    //보정된 수평 이동 값을 저장하는 변수

    private Vector3 moveDir = new Vector3(0, 0, 0); //이동 방향을 나타내는 벡터

    [SerializeField] private float speed = 10f;   //캐릭터의 이동 속도
    [SerializeField] private float runSpeed = 20f;  //달리기 속도
    [SerializeField] private float rotSpeed = 500f;  //캐릭터의 회전 속도

    private Animator _animator;

    public GameObject bullet;        // 총알 Prefab
    public Transform fireTr;         // 총알 발사 위치 (플레이어 앞)

    public int playerHp = 5;
    [SerializeField] private int playerExp = 0;
    [SerializeField] private int maxExp = 10;
    private int playerLv = 1;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerExp >= maxExp)
        {
            LevelUP();
        }
        _animator.SetBool("p_Attack", false);
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");

        v1 = v * Mathf.Sqrt(1 - (Mathf.Pow(h, 2) / 2));
        h1 = h * Mathf.Sqrt(1 - (Mathf.Pow(v, 2) / 2));

        float currentSpeed = speed;

        if (v > 0 && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            currentSpeed = runSpeed;
        }

        moveDir = (Vector3.forward * v1 + Vector3.right * h1);

        // 캐릭터 이동
        transform.Translate(moveDir * currentSpeed * Time.deltaTime, Space.Self);
        // 회전
        transform.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));

        _animator.SetFloat("p_V", v1);
        _animator.SetFloat("p_H", h1);

        // 총알 발사 (마우스 왼쪽 버튼 클릭)
        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
            _animator.SetBool("p_Attack", true);
        }
        if (playerHp <= 0)
        {
            
        }
    }
    
    // 총알 발사 메서드
    private void FireBullet()
    {
        if (bullet != null && fireTr != null)
        {
            // 총알 인스턴스를 발사 위치에서 생성
            Instantiate(bullet, fireTr.position, fireTr.rotation);
        }
        else
        {
            Debug.LogWarning("Bullet or Fire Transform is not set up!");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Exp"))
        {
            playerExp++;
            Destroy(other.gameObject);
            Debug.Log("Exp: " + playerExp);
        }
    }
    void LevelUP()
    {
        playerExp = 0;
        maxExp += 5;
        playerLv += 1;
        Debug.Log("Level UP");
    }
}

