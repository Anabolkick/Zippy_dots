using UnityEngine;

public class ProjectileDestroyer : MonoBehaviour
{

    void Update()
    {
        if (transform.position.y <= -5.4f)
        {
            if (StateManager.InGame)
            {
                ScoreChange.DecreaseScore();
                SoundManager.Instance.PlayPointLoseSound();
            }
            Destroy(this.gameObject);
        }
    }
}
