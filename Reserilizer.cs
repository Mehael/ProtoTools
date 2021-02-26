using UnityEditor;
 
namespace EditorExtensions.MenuItems {
    public static class ReserializeAssets {
        [MenuItem("Tools/Force Re-Serialize Assets")]
        public static void ForceReSerializeAssets() {
            AssetDatabase.ForceReserializeAssets();
        }
    }
}