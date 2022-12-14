using System.Runtime.InteropServices;

namespace ClipboardEnterRemover
{
    internal class Background
    {
        readonly System.Threading.Thread? _t;
        public Background()
        {
            //클립보드 엔터제거기 실행 상태 디스플레이용 쓰레드
            _t = new Thread(() => {
                Thread.Sleep(1000);
                while (true)
                {
                    if (Program.MainFormClosed)
                        return;

                    if (Program.BackgrndRun && ClipboardMonitor.MonitorStatus == ClipboardMonitor.Status.Stop)
                    {
                        ClipboardMonitor.OnClipboardChange += (format, data) =>
                        {
                            Background.SetClipboard((string)data);
                        };
                        ClipboardMonitor.Start();
                        MainForm.MainProgressBarVisible?.Invoke(true);
                    }
                    else if (!Program.BackgrndRun && ClipboardMonitor.MonitorStatus == ClipboardMonitor.Status.Start)
                    {
                        ClipboardMonitor.Stop();
                        MainForm.MainProgressBarVisible?.Invoke(false);
                    }
                    Thread.Sleep(500);
                }
            });
            _t.IsBackground = true;
            _t.Start();
        }

        /// <summary>
        /// 클립보드의 DATA를 엔터 값을 제거한 값으로 대체시킨다.
        /// </summary>
        /// <param name="data"></param>
        static void SetClipboard(string data)
        {
            if (string.IsNullOrEmpty(data))
                return;
            if (Program.ClipboardData.RawData == data)
                return;

            Program.ClipboardData.RawData = data;
            Program.ClipboardData.EnterRemoveData = TextRemover.RemoveText(data);
            MainForm.ListLogging?.Invoke("catch data", data);

            try
            {
                Clipboard.SetText(Program.ClipboardData.EnterRemoveData);
            }
            catch (ExternalException ex)
            {
                MainForm.ListLogging?.Invoke("ClipboardError", ex.Message);
            }
            catch (Exception ex)
            {
                MainForm.ListLogging?.Invoke("Exception", ex.Message);
            }
        }
    }
}
