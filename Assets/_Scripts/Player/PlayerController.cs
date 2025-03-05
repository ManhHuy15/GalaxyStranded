using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float _moveSpeed = 2f;

    [SerializeField] private List<GameObject> BowIdle = new List<GameObject>();

    private Vector3 _input;

    private float x;
    private float y;
    private Rigidbody2D _rb;
    //private Animator _animator;
    private bool _isMoving;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        //_animator = GetComponent<Animator>();
    }

    void Update()
    {
        GetInput();
        //Animate();
        Direction();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        _input = new Vector2(x, y).normalized;
            
    }

    private void Move()
    {
       _rb.MovePosition(transform.position + _input * _moveSpeed * Time.fixedDeltaTime);
    }

    private void Direction()
    {
        if( _input.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            DisableAllObject();
            BowIdle[1].SetActive(true);
        }
        else if (_input.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            DisableAllObject();
            BowIdle[1].SetActive(true);
        }

        if (_input.y > 0) {
            DisableAllObject();
            BowIdle[2].SetActive(true);
        }else if (_input.y < 0)
        {
            DisableAllObject();
            BowIdle[0].SetActive(true);
        }
    }


    private void DisableAllObject()
    {
        for (int i = 0; i < BowIdle.Count; i++)
        {
            BowIdle[i].SetActive(false);
        }
    }

    //private void Animate()
    //{
    //    if (_input.magnitude > 0.1f || _input.magnitude < -0.1f)
    //    {
    //        _isMoving = true;

    //        if (_input.x > 0) transform.localScale = new Vector3(-1, 1, 1);
    //        else if (_input.x < 0) transform.localScale = new Vector3(1, 1, 1);

    //    }
    //    else
    //    {
    //        _isMoving = false;
    //    }

    //    if (_isMoving)
    //    {
    //        _animator.SetFloat("x", x);
    //        _animator.SetFloat("y", y);
    //    }
    //    _animator.SetBool("Moving", _isMoving);

    //}
}
