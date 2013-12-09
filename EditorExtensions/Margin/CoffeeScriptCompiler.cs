﻿using MadsKristensen.EditorExtensions;
using System.Runtime.InteropServices;
using System.Windows.Threading;

[ComVisible(true)]
public class CoffeeScriptCompiler : ScriptRunnerBase
{
	public CoffeeScriptCompiler(Dispatcher dispatcher)
		: base(dispatcher)
	{ }

	protected override string CreateSource(string source, string state)
	{
		string clean = source
			.Replace("\\", "\\\\")
			.Replace("\n", "\\n")
			.Replace("\r", "\\r")
			.Replace("'", "\\'");

		string bare = WESettings.GetBoolean(WESettings.Keys.WrapCoffeeScriptClosure) ? "false" : "true";

		string compiler = "MadsKristensen.EditorExtensions.Resources.Scripts.CoffeeScript-1.4.js";
		if (WESettings.GetBoolean(WESettings.Keys.EnableIcedCoffeeScript))
		{
			compiler = "MadsKristensen.EditorExtensions.Resources.Scripts.IcedCoffeeScript-1.3.3.js";
		}

		string script = ReadResourceFile(compiler) +
				"try{" +
					"var result = CoffeeScript.compile('" + clean + "', { bare: " + bare + ", runtime:'inline' });" +
					"external.Execute(result, '" + state.Replace("\\", "\\\\") + "');" +
				"}" +
				"catch (err){" +
					"external.Execute('ERROR:' + err, '" + state.Replace("\\", "\\\\") + "');" +
				"}";

		return script;
	}
}