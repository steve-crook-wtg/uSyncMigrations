using Newtonsoft.Json;

using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Strings;
using Umbraco.Extensions;

using uSync.Migrations.Core.Legacy.Grid;
using static Lucene.Net.Queries.Function.ValueSources.MultiFunction;

namespace uSync.Migrations.Migrators.BlockGrid.BlockMigrators;

public class GridEmbedBlockMigrator : GridBlockMigratorSimpleBase, ISyncBlockMigrator
{
    public GridEmbedBlockMigrator(IShortStringHelper shortStringHelper)
         : base(shortStringHelper)
    {
    }

    public string[] Aliases => new[] { "embed" };

    public override string GetEditorAlias(ILegacyGridEditorConfig editor) => "OEmbed Picker"; // this must already exist before migration

    public override Dictionary<string, object> GetPropertyValues(GridValue.GridControl control, SyncMigrationContext context)
    {
        var properties = new Dictionary<string, object>();
        if (control.Value == null) return properties;

        var embed = control.Value.ToString();

        if (!embed.Trim().StartsWith("["))
        {
            embed = $"[{embed}]"; // new version has a list of embeds, not just one
        }

        properties.Add("embed", embed);

        return properties;
    }
}


