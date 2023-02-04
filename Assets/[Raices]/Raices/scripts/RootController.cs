using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{

    [SerializeField] float tickTime = .5f;

    [SerializeField] RootFragment prefab;

    List<RootFragment> fragments = new List<RootFragment>();
    List<RootFragment> tFragments = new List<RootFragment>();

    [SerializeField] int tickCounter = 0;

    IEnumerator Start()
    {

        AddFragment(transform);

        while (true)
        {
            yield return new WaitForSeconds(tickTime);
            //Debug.Log($"Tick {tickCounter}");
            for (int i = 0; i < fragments.Count; i++)
            {
                fragments[i].Next();
            }
            fragments.AddRange(tFragments);
            tFragments = new List<RootFragment>();
            tickCounter++;
        }
    }

    public RootFragment AddFragment(Transform spawner)
    {
        RootFragment f = Instantiate(GetPrefab(), spawner.position, spawner.rotation);
        f.controller = this;
        tFragments.Add(f);
        return f;
    }

    RootFragment GetPrefab()
    {
        return prefab;
    }
}
