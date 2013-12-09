using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadsKristensen.EditorExtensions.ScriptEngines
{
	interface IScriptEngine : IDisposable
	{
		bool IsAvailable();
		void Compile(string source, object external);
	}
}
