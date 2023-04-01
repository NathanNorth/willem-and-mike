using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public float movementSpeed = 1f; //lower is faster
    public LayerMask collision;
    public LayerMask trigger;

    public Dir startingDirection = Dir.South;
    
    public bool dialogLock = false;
    
    private float _moveCooldown = 0f;
    private Animator _animator;

    public enum Dir
    {
        North, South, East, West
    }

    private Vector2 _dir; // Hold player direction
    private static readonly int X = Animator.StringToHash("X");
    private static readonly int Y = Animator.StringToHash("Y");
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");

    private bool _walkingLock = false;

    // Start is called before the first frame update
    private void Start()
    {
        switch (startingDirection)
        {
            case Dir.North: _dir = Vector2.up;
                Debug.Log("up");
                break;
            case Dir.South: _dir = Vector2.down;
                break;
            case Dir.East: _dir = Vector2.right;
                break;
            case Dir.West: _dir = Vector2.left;
                break;
        }
        _animator = GetComponent<Animator>();
        
        _animator.SetFloat(X, _dir.x);
        _animator.SetFloat(Y, _dir.y);
    }

    // Update is called once per frame
    void Update()
    {
        //scene changes
        RaycastHit2D currentTileCast = Physics2D.Raycast(transform.position, Vector2.up, .25f, trigger); //stay inside tile
        if(currentTileCast && currentTileCast.transform != null) currentTileCast.transform.gameObject.GetComponent<SceneTrigger>().TriggerSceneChange();
        
        if (dialogLock) return; //don't move while in dialog
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _dir,  1, collision);
            if(hit.transform != null) hit.transform.GetComponent<Dialog>()?.TriggerDialog(this); //try trigger dialog
        }
        if (_walkingLock) return; //we dont run any ticks while on cooldown
        if (GetWASD().Equals(Vector2.zero)) return; //we dont run any ticks while no input pressed
        
        //actually run tick
        _moveCooldown = movementSpeed;
        Vector2 current = this.transform.position;
        Vector2 potential = current + GetWASD();
        _dir = potential - current;
        
        //direction animations
        _animator.SetFloat(X, _dir.x);
        _animator.SetFloat(Y, _dir.y);

        bool cast = Physics2D.Raycast(current, _dir, 1, collision);
        if (!cast)
        {
            StartCoroutine(MoveRoutine(current, potential));
        }
    }

    private static Vector2 GetWASD()
    {
        var movement = new Vector2();
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            movement.y += 1;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            movement.y -= 1;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            movement.x -= 1;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            movement.x += 1;
        }

        return movement;
    }

    private IEnumerator MoveRoutine(Vector2 current, Vector2 potential)
    {
        _animator.SetBool(IsWalking, true);
        _walkingLock = true;
        for (var t = 0f; t < movementSpeed; t += Time.deltaTime)
        {
            this.transform.position = Vector2.Lerp(current, potential, t/movementSpeed);

            yield return null;
        }

        _walkingLock = false;
        this.transform.position = potential;
        _animator.SetBool(IsWalking, false);
    }
}