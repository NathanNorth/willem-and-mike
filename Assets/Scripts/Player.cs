using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public float movementSpeed = 1f; //lower is faster
    private float _moveCooldown = 0f;
    
    //Initialize player sprites
    private SpriteRenderer _render;
    public Sprite north;
    public Sprite south;
    public Sprite east;
    public Sprite west;

    public bool dialogLock = false;

    private Vector2 _dir = Vector2.down; // Hold player direction

    // Start is called before the first frame update
    void Start()
    {
        _render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogLock) return; //don't move while in dialog
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _dir,  1);
            if(hit.transform != null) hit.transform.GetComponent<Dialog>()?.TriggerDialog(this); //try trigger dialog
        }
        
        _moveCooldown = Mathf.Max(_moveCooldown - Time.deltaTime, 0f);
        if (_moveCooldown > 0f) return; //we dont run any ticks while on cooldown
        if (GetWASD().Equals(Vector2.zero)) return; //we dont run any ticks while no input pressed
        
        //actually run tick
        _moveCooldown = movementSpeed;
        Vector2 current = this.transform.position;
        Vector2 potential = current + GetWASD();
        _dir = potential - current;
        
        if (_dir == Vector2.down) // set player direction
        {
            _render.sprite = south;
        }
        else if (_dir == Vector2.up)
        {
            _render.sprite = north;
        }
        else if (_dir == Vector2.right)
        {
            _render.sprite = east;
        }
        else if (_dir == Vector2.left)
        {
            _render.sprite = west;
        }
        
        bool cast = Physics2D.Raycast(current, _dir, 1);
        if (!cast)
        {
            StartCoroutine(MoveRoutine(current, potential));
        }
    }

    private static Vector2 GetWASD()
    {
        var movement = new Vector2();
        if (Input.GetKey(KeyCode.W))
        {
            movement.y += 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement.y -= 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movement.x -= 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement.x += 1;
        }

        return movement;
    }

    private IEnumerator MoveRoutine(Vector2 current, Vector2 potential)
    {
        for (var t = 0f; t < movementSpeed; t += Time.deltaTime)
        {
            this.transform.position = Vector2.Lerp(current, potential, t/movementSpeed);

            yield return null;
        }

        this.transform.position = potential;
    }
}