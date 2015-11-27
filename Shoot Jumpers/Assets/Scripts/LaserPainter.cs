using UnityEngine;
using System.Collections;

public class LaserPainter : MonoBehaviour
{
    LineRenderer line;
    Light light;
    bool cursorShouldBeLocked = true;
    bool locked = false;
    public float missileLock = 4f;
    public float painterTick = 0;
    public Rigidbody rocketBall;
    public Rigidbody rocket;
    Rigidbody instanciateProjectileRocket;
    Rigidbody instanciateProjectilerocketBall;
    public Vector3 rocketHolder;
    float rocketBallX = 0;
    float rocketBallY = 20;
    float rocketBallZ = 7;
    public int launcherRows = 3;
    public int launcherCol = 2;
    public float missileDelay = 1.2f;
    public float missileTick = 0f;
    private bool LAUNCH = false;
    private bool holderActive = false;
    private float despawnTimer = 10f;
    public float despawnTick = 0f;

    void Start()
    {
        Debug.Log("Rocket Painter Online");
        instanciateProjectilerocketBall = gameObject.GetComponent<Rigidbody>();
        line = gameObject.GetComponent<LineRenderer>();
        light = gameObject.GetComponent<Light>();
        line.enabled = false;
        light.enabled = false;
        line.material.color = Color.blue;
    }
    // Update is called once per frame
    void Update()
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
        if (holderActive)
        {
            rocketHolder = new Vector3(instanciateProjectilerocketBall.position.x, instanciateProjectilerocketBall.position.y, instanciateProjectilerocketBall.position.z);
            despawnTick += Time.deltaTime;
            if (despawnTimer < despawnTick )
            {
                despawnTick = 0;
                holderActive = false;
            }
            

        }
        if (LAUNCH)
        {
            missileTick += Time.deltaTime;
            if (missileTick > missileDelay + 0.5)
            {
                for (int i = 0; i < launcherRows; i++)
                {
                    for (int j = 0; j < launcherCol; j++)
                    {
                        instanciateProjectileRocket = Instantiate(rocket, rocketHolder, transform.rotation) as Rigidbody;
                        Destroy(instanciateProjectileRocket.gameObject, 10f);
                        //instanciateProjectileRocket.velocity = transform.TransformDirection(new Vector3(rocketBallX, rocketBallY, rocketBallZ));
                    }
                }
                LAUNCH = false;
                missileTick = 0;
            }
        }        
        if (Input.GetButtonDown("Fire1"))
        {
            StopCoroutine("Paint");
            StartCoroutine("Paint");
        }
    }

    IEnumerator Paint()
    {
        while (Input.GetButton("Fire1"))
        {
            line.material.mainTextureOffset = new Vector2(0, Time.time);
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            light.enabled = true;
            line.enabled = true;
            line.SetPosition(0, ray.origin);

            if (Physics.Raycast(ray, out hit, 100))
            {
                line.SetPosition(1, hit.point);
                while (hit.rigidbody)
                {
                    line.material.color = Color.red;
                    painterTick += Time.deltaTime;
                    if (painterTick >= missileLock + 0.5)
                    {
                        Debug.Log("Missile Locked");
                        instanciateProjectilerocketBall = Instantiate(rocketBall, transform.position, transform.rotation) as Rigidbody;
                        Destroy(instanciateProjectilerocketBall.gameObject, 10f);
                        instanciateProjectilerocketBall.velocity = transform.TransformDirection(new Vector3(rocketBallX, rocketBallY, rocketBallZ));
                        holderActive = true;
                        LAUNCH = true;
                        painterTick = 0;
                        missileTick = 0;
                    }
                break;
                }
                while (!hit.rigidbody)
                {
                    if (painterTick > 0)
                    {
                        painterTick = 0;
                        Debug.Log("Target Lost");
                    }
                break;
                }
            }
            else
            {
                line.SetPosition(1, ray.GetPoint(100));
            }

            yield return null;
            line.material.color = Color.blue;
            line.enabled = false;
            light.enabled = false;
        }
    }
}
