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

    private Vector2 _startPosition;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _lastVelocity;
    private bool delay = false;

    private int _inputSize = 0;
    private Vector2[] _directions = new Vector2[100];
    private float[] _launchForces = new float[100];

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetLaunch();
    }
    // Start is called before the first frame update
    void Start()
    {
        _startPosition = _rigidbody2D.position;
        _rigidbody2D.isKinematic = true;

        int i = 0;
        /*while(i < _inputSize)
        {
            if(delay == false)
            {
                Debug.LogError("ball nr " + i);
                LaunchBall(_directions[i], _launchForces[i]);
                i++;
            }
        }*/
        LaunchBall(_directions[i], _launchForces[i]);
        /*for(int i = 0; i < _inputSize; i++)
        {
            Debug.LogError("ball nr " + i);
            StartCoroutine(DelayOnStart());
            //Thread.Sleep(3000);
            LaunchBall(_directions[i], _launchForces[i]);
        }*/
    }

    private IEnumerator DelayOnStart()
    {
        yield return new WaitForSeconds(5);
    }

    private void NotOnMouseUp()
    {
        Vector2 currentPosition = _rigidbody2D.position;
        Vector2 direction = _startPosition - currentPosition;

        _rigidbody2D.isKinematic = false;
        _rigidbody2D.constraints = RigidbodyConstraints2D.None;
        _rigidbody2D.AddForce(direction * _launchForce);
        delay = true;
        //StartCoroutine(ResetAfterTimeEnds());

    }

    private void NotOnMouseDrag()
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
        Debug.LogError("OnCollisionEnter");
        //var speed = _lastVelocity.magnitude;
        //var direction = Vector3.Reflect(_lastVelocity.normalized, collision.contacts[0].normal);
        //_rigidbody2D.velocity = direction * speed / (float)1.5;
        if (delay == true)
        {
            delay = false;
            Debug.LogWarning("Delay started");
            StartCoroutine(ResetAfterDelay());
        }
    }

    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(5);
        Debug.LogError("Reset started");
        _rigidbody2D.position = _startPosition;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;

        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        delay = false;
    }

    private void SetLaunch()
    {
        ReadTextFile("C:/Users/buciu/Desktop/input.txt");
    }

    private void ReadTextFile(string filePath)
    {
        //Vector2 direction;
        //float launchForce;
        StreamReader streamReader = new StreamReader(filePath);
        while (!streamReader.EndOfStream)
        {
            string input = streamReader.ReadLine();
            string[] words = input.Split(' ');
            _directions[_inputSize].x = (float)System.Convert.ToDouble(words[0]);
            _directions[_inputSize].y = (float)System.Convert.ToDouble(words[1]);
            _launchForces[_inputSize] = (float)System.Convert.ToDouble(words[2]);
            Debug.Log("x: " + _directions[_inputSize].x + " y: " + _directions[_inputSize].y + " force: "
                + _launchForces[_inputSize]);
            _inputSize++;
            Debug.LogWarning(" inputSize: " + _inputSize);
            /*direction.x = System.Convert.ToInt32(words[0]);
            direction.y = System.Convert.ToInt32(words[1]);
            launchForce = (float)System.Convert.ToDouble(words[2]);*/

        }
        streamReader.Close();
    }

    private void LaunchBall(Vector2 _direction, float _launchForce)
    {
        _rigidbody2D.isKinematic = false;
        _rigidbody2D.constraints = RigidbodyConstraints2D.None;
        _rigidbody2D.AddForce(_direction * _launchForce);
        delay = true;
    }

}
