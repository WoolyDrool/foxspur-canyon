// GENERATED AUTOMATICALLY FROM 'Assets/Project/Runtime/Control/Input Sets/Player.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Player : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Player()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player"",
    ""maps"": [
        {
            ""name"": ""Main"",
            ""id"": ""c6d93658-7158-48e1-ade6-ab216d68dfdf"",
            ""actions"": [
                {
                    ""name"": ""WasdMovement"",
                    ""type"": ""Value"",
                    ""id"": ""eab777f9-feb0-4438-a4b0-b5e9d8667fd6"",
                    ""expectedControlType"": ""Key"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ArrowMovement"",
                    ""type"": ""Button"",
                    ""id"": ""389d280f-cd27-4dc6-b042-4f9be61160d7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""cd7ad4ce-37a7-4ca3-9b54-017aab0e2d7a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""2b41e52e-2ca4-4517-a483-7e6c5809e018"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""644d2043-b4b9-4c3b-8f21-86835569ed0e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""5ef1a3d2-57cc-4d36-a7b1-0ffb3e9e693d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Flashlight"",
                    ""type"": ""Button"",
                    ""id"": ""06b22ba0-f761-435b-b3ec-a4624eb8f5e3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Scroll Wheel"",
                    ""type"": ""Button"",
                    ""id"": ""0e758083-d0ea-426c-b663-e6b3f99dea7f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Primary Fire"",
                    ""type"": ""Button"",
                    ""id"": ""faea1f89-f229-4a5c-9f3a-dc6e4eceaed6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Alt Fire"",
                    ""type"": ""Button"",
                    ""id"": ""7433294e-03db-4991-95c5-9dc32ff9530e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""2bd3710b-fb80-4089-bc28-208b293677b4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""vector"",
                    ""id"": ""63e86692-f195-4a4b-a245-92a52f8a802f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WasdMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""05d92673-05b4-43df-872a-e5f04216630b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WasdMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5e980e99-7b1b-415b-a11b-94e2c5a14f9e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WasdMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1c2765f5-2ff6-405d-841e-94aab4a02e07"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WasdMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1b815b47-2a0d-4c9b-b450-c312c86a751e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WasdMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""vector"",
                    ""id"": ""b8ec1d8d-09db-4090-b106-723e794907b7"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ArrowMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0ccac3a2-2c73-469e-adac-cbf3da2120cd"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ArrowMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d710400c-671d-4868-b705-6702a7d9bb43"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ArrowMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""41ae7014-0509-4353-b531-0db042a6dd1e"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ArrowMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""92a97a6b-1fe5-43c7-b4dc-26532f2dc533"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ArrowMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""540935ef-1702-4456-a72d-984a845c86f8"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""92c1928f-a4ed-4be2-b225-19ae2b814016"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7241fd22-946b-4688-8736-de4841445670"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e801b33-cf80-465f-a2bd-3efaa6e8dceb"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e1164e0-d7f2-4d60-9f71-0fb12c47cc36"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Flashlight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""vector"",
                    ""id"": ""dc6bd589-7a1c-483f-b17a-914f1499eab9"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Scroll Wheel"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f08df110-75ce-4c1d-9b38-5fa347c3efcc"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Scroll Wheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""c8c414cb-1323-4ec0-b0db-696e0ff2d8d0"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Scroll Wheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4d08961c-c901-4d02-b319-6c8db4128da1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Primary Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7f05ab78-18a3-4ac1-af9c-38b3693fe1fd"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Alt Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a734145b-367f-4b7c-9a3e-d2f34635102d"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Main
        m_Main = asset.FindActionMap("Main", throwIfNotFound: true);
        m_Main_WasdMovement = m_Main.FindAction("WasdMovement", throwIfNotFound: true);
        m_Main_ArrowMovement = m_Main.FindAction("ArrowMovement", throwIfNotFound: true);
        m_Main_Jump = m_Main.FindAction("Jump", throwIfNotFound: true);
        m_Main_Crouch = m_Main.FindAction("Crouch", throwIfNotFound: true);
        m_Main_Sprint = m_Main.FindAction("Sprint", throwIfNotFound: true);
        m_Main_Interact = m_Main.FindAction("Interact", throwIfNotFound: true);
        m_Main_Flashlight = m_Main.FindAction("Flashlight", throwIfNotFound: true);
        m_Main_ScrollWheel = m_Main.FindAction("Scroll Wheel", throwIfNotFound: true);
        m_Main_PrimaryFire = m_Main.FindAction("Primary Fire", throwIfNotFound: true);
        m_Main_AltFire = m_Main.FindAction("Alt Fire", throwIfNotFound: true);
        m_Main_Reload = m_Main.FindAction("Reload", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Main
    private readonly InputActionMap m_Main;
    private IMainActions m_MainActionsCallbackInterface;
    private readonly InputAction m_Main_WasdMovement;
    private readonly InputAction m_Main_ArrowMovement;
    private readonly InputAction m_Main_Jump;
    private readonly InputAction m_Main_Crouch;
    private readonly InputAction m_Main_Sprint;
    private readonly InputAction m_Main_Interact;
    private readonly InputAction m_Main_Flashlight;
    private readonly InputAction m_Main_ScrollWheel;
    private readonly InputAction m_Main_PrimaryFire;
    private readonly InputAction m_Main_AltFire;
    private readonly InputAction m_Main_Reload;
    public struct MainActions
    {
        private @Player m_Wrapper;
        public MainActions(@Player wrapper) { m_Wrapper = wrapper; }
        public InputAction @WasdMovement => m_Wrapper.m_Main_WasdMovement;
        public InputAction @ArrowMovement => m_Wrapper.m_Main_ArrowMovement;
        public InputAction @Jump => m_Wrapper.m_Main_Jump;
        public InputAction @Crouch => m_Wrapper.m_Main_Crouch;
        public InputAction @Sprint => m_Wrapper.m_Main_Sprint;
        public InputAction @Interact => m_Wrapper.m_Main_Interact;
        public InputAction @Flashlight => m_Wrapper.m_Main_Flashlight;
        public InputAction @ScrollWheel => m_Wrapper.m_Main_ScrollWheel;
        public InputAction @PrimaryFire => m_Wrapper.m_Main_PrimaryFire;
        public InputAction @AltFire => m_Wrapper.m_Main_AltFire;
        public InputAction @Reload => m_Wrapper.m_Main_Reload;
        public InputActionMap Get() { return m_Wrapper.m_Main; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainActions set) { return set.Get(); }
        public void SetCallbacks(IMainActions instance)
        {
            if (m_Wrapper.m_MainActionsCallbackInterface != null)
            {
                @WasdMovement.started -= m_Wrapper.m_MainActionsCallbackInterface.OnWasdMovement;
                @WasdMovement.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnWasdMovement;
                @WasdMovement.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnWasdMovement;
                @ArrowMovement.started -= m_Wrapper.m_MainActionsCallbackInterface.OnArrowMovement;
                @ArrowMovement.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnArrowMovement;
                @ArrowMovement.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnArrowMovement;
                @Jump.started -= m_Wrapper.m_MainActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnJump;
                @Crouch.started -= m_Wrapper.m_MainActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnCrouch;
                @Sprint.started -= m_Wrapper.m_MainActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnSprint;
                @Interact.started -= m_Wrapper.m_MainActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnInteract;
                @Flashlight.started -= m_Wrapper.m_MainActionsCallbackInterface.OnFlashlight;
                @Flashlight.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnFlashlight;
                @Flashlight.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnFlashlight;
                @ScrollWheel.started -= m_Wrapper.m_MainActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnScrollWheel;
                @PrimaryFire.started -= m_Wrapper.m_MainActionsCallbackInterface.OnPrimaryFire;
                @PrimaryFire.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnPrimaryFire;
                @PrimaryFire.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnPrimaryFire;
                @AltFire.started -= m_Wrapper.m_MainActionsCallbackInterface.OnAltFire;
                @AltFire.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnAltFire;
                @AltFire.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnAltFire;
                @Reload.started -= m_Wrapper.m_MainActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnReload;
            }
            m_Wrapper.m_MainActionsCallbackInterface = instance;
            if (instance != null)
            {
                @WasdMovement.started += instance.OnWasdMovement;
                @WasdMovement.performed += instance.OnWasdMovement;
                @WasdMovement.canceled += instance.OnWasdMovement;
                @ArrowMovement.started += instance.OnArrowMovement;
                @ArrowMovement.performed += instance.OnArrowMovement;
                @ArrowMovement.canceled += instance.OnArrowMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Flashlight.started += instance.OnFlashlight;
                @Flashlight.performed += instance.OnFlashlight;
                @Flashlight.canceled += instance.OnFlashlight;
                @ScrollWheel.started += instance.OnScrollWheel;
                @ScrollWheel.performed += instance.OnScrollWheel;
                @ScrollWheel.canceled += instance.OnScrollWheel;
                @PrimaryFire.started += instance.OnPrimaryFire;
                @PrimaryFire.performed += instance.OnPrimaryFire;
                @PrimaryFire.canceled += instance.OnPrimaryFire;
                @AltFire.started += instance.OnAltFire;
                @AltFire.performed += instance.OnAltFire;
                @AltFire.canceled += instance.OnAltFire;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
            }
        }
    }
    public MainActions @Main => new MainActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IMainActions
    {
        void OnWasdMovement(InputAction.CallbackContext context);
        void OnArrowMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnFlashlight(InputAction.CallbackContext context);
        void OnScrollWheel(InputAction.CallbackContext context);
        void OnPrimaryFire(InputAction.CallbackContext context);
        void OnAltFire(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
    }
}
