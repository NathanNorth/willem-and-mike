using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanDialogue : Dialog
{
    public Dir startingDirection = Dir.South;


    private Vector2 _dir; //holds player direction
    private Animator _animator;
    private static readonly int X = Animator.StringToHash("X");
    private static readonly int Y = Animator.StringToHash("Y");
    
    public enum Dir
    {
        North, South, East, West
    }

    private void Start()
    {
        _dir = startingDirection switch
        {
            Dir.North => Vector2.up,
            Dir.South => Vector2.down,
            Dir.East => Vector2.right,
            Dir.West => Vector2.left,
            _ => _dir
        };

        _animator = GetComponent<Animator>();
        _animator.SetFloat(X, _dir.x);
        _animator.SetFloat(Y, _dir.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public override void TriggerDialog(Player player)
    {
        base.TriggerDialog(player);
        FacePlayer(player.transform.position);
    }

    private void FacePlayer(Vector2 playerPos)
    {
        Vector2 myPos = this.transform.position;
        
        if (myPos.x < playerPos.x && (int)myPos.y == (int)playerPos.y) //Player is to the right
        {
            _dir = Vector2.right;
        }
        else if (myPos.y < playerPos.y && (int)myPos.x == (int)playerPos.x) //Player is up
        {
            _dir = Vector2.up;
        }
        else if (myPos.y > playerPos.y && (int)myPos.x == (int)playerPos.x) //Player is down
        {
            _dir = Vector2.down;
        }
        else if (myPos.x > playerPos.x && (int)myPos.y == (int)playerPos.y) //Player is to the left
        {
            _dir = Vector2.left;
        }
        
        _animator.SetFloat(X, _dir.x);
        _animator.SetFloat(Y, _dir.y);
    }
}
