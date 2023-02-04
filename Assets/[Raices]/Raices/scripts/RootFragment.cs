using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootFragment : MonoBehaviour
{
    [SerializeField] int step = 0;

    [SerializeField] Sprite part1;
    [SerializeField] Sprite part2;

    [SerializeField] Transform spawner;

    [HideInInspector] public RootController controller;

    public int Step { get => step; set => step = value; }

    SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void Next()
    {
        Step++;
        switch (Step)
        {
            case 1:
                // colocar fragmento 1
                sprite.sprite = part1;
                // crear siguiente
                controller.AddFragment(spawner);
                break;
            case 2:
                // colocar fragmento 2
                sprite.sprite = part2;
                break;
        }
    }
}
