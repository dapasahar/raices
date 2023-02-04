using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RootFragment : MonoBehaviour
{
    [SerializeField] int step = 0;

    [SerializeField] Sprite part1;
    [SerializeField] Sprite part2;

    [SerializeField] Transform primarySpawner;
    [SerializeField] Transform secondarySpawner;

    [HideInInspector] public RootController controller;

    SpriteRenderer spriteRenderer;

    public RootFragment father;

    public bool Detenida
    {
        get
        {
            if (transform.position.y > controller.TopLimit) return true;
            return false;
        }
    }

    public bool Completo
    {
        get
        {
            return step >= 3;  
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Next()
    {
        step++;
        switch (step)
        {
            case 1:
                // colocar fragmento 1
                spriteRenderer.sprite = part1;
                // crear siguiente
                controller.AddFragment(primarySpawner, this);
                if (secondarySpawner != null) controller.AddFragment(secondarySpawner, this);
                break;
            case 2:
                // colocar fragmento 2
                spriteRenderer.sprite = part2;
                break;
        }
    }
}
