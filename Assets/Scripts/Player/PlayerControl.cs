using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{
    public float rotationSpeed = 100.0f;
    private float _delta = 0.0f;
    private Quaternion _startRotation;
    private float _direction = 1;
    private bool _isMove = true;

    void Start()
    {
        EventManager.OnLoseEvent.AddListener(StopMove);
        _startRotation = transform.rotation;
        StartCoroutine(PlayerRotation());
    }

    IEnumerator PlayerRotation()
    {
        while (_isMove)
        {
            Quaternion tmp = transform.rotation;
            transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * -rotationSpeed * _direction);
            _delta += Quaternion.Angle(tmp, transform.rotation) * _direction;

            if (_delta > 360.0f || _delta < -360f)
            {
                _delta = 0.0f;
                transform.rotation = _startRotation;
            }

            yield return null;
        }
    }

    void StopMove()
    {
        _isMove = false;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                StartCoroutine(ChangeDirection());
            }
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    StartCoroutine(ChangeDirection());
        //}
    }

    IEnumerator ChangeDirection()
    {
        _direction = -_direction;
        EventManager.OnDirectionChangedEvent.Invoke();
        var delta = rotationSpeed * 0.35f;
        rotationSpeed -= delta;
        for (int i = 0; i < 5; i++)
        {
            rotationSpeed += delta / 5;
            yield return new WaitForSeconds(0.15f);
        }
    }

}
