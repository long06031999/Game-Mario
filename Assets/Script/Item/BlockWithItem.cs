using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockWithItem : MonoBehaviour
{

    public float doNayCuaKhoi = 0.5f;
    public float tocDo = 4f;
    private bool duocNay = true;
    private Vector3 viTriLucDau;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        //húc vào phía dưới
        if (col.collider.tag == "Player" && col.contacts[0].normal.y>0)
        {
            viTriLucDau = transform.position;
            KhoiNayLen();
        }
        //chạm vào khối
        else
        {
            print("chạm vào khối");
        }
    }

    private void KhoiNayLen()
    {
        if (duocNay)
        {
            StartCoroutine(KhoiNay());
            duocNay = false;
        }
    }

    IEnumerator KhoiNay()
    {
        while (true)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + tocDo * Time.deltaTime);
            if (transform.localPosition.y >= viTriLucDau.y + doNayCuaKhoi) break;
            yield return null;
        }
        while (true)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - tocDo * Time.deltaTime);
            if (transform.localPosition.y <= viTriLucDau.y) break;
            yield return null;
        }
    }
}
