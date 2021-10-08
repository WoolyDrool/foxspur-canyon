using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using Pathfinding;
using Random = UnityEngine.Random;

public class CreatureTasks : MonoBehaviour
{
    private AIPath _path;

    public Transform nextPos;
    private void Awake()
    {
        _path = GetComponent<AIPath>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    [Task]
    void SetColor(float r, float g, float b)
    {
        this.GetComponent<Renderer>().material.color = new Color(r, g, b);
        Task.current.Succeed();
    }

    [Task]
    void SetRandomPositionInRadius()
    {
        _path.destination = nextPos.position;
        
        if(_path.reachedDestination)
            Task.current.Succeed();
    }
}
