using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClipboardEnterRemover
{
    internal class Background
    {
        System.Threading.Thread? _t;
        public Background()
        {
            //클립보드 엔터제거기 실행 상태 디스플레이용 쓰레드
            _t = new Thread(new ThreadStart(() => {
                while (true)
                {
                    if (Program.MainFormClosed)
                        return;

                    if (Program.BackgrndRun && ClipboardMonitor.MonitorStatus == ClipboardMonitor.Status.Stop)
                    {
                        ClipboardMonitor.OnClipboardChange += (format, data) =>
                        {
                            SetClipboard((string)data);
                        };
                        ClipboardMonitor.Start();
                        Program.deFunction.MainProgressBarVisible(true);
                    }
                    else if (!Program.BackgrndRun && ClipboardMonitor.MonitorStatus == ClipboardMonitor.Status.Start)
                    {
                        ClipboardMonitor.Stop();
                        Program.deFunction.MainProgressBarVisible(false);
                    }
                    Thread.Sleep(500);
                }
            }));
        }

        public void Start()
        {
            _t.IsBackground = true;
            _t.Start();
        }

        /// <summary>
        /// 클립보드의 DATA를 엔터 값을 제거한 값으로 대체시킨다.
        /// </summary>
        /// <param name="data"></param>
        void SetClipboard(string data)
        {
            if (string.IsNullOrEmpty(data))
                return;
            if (Program.ClipboardData.RawData == data)
                return;

            Program.ClipboardData.RawData = data;
            Program.ClipboardData.EnterRemoveData = TextRemover.RemoveText(data);
            Program.deFunction.ListLogging("catch data", data);

            try
            {
                Clipboard.SetText(Program.ClipboardData.EnterRemoveData);
            }
            catch (ExternalException ex)
            {
                Program.deFunction.ListLogging("ClipboardError", ex.Message);
            }
            catch (Exception ex)
            {
                Program.deFunction.ListLogging("Exception", ex.Message);
            }
        }
    }
}
