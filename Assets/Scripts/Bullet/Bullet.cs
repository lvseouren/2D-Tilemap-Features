using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class Bullet : PhysicsObject
{
    enum Direction
    {
        left = 1,
        right = 2
    }
    bool isValid = false;
    public GameObject bulletVisuals;
    public GameObject collidedParticleSystem;
    public CircleCollider2D bulletCollider2D;

    private float durationOfCollidedParticleSystem;
    Direction direction;
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

    public void OnGenerate(Vector3 pos, bool isLeft)
    {
        bulletCollider2D.enabled = true;
        collidedParticleSystem.SetActive(false);
        bulletVisuals.SetActive(true);
        pos.y = pos.y + 0.34f;
        transform.position = pos;
        rb2d.position = pos;
        rb2d.velocity = new Vector2(5,0);
        if (isLeft)
            rb2d.velocity *= -1;
        direction = isLeft ? Direction.left : Direction.right;
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
            Vector3 pos = transform.position + new Vector3(0.7f, 0, 0)*(direction==Direction.left?-1:1);
            TileBase tile = TilemapDataManager.Instance.GetTile(pos);
            TilemapDataManager.Instance.ClearTile(pos);
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
        BulletPool.Instance.Recycle(this);
    }

}
