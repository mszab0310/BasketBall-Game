using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody2D.isKinematic = true;
    }
}
