using UnityEngine;
using System.IO;
using System.Collections.Generic;
using SimpleJSON;
using UnityEditor;


public class AudioInkFlagSetter : MonoBehaviour
{
    [System.Serializable]
    public class AudioFlag
    {
        public AudioClip clip;
        public string inkVariableName;
    }

    public List<AudioSource> audioSources;
    public List<AudioFlag> audioFlags;

    public TextAsset inkJsonFile; // 🎯 DRAG THE JSON FILE HERE!
    private string fullFilePath = "";

    private string lastPlayed = "";

void Start()
{
    if (inkJsonFile == null)
    {
        Debug.LogError("❌ You forgot to drag your Ink JSON file into the inspector!");
        return;
    }

    // Get the full file path
    fullFilePath = Application.dataPath + "/" + GetRelativePath(inkJsonFile);

#if UNITY_EDITOR
    ResetAllFlagsOnPlay(); // ✅ Reset the variable at Play start (Editor only)
#endif
}


    void Update()
{
    if (audioSources == null || audioSources.Count == 0)
        return;

    foreach (var source in audioSources)
    {
        if (source == null || source.clip == null || !source.isPlaying)
            continue;

        string currentName = source.clip.name;

        if (currentName != lastPlayed)
        {
            lastPlayed = currentName;

            foreach (var flag in audioFlags)
            {
                if (flag.clip.name == currentName)
                {
                    Debug.Log("🎧 Matched sound: " + currentName + " → Setting: " + flag.inkVariableName);
                    SetInkFlag(flag.inkVariableName);
                    break;
                }
            }
        }
    }
}


    void SetInkFlag(string variableName)
{
    if (string.IsNullOrEmpty(fullFilePath) || !File.Exists(fullFilePath))
    {
        Debug.LogError("❌ Cannot find file at: " + fullFilePath);
        return;
    }

    string jsonText = File.ReadAllText(fullFilePath);
    var root = JSON.Parse(jsonText);
    var rootArray = root["root"].AsArray;

    // Find the global decl section
    for (int i = 0; i < rootArray.Count; i++)
    {
        var item = rootArray[i];
        if (item != null && item["global decl"] != null)
        {
            var globalDecl = item["global decl"].AsArray;

            // Go through every value and check for VAR= match
            for (int j = 0; j < globalDecl.Count; j++)
            {
                var entry = globalDecl[j];
                if (entry != null && entry.IsObject && entry["VAR="] != null)
                {
                    string foundVar = entry["VAR="];
                    if (foundVar == variableName)
                    {
                        // The boolean value is right before this entry
                        int boolIndex = j - 1;
                        if (boolIndex >= 0 && globalDecl[boolIndex].IsBoolean)
                        {
                            globalDecl[boolIndex].AsBool = true;
                            File.WriteAllText(fullFilePath, root.ToString(2));
                            Debug.Log($"✅ Successfully changed {variableName} to true in Ink JSON!");
                            return;
                        }
                    }
                }
            }

            Debug.LogWarning($"⚠️ Could not find variable '{variableName}' in global decl array.");
            return;
        }
    }

    Debug.LogWarning($"⚠️ No 'global decl' section found in Ink JSON.");
}



    // Converts Unity TextAsset path to actual filesystem path
    private string GetRelativePath(TextAsset asset)
    {
        string path = UnityEditor.AssetDatabase.GetAssetPath(asset);
        return path.Replace("Assets/", "");
    }

private void ResetAllFlagsOnPlay()
{
    if (!File.Exists(fullFilePath))
    {
        Debug.LogWarning("⚠️ Ink JSON file not found for reset: " + fullFilePath);
        return;
    }

    string jsonText = File.ReadAllText(fullFilePath);
    var root = JSON.Parse(jsonText);
    var rootArray = root["root"].AsArray;

    List<string> flagsToReset = new List<string>
    {
        "PiekritiPriesterim",
        "PiekritiBomzim",
        "PiekritiMaksliniekam",
        "PiekritiTantei",
        "PiekritiPankam",
        "PiekritiPianistei"
    };

    bool changedSomething = false;

    for (int i = 0; i < rootArray.Count; i++)
    {
        var item = rootArray[i];
        if (item != null && item["global decl"] != null)
        {
            var globalDecl = item["global decl"].AsArray;

            for (int j = 0; j < globalDecl.Count; j++)
            {
                var entry = globalDecl[j];
                if (entry != null && entry.IsObject && entry["VAR="] != null)
                {
                    string foundVar = entry["VAR="];
                    if (flagsToReset.Contains(foundVar))
                    {
                        int boolIndex = j - 1;
                        if (boolIndex >= 0 && globalDecl[boolIndex].IsBoolean)
                        {
                            globalDecl[boolIndex].AsBool = false;
                            changedSomething = true;
                            Debug.Log($"🔄 Reset {foundVar} to FALSE at Play start");
                        }
                    }
                }
            }
        }
    }

    if (changedSomething)
    {
        File.WriteAllText(fullFilePath, root.ToString(2));
        Debug.Log("💾 All matching variables reset and saved.");
    }
    else
    {
        Debug.Log("⚠️ No matching flags were found to reset.");
    }
}



}
