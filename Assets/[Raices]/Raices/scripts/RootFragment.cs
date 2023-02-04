using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootFragment : MonoBehaviour
{
    [SerializeField] int step = 0;

    [SerializeField] Sprite part1;
    [SerializeField] Sprite part2;

    [SerializeField] Transform primarySpawner;
    [SerializeField] Transform secondarySpawner;

    [HideInInspector] public RootController controller;

    SpriteRenderer spriteRenderer;

    public bool toRemove = false;

    public RootFragment father;

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
                if (transform.position.y > controller.TopLimit)
                {
                    toRemove = true;
                    if (father != null) father.toRemove = true;
                    return;
                }
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
            case 3:
                toRemove = true;
                if (father != null) father.toRemove = true;
                break;
        }
    }

    private void LateUpdate()
    {
        if (toRemove)
        {
            controller.RemoveFragment(this);
        }
    }
}
