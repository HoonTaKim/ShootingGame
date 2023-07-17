using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalyerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid = null;
    [SerializeField] float posX = 0f;
    [SerializeField] float posY = 0f;

    [Header("Bullet Info")]
    [SerializeField] Transform bulletS = null;
    [SerializeField] Transform shild = null;
    [SerializeField] GameObject[] bPrefab = new GameObject[3];
    [SerializeField] GameObject subWepon = null;
    [SerializeField] bool subWeponOn = false;
    
    [Header("Player Info")]
    [SerializeField] int pHP = 0;
    [SerializeField] int pBoom = 0;
    [SerializeField] int pBullet = 0;
    [SerializeField] bool pHit = false;
    [SerializeField] float speed = 0f;
    [SerializeField] int bulletIdx = 0;

    [SerializeField] SpriteRenderer sp = null;
    [SerializeField] Color objColor;

    [SerializeField] float timeSave = 0f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        bulletS = transform.GetChild(0);
        shild = transform.GetChild(2);
        objColor = sp.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        pHP = 3;
        speed = 3.3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Inst.gameOver)
        {
            Move();

            timeSave += Time.deltaTime;
            if (Input.GetKey(KeyCode.Slash) && timeSave > 0.1f)
            {
                Attack();
                timeSave = 0f;
            }
            if (Input.GetKeyDown(KeyCode.Period))
            {
                Use_Boom();
            } 
        }
    }

    void Move()
    {
        posX = Input.GetAxisRaw("Horizontal");
        posY = Input.GetAxisRaw("Vertical");
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(posX, posY, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BoomItem")
        {
            Item_Boom();
            DictionaryPool.Inst.Set(collision.gameObject);
        }
        if (collision.tag == "HPItem")
        {
            Item_PlayerHP();
            DictionaryPool.Inst.Set(collision.gameObject);
        }
        if (collision.tag == "BulletItem")
        {
            Item_Bullet();
            DictionaryPool.Inst.Set(collision.gameObject);
        }
    }

    void Attack()
    {
        DictionaryPool.Inst.Get(bPrefab[bulletIdx], bulletS.transform.position, Quaternion.identity);
    }

    void Hit(int damage)
    {
        if (!pHit)
        {
            pHP -= damage;
            GameManager.Inst.PlayerHPSave(pHP, false);
            StartCoroutine(PlayerHitTime());
            if (pHP <= 0)
            {
                Die();
            } 
        }
    }

    void BossHit(int damage)
    {
        pHP -= damage;
        GameManager.Inst.PlayerHPSave(pHP, false);
        if (pHP <= 0)
        {
            Die();
        }
    }

    IEnumerator PlayerHitTime()
    {
        pHit = true;
        for (int i = 0; i < 15; i++)
        {
            sp.color = Color.gray;
            yield return new WaitForSeconds(0.03f);
            sp.color = objColor;
            yield return new WaitForSeconds(0.03f);
        }
        pHit = false;
    }

    void OnShild()
    {
        GameManager.Inst.playerShild = true;
        shild.gameObject.SetActive(true);
        pHit = true;
    }

    void OffShild()
    {
        GameManager.Inst.playerShild = true;
        shild.gameObject.SetActive(false);
        pHit = false;
    }

    void Use_Boom()
    {
        if (pBoom > 0)
        {
            BoomEffSpwner.Inst.OnBoomEffect(this.transform.position);
            pBoom--;
            GameManager.Inst.PlayerBoomSave(pBoom);
        }
    }

    void Item_Boom()
    {
        if (pBoom < 3)
        {
            pBoom++;
            GameManager.Inst.PlayerBoomSave(pBoom);
        }
    }

    void Item_PlayerHP()
    {
        if (pHP < 5)
        {
            pHP++;
            GameManager.Inst.PlayerHPSave(pHP - 1, true);
        }
    }

    void Item_Bullet()
    {
        pBullet++;
        if (pBullet < 3)
        {
            bulletIdx++;
            GameManager.Inst.PlayerBulletSave(bulletIdx); 
        }
        if (pBullet > 2 && !subWeponOn)
        {
            subWepon.SetActive(true);
            subWeponOn = true;
        }
    }

    void Die()
    {
        this.gameObject.SetActive(false);
    }
}
