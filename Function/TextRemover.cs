using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipboardEnterRemover
{
    internal class TextRemover
    {
        static readonly string[] _RemoveTaget = { "-\r", "-\n", "\r", "\n" };
        /// <summary>
        /// 엔터값 등 제거할 값을 제거하고 반환한다.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="oldvalues"></param>
        /// <returns></returns>
        public static string RemoveText(string text)
        {
            for (int i = 0; i < _RemoveTaget.Length; i++)
            {
                text = text.Replace(_RemoveTaget[i], string.Empty);
            }
            return text;
        }
    }
}
