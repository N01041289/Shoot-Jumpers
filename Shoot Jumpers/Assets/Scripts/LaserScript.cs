using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {
    Light light;
    LineRenderer line;  
    public int laserDamage = 5;
    bool cursorShouldBeLocked = true;

    void Start ()
    {
        Debug.Log("Lazer Script Online");
        line = gameObject.GetComponent<LineRenderer>();
        light = gameObject.GetComponent<Light>();
        line.enabled = false;
        light.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (cursorShouldBeLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            StopCoroutine("FireLaser");
            StartCoroutine("FireLaser");
        }
	}
    IEnumerator FireLaser()
    {
        while (Input.GetButton("Fire1"))
        {
            light.enabled = true;
            line.enabled = true;

            line.material.mainTextureOffset = new Vector2(0, Time.time);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            line.SetPosition(0, ray.origin);

            if (Physics.Raycast(ray, out hit, 100))
            {
                line.SetPosition(1, hit.point);
                if (hit.rigidbody)
                {
                        hit.rigidbody.AddForceAtPosition(transform.forward * laserDamage, hit.point);
                }
            }
            else
            { 
                line.SetPosition(1, ray.GetPoint(100));
            }

            yield return null;

            line.enabled = false;
            light.enabled = false;
        }


    }
}
