using UnityEngine;
using System.IO;

#if UNITY_EDITOR


/// <summary>
/// A baby TTInclude importer so the editor can show ttinclude contents
/// </summary>
[UnityEditor.AssetImporters.ScriptedImporter(1, "ttinclude")]
public class TTIncludeImporter : UnityEditor.AssetImporters.ScriptedImporter
{
    public override void OnImportAsset(UnityEditor.AssetImporters.AssetImportContext ctx)
    {
        var asset = new TextAsset(File.ReadAllText(ctx.assetPath));
        ctx.AddObjectToAsset("text", asset);
        ctx.SetMainObject(asset);
    }
}
#endif
