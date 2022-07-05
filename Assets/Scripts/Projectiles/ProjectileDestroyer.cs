using UnityEngine;

public class ProjectileDestroyer : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y <= -5.5f || transform.position.y >= 6f || 
            transform.position.x <= -3.5f || transform.position.x >= 3.5f)
        {
            Destroy(this.gameObject);
        }
    }
}
