using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Framework.Controls
{
    public class Environment
    {
        static ControlsSources controlSource = ControlsSources.Datx;
        static Interpreter interpreter = null;
        public static Interpreter Interpreter
        {
            get
            {
                if (System.Web.HttpContext.Current.Items["Interpreter"] == null)
                    System.Web.HttpContext.Current.Items["Interpreter"] = new Interpreter();
                return (Interpreter)System.Web.HttpContext.Current.Items["Interpreter"];
            }
            set { interpreter = value; }
        }
        public static ControlsSources ControlSource { get { return controlSource; } set { controlSource = value; } }
        public static Dictionary<string, string> Dependencies { get { return Interpreter.Dependencies; } }
    }
}
