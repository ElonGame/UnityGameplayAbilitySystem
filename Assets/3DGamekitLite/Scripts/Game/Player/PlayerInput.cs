using UnityEngine;
using System;
using System.Collections;
using Gamekit3D;
using static PlayerInputs;
using Cinemachine;
using GameplayAbilitySystem.AbilitySystem;
using GameplayAbilitySystem.GameplayTags;
using GameplayAbilitySystem.AttributeSystem;
using Unity.Entities;

public class PlayerInput : MonoBehaviour, IPlayerActions
{
    public static PlayerInput Instance
    {
        get { return s_Instance; }
    }

    [SerializeField]
    private AbilityRegistryScriptableObject AbilityRegistry;

    private GrantedAbilitiesAuthoring GrantedAbilities;
    private PlayerAttributeAuthoringScript AttributeAuthor;

    protected static PlayerInput s_Instance;

    [HideInInspector]
    public bool playerControllerInputBlocked;

    protected Vector2 m_Movement;
    protected Vector2 m_Camera;
    protected bool m_Jump;
    protected bool m_Attack;
    protected bool m_Pause;
    protected bool m_ExternalInputBlocked;
    protected bool m_AimProjectile;

    [SerializeField]
    protected float mouseSensitivity = 1;
    public Vector2 MoveInput
    {
        get
        {
            if (playerControllerInputBlocked || m_ExternalInputBlocked)
                return Vector2.zero;
            return m_Movement;
        }
    }

    public Vector2 CameraInput
    {
        get
        {
            if (playerControllerInputBlocked || m_ExternalInputBlocked)
                return Vector2.zero;
            return m_Camera * mouseSensitivity;
        }
    }

    public bool JumpInput
    {
        get { return m_Jump && !playerControllerInputBlocked && !m_ExternalInputBlocked; }
    }

    public bool Attack
    {
        get { return m_Attack && !playerControllerInputBlocked && !m_ExternalInputBlocked; }
    }

    public bool Pause
    {
        get { return m_Pause; }
    }

    PlayerInputs inputs;

    public void OnEnable()
    {
        if (inputs == null)
        {
            inputs = new PlayerInputs();
            inputs.Player.SetCallbacks(this);
        }
        inputs.Player.Enable();
    }

    public void OnDisable()
    {
        inputs.Player.Disable();
    }

    public bool AimProjectile { get { return m_AimProjectile; } }

    WaitForSeconds m_AttackInputWait;
    Coroutine m_AttackWaitCoroutine;

    const float k_AttackInputDuration = 0.03f;

    private EntityManager dstManager;

    void Awake()
    {
        m_AttackInputWait = new WaitForSeconds(k_AttackInputDuration);
        GrantedAbilities = GetComponent<GrantedAbilitiesAuthoring>();
        AttributeAuthor = GetComponent<MyPlayerAttributeAuthoringScript>();
        CinemachineCore.GetInputAxis = GetAxisCustom;
        if (s_Instance == null)
            s_Instance = this;
        else if (s_Instance != this)
            throw new UnityException("There cannot be more than one PlayerInput script.  The instances are " + s_Instance.name + " and " + name + ".");

        dstManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    private float GetAxisCustom(string axisName)
    {
        switch (axisName)
        {
            case "CameraX":
                return CameraInput.x;
            case "CameraY":
                return CameraInput.y;
            default:
                return 0;
        }
    }

    void Update()
    {
        // m_Pause = Input.GetButtonDown("Pause");
    }
    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        var vector = context.ReadValue<Vector2>();
        m_Movement.Set(vector.x, vector.y);
    }

    public void OnLook(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        var vector = context.ReadValue<Vector2>() * mouseSensitivity;
        m_Camera.Set(vector.x, vector.y);
    }

    public void OnFire(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            if (m_AttackWaitCoroutine != null)
                StopCoroutine(m_AttackWaitCoroutine);

            m_AttackWaitCoroutine = StartCoroutine(AttackWait());
        }
    }

    IEnumerator AttackWait()
    {
        m_Attack = true;

        yield return m_AttackInputWait;

        m_Attack = false;
    }

    public bool HaveControl()
    {
        return !m_ExternalInputBlocked;
    }

    public void ReleaseControl()
    {
        m_ExternalInputBlocked = true;
    }

    public void GainControl()
    {
        m_ExternalInputBlocked = false;
    }


    public void OnAim(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        m_AimProjectile = context.ReadValueAsButton();
    }

    public void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        m_Jump = context.ReadValueAsButton();
    }

    public void OnAbility1(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        // Get ability spec matching granted ability [0]
        var abilitySpec = AbilityRegistry.Get(GrantedAbilities.GrantedAbilities[0].Tag);

        // Get the ability source and target
        var source = AttributeAuthor.ActorAttributeEntity;
        var target = AttributeAuthor.ActorAttributeEntity;

        // Get brain from ability spec and copy to this instantiation
        var specBrain = dstManager.GetSharedComponentData<AbilityBrainComponent.Component>(abilitySpec);

        // Create the ability spec
        var abilityEntity = dstManager.CreateEntity(
            typeof(AbilityAttributeTargetSingleComponent),
            typeof(AbilityAttributeSourceComponent),
            typeof(AbilitySpecEntityComponent),
            typeof(AbilityBrainComponent.Component)
        );

        dstManager.SetComponentData<AbilityAttributeTargetSingleComponent>(abilityEntity, target);
        dstManager.SetComponentData<AbilityAttributeSourceComponent>(abilityEntity, target);
        dstManager.SetComponentData<AbilitySpecEntityComponent>(abilityEntity, abilitySpec);
        dstManager.SetSharedComponentData<AbilityBrainComponent.Component>(abilityEntity, specBrain);

    }

    public void OnAbility2(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnAbility3(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnAbility4(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }
}
