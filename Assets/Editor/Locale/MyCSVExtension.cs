using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Localization;
using UnityEditor.Localization.Plugins.CSV;
using CsvHelper;
using UnityEditor;
using UnityEngine.Localization.Metadata;

public static class MyCSVExtension
{
    //将所有Module Collection(string类型)的数据导出到一个csv文件中
    [MenuItem("Localization/CSV2/Emport All Collections In One CSV")]
    public static void ExportAllCollection()
    {
        // Get every String Table Collection
        var stringTableCollections = LocalizationEditorSettings.GetStringTableCollections();
        string fileName = "OneForAll.csv";
        int index = 0;
        using (var stream = new StreamWriter(fileName, false, Encoding.UTF8))
        {
            using (var csvWriter = new CsvWriter(stream, CultureInfo.InvariantCulture))
            {
                //write header
                //key&id
                csvWriter.WriteField("Key");
                csvWriter.WriteField("Id");

                var locales = LocalizationEditorSettings.GetLocales();
                foreach(var locale in locales)
                {
                    csvWriter.WriteField(locale.name);
                }
                csvWriter.NextRecord();
                foreach (var collection in stringTableCollections)
                {
                    //写入collection 名称，写入分割content--不需要
                    //写入collection内容
                    //csvWriter.WriteField(collection.name);
                    //csvWriter.NextRecord();
                    foreach (var row in collection.GetRowEnumerator())
                    {
                        if (row.TableEntries[0] != null && row.TableEntries[0].SharedEntry.Metadata.HasMetadata<ExcludeEntryFromExport>())
                            continue;

                        csvWriter.WriteField(row.KeyEntry.Key);
                        csvWriter.WriteField(row.KeyEntry.Id);
                        int i = 0;
                        foreach(var table in collection.StringTables)
                        {
                            var entry = row.TableEntries[i++];
                            csvWriter.WriteField(entry.LocalizedValue, true);
                        }
                        csvWriter.NextRecord();
                    }
                    //csvWriter.NextRecord();
                    //csvWriter.NextRecord();
                    //foreach (var cell in columnMappings)
                    //{
                    //    cell.WriteEnd(collection);
                    //}
                }
            }
        }
    }

    //读取总表，并刷新对应Collection中的数据
    public static void ImportAll()
    {

    }


}

