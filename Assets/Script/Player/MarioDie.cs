using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioDie : MonoBehaviour
{

    Vector2 locationDie;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(AnimtionMarioDie());
    }

    IEnumerator AnimtionMarioDie()
    {
        while (true)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + 20.5f * Time.deltaTime);
            if (transform.localPosition.y > locationDie.y + 120f)
            {
                break;
            }
            yield return null;

            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - 20.5f * Time.deltaTime);
            if (gameObject.transform.position.y < -10f)
            {
                Destroy(gameObject);
                break;
            }
            yield return null;
        }
    }
}
