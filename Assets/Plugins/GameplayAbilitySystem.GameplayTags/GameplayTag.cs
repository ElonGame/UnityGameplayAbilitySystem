using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Mathematics;

namespace GameplayAbilitySystem.GameplayTags
{
    [Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public struct GameplayTag : IEquatable<GameplayTag>
    {
        [FieldOffset(0)]
        private uint Id;

        [FieldOffset(0)]
        public byte L0;

        [FieldOffset(1)]
        public byte L1;

        [FieldOffset(2)]
        public byte L2;

        [FieldOffset(3)]
        public byte L3;


        /// <summary>
        /// Checks if the two <see cref="GameplayTag"/> are identical.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(GameplayTag other)
        {
            return other.Id == Id;
        }


        /// <summary>
        /// Checks if this <see cref="GameplayTag"/> is a parent of the other <see cref="GameplayTag"/>, or are identical
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsParentOfOrEqualTo(GameplayTag other)
        {
            var mask = 0xFFFFFFFF;

            // Get the MSB parent byte that is non-zero
            mask = math.select(0xFFFFFFFF, 0xFFFFFF00, L2 == 0);
            mask = math.select(0xFFFFFFFF, 0xFFFF0000, L3 == 0);
            mask = math.select(0xFFFFFFFF, 0xFF000000, L1 == 0);
            mask = math.select(0xFFFFFFFF, 0x00000000, L0 == 0);

            // Apply the mask the other tag, so that the parent parts can be comapred
            // If the parent MSB are identical, then this tag is a parent
            return (mask & other.Id) == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

    }

}