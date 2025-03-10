using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private List<GameObject> PlayerIdle = new List<GameObject>();
    [SerializeField] private List<GameObject> PlayerWalk = new List<GameObject>();
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] bool isSword ;
    private Vector3 _input;
    private Vector2 lastDirect;
    private float x;
    private float y;
    private Rigidbody2D _rb;
    private float currentTime;
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
        Attack();
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
        _isMoving = _input.magnitude > 0.1f;

    }

    private void Move()
    {
       _rb.MovePosition(transform.position + _input * _moveSpeed * Time.fixedDeltaTime);
    }

    private void Direction()
    {
        if (_input.magnitude > 0.1f || _input.magnitude < -0.1f)
        {
            if (_input.x > 0)
                transform.localScale = new Vector3(-1, 1, 1); 
            else if (_input.x < 0)
                transform.localScale = new Vector3(1, 1, 1); 

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x != 0 && !PlayerWalk[1].activeSelf)
                {
                    DisableAllObject();
                    PlayerWalk[1].SetActive(true);
                    lastDirect = new Vector2(x, 0);
                }
            }
            else 
            {
                if (y > 0 && !PlayerWalk[2].activeSelf)
                {
                    DisableAllObject();
                    PlayerWalk[2].SetActive(true);
                    lastDirect = new Vector2(0, y);
                }
                else if (y < 0 && !PlayerWalk[0].activeSelf) 
                {
                    DisableAllObject();
                    PlayerWalk[0].SetActive(true);
                    lastDirect = new Vector2(0, y);
                }
            }
        }
        else 
        {
            if (lastDirect.x != 0 && !PlayerIdle[1].activeSelf)
            {
                DisableAllObject();
                PlayerIdle[1].SetActive(true);
                
            }
            else if (lastDirect.y < 0 && !PlayerIdle[0].activeSelf)
            {
                DisableAllObject();
                PlayerIdle[0].SetActive(true);
            }else if (lastDirect.y > 0 && !PlayerIdle[2].activeSelf)
            {
                DisableAllObject();
                PlayerIdle[2].SetActive(true);
            }
        }
    }

    private void DisableAllObject()
    {
        foreach (GameObject obj in PlayerIdle)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in PlayerWalk)
        {
            obj.SetActive(false);
        }
    }

    void Attack()
    {
        if(isSword) return;
        currentTime += Time.deltaTime;
        if (currentTime < 1f) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bows = GameObject.FindGameObjectWithTag("Bow");
            Vector3 spawnOffset = bows.transform.forward * 0.5f;
            Vector3 pos = bows.transform.position + spawnOffset;
            pos.z = -1f;

            float angle = Mathf.Atan2(lastDirect.y, lastDirect.x) * Mathf.Rad2Deg;
            SpawnerManager.Instance.SpawnObject(arrowPrefab, pos, Quaternion.Euler(0, 0, angle));
            currentTime = 0;
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
