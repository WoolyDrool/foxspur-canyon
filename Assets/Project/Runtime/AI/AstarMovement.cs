using System;
using System.Collections;
using System.Collections.Generic;
using Panda;
using Pathfinding;
using UnityEngine;

public class AstarMovement : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 targetPosition;
    private Seeker _seeker;
    private CharacterController _controller;

    public Path path;

    public float speed = 2;
    public float nextWaypointDistance = 3;
    private int _currentWaypoint = 0;

    public float repathRate = 0.5f;
    private float _lastRepath = float.NegativeInfinity;
    
    void Start()
    {
        _seeker = GetComponent<Seeker>();
        _controller = GetComponent<CharacterController>();
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            _currentWaypoint = 0;
        }
    }

    [Task]
    void MoveToPlayerPosition()
    {
        targetPosition = playerTransform.position;
        
        if (Time.time > _lastRepath + repathRate && _seeker.IsDone())
        {
            _lastRepath = Time.time;

            _seeker.StartPath(transform.position, targetPosition, OnPathComplete);
        }


        if (path == null)
        {
            return;
        }
        
        if(_currentWaypoint > path.vectorPath.Count) return;
        if (_currentWaypoint == path.vectorPath.Count)
        {
            _currentWaypoint++;
            return;
        }

        Vector3 dir = (path.vectorPath[_currentWaypoint] - transform.position).normalized;

        Vector3 velocity = dir * speed;

        _controller.SimpleMove(velocity);

        if ((transform.position - path.vectorPath[_currentWaypoint]).sqrMagnitude <
            nextWaypointDistance * nextWaypointDistance)
        {
            _currentWaypoint++;
            return;
        }
    }
    
    
    void Update()
    {
        /*targetPosition = playerTransform.position;
        
        if (Time.time > _lastRepath + repathRate && _seeker.IsDone())
        {
            _lastRepath = Time.time;

            _seeker.StartPath(transform.position, targetPosition, OnPathComplete);
        }


        if (path == null)
        {
            return;
        }
        
        if(_currentWaypoint > path.vectorPath.Count) return;
        if (_currentWaypoint == path.vectorPath.Count)
        {
            _currentWaypoint++;
            return;
        }

        Vector3 dir = (path.vectorPath[_currentWaypoint] - transform.position).normalized;

        Vector3 velocity = dir * speed;

        _controller.SimpleMove(velocity);

        if ((transform.position - path.vectorPath[_currentWaypoint]).sqrMagnitude <
            nextWaypointDistance * nextWaypointDistance)
        {
            _currentWaypoint++;
            return;
        }*/
    }
    
    public void OnDisable()
    {
        _seeker.pathCallback -= OnPathComplete;
    }
    
}
