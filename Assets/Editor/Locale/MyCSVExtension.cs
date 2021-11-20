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
using System.Dynamic;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization;

public static class MyCSVExtension
{
    const string csvFileName = "OneForAll.csv";
    //将所有Module Collection(string类型)的数据导出到一个csv文件中
    [MenuItem("Localization/CSV2/Emport All Collections In One CSV")]
    public static void ExportAllCollection()
    {
        // Get every String Table Collection
        var stringTableCollections = LocalizationEditorSettings.GetStringTableCollections();
        int index = 0;
        using (var stream = new StreamWriter(csvFileName, false, Encoding.UTF8))
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

    [MenuItem("Localization/CSV2/Import The OneForAll CSV")]
    //读取总表，并刷新对应Collection中的数据
    public static void ImportAll()
    {
        using (var stream = new StreamReader(csvFileName))
        {
            using (var csv = new CsvReader(stream, CultureInfo.InvariantCulture))
            {
                Dictionary<string, string> properties = new Dictionary<string, string>();
                properties.Add("Key", string.Empty);
                properties.Add("Id", string.Empty);
                properties.Add("Chinese (Simplified) (zh-Hans)", string.Empty);
                properties.Add("English (en)", string.Empty);
                properties.Add("Portuguese (pt)", string.Empty);
                //var dynamicObject = new ExpandoObject() as IDictionary<string, Object>;
                //foreach (var property in properties)
                //{
                //    dynamicObject.Add(property.Key, property.Value);
                //}

                var records = csv.GetRecords<dynamic>();
                foreach(ExpandoObject record in records)
                {
                    IDictionary<string, object> dict = record;
                    var key = dict["Key"] as string;
                    var collectionName = GetCollectionNameByKey(key);
                    var id = dict["Id"] as string;
                    var locales = LocalizationEditorSettings.GetLocales();
                    foreach(Locale locale in locales)
                    {
                        LocalizationTableCollection collection = LocalizationEditorSettings.GetStringTableCollection(collectionName);
                        if (collection == null)
                            continue;
                        StringTable localTable = collection.GetTable(locale.Identifier) as StringTable;
                        var value = dict[locale.name] as string;
                        if (localTable == null)
                        {
                            if(!string.IsNullOrEmpty(value))
                            { 
                                localTable = collection.AddNewTable(locale.Identifier) as StringTable;
                            }else
                                continue;
                        }
                        StringTableEntry entry = localTable.GetEntry(key);
                        if (entry == null)
                        {
                            entry = localTable.AddEntry(key, value);
                        }
                        else
                            entry.Data.Localized = value;
                    }
                }
            }
        }
    }

    static string GetCollectionNameByKey(string key)
    {
        var data = key.Split('_');
        return data[0];
    }
}

