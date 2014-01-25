﻿using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace MadsKristensen.EditorExtensions
{
    [Export(typeof(IWpfTextViewMarginProvider))]
    [Name(LessMargin.MarginName)]
    [Order(After = PredefinedMarginNames.RightControl)]
    [MarginContainer(PredefinedMarginNames.Right)]
    [ContentType("LESS")]
    [ContentType("CoffeeScript")]
    [ContentType("Markdown")]
    [ContentType("TypeScript")]
    [TextViewRole(PredefinedTextViewRoles.Debuggable)]
    internal sealed class MarginFactory : IWpfTextViewMarginProvider
    {
        public IWpfTextViewMargin CreateMargin(IWpfTextViewHost textViewHost, IWpfTextViewMargin containerMargin)
        {
            string source = textViewHost.TextView.TextBuffer.CurrentSnapshot.GetText();
            ITextDocument document;

            if (textViewHost.TextView.TextDataModel.DocumentBuffer.Properties.TryGetProperty(typeof(ITextDocument), out document))
            {
                switch (textViewHost.TextView.TextBuffer.ContentType.DisplayName)
                {
                    case "LESS":
                        bool showLess = WESettings.GetBoolean(WESettings.Keys.ShowLessPreviewWindow);
                        return new LessMargin("CSS", source, showLess, document);

                    //case "scss":
                    //    return new ScssMargin("CSS", source, true, document);

                    case "CoffeeScript":
                        bool showCoffee = WESettings.GetBoolean(WESettings.Keys.ShowCoffeeScriptPreviewWindow);
                        return new CoffeeScriptMargin("JavaScript", source, showCoffee, document);
                        
                    case "markdown":
                        return new MarkdownMargin("text", source, true, document);

                    case "TypeScript":
                        bool showTypeScript = WESettings.GetBoolean(WESettings.Keys.ShowTypeScriptPreviewWindow);
                        return new TypeScriptMargin("JavaScript", source, showTypeScript, document);
                }
            }

            return null;
        }
    }
}