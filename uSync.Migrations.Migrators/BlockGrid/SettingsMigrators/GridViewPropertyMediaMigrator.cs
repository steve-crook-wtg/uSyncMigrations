using Newtonsoft.Json;
using Umbraco.Cms.Core.Models;
using Umbraco.Extensions;
using uSync.Migrations.Migrators.BlockGrid.Models;

namespace uSync.Migrations.Migrators.BlockGrid.SettingsMigrators;

public class GridViewPropertyMediaMigrator : IGridSettingsViewMigrator
{
    // This is a migration from the more general media picker to one which only allows images and videos
    // Used specifically for the "Background image or video" row setting

    public string ViewKey => "MediaPicker";

    public string GetNewDataTypeAlias(string gridAlias, string? configItemLabel) => "Image and Video Media Picker";

    public object ConvertContentString(string value)
    {
        return value;
    }

    public NewDataTypeInfo? GetAdditionalDataType(string dataTypeAlias, IEnumerable<GridSettingsConfigurationItemPrevalue>? preValues)
    {
        NewDataTypeInfo newDataTypeInfo = 
            new NewDataTypeInfo(dataTypeAlias.ToGuid(), 
                                dataTypeAlias, 
                                dataTypeAlias, 
                                "Umbraco.MediaPicker3", 
                                nameof(ValueStorageType.Ntext), 
                                new MediaPickerConfig()
                                {
                                    Filter = "Image,umbracoMediaVectorGraphics,umbracoMediaVideo"
                                });

        return newDataTypeInfo;
    }
}

public class MediaPickerConfig
{
    [JsonProperty("filter")]
    public string Filter { get; set; } = string.Empty;

    [JsonProperty("multiple")]
    public bool Multiple { get; set; }
}