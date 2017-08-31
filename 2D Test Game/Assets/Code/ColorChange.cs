using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour {
        
    public SpriteRenderer sR;    
    public Color[] colorArray;

    private int currentIndex = 0;

    private void Awake()
    {
        Debug.Log("Awake");
    }

    // Use this for initialization
    void Start () {
        Debug.Log("Start");

        if (sR == null)
        {
            sR = GetComponent<SpriteRenderer>();
        }        
	}

    private void OnEnable()
    {
        Debug.Log("Enable");
    }

    // Update is called once per frame
    void Update () {
        Debug.Log("up");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentIndex += 1;
            if (currentIndex > 3)
            {
                currentIndex = 0;
            }
        }
        sR.color = colorArray[currentIndex];
    }

    public void FixedUpdate()
    {
        Debug.Log("fixup");
    }

    public void OnDisable()
    {
        Debug.Log("ondis");
    }

    public void OnDestroy()
    {
        Debug.Log("dead");
    }
}