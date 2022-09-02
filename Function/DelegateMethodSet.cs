using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipboardEnterRemover
{
    public delegate void progressBarVisible(bool visible);
    public delegate void listLogging(string title, string content);
    public class DelegateFunction
    {
        public progressBarVisible? MainProgressBarVisible;
        public listLogging? ListLogging;
    }
}
