// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerActionControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActionControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Land"",
            ""id"": ""50439f32-413f-49dc-b42f-6fb6694e10f5"",
            ""actions"": [
                {
                    ""name"": ""Move Horizontal"",
                    ""type"": ""Value"",
                    ""id"": ""8900bd8e-6aba-4c71-a435-4d6cf2bdb87d"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": ""Clamp(min=-1,max=1)"",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""20978280-0f33-44aa-b282-3580d39da63d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Activate ability"",
                    ""type"": ""Button"",
                    ""id"": ""930d1540-7799-4909-8163-86d6fe08a0ff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""316023ab-6fc5-44d7-b9e7-b8c63f46fc8c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Activate Something"",
                    ""type"": ""Button"",
                    ""id"": ""b814d7ac-db8b-4e79-ab7f-68a851983800"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""21506747-d949-4725-8ec1-77097917f216"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""2f394229-6580-48df-9a28-e350e95532b7"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move Horizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0e39b55a-8ff2-48bf-ad0a-cc4a1561b139"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""fd73d35f-7af6-4c62-bc0b-7cd9a2f65bd5"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""37d312f0-5b9c-413e-85fc-46dfda25a17d"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""26299446-13e6-4e7f-85cc-43df41c852d2"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Activate ability"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e1627ac-b876-4e2f-8fa2-0c7bf6639ee4"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a23269e4-f350-4c29-ba5d-9f8cc96eb396"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Activate Something"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Land
        m_Land = asset.FindActionMap("Land", throwIfNotFound: true);
        m_Land_MoveHorizontal = m_Land.FindAction("Move Horizontal", throwIfNotFound: true);
        m_Land_Attack = m_Land.FindAction("Attack", throwIfNotFound: true);
        m_Land_Activateability = m_Land.FindAction("Activate ability", throwIfNotFound: true);
        m_Land_Jump = m_Land.FindAction("Jump", throwIfNotFound: true);
        m_Land_ActivateSomething = m_Land.FindAction("Activate Something", throwIfNotFound: true);
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

    // Land
    private readonly InputActionMap m_Land;
    private ILandActions m_LandActionsCallbackInterface;
    private readonly InputAction m_Land_MoveHorizontal;
    private readonly InputAction m_Land_Attack;
    private readonly InputAction m_Land_Activateability;
    private readonly InputAction m_Land_Jump;
    private readonly InputAction m_Land_ActivateSomething;
    public struct LandActions
    {
        private @PlayerActionControls m_Wrapper;
        public LandActions(@PlayerActionControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveHorizontal => m_Wrapper.m_Land_MoveHorizontal;
        public InputAction @Attack => m_Wrapper.m_Land_Attack;
        public InputAction @Activateability => m_Wrapper.m_Land_Activateability;
        public InputAction @Jump => m_Wrapper.m_Land_Jump;
        public InputAction @ActivateSomething => m_Wrapper.m_Land_ActivateSomething;
        public InputActionMap Get() { return m_Wrapper.m_Land; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LandActions set) { return set.Get(); }
        public void SetCallbacks(ILandActions instance)
        {
            if (m_Wrapper.m_LandActionsCallbackInterface != null)
            {
                @MoveHorizontal.started -= m_Wrapper.m_LandActionsCallbackInterface.OnMoveHorizontal;
                @MoveHorizontal.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnMoveHorizontal;
                @MoveHorizontal.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnMoveHorizontal;
                @Attack.started -= m_Wrapper.m_LandActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnAttack;
                @Activateability.started -= m_Wrapper.m_LandActionsCallbackInterface.OnActivateability;
                @Activateability.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnActivateability;
                @Activateability.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnActivateability;
                @Jump.started -= m_Wrapper.m_LandActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnJump;
                @ActivateSomething.started -= m_Wrapper.m_LandActionsCallbackInterface.OnActivateSomething;
                @ActivateSomething.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnActivateSomething;
                @ActivateSomething.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnActivateSomething;
            }
            m_Wrapper.m_LandActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveHorizontal.started += instance.OnMoveHorizontal;
                @MoveHorizontal.performed += instance.OnMoveHorizontal;
                @MoveHorizontal.canceled += instance.OnMoveHorizontal;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Activateability.started += instance.OnActivateability;
                @Activateability.performed += instance.OnActivateability;
                @Activateability.canceled += instance.OnActivateability;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @ActivateSomething.started += instance.OnActivateSomething;
                @ActivateSomething.performed += instance.OnActivateSomething;
                @ActivateSomething.canceled += instance.OnActivateSomething;
            }
        }
    }
    public LandActions @Land => new LandActions(this);
    public interface ILandActions
    {
        void OnMoveHorizontal(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnActivateability(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnActivateSomething(InputAction.CallbackContext context);
    }
}
