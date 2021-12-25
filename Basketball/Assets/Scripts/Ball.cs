using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float _launchForce = 200;
    [SerializeField] float _maxDragDistance = 5;
    public float _timeScale = 1f;

    private Vector2 _startPosition;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _lastVelocity;
    private bool delay = false;
    private bool didScore = false;
    public GameObject hoop;
    private float _distanceFromHoop;
    private float _minDistanceFromHoop;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _minDistanceFromHoop = Vector2.Distance(_rigidbody2D.position, hoop.transform.position);
        _startPosition = _rigidbody2D.position;
        _rigidbody2D.isKinematic = true;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnMouseUp()
    {
        Vector2 currentPosition = _rigidbody2D.position;
        Vector2 direction = _startPosition - currentPosition;

        _rigidbody2D.isKinematic = false;
        _rigidbody2D.constraints = RigidbodyConstraints2D.None;
        _rigidbody2D.AddForce(direction * _launchForce);
        delay = true;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = mousePosition;

        float distance = Vector2.Distance(desiredPosition, _startPosition);
        if (distance > _maxDragDistance)
        {
            Vector2 direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + (direction * _maxDragDistance);
        }
        _rigidbody2D.position = desiredPosition;
    }
    // Update is called once per frame
    void Update()
    {
        _lastVelocity = _rigidbody2D.velocity;
        Time.timeScale = _timeScale;
        _distanceFromHoop = Vector2.Distance(_rigidbody2D.position, hoop.transform.position);
        if (_distanceFromHoop < _minDistanceFromHoop)
        {
            _minDistanceFromHoop = _distanceFromHoop;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (delay == true)
        {
            delay = false;
            StartCoroutine(ResetAfterDelay());
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogWarning("Trigggerrrrrr");
        if (collision.gameObject == hoop && _rigidbody2D.position.y > collision.gameObject.transform.position.y && didScore == false)
        {
            ScoreScript.scoreValue += 1;
            didScore = true;
        }

    }


    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(5);
        _rigidbody2D.position = _startPosition;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        delay = false;
        didScore = false;
    }


    public void LaunchBall(Vector2 direction, float launchForce)
    {
        _rigidbody2D.isKinematic = false;
        _rigidbody2D.constraints = RigidbodyConstraints2D.None;
        _rigidbody2D.AddForce(direction * launchForce);
        delay = true;
    }

    public float GetMinDistanceFromHoop()
    {
        return _minDistanceFromHoop;
    }

}
