using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float _launchForce = 150;
    [SerializeField] float _maxDragDistance = 5;

    private Vector2 _startPosition;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _lastVelocity;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _startPosition = _rigidbody2D.position;
        _rigidbody2D.isKinematic = true;
        //_rigidbody2D.transform.eulerAngles = new Vector3(0, 1, 1);
       
    }

    private void OnMouseUp()
    {
        Vector2 currentPosition = _rigidbody2D.position;
        Vector2 direction = _startPosition - currentPosition;

        _rigidbody2D.isKinematic = false;
        _rigidbody2D.constraints = RigidbodyConstraints2D.None;
        _rigidbody2D.AddForce(direction * _launchForce);

    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = mousePosition;

        float distance = Vector2.Distance(desiredPosition, _startPosition);
        if(distance > _maxDragDistance)
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
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var speed = _lastVelocity.magnitude;
        var direction = Vector3.Reflect(_lastVelocity.normalized, collision.contacts[0].normal);

        _rigidbody2D.velocity = direction * speed / (float)1.5;
        StartCoroutine(ResetAfterDelay());
    }

    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        _rigidbody2D.position = _startPosition;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;

        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

}
