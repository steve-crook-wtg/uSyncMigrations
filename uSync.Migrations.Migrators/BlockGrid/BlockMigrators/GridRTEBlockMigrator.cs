using System.Text.RegularExpressions;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Strings;

using uSync.Migrations.Core.Legacy.Grid;
using uSync.Migrations.Migrators.BlockGrid.Extensions;

namespace uSync.Migrations.Migrators.BlockGrid.BlockMigrators;

public class GridRTEBlockMigrator : GridBlockMigratorSimpleBase, ISyncBlockMigrator
{

    public GridRTEBlockMigrator(IShortStringHelper shortStringHelper)
        : base(shortStringHelper) { }

    public string[] Aliases => new[] { "rte" };

    public override string GetEditorAlias(ILegacyGridEditorConfig editor) => "Richtext Editor";


    //public override Dictionary<string, object> GetPropertyValues(GridValue.GridControl control, SyncMigrationContext context)
    //{
    //    var originalHtml = control.Value?.ToString() ?? string.Empty;

    //    var paramRegex = new Regex(@"(\w+)=""([^""]*)""");
    //    var macroRegex = new Regex(
    //        @"<div[^>]*class\s*=\s*""[^""]*\bumb-macro-holder\b[^""]*""[^>]*>.*?<!--\s*<\?UMBRACO_MACRO\s+macroAlias=""(?<alias>[^""]+)""(?<params>[^>]*?)\s*/>\s*-->.*?</div>",
    //        RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

    //    var blocks = new List<object>();
    //    bool foundAny = false;

    //    string updatedHtml = macroRegex.Replace(originalHtml, match =>
    //    {
    //        foundAny = true;

    //        var alias = match.Groups["alias"].Value;
    //        var rawParams = match.Groups["params"].Value;

    //        var paramDict = new Dictionary<string, object>();
    //        bool isIconLink = alias.ToLower().Contains("icon");

    //        foreach (Match paramMatch in paramRegex.Matches(rawParams))
    //        {
    //            var key = paramMatch.Groups[1].Value;
    //            var value = paramMatch.Groups[2].Value;

    //            if (isIconLink && key == "type")
    //            {
    //                switch (value)
    //                {
    //                    case "pdf": value = "[\"PDF\"]"; break;
    //                    case "audio": value = "[\"Audio\"]"; break;
    //                    case "globe": value = "[\"Globe\"]"; break;
    //                }
    //            }

    //            paramDict[key] = value;
    //        }

    //        var contentTypeKey = context.GetContentTypeKeyOrDefault(alias, Guid.Empty);
    //        if (contentTypeKey == Guid.Empty)
    //            return match.Value; // no replacement, keep original

    //        var contentGuid = Guid.NewGuid();
    //        var contentUdi = Udi.Create("element", contentGuid);

    //        blocks.Add(new
    //        {
    //            contentTypeKey = contentTypeKey,
    //            contentUdi = contentUdi.ToString(),
    //            content = paramDict
    //        });

    //        return isIconLink
    //            ? $"<p><umb-rte-block-inline class=\"ng-scope ng-isolate-scope\" data-content-udi=\"{contentUdi}\"><!--Umbraco-Block--></umb-rte-block-inline></p>"
    //            : $"<umb-rte-block class=\"ng-scope ng-isolate-scope\" data-content-udi=\"{contentUdi}\"><!--Umbraco-Block--></umb-rte-block>";
    //    });

    //    if (!foundAny)
    //    {
    //        return base.GetPropertyValues(control, context);
    //    }

    //    return new Dictionary<string, object>
    //    {
    //        { "value", updatedHtml },
    //        { "blocks", blocks }
    //    };
    //}


}


