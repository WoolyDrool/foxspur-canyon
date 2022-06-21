using System;
using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

public class FootstepSound : MonoBehaviour
{
    [Header("Clips")]
    [SerializeField] private AudioSource _footstepAudio;
    [SerializeField] private AudioSource _jumpAudio;
    [SerializeField] private AudioSource _breathingAudio;
    [SerializeField] private float stepInterval;
    [SerializeField] private float speed;
    private float sprintSpeed;
    private AudioClip _clipToPlay;
    private int _index;
    private bool _isPlaying;

    [SerializeField] private CharacterController _controller;
    [SerializeField] private PlayerController _player;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField]private PlayerProfileInterpreter _interpreter;

    [SerializeField] private bool _isWalking;
    [SerializeField] private bool _isRunning;
    private bool _isJumping;

    [SerializeField] private float _stepCycle;
    [SerializeField] private float _nextStep;
    [SerializeField] private float _runstepLengthen;
    [SerializeField] private float _dynamicSpeed;
    private float sprintTimer;
    public int sprintSoundStartTime;

    public AudioClip[] footstepSounds;
    public AudioClip[] jumpSounds;
    void Awake()
    {
    }

    private void Start()
    {
        if (!_interpreter)
            _interpreter = GameManager.instance.ppi;
            
//        jumpSounds = _interpreter.gender.jumpSounds;
        speed = _movement.walkSpeed;
        sprintSpeed = _movement.runSpeed;
        _stepCycle = 0;
        _nextStep = _stepCycle / 2f;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
            //PlayJumpSound();
        //}
        
        _isWalking = _player.status == Status.walking;
        _isRunning = _player.status == Status.sprinting;
        
        _dynamicSpeed = _isRunning ? sprintSpeed : speed;

        if (_controller.isGrounded)
        {
            ProgressStepCycle(_dynamicSpeed);
        }
        
        if (_isRunning)
        {
            sprintTimer += 1 * Time.deltaTime;
            if ((int) sprintTimer == sprintSoundStartTime)
            {
                if(!_breathingAudio.isPlaying)
                    _breathingAudio.Play();
            }
        }
        else
        {
            sprintTimer = 0;
            _breathingAudio.Stop();
        }
        
       
    }

    private void PlayJumpSound()
    {
        int n = Random.Range(1, jumpSounds.Length);
        _jumpAudio.clip = jumpSounds[n];
        _jumpAudio.PlayOneShot(_jumpAudio.clip);
    }

    private void ProgressStepCycle(float speed)
    {
        if (_isWalking || _isRunning)
        {
            _stepCycle += (speed * (_isRunning ? 1f : _runstepLengthen)) * Time.deltaTime;
        }

        if (!(_stepCycle > _nextStep))
        { 
            return;
        }

        _nextStep = _stepCycle + stepInterval;

        PlayFootStepsAudio();

    }

    private void PlayFootStepsAudio()
    {
        if (!_controller.isGrounded)
        {
            return;
        }
        
        int n = Random.Range(1, footstepSounds.Length);
        _footstepAudio.clip = footstepSounds[n];
        _footstepAudio.PlayOneShot(_footstepAudio.clip);
    }

}
