using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

    public float speed = 20;
    public Rigidbody projectile;

    // Use this for initialization
    void Start()
    {
        Debug.Log("Test Script Online");
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Firing");

            Rigidbody instanciateProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
            instanciateProjectile.velocity = transform.TransformDirection(new Vector3(0,0,speed));
        }
    }
}
