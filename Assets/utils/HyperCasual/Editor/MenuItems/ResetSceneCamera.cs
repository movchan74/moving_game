using UnityEditor;
using UnityEngine;

namespace HyperCasual.Editor.MenuItems
{
    /// <summary>
    /// Provides menu items for resetting the scene's current view.
    /// </summary>
    public static class ResetSceneCamera
    {
        [MenuItem(MenuName.SceneMenu + "StartGameplayPhase Camera/XZ" + MenuKey.CtrlShift.Y)]
        public static void XZ()
        {
            var current = SceneView.lastActiveSceneView;
            current.orthographic = true;
            current.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
            current.size = 200.0f;
            current.camera.farClipPlane = 2000.0f;
            current.camera.nearClipPlane = 0.01f;
            current.FixNegativeSize();
        }
        
        [MenuItem(MenuName.SceneMenu + "StartGameplayPhase Camera/XY" + MenuKey.CtrlShift.L)]
        public static void XY()
        {
            var current = SceneView.lastActiveSceneView;
            current.orthographic = true;
            current.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            current.size = 200.0f;
            current.camera.farClipPlane = 2000.0f;
            current.camera.nearClipPlane = 0.01f;
            current.FixNegativeSize();
        }

        [MenuItem(MenuName.SceneMenu + "StartGameplayPhase Camera/Isometric" + MenuKey.CtrlShift.V)]
        public static void Isometric()
        {
            var view = SceneView.lastActiveSceneView;
            view.orthographic = true;
            view.rotation = Quaternion.Euler(30.0f, 45.0f, 0.0f);
            view.size = 200.0f;
            view.FixNegativeSize();
        }
    }
}