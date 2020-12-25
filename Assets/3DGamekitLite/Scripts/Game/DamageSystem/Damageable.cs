﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Gamekit3D.Message;
using UnityEngine.Serialization;
using Unity.Entities;
using MyGameplayAbilitySystem;
using Unity.Mathematics;
using GameplayAbilitySystem.AbilitySystem;
using GameplayAbilitySystem.AttributeSystem;

namespace Gamekit3D
{
    public partial class Damageable : MonoBehaviour, IConvertGameObjectToEntity
    {

        public int maxHitPoints;
        [Tooltip("Time that this gameObject is invulnerable for, after receiving damage.")]
        public float invulnerabiltyTime;


        [Tooltip("The angle from the which that damageable is hitable. Always in the world XZ plane, with the forward being rotate by hitForwardRoation")]
        [Range(0.0f, 360.0f)]
        public float hitAngle = 360.0f;
        [Tooltip("Allow to rotate the world forward vector of the damageable used to define the hitAngle zone")]
        [Range(0.0f, 360.0f)]
        [FormerlySerializedAs("hitForwardRoation")] //SHAME!
        public float hitForwardRotation = 360.0f;

        public bool isInvulnerable { get; set; }
        private int m_currentHitPoints { get; set; }
        public int currentHitPoints { get { return m_currentHitPoints; } }


        public UnityEvent OnDeath, OnReceiveDamage, OnHitWhileInvulnerable, OnBecomeVulnerable, OnResetDamage;

        [Tooltip("When this gameObject is damaged, these other gameObjects are notified.")]
        [EnforceType(typeof(Message.IMessageReceiver))]
        public List<MonoBehaviour> onDamageMessageReceivers;

        protected float m_timeSinceLastHit = 0.0f;
        protected Collider m_Collider;

        private Entity attributeEntity;
        private EntityManager dstManager;
        private List<DamageMessage> damageMessagesToAction = new List<DamageMessage>();
        private bool wasDamaged = false;

        System.Action schedule;
        private Entity abilitySystemEntity;

        void Start()
        {
            MyPlayerAttributeAuthoringScript attributeAuthoringScript = GetComponent<MyPlayerAttributeAuthoringScript>();
            // if (attributeAuthoringScript != null)
            // {
            attributeEntity = GetComponent<MyPlayerAttributeAuthoringScript>().ActorAttributeEntity;
            this.dstManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            // }

            ResetDamage();
            m_Collider = GetComponent<Collider>();
        }

        void Update()
        {
            m_currentHitPoints = (int)(dstManager.GetComponentData<MyAttributeValues>(this.attributeEntity).CurrentValue[EMyPlayerAttribute.Health]);
            if (isInvulnerable)
            {
                m_timeSinceLastHit += Time.deltaTime;
                if (m_timeSinceLastHit > invulnerabiltyTime)
                {
                    m_timeSinceLastHit = 0.0f;
                    isInvulnerable = false;
                    OnBecomeVulnerable.Invoke();
                }
            }

            // This logic is to delay the processing of hp checks by a frame, since the
            // attribute update logic runs after one frame
            if (wasDamaged)
            {
                wasDamaged = false;
                for (var j = 0; j < damageMessagesToAction.Count; j++)
                {
                    var data = damageMessagesToAction[j];
                    if (currentHitPoints <= 0)
                        schedule += OnDeath.Invoke; //This avoid race condition when objects kill each other.
                    else
                        OnReceiveDamage.Invoke();

                    var messageType = currentHitPoints <= 0 ? MessageType.DEAD : MessageType.DAMAGED;

                    for (var i = 0; i < onDamageMessageReceivers.Count; ++i)
                    {
                        var receiver = onDamageMessageReceivers[i] as IMessageReceiver;
                        receiver.OnReceiveMessage(messageType, this, data);
                    }
                }
                damageMessagesToAction.Clear();
            }

            if (damageMessagesToAction.Count > 0)
            {
                wasDamaged = true;
            }

        }

