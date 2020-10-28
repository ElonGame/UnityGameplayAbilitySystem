using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace GameplayAbilitySystem.GameplayTags
{
    [CreateAssetMenu(fileName = "GameplayTag", menuName = "Gameplay Ability System/Gameplay Tags/Gameplay Tag", order = 1)]
    public partial class GameplayTagScriptableObject : ScriptableObject
    {
        public string GameplayTagString;

        [SerializeField]
        public GameplayTag Tag;
    }

    public class GameplayTagIdAssigner
    {
        [MenuItem("Assets/Generate Tag IDs")]
        private static void GenerateTagIDs()
        {
            if (!EditorUtility.DisplayDialog("Auto Generate Gameplay Tag ID?", "Auto generate IDs for all GameplayTag assets in this folder and all subfolders?", "OK", "Cancel"))
                return;

            // Assign name of SO to internal string for debugging
            GameplayTagScriptableObject[] selectedAsset = Selection.GetFiltered<GameplayTagScriptableObject>(SelectionMode.DeepAssets);
            foreach (var asset in selectedAsset)
            {
                if (asset.GameplayTagString == "")
                {
                    asset.GameplayTagString = asset.name;
                }
            }

            // Order by name
            selectedAsset = selectedAsset.OrderBy(x => x.GameplayTagString).ToArray();

            List<List<string>> Tags = new List<List<string>>() { new List<string>(), new List<string>(), new List<string>(), new List<string>() };
            foreach (GameplayTagScriptableObject obj in selectedAsset)
            {
                if (obj.GameplayTagString == "")
                {
                    obj.GameplayTagString = obj.name;
                }

                var ids = obj.GameplayTagString.Split('.');
                byte[] tagIds = new byte[4];

                var scriptableObject = (GameplayTagScriptableObject)obj;
                for (var i = 0; i < ids.Length; i++)
                {
                    if (i >= 4) break;
                    var tagString = ids[i];

                    // Check if this string already exists at this level
                    var existingIndex = Tags[i].IndexOf(tagString);
                    if (existingIndex >= 0)
                    {
                        tagIds[i] = (byte)(existingIndex + 1);
                    }
                    else
                    {
                        Tags[i].Add(tagString);
                        // We want the just added index +1
                        tagIds[i] = (byte)(Tags[i].Count);
                    }
                }
                scriptableObject.Tag.L0 = tagIds[0];
                scriptableObject.Tag.L1 = tagIds[1];
                scriptableObject.Tag.L2 = tagIds[2];
                scriptableObject.Tag.L3 = tagIds[3];
            }
        }
    }

}