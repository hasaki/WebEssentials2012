﻿using Microsoft.VisualStudio.Shell;
using System.ComponentModel;

namespace MadsKristensen.EditorExtensions
{
    class GeneralOptions : DialogPage
    {
        public GeneralOptions()
        {
            Settings.Updated += delegate { LoadSettingsFromStorage(); };
        }

        public override void SaveSettingsToStorage()
        {
            Settings.SetValue(WESettings.Keys.EnableMustache, EnableMustache);
            Settings.SetValue(WESettings.Keys.EnableHtmlZenCoding, EnableHtmlZenCoding);
            Settings.SetValue(WESettings.Keys.KeepImportantComments, KeepImportantComments);
	        Settings.SetValue(WESettings.Keys.UseV8ScriptEngine, UseV8ScriptEngine);

            Settings.Save();
        }

        public override void LoadSettingsFromStorage()
        {
            EnableMustache = WESettings.GetBoolean(WESettings.Keys.EnableMustache);
            EnableHtmlZenCoding = WESettings.GetBoolean(WESettings.Keys.EnableHtmlZenCoding);
            KeepImportantComments = WESettings.GetBoolean(WESettings.Keys.KeepImportantComments);
	        UseV8ScriptEngine = WESettings.GetBoolean(WESettings.Keys.UseV8ScriptEngine);
        }

        // MISC
        [LocDisplayName("Enable Mustache/Handlebars")]
        [Description("Enable colorization Mustache/Handlebars syntax in the HTML editor")]
        [Category("Misc")]
        public bool EnableMustache { get; set; }

        [LocDisplayName("Enable HTML ZenCoding")]
        [Description("Enables ZenCoding in the HTML editor")]
        [Category("Misc")]
        public bool EnableHtmlZenCoding { get; set; }

        [LocDisplayName("Save UTF-8 files with BOM")]
        [Description("Whether to use BOM « byte-order-mark » when saving UTF-8 files")]
        [Category("Misc")]
        public bool UseBom { get; set; }

		[LocDisplayName("Use V8 Script Engine (if available)")]
		[Description("Whether to use the V8 Script Engine if its available")]
		[Category("Misc")]
		public bool UseV8ScriptEngine { get; set; }

        [LocDisplayName("Keep important comments")]
        [Description("Don't strip important comments when minifying JS and CSS. Important comments follows this pattern: /*! text */")]
        [Category("Minification")]
        public bool KeepImportantComments { get; set; }
    }
}
