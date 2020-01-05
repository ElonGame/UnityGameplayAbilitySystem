/*
 * Created on Sun Jan 05 2020
 *
 * The MIT License (MIT)
 * Copyright (c) 2020 Sahil Jain
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software
 * and associated documentation files (the "Software"), to deal in the Software without restriction,
 * including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
 * and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so,
 * subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial
 * portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
 * TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
 * THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */



namespace GameplayAbilitySystem.GameplayTags.Interfaces {
    public interface IGameplayTagBufferElement { }
    public interface IAbilityTagsBufferElement : IGameplayTagBufferElement { }
    public interface ICancelAbilitiesWithTagsBufferElement : IGameplayTagBufferElement { }
    public interface IBlockAbilitiesWithTagsBufferElement : IGameplayTagBufferElement { }
    public interface IActivationOwnedTagsBufferElement : IGameplayTagBufferElement { }
    public interface IActivationRequiredTagsBufferElement : IGameplayTagBufferElement { }
    public interface IActivationBlockedTagsBufferElement : IGameplayTagBufferElement { }
    public interface ISourceRequiredTagsBufferElement : IGameplayTagBufferElement { }
    public interface ISourceBlockedTagsBufferElement : IGameplayTagBufferElement { }
    public interface ITargetRequiredTagsBufferElement : IGameplayTagBufferElement { }
    public interface ITargetBlockedTagsBufferElement : IGameplayTagBufferElement { }

    public interface IGameplayEffectAssetTagsBufferElement : IGameplayTagBufferElement { }
    public interface IGrantedTagsBufferElement : IGameplayTagBufferElement { }
    public interface IOngoingTagsRequirementsBufferElement : IGameplayTagBufferElement { }
    public interface IApplicationTagRequirementsBufferElement : IGameplayTagBufferElement { }
    public interface IGrantedApplicationImmunityTagsBufferElement : IGameplayTagBufferElement { }
    public interface IRemoveGameplayEffectsWithTagsBufferElement : IGameplayTagBufferElement { }


}

