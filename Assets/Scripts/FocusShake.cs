using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusShake : MonoBehaviour
{
    float speed = 15.0f; //how fast it shakes
    float amount = 0.02f; //how much it shakes
    Vector3 startingPos;
    public GameObject cube;
    public bool shake;

    // Start is called before the first frame update
    void Start()
    {
        startingPos.x = transform.position.x;
        startingPos.y = transform.position.y;
        startingPos.z = transform.position.z;
        shake = false;
    }

    public void Focus(bool s)
    {
        shake = s;
    }

    // Update is called once per frame
    void Update()
    {
        if (shake)
        {
            cube.transform.position = new Vector3(startingPos.x + (Mathf.Sin(Time.time * speed) * amount), startingPos.y + (Mathf.Sin(Time.time * speed) * amount), startingPos.z + (Mathf.Sin(Time.time * speed) * amount));
        }

    }
}
