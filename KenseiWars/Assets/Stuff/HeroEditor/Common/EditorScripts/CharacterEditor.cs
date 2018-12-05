using System;
using System.Linq;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.HeroEditor.Common.ExampleScripts;
using HeroEditor.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.HeroEditor.Common.EditorScripts
{
    /// <summary>
    /// Character editor UI and behaviour.
    /// </summary>
    public class CharacterEditor : CharacterEditorBase
    {
        [Header("Other")]
        public FirearmCollection FirearmCollection;
        public bool UseEditorColorField = true;
        public string PrefabFolder;
        public string TestRoomSceneName;
	    
        private static Character _temp;

        /// <summary>
        /// Called automatically on app start.
        /// </summary>
        public void Awake()
        {
            RestoreTempCharacter();
        }

		/// <summary>
        /// Remove all equipment.
        /// </summary>
        public void Reset()
        {
            ((Character) Character).ResetEquipment();
            InitializeDropdowns();
        }

        #if UNITY_EDITOR

        /// <summary>
        /// Save character to prefab.
        /// </summary>
        public void Save()
        {
            PrefabFolder = UnityEditor.EditorUtility.SaveFilePanel("Save character prefab", PrefabFolder, "New character", "prefab");

            if (PrefabFolder.Length > 0)
            {
                Save("Assets" + PrefabFolder.Replace(Application.dataPath, null));
            }
        }

        /// <summary>
        /// Load character from prefab.
        /// </summary>
        public void Load()
        {
	        FeatureTip();
		}

		public override void Save(string path)
        {
            Character.transform.localScale = Vector3.one;
            UnityEditor.PrefabUtility.CreatePrefab(path, Character.gameObject);
            Debug.LogFormat("Prefab saved as {0}", path);
        }

        public override void Load(string path)
        {
	        var character = UnityEditor.AssetDatabase.LoadAssetAtPath<Character>(path);

	        Character.GetComponent<Character>().Firearm.Params = character.Firearm.Params; // TODO: Workaround
			Load(character);
		}

	    #else

        public override void Save(string path)
        {
            throw new NotSupportedException();
        }

        public override void Load(string path)
        {
            throw new NotSupportedException();
        }

        #endif

        /// <summary>
        /// Test character with demo setup.
        /// </summary>
        public void Test()
        {
            FeatureTip();
		}

		protected override void SetFirearmParams(SpriteGroupEntry entry)
        {
            if (entry == null) return;

            if (FirearmCollection.Firearms.Count(i => i.Name == entry.Name) != 1) throw new Exception("Please check firearms params for: " + entry.Name);

			((Character) Character).Firearm.Params = FirearmCollection.Firearms.Single(i => i.Name == entry.Name);
		}

	    protected override void OpenPalette(GameObject palette, Color selected)
        {
            #if UNITY_EDITOR_WIN

            if (UseEditorColorField)
            {
                EditorColorField.Open(selected);
            }
            else

            #endif

            {
                Editor.SetActive(false);
                palette.SetActive(true);
            }
        }

        private void RestoreTempCharacter()
        {
            if (_temp == null) return;

	        Character.GetComponent<Character>().Firearm.Params = _temp.Firearm.Params; // TODO: Workaround
			Load(_temp);
	        Destroy(_temp.gameObject);
            _temp = null;
        }

	    protected override void FeedbackTip()
	    {
			#if UNITY_EDITOR

		    var success = UnityEditor.EditorUtility.DisplayDialog("Hero Editor", "Hi! Thank you for using my asset! I hope you enjoy making your game with it. The only thing I would ask you to do is to leave a review on the Asset Store. It would be awasome support for my asset, thanks!", "Review", "Later");
			
			RequestFeedbackResult(success, true);

			#endif
	    }

	    private void FeatureTip()
	    {
			#if UNITY_EDITOR

		    if (UnityEditor.EditorUtility.DisplayDialog("Hero Editor", "This feature is available only in PRO asset version!", "Navigate", "Cancel"))
		    {
			    Application.OpenURL(LinkToProVersion);
		    }

			#endif
		}
    }
}