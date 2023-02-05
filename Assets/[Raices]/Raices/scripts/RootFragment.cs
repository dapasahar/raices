using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RootFragment : MonoBehaviour
{
    [SerializeField] int step = 0;

    [SerializeField] Transform primarySpawner;
    [SerializeField] Transform secondarySpawner;

    [SerializeField] Transform collisionChecker;

    [HideInInspector] public RootController controller;

    RootRenderer rootRenderer;

    Collider2D self;

    public RootFragment father;

    public int siguiente = 5;

    public bool Detenida
    {
        get
        {
            if (transform.position.y > controller.TopLimit) return true;
            Collider2D coll = Physics2D.OverlapCircle(collisionChecker.position, .08f);
            if (step == 0 && coll != null && coll != self)
            {
                return true;
            }
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
        rootRenderer = GetComponentInChildren<RootRenderer>();
        self = GetComponent<Collider2D>();
    }

    public void Next()
    {
        step++;
        switch (step)
        {
            case 1:
                // colocar fragmento 1
                rootRenderer.Render(RootRenderer.FRAG_1);
                // crear siguiente
                siguiente--;
                RootFragment f = controller.AddFragment(primarySpawner, this, siguiente == 0);
                f.siguiente= siguiente == 0 ? Random.Range(5,9) : siguiente;
                if (secondarySpawner != null) controller.AddFragment(secondarySpawner, this, siguiente == 0);
                break;
            case 2:
                // colocar fragmento 2
                rootRenderer.Render(RootRenderer.FRAG_2);
                break;
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(collisionChecker.position, .08f);
    }*/
}
