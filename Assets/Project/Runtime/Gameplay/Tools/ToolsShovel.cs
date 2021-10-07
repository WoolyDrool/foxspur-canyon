using UnityEngine;

namespace Project.Runtime.Gameplay.Tools
{
    public class ToolsShovel : EquippableObject
    {
        private const string DIG_TRIGGER = "Dig";
        public float rangeOfSwing = 5;
        public LayerMask digLayers;
        [SerializeField] private Transform playerCamera;
        private RaycastHit _rayHit;
        public AudioClip digSound;
        public AudioClip hitSound;
        private AudioSource _source;
        
        public override void Awake()
        {
            base.Awake();
        }
        
        void Start()
        {
            playerCamera = FindObjectOfType<CameraMovement>().transform;
            _source = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _source.PlayOneShot(digSound);
                objectAnimator.SetTrigger(DIG_TRIGGER);
            }
        }
        
        public void SwingShovel()
        {
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out _rayHit, rangeOfSwing, digLayers))
            {
                _rayHit.collider.SendMessage("ProcessDig", SendMessageOptions.DontRequireReceiver);
                _source.PlayOneShot(hitSound);
            }
            
            objectAnimator.SetTrigger(DIG_TRIGGER);
        }
    }
}
