using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject visualEffect;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Bullet")
        {
            if (collision.gameObject.tag == "Virus")
            {
                StartCoroutine(Wait(0.3f));
                Destroy(collision.gameObject);
                GameManager.instance.DisInfected++;
            }
            var createdEffect = Instantiate(visualEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(createdEffect, 1f);

        }
    }


    IEnumerator Wait(float time)
    {
        print("before wait");
        yield return new WaitForSeconds(time);
        print("After wait");
    }
}
