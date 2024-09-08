using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class InkVariableManager : MonoBehaviour
{
    public static InkVariableManager Instance;

    private Dictionary<string, object> globalVariables = new Dictionary<string, object>();
    private Story story;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitializeStory(TextAsset inkJSONAsset)
    {
        story = new Story(inkJSONAsset.text);
        ApplyGlobalVariables();
    }

    public void SetVariable(string key, object value)
    {
        if (story != null)
        {
            story.variablesState[key] = value;
            globalVariables[key] = value;
        }
        else
        {
            Debug.LogWarning("Story instance is not initialized.");
        }
    }

    public object GetVariable(string key)
    {
        if (globalVariables.ContainsKey(key))
        {
            return globalVariables[key];
        }
        return null;
    }

    private void ApplyGlobalVariables()
    {
        if (story != null)
        {
            foreach (var kvp in globalVariables)
            {
                story.variablesState[kvp.Key] = kvp.Value;
            }
        }
    }

    public Story GetStoryInstance()
    {
        return story;
    }
}
