using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{

    [SerializeField] float tickTime = .5f;

    [SerializeField] RootFragment normalPrefab;
    [SerializeField] RootFragment turnLeftPrefab;
    [SerializeField] RootFragment turnRightPrefab;
    [SerializeField] RootFragment forkPrefab;

    List<RootFragment> fragments = new List<RootFragment>();
    List<RootFragment> tFragments = new List<RootFragment>();

    [SerializeField] int tickCounter = 0;

    [SerializeField] int turnCounter = 5;

    RootFragment[] specialFragments;

    IEnumerator Start()
    {
        specialFragments = new RootFragment[] { turnLeftPrefab, turnRightPrefab, forkPrefab };

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
        if (tickCounter > turnCounter)
        {
            tickCounter = 0;
            turnCounter = Random.Range(5, 8);
            return specialFragments[Random.Range(0, specialFragments.Length)];
        }
        return normalPrefab;
    }
}
