using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Threading;
using MadsKristensen.EditorExtensions;
using MadsKristensen.EditorExtensions.ScriptEngines;
using V8ScriptEngine = Microsoft.ClearScript.V8.V8ScriptEngine;

[ComVisible(true)]
public abstract class ScriptRunnerBase : IDisposable
{
	private Lazy<IScriptEngine> _scriptEngine = null;
    private bool _disposed;

    protected ScriptRunnerBase(Dispatcher dispatcher)
    {
	    _scriptEngine = new Lazy<IScriptEngine>(() =>
	    {
			var useV8 = WESettings.GetBoolean(WESettings.Keys.UseV8ScriptEngine);
			if (useV8)
			{
				var v8 = new MadsKristensen.EditorExtensions.ScriptEngines.V8ScriptEngine(dispatcher);
				if (v8.IsAvailable())
					return v8;
			}

			return new BrowserScriptEngine(dispatcher);
	    }, false);
    }

	protected abstract string CreateSource(string source, string state);

    public void Compile(string source, string state)
    {
	    _scriptEngine.Value.Compile(CreateSource(source, state), this);
    }

    public void Execute(string result, string state)
    {
        OnCompleted(result, state);
    }

    protected static string ReadResourceFile(string resourceFile)
    {
        using (Stream s = typeof(JsHintCompiler).Assembly.GetManifestResourceStream(resourceFile))
        using (var reader = new StreamReader(s))
        {
            return reader.ReadToEnd();
        }
    }

    public event EventHandler<CompilerEventArgs> Completed;

    protected void OnCompleted(string message, string data)
    {
        if (Completed != null)
        {
            Completed(this, new CompilerEventArgs() { Result = message, State = data });
        }
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            Completed = null;

			if(_scriptEngine.IsValueCreated)
				_scriptEngine.Value.Dispose();
	        _scriptEngine = null;

            _disposed = true;
        }
    }
}

public class CompilerEventArgs : EventArgs
{
    public string Result { get; set; }
    public string State { get; set; }
}