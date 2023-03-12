/// <summary>
// This class made for reducing call of FindGameObjectsWithTag function in every AI objects
// FindGameObjectsWithTag is too much cost in update loop.
/// </summary>

using UnityEngine;
using System.Collections.Generic;

public class AIManager : MonoBehaviour
{
    public static AIManager TargetFinder;
    public Dictionary<string, TargetCollector> TargetList = new Dictionary<string, TargetCollector>();
    public int TargetTypeCount = 0;
    public float UpdateInterval = 0.1f;
    private float timeTmp;
    public string PlayerTag = "Player";

    private void Awake()
    {
        TargetFinder = this;
    }

    void Start()
    {
        TargetList = new Dictionary<string, TargetCollector>();
    }

    public void Clear()
    {
        foreach (var target in TargetList)
        {
            if (target.Value != null)
                target.Value.Clear();
        }
        TargetList.Clear();
        TargetList = new Dictionary<string, TargetCollector>(0);
    }

    public TargetCollector FindTargetTag(string tag)
    {

        if (TargetList.ContainsKey(tag))
        {
            TargetCollector targetcollector;
            if (TargetList.TryGetValue(tag, out targetcollector))
            {
                targetcollector.IsActive = true;
                return targetcollector;
            }
            else
            {
                return null;
            }
        }
        else
        {
            TargetList.Add(tag, new TargetCollector(tag));
        }
        return null;
    }

    void Update()
    {
        if (Time.time > timeTmp + UpdateInterval)
        {
            int count = 0;

            foreach (var target in TargetList)
            {
                if (target.Value != null)
                {
                    if (target.Value.IsActive)
                    {
                        target.Value.SetTarget(target.Key);
                        target.Value.IsActive = false;
                        count += 1;
                    }
                }
            }
            TargetTypeCount = count;
            timeTmp = Time.time;
        }
    }

    public bool IsPlayerAround(Vector3 position, float distance)
    {
        TargetCollector player = FindTargetTag(PlayerTag);
        if (player != null && player.Targets.Length > 0)
        {
            for (int i = 0; i < player.Targets.Length; i++)
            {
                if (player.Targets[i] != null)
                {
                    if (Vector3.Distance(position, player.Targets[i].transform.position) <= distance)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

}

public class TargetCollector
{
    public GameObject[] Targets;
    public bool IsActive;

    public TargetCollector(string tag)
    {
        SetTarget(tag);
    }
    public void Clear()
    {
        Targets = null;
    }
    public void SetTarget(string tag)
    {
        Targets = (GameObject[])GameObject.FindGameObjectsWithTag(tag);
    }

}

