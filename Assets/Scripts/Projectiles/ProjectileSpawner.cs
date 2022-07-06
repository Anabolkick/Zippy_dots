using UnityEngine;


public class ProjectileSpawner : MonoBehaviour
{
    public GameObject Projectile;
    private float _spawnTimer = 1f;
    [Header("SpawnRate")]
    public float Difficult = 1f;
    public float SpawnDelay = 1.6f;
    [Header("Spawn position")]
    [Tooltip("0 +- xRange")]
    [SerializeField] private float xRange = 2.4f;
    [SerializeField]  private float y = 5.5f;

    void FixedUpdate()
    {
        var blue = new Color(33f/255f, 167f/255f, 227f/255f);
        var red = new Color(255f/255f, 5f/255f, 5f/255f);
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0)
        {
            var sprite_renderer = Projectile.GetComponent<SpriteRenderer>();

            int color = Random.Range(0, 2);
            Projectile.tag = color == 0 ? "Red" : "Blue";
            sprite_renderer.color = color == 0 ? red : blue;
            if (color == 1)
            {
                var part = Projectile.GetComponent<ParticleSystem>().main;
                part.startColor = blue;
            }
            else 
            {
                var part = Projectile.GetComponent<ParticleSystem>().main;
                part.startColor = red;
            }


            var x_pos = Random.Range(-xRange, xRange);
            var proj =  Instantiate(Projectile, new Vector2(x_pos, y), Quaternion.identity);
            var rb = proj.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2((-x_pos * Random.Range(4, 16)) * (Difficult*Random.Range(0.65f, 1f)) , -55f * Difficult * Random.Range(1f, 3f)), ForceMode2D.Force);
            //rb.AddForce(new Vector2((-x_pos * Random.Range(2, 18)) * (difficult * Random.Range(0.65f, 1f)), -180), ForceMode2D.Force);
            //rb.AddTorque(Random.Range(0.25f, 0.9f),ForceMode2D.Impulse);

            _spawnTimer = SpawnDelay;
        }
    }
}


