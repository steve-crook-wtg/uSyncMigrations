using Newtonsoft.Json.Linq;

using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Extensions;

using uSync.Migrations.Core.Extensions;

namespace uSync.Migrations.Migrators.Core;

[SyncMigrator(UmbEditors.Aliases.TinyMce, typeof(RichTextConfiguration), IsDefaultAlias = true)]
[SyncMigrator("Umbraco.TinyMCEv3")]
[SyncMigratorVersion(7, 8)]
public class RichTextBoxMigrator : SyncPropertyMigratorBase
{
    public override object? GetConfigValues(SyncMigrationDataTypeProperty dataTypeProperty, SyncMigrationContext context)
    {
        bool isWTG = context.Metadata.DatabaseName.Contains("wisetechglobal") &&
                        !context.Metadata.DatabaseName.Contains("forms") &&
                        !context.Metadata.DatabaseName.Contains("partners");

        if (dataTypeProperty.PreValues?.Count > 0)
        {
            var config = new RichTextConfiguration().MapPreValues(dataTypeProperty.PreValues) as RichTextConfiguration;

            if (config is not null)
            {
                if (config.Editor is JObject editor)
                {
                    var toolbar = editor["toolbar"] as JArray;
                    if (toolbar?.Count > 0)
                    {
                        var replacements = new Dictionary<string, string>
                        {
                            { "code", "ace" },
                            { "styleselect", "styles" },
                        };

                        foreach (var replacement in replacements)
                        {
                            var idx = toolbar.FindIndex(x => replacement.Key.Equals(x.ToString()) == true);
                            if (idx >= 0)
                            {
                                toolbar.RemoveAt(idx);
                                toolbar.Insert(idx, replacement.Value);
                            }
                        }

                        if (isWTG && !toolbar.Any(t => t.ToString() == "umbblockpicker"))
                        {
                            toolbar.Add("umbblockpicker");
                        }

                        if (toolbar.Any(t => t.ToString() == "umbmacro"))
                        {
                            toolbar.RemoveAt(toolbar.FindIndex(x => x.ToString() == "umbmacro"));
                        }
                    }

                    if (context.Metadata.SourceVersion < 8)
                    {
                        var stylesheets = editor["stylesheets"] as JArray;
                        if (stylesheets?.Count > 0)
                        {
                            for (int i = 0; i < stylesheets.Count; i++)
                            {
                                stylesheets[i].Replace($"/css/{stylesheets[i]}.css");
                            }
                        }
                    }

                    if (editor["mode"] is null)
                    {
                        editor["mode"] = "classic";
                    }
                }

                if (config.OverlaySize is null)
                {
                    config.OverlaySize = "small";
                }

                if (isWTG)
                {
                    config.Blocks =
                        [
                            new RichTextConfiguration.RichTextBlockConfiguration()
                            {
                                BackgroundColor = null,
                                ContentElementTypeKey = Guid.Parse("a2922d1e-b524-4b81-ae36-4f46785425ff"),
                                DisplayInline = false,
                                EditorSize = "medium",
                                ForceHideContentEditorInOverlay = false,
                                IconColor = null,
                                Label = "Hubspot Form",
                                SettingsElementTypeKey = null,
                                Stylesheet = null,
                                Thumbnail = null,
                                View = null
                            },
                            new RichTextConfiguration.RichTextBlockConfiguration()
                            {
                                BackgroundColor = null,
                                ContentElementTypeKey = Guid.Parse("6afe0da0-4644-49f3-b4b7-d8b7c86b778d"),
                                DisplayInline = true,
                                EditorSize = "medium",
                                ForceHideContentEditorInOverlay = false,
                                IconColor = null,
                                Label = "Icon Link",
                                SettingsElementTypeKey = null,
                                Stylesheet = null,
                                Thumbnail = null,
                                View = null
                            },
                            new RichTextConfiguration.RichTextBlockConfiguration()
                            {
                                BackgroundColor = null,
                                ContentElementTypeKey = Guid.Parse("57ddd548-e3a6-41ad-9b82-95d08be124f1"),
                                DisplayInline = false,
                                EditorSize = "medium",
                                ForceHideContentEditorInOverlay = false,
                                IconColor = null,
                                Label = "YourIR Announcements List",
                                SettingsElementTypeKey = null,
                                Stylesheet = null,
                                Thumbnail = null,
                                View = null
                            }
                        ];
                }
            }

            return config;
        }

        return base.GetConfigValues(dataTypeProperty, context);
    }

    public override string? GetContentValue(SyncMigrationContentProperty contentProperty, SyncMigrationContext context)
    {
        var richTextValue = string.Empty;

        if (string.IsNullOrWhiteSpace(contentProperty.Value) == false)
            richTextValue = GuidExtensions.LocalLink2Udi(contentProperty.Value);

        return richTextValue;
    }
}