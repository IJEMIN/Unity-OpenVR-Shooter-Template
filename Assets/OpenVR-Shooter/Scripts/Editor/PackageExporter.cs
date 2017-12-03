// 단순 유니티 패키지 제작용 스크립트


using UnityEngine;
using System.Collections;
using UnityEditor;
 
public static class ExportPackage {
 

    [MenuItem("Export/Export with tags and layers, Input settings")]
    public static void export()
    {
        string[] projectContent = new string[] {"Assets", "ProjectSettings/TagManager.asset","ProjectSettings/InputManager.asset","ProjectSettings/ProjectSettings.asset"};
        AssetDatabase.ExportPackage(projectContent, "OpenVR-Shooter.unitypackage",ExportPackageOptions.Interactive | ExportPackageOptions.Recurse |ExportPackageOptions.IncludeDependencies);
        Debug.Log("Project Exported");
    }
 
}