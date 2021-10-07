using UnityEngine;

namespace Project.Runtime.Gameplay.Tools
{
    public class ToolsHatchet : EquippableObject
    {
        private const string SWING_TRIGGER = "Swing";
        public float rangeOfSwing = 5;
        public int damagePerSwing = 50;
        public LayerMask damageLayers;
        [SerializeField] private Transform playerCamera;
        private RaycastHit _rayHit;
        public AudioClip swingSound;
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
                _source.PlayOneShot(swingSound);
                objectAnimator.SetTrigger(SWING_TRIGGER);
            }
        }
        
        public void SwingHatchet()
        {
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out _rayHit, rangeOfSwing, damageLayers))
            {
                _rayHit.collider.SendMessage("TakeDamage", damagePerSwing);
            }
            
            objectAnimator.SetTrigger(SWING_TRIGGER);
        }
    }
}