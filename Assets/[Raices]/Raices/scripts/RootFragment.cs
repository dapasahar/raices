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
            if (collisionChecker.position.y > controller.TopLimit) return true;
            if (collisionChecker.position.x > controller.RightLimit + .3f) return true;
            if (collisionChecker.position.x < controller.LefLimit - .3f) return true;
            Collider2D coll = Physics2D.OverlapCircle(collisionChecker.position, .08f);
            if (step == 0 && coll != null && coll != self)
            {
                if (coll.CompareTag("Agua"))
                {
                    LevelController.Instance.Victoria();
                    controller.run = false;
                }
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
                bool special = siguiente == 0 
                    || collisionChecker.position.x > controller.RightLimit - .4f 
                    || collisionChecker.position.x < controller.LefLimit + .4f;

                RootFragment f = controller.AddFragment(primarySpawner, this, special);
                f.siguiente= siguiente == 0 ? Random.Range(5,9) : siguiente;
                if (secondarySpawner != null) controller.AddFragment(secondarySpawner, this, special);
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
