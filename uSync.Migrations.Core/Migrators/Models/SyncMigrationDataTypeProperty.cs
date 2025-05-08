using System.Collections.ObjectModel;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Umbraco.Extensions;

using uSync.Migrations.Core.Models;

namespace uSync.Migrations.Core.Migrators.Models;

public sealed class SyncMigrationDataTypeProperty : SyncMigrationPropertyBase
{
    public SyncMigrationDataTypeProperty(string dataTypeAlias, string editorAlias, string databaseType, IList<PreValue> preValues)
        : base(editorAlias)
    {
        DatabaseType = databaseType;
        DataTypeAlias = dataTypeAlias;
        PreValues = new ReadOnlyCollection<PreValue>(preValues);
        ConfigAsString = TranslatePreValuesToConfig(preValues);
    }

    private string? TranslatePreValuesToConfig(IList<PreValue> preValues)
    {
        var json = new Dictionary<string, object>();
        foreach (var oPreValue in preValues)
        {
            json.TryAdd(oPreValue.Alias, oPreValue?.Value.ToString().DetectIsJson() == true ? JsonConvert.DeserializeObject(oPreValue.Value) : oPreValue?.Value);
        }
        return JsonConvert.SerializeObject(json);
    }

    public SyncMigrationDataTypeProperty(string dataTypeAlias, string editorAlias, string databaseType, string? config)
        : base(editorAlias)
    {
        DataTypeAlias = dataTypeAlias;
        DatabaseType = databaseType;
        ConfigAsString = config;
        PreValues = TranslateConfigToPreValues(config);
    }

    private IReadOnlyCollection<PreValue>? TranslateConfigToPreValues(string? config)
    {
        if (string.IsNullOrWhiteSpace(config))
        {
            return null;
        }
        var preValuesDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(config);
        if (preValuesDictionary == null)
        {
            return null;
        }

        foreach (var key in preValuesDictionary.Keys)
        {
            var value = preValuesDictionary[key];

        }

        List<PreValue> preValues = preValuesDictionary.Select(kvp => new PreValue() { Alias = kvp.Key, Value = kvp.Value != null ? kvp.Value.ToString() : "null" }).ToList();

        return new ReadOnlyCollection<PreValue>(preValues);
    }

    public string DataTypeAlias { get; private set; }

    public string DatabaseType { get; private set; }

    public IReadOnlyCollection<PreValue>? PreValues { get; private set; }

    public string? ConfigAsString { get; private set; }
}