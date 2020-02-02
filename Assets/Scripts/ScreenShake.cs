using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public GameObject objectToShake;
    [SerializeField] float shakeDuration;
    [SerializeField] Vector3 maxShake;
    Vector3 startPos;
    float dieTime = 0.0f;
    Coroutine shake;
    bool shakie;

    private void Start()
    {
        objectToShake = Camera.main.transform.gameObject;
        startPos = transform.position;
    }

    public void Shake()
    {
        transform.position = startPos;
        dieTime = Time.time + shakeDuration;
        shakie = true;
    }

    void Update()
    {
        if (shakie)
        {
            if (Time.time < dieTime)
            {
                objectToShake.transform.position += new Vector3(Random.Range(-maxShake.x, maxShake.x), Random.Range(-maxShake.y, maxShake.y), Random.Range(-maxShake.z, maxShake.z));
            }
            else
            {
                transform.position = startPos;
                shakie = false;
            }
        }
    }
}
