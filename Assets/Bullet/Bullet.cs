using UnityEngine;
using System.Collections;

public class Bullet : PhysicsObject
{
    bool isValid = false;
    public GameObject bulletVisuals;
    public GameObject collidedParticleSystem;
    public CircleCollider2D bulletCollider2D;

    private float durationOfCollidedParticleSystem;
    // Use this for initialization
    void Start()
    {
        gravityModifier = 0f;
        durationOfCollidedParticleSystem = collidedParticleSystem.GetComponent<ParticleSystem>().main.duration;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnGenerate(Vector3 pos)
    {
        transform.position = pos;
        rb2d.velocity = new Vector2(5,0);
        isValid = true;
    }

    void FixedUpdate()
    {
    }

    void OnTriggerEnter2D(Collider2D theCollider)
    {
        if (isValid && theCollider.CompareTag("Ground"))
        {
            BulletCollided();
        }
    }

    void BulletCollided()
    {
        rb2d.velocity = Vector2.zero;
        bulletCollider2D.enabled = false;
        bulletVisuals.SetActive(false);
        collidedParticleSystem.SetActive(true);
        Invoke("DeactivateBulletGameObject", durationOfCollidedParticleSystem);

    }

    void DeactivateBulletGameObject()
    {
        isValid = false;
        gameObject.SetActive(false);
    }

}
