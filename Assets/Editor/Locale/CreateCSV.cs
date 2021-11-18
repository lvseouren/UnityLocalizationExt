using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Localization;
using UnityEditor.Localization.Plugins.CSV.Columns;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization.Settings;
using System.IO;
using UnityEditor.Localization.Plugins.CSV;
using System.Text;

public class CreateCSV 
{
    [MenuItem("CSV/exportCSV", false)]
    public static void ExportCSV()
    {
        var savePath = Application.dataPath + "/CSVFile";
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }
        else
        {
            foreach (string d in Directory.GetFileSystemEntries(savePath))
            {
                if (File.Exists(d))
                {
                    File.Delete(d);
                }
                else
                    Directory.Delete(d);
            }
        }

        var collections = LocalizationEditorSettings.GetStringTableCollections();
        var totalPath = path + "/Total.csv";

        foreach (var collection in collections)
        {
            ExportTable(collection, false, savePath);
        }
    }

    public static void ExportTable(StringTableCollection collection, bool includeComments, string path)
    {
        List<CsvColumns> list = new List<CsvColumns>();
        list.Add(new KeyIdColumns { IncludeSharedComments = includeComments });
        
        foreach (var tab in collection.StringTables)
        {
            LocaleIdentifier identifier = tab.LocaleIdentifier;
            list.Add(new LocaleColumns { LocaleIdentifier = identifier, IncludeComments = includeComments });
        }
        ExportTotal(list, collection, totalPath);
        Export(list, collection, path);
    }


    public static void Export(IList<CsvColumns> cellMappings, StringTableCollection collection, string path)
    {
        var name = collection.TableCollectionName;
        string filePath = path + "/" + name + ".csv";
        
        using (var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
        {
            var stream = new StreamWriter(fs, Encoding.UTF8);
            Csv.Export(stream, collection, cellMappings);
        }
        //EditorUtility.RevealInFinder(filePath);
    }

    public static void ExportTotal(IList<CsvColumns> cellMappings, StringTableCollection collection, string path)
    {
        using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
        {
            var stream = new StreamWriter(fs, Encoding.UTF8);
            Csv.Export(stream, collection, cellMappings);
        }
    }
}


