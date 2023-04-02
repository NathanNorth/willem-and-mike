using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanDialogue : Dialog
{
    public Dir startingDirection = Dir.South;

    public Sprite north;
    public Sprite east;
    public Sprite south;
    public Sprite west;

    // private Vector2 _dir; //holds player direction
    private Animator _animator;
    private static readonly int X = Animator.StringToHash("X");
    private static readonly int Y = Animator.StringToHash("Y");

    private SpriteRenderer spriteRenderer;

    private Sprite[] Dirs()
    {
        return new[]{ north, east, south, west };
    }

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Dirs()[(int) startingDirection];
    }

    public override void TriggerDialog(Player player)
    {
        base.TriggerDialog(player);
        FacePlayer(player.transform.position);
    }

    private static readonly Dictionary<Vector2, int> translation = new Dictionary<Vector2, int>()
    {
        { new Vector2(0, 1), 0},
        { new Vector2(1, 0), 1},
        { new Vector2(0, -1), 2},
        { new Vector2(-1, 0), 3},
    };

    protected void FacePlayer(Vector2 playerPos)
    {
        Vector2 myPos = this.transform.position;
        Vector2 dir = playerPos - myPos;
        int ordinal = translation[dir];
        Sprite sprite = Dirs()[ordinal];
        spriteRenderer.sprite = sprite;
    }
}
