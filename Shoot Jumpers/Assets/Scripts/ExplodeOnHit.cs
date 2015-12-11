using UnityEngine;
using System.Collections;

public class ExplodeOnHit : MonoBehaviour {

    public float explosiveRadius = 25;
    public float explosiveStrength = 100.0f;

	void Start () 
    {
	
	}
	


	void Update () 
    {
	
	}

    void OnCollisionEnter(Collision coll)
    {
        Collider[] hit = Physics.OverlapSphere(transform.position,explosiveRadius);

        foreach(Collider point in hit)
        {
            Rigidbody body = point.gameObject.GetComponent<Rigidbody>();
            if (body !=null)
            {
                Vector3 direction = body.transform.position - transform.position;
                direction = direction.normalized;
                body.AddForce(direction * explosiveStrength);
            }
            //point.rigidbody.AddForce();
        }
        Destroy(gameObject);
    }
}