        public void UpdateAttributeEntity(EntityManager dstManager, Entity abilitySystemActor, Entity attributeEntity)
        {
            dstManager.SetComponentData(abilitySystemActor, new PlayerAttributeAuthoringScript.Component() { Value = attributeEntity });
        }

        public void ResetDamage()
        {

            MyPlayerAttributeAuthoringScript attributeAuthoringScript = GetComponent<MyPlayerAttributeAuthoringScript>();

            var newAttributeEntity = attributeAuthoringScript.InitialiseAttributeEntity(dstManager);
            dstManager.DestroyEntity(this.attributeEntity);
            this.attributeEntity = newAttributeEntity;
            UpdateAttributeEntity(dstManager, abilitySystemEntity, newAttributeEntity);
            isInvulnerable = false;
            m_timeSinceLastHit = 0.0f;
            OnResetDamage.Invoke();
        }

        public void SetColliderState(bool enabled)
        {
            m_Collider.enabled = enabled;

        }

        public void ApplyDamage(DamageMessage data)
        {
            if (currentHitPoints <= 0)
            {//ignore damage if already dead. TODO : may have to change that if we want to detect hit on death...
                return;
            }

            if (isInvulnerable)
            {
                OnHitWhileInvulnerable.Invoke();
                return;
            }

            Vector3 forward = transform.forward;
            forward = Quaternion.AngleAxis(hitForwardRotation, transform.up) * forward;

            //we project the direction to damager to the plane formed by the direction of damage
            Vector3 positionToDamager = data.damageSource - transform.position;
            positionToDamager -= transform.up * Vector3.Dot(transform.up, positionToDamager);

            if (Vector3.Angle(forward, positionToDamager) > hitAngle * 0.5f)
                return;

            isInvulnerable = true;
            MyInstantAttributeUpdateSystem.CreateAttributeModifier(dstManager, new MyInstantGameplayAttributeModifier()
            {
                Attribute = EMyPlayerAttribute.Health,
                Operator = EMyAttributeModifierOperator.Add,
                Value = (half)(-data.amount)
            }, new GameplayAbilitySystem.AttributeSystem.GameplayEffectContextComponent()
            {
                Source = Entity.Null,
                Target = Entity.Null
            });

            // Create a poison effect, that does 1 damage every 1s tick
            var poisonEntity = dstManager.CreateEntity(typeof(DurationStateComponent), typeof(TimeDurationComponent), typeof(GameplayEffectContextComponent));
            // dstManager.SetComponentData(poisonEntity, new DotGameplayEffect()
            // {
            //     DamagePerTick = 1f
            // });

            dstManager.SetComponentData(poisonEntity, new GameplayEffectContextComponent()
            {
                Target = this.attributeEntity,
                Source = Entity.Null
            });

            dstManager.SetComponentData(poisonEntity, TimeDurationComponent.New(1f, 10f));

            damageMessagesToAction.Add(data);
        }

        void LateUpdate()
        {
            if (schedule != null)
            {
                schedule();
                schedule = null;
            }

        }


#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Vector3 forward = transform.forward;
            forward = Quaternion.AngleAxis(hitForwardRotation, transform.up) * forward;

            if (Event.current.type == EventType.Repaint)
            {
                UnityEditor.Handles.color = Color.blue;
                UnityEditor.Handles.ArrowHandleCap(0, transform.position, Quaternion.LookRotation(forward), 1.0f,
                    EventType.Repaint);
            }


            UnityEditor.Handles.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
            forward = Quaternion.AngleAxis(-hitAngle * 0.5f, transform.up) * forward;
            UnityEditor.Handles.DrawSolidArc(transform.position, transform.up, forward, hitAngle, 1.0f);
        }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            abilitySystemEntity = entity;
        }
    }
#endif
}
