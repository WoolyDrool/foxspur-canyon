using System.Collections;
using System.Collections.Generic;
using Panda;
using Pathfinding;
using UnityEngine;

public class AICreatureFollow : MonoBehaviour
{
    
    public float closeEnoughDistance;
    public float followSpeed;
    public float runAwaySpeed;
    public float nextWaypointDistance = 3;
    public Path path;
    public float repathRate = 0.5f;
    
    private Camera _playerCamera;
    private Seeker _seeker;
    private CharacterController _controller;
    private Transform playerTransform;
    private int _currentWaypoint = 0;
    [Task] public bool shouldFollow;
    [Task] public bool isIdle;
    [Task] public bool shouldRun;
    private float _lastRepath = float.NegativeInfinity;
    public LayerMask mask;
    [SerializeField] private Vector3 targetPosition;
    
    void Start()
    {
        _seeker = GetComponent<Seeker>();
        _controller = GetComponent<CharacterController>();
        _playerCamera = GameManager.instance.playerManager._playerController.camera.GetComponent<Camera>();
        playerTransform = GameManager.instance.playerManager.playerTransform;
    }
    
    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            //_currentWaypoint = 0;
        }
    }

    [Task]
    void MoveToPlayerPosition()
    {
        targetPosition = -playerTransform.forward - playerTransform.position * closeEnoughDistance;
        
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

        Vector3 velocity = dir * followSpeed;

        _controller.SimpleMove(velocity);

        if ((transform.position - path.vectorPath[_currentWaypoint]).sqrMagnitude <
            nextWaypointDistance * nextWaypointDistance)
        {
            _currentWaypoint++;
            return;
        }
    }
    
    [Task]
    void RunAway()
    {
        Vector3 runAwayPosition = new Vector3(0, 0, 0);

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

        Vector3 velocity = dir * runAwaySpeed;

        _controller.SimpleMove(velocity);

        if ((transform.position - path.vectorPath[_currentWaypoint]).sqrMagnitude <
            nextWaypointDistance * nextWaypointDistance)
        {
            
            return;
        }
    }

    [Task]
    void DrawPlayerRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out hit, 15, mask))
        {
            if (hit.transform == this.transform)
            {
                shouldRun = true;
                Task.current.Succeed();
            }
        }
    }
    
    void Update()
    {
        targetPosition = playerTransform.position /*- (Vector3.back * closeEnoughDistance)*/;
        
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

        Vector3 velocity = dir * followSpeed;

        _controller.SimpleMove(velocity);

        if ((transform.position - path.vectorPath[_currentWaypoint]).sqrMagnitude <
            nextWaypointDistance * nextWaypointDistance)
        {
            _currentWaypoint++;
            return;
        }
    }
}
