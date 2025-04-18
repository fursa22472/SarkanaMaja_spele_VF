using UnityEngine;
using System.IO;
using System.Collections.Generic;
using SimpleJSON;

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
    public TextAsset assignedJsonFile; // ✅ Drag the same JSON file here as on the Ink character

    private string fullFilePath = "";
    private string lastPlayed = "";

    void Start()
    {
        if (assignedJsonFile == null)
        {
            Debug.LogError("❌ You forgot to assign your Ink JSON in the Inspector!");
            return;
        }

        // ✅ This is the only path we’ll use
        string filename = assignedJsonFile.name + "_runtime.json";
        fullFilePath = Path.Combine(Application.persistentDataPath, filename);

        // ✅ Create the writable version only once
        if (!File.Exists(fullFilePath))
        {
            File.WriteAllText(fullFilePath, assignedJsonFile.text);
            Debug.Log("📄 Created runtime JSON: " + fullFilePath);
        }

        ResetAllFlagsOnPlay();
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
                        SetInkFlag(flag.inkVariableName);
                        break;
                    }
                }
            }
        }
    }

    void SetInkFlag(string variableName)
    {
        if (!File.Exists(fullFilePath))
        {
            Debug.LogError("❌ JSON not found: " + fullFilePath);
            return;
        }

        string jsonText = File.ReadAllText(fullFilePath);
        var root = JSON.Parse(jsonText);
        var rootArray = root["root"].AsArray;

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
                        if (foundVar == variableName)
                        {
                            int boolIndex = j - 1;
                            if (boolIndex >= 0 && globalDecl[boolIndex].IsBoolean)
                            {
                                globalDecl[boolIndex].AsBool = true;
                                File.WriteAllText(fullFilePath, root.ToString(2));
                                Debug.Log($"✅ Set {variableName} to TRUE");
                                return;
                            }
                        }
                    }
                }
            }
        }

        Debug.LogWarning($"⚠️ Couldn't find variable: {variableName}");
    }

    void ResetAllFlagsOnPlay()
    {
        if (!File.Exists(fullFilePath)) return;

        string jsonText = File.ReadAllText(fullFilePath);
        var root = JSON.Parse(jsonText);
        var rootArray = root["root"].AsArray;

        List<string> flagsToReset = new List<string>
        {
            "PiekritiPriesterim", "PiekritiBomzim", "PiekritiMaksliniekam",
            "PiekritiTantei", "PiekritiPankam", "PiekritiPianistei"
        };

        bool changed = false;

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
                                changed = true;
                                Debug.Log($"🔄 Reset {foundVar} to FALSE");
                            }
                        }
                    }
                }
            }
        }

        if (changed)
        {
            File.WriteAllText(fullFilePath, root.ToString(2));
            Debug.Log("💾 Saved reset JSON");
        }
    }

    // 🔁 Used by the Ink system to get the same JSON path
    public string GetRuntimeJsonPath()
    {
        return fullFilePath;
    }
}
