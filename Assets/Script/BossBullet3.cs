using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet3 : MonoBehaviour
{
    [SerializeField] GameObject bulletRotaion = null;
    [SerializeField] GameObject bulletPrefab = null;

    private void OnEnable()
    {
        StartCoroutine(BulletFire());
    }

    IEnumerator BulletFire()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i <= 360; i += 45)
        {
            bulletRotaion.transform.rotation = Quaternion.Euler(new Vector3(0, 0, i));
            DictionaryPool.Inst.Get(bulletPrefab, this.transform.position, bulletRotaion.transform.rotation);
        }
        DictionaryPool.Inst.Set(this.gameObject);

    }
}
