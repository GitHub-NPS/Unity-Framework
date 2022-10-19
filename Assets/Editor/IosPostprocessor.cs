#if UNITY_IOS
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.iOS.Xcode;
using UnityEngine;

public class IosPostprocessor : IPostprocessBuildWithReport
{
    public int callbackOrder => 100;
 
    public void OnPostprocessBuild(BuildReport report)
    {
        var pathToBuiltProject = report.summary.outputPath;
        var target = report.summary.platform;
        Debug.LogFormat("Postprocessing build at \"{0}\" for target {1}", pathToBuiltProject, target);
        if (target != BuildTarget.iOS)
            return;
 
        PBXProject project = new PBXProject();
        string pbxFilename = pathToBuiltProject + "/Unity-iPhone.xcodeproj/project.pbxproj";
        project.ReadFromFile(pbxFilename);
 
#if UNITY_2019_3_OR_NEWER
        string targetGUID = project.GetUnityMainTargetGuid();
#else
        string targetName = PBXProject.GetUnityTargetName();
        string targetGUID = project.TargetGuidByName(targetName);
#endif

        var token = project.GetBuildPropertyForAnyConfig(targetGUID, "USYM_UPLOAD_AUTH_TOKEN");
        if (string.IsNullOrEmpty(token))
        {
            token = "FakeToken";
        }

        string targetGUID2 = project.TargetGuidByName("UnityFramework");
        project.SetBuildProperty(targetGUID, "USYM_UPLOAD_AUTH_TOKEN", token);
        project.SetBuildProperty(targetGUID2, "USYM_UPLOAD_AUTH_TOKEN", token);
        project.SetBuildProperty(project.ProjectGuid(), "USYM_UPLOAD_AUTH_TOKEN", token);
        project.WriteToFile(pbxFilename);

        // except encryption
        string plistFilename = pathToBuiltProject + "/Info.plist";  
        PlistDocument plist = new PlistDocument();
        plist.ReadFromString(File.ReadAllText(plistFilename));

        PlistElementDict rootDict = plist.root;
        rootDict.SetBoolean("ITSAppUsesNonExemptEncryption", false);

        File.WriteAllText(plistFilename, plist.WriteToString());
    }
}
#endif