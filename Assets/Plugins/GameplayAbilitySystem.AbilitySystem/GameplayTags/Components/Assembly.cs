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

using GameplayAbilitySystem.AbilitySystem.GameplayTags.Components;
using GameplayAbilitySystem.AbilitySystem.GameplayTags.Interfaces;
using Unity.Entities;

[assembly: RegisterGenericComponentType(typeof(GameplayTagsBufferElement<IAbilityTagsBufferElement>))]
[assembly: RegisterGenericComponentType(typeof(GameplayTagsBufferElement<ICancelAbilitiesWithTagsBufferElement>))]
[assembly: RegisterGenericComponentType(typeof(GameplayTagsBufferElement<IBlockAbilitiesWithTagsBufferElement>))]
[assembly: RegisterGenericComponentType(typeof(GameplayTagsBufferElement<IActivationOwnedTagsBufferElement>))]
[assembly: RegisterGenericComponentType(typeof(GameplayTagsBufferElement<IActivationRequiredTagsBufferElement>))]
[assembly: RegisterGenericComponentType(typeof(GameplayTagsBufferElement<IActivationBlockedTagsBufferElement>))]
[assembly: RegisterGenericComponentType(typeof(GameplayTagsBufferElement<ISourceRequiredTagsBufferElement>))]
[assembly: RegisterGenericComponentType(typeof(GameplayTagsBufferElement<ISourceBlockedTagsBufferElement>))]
[assembly: RegisterGenericComponentType(typeof(GameplayTagsBufferElement<ITargetRequiredTagsBufferElement>))]
[assembly: RegisterGenericComponentType(typeof(GameplayTagsBufferElement<ITargetBlockedTagsBufferElement>))]

[assembly: RegisterGenericComponentType(typeof(GameplayTagsBufferElement<IGameplayEffectAssetTagsBufferElement>))]
[assembly: RegisterGenericComponentType(typeof(GameplayTagsBufferElement<IGrantedTagsBufferElement>))]
[assembly: RegisterGenericComponentType(typeof(GameplayTagsBufferElement<IOngoingTagsRequirementsBufferElement>))]
[assembly: RegisterGenericComponentType(typeof(GameplayTagsBufferElement<IApplicationTagRequirementsBufferElement>))]
[assembly: RegisterGenericComponentType(typeof(GameplayTagsBufferElement<IGrantedApplicationImmunityTagsBufferElement>))]
[assembly: RegisterGenericComponentType(typeof(GameplayTagsBufferElement<IRemoveGameplayEffectsWithTagsBufferElement>))]


[assembly: RegisterGenericComponentType(typeof(GameplayTagsBufferElement<IActorOwnedGameplayTags>))]
