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
    private int _index;
    private float _distanceLeftToHoop;
    private Vector2 _leftCollider;
    private Vector2 _hoopCollider;
    private bool _wasAbove;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _minDistanceFromHoop = Vector2.Distance(_rigidbody2D.position, hoop.transform.position);
        _startPosition = _rigidbody2D.position;
        _rigidbody2D.isKinematic = true;
        _leftCollider = Camera.main.GetComponent<EdgeCollider2D>().points[0];
        _hoopCollider = new Vector2(hoop.transform.position.x, hoop.transform.position.y);
        _distanceLeftToHoop = Vector2.Distance(_leftCollider, _hoopCollider);
        _wasAbove = (_rigidbody2D.position.y > hoop.transform.position.y);

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
            _wasAbove = (_rigidbody2D.position.y > hoop.transform.position.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsScore(collision))
        {
            ScoreScript.scoreValue += 1;
            didScore = true;
        }

    }

    private bool IsScore(Collider2D collision)
        => collision.gameObject.name.Equals("Hoop") 
            && _rigidbody2D.position.y > collision.gameObject.transform.position.y 
        && !didScore;
    


    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(7);
        _rigidbody2D.position = _startPosition;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        delay = false;
    }


    public void LaunchBall(Vector2 direction, float launchForce, int index)
    {
        _index = index;
        didScore = false;
        this._rigidbody2D.isKinematic = false;
        _launchForce = launchForce;
        _rigidbody2D.constraints = RigidbodyConstraints2D.None;
        _rigidbody2D.AddForce(direction * launchForce);
        delay = true;
        if (delay)
        {
            delay = false;
            StartCoroutine(ResetAfterDelay());
        }
    }

    public int GetIndex()
    {
        return _index;
    }

    public float GetMinDistanceFromHoop()
    {
        return _minDistanceFromHoop;
    }
    public float GetDistanceFromLeftToHoop()
    {
        return _distanceLeftToHoop;
    }

    private float percentageFitness()
    {
        float accuracy = 0f;
        accuracy = 100 - _minDistanceFromHoop / _distanceLeftToHoop * 100;
        int scored = didScore ? 100 : 0;
        return accuracy * 0.4f + scored * 0.6f;
    }

    private float betterPercentageFitness()
    {
        float accuracy = 0f;
        accuracy = 100 - _minDistanceFromHoop / _distanceLeftToHoop * 100;
        int scored = didScore ? 100 : 0;
        int above = _wasAbove ? 100 : 0;
        return 0.6f*accuracy + 0.2f*above + 0.2f*scored;
    }
    public float GetFitness()
    {
        return this.betterPercentageFitness();
    }

}
