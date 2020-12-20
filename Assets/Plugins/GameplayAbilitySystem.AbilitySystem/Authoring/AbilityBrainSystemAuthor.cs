using GameplayAbilitySystem.GameplayTags;
using Unity.Entities;
using UnityEngine;

namespace GameplayAbilitySystem.AbilitySystem
{
    public abstract class AbilityBrainSystemAuthor<T> : MonoBehaviour
    where T : AbilityBrainSystem
    {
        public GameplayTagScriptableObject Brain;
        public void Start()
        {
            var dstManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var system = dstManager.World.GetOrCreateSystem<T>();
            World.DefaultGameObjectInjectionWorld.GetExistingSystem<SimulationSystemGroup>().AddSystemToUpdateList(system);
            system.SetBrain(Brain.Tag);
            system.Enabled = true;
            Debug.Log(typeof(T));
        }

    }

    public abstract class AbilityBrainSystem : SystemBase
    {
        protected GameplayTag brainTag;
        public void SetBrain(GameplayTag brainTag)
        {
            this.brainTag = brainTag;
        }
    }


}

// Outer class 
public class Outer_class
{

    // Non-static data  
    // member of outer class 
    public int number = 1000000;

    // Inner class 
    public class Inner_class
    {

        // Static method of Inner class 
        public static void method1()
        {
            // Creating the object of the outer class 
            Outer_class obj = new Outer_class();

            // Displaying the value of a  
            // static member of the outer class 
            // with the help of obj 
            obj.number = 2;
        }
    }
}