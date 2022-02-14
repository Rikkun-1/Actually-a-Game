using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// loads json text assets
/// </summary>
public class TemplateLoader : ITemplateLoader
{
    public List<Tuple<string, string>> LoadSingleTemplateFile()
    {
        var textAssets = Resources.LoadAll<TextAsset>("EntityTemplate/SingleEntity");

        return textAssets.Select(textAsset => new Tuple<string, string>(textAsset.name, textAsset.text))
                         .ToList();
    }

    public List<Tuple<string, string>> LoadGroupTemplateFiles()
    {
        var textAssets = Resources.LoadAll<TextAsset>("EntityTemplate/GroupEntity");

        return textAssets.Select(textAsset => new Tuple<string, string>(textAsset.name, textAsset.text))
                         .ToList();
    }

    public Tuple<string, string> LoadSavedEntityFile(string saveFileName)
    {
        var textAssets = Resources.Load<TextAsset>($"EntityTemplate/SaveFile/{saveFileName}");
        
        if (textAssets == null)
        {
            throw new Exception($"no save file : {saveFileName}");
        }

        return new Tuple<string, string>(textAssets.name, textAssets.text);
    }
}
