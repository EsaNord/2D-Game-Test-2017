using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColorChange : MonoBehaviour {

    Color color;
    SpriteRenderer sR;
    Transform t;
    int time;

    // Use this for initialization
    void Start () {
        sR = GetComponent<SpriteRenderer>();
        t = GetComponent<Transform>();
        time = 0;
    }

    // Update is called once per frame
    void Update () {
        if (time == 10)
        {
            color = sR.color;
            color.r = Random.Range(0f, 1f);
            color.g = Random.Range(0f, 1f);
            color.b = Random.Range(0f, 1f);
            sR.color = color;

            t.localScale = new Vector3(Random.Range(5f, 15f), Random.Range(5f, 15f), 1f);
            t.position = new Vector2(Random.Range(-7f, 7f), Random.Range(-4f, 4f));
            time = 0;
        }
        time += 1;
    }
}