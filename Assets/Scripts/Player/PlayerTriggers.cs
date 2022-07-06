using System.Collections;
using UnityEngine;

public class PlayerTriggers : MonoBehaviour
{
    void Awake()
    {
        EventManager.OnLoseEvent.AddListener(DestroyPlayer);
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        var prSyst = coll.gameObject.GetComponent<ParticleSystem>();
        var transColl = coll.gameObject.GetComponent<Transform>();

        if (coll.gameObject.tag == this.gameObject.tag)
        {
            ScoreChange.IncreaseScore();
        }
        else
        {
            EventManager.OnLoseEvent.Invoke();
        }
        DestroyProjAnim(coll);
    }

    private void DestroyProjAnim(Collider2D collider)
    {
        collider.gameObject.GetComponent<Rigidbody2D>().simulated = false;
        collider.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        var prSyst = collider.gameObject.GetComponent<ParticleSystem>();
        var transColl = collider.gameObject.GetComponent<Transform>();
        transColl.position = (transform.position + transColl.position) / 2;
        var emission = prSyst.emission;
        emission.enabled = true;
        prSyst.Play();
        StartCoroutine(TimerToDestroyObj(prSyst.main.duration, collider.gameObject));
    }

    private void DestroyPlayerAnim()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        var emission = gameObject.GetComponent<ParticleSystem>().emission;
        emission.enabled = true;
        gameObject.GetComponent<ParticleSystem>().Play();
    }

    private void DestroyPlayer()
    {
        DestroyPlayerAnim();
        GetComponentInParent<PlayerControl>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<TrailRenderer>().enabled = false;
    }

    private IEnumerator TimerToDestroyObj(float time, GameObject obj)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
}
