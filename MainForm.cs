using System.Runtime.InteropServices;
using System.Threading;
using ClipboardHelper;

namespace ClipboardEnterRemover
{
    public partial class MainForm : Form
    {
        static readonly string[] _RemoveTaget = {"\r","\n","-\r","-\n"};
        string _CurrentData = string.Empty;
        string _CurrentRemoveData = string.Empty;
        System.Threading.Thread _t;
        bool isStart = false;
        bool thisClosed = false;
        struct StatusName
        {
            public const string StartText = "중지";
            public const string StopText = "시작하기";
        }
        public MainForm()
        {
            InitializeComponent();
            _t = new Thread(new ThreadStart(() => {
                while (true)
                {
                    if (thisClosed)
                        return;

                    if (isStart)
                    {
                        if(ClipboardMonitor.MonitorStatus == ClipboardMonitor.Status.Stop)
                        {
                            ClipboardMonitor.OnClipboardChange += (format, data) =>
                            {
                                SetClipboard((string)data);
                            };
                            ClipboardMonitor.Start();
                        }
                        if (prbStatus.InvokeRequired)
                        {
                            prbStatus.Invoke(new MethodInvoker(() =>
                            {
                                prbStatus.Show();
                            }));
                        }
                        else
                        {
                            prbStatus.Show();
                        }
                    }
                    else
                    {
                        if(ClipboardMonitor.MonitorStatus == ClipboardMonitor.Status.Start)
                            ClipboardMonitor.Stop();

                        if (prbStatus.InvokeRequired)
                        {
                            prbStatus.Invoke(new MethodInvoker(() =>
                            {
                                prbStatus.Hide();
                            }));
                        }
                        else
                        {
                            prbStatus.Hide();
                        }
                    }
                    Thread.Sleep(500);
                }
            }));
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            prbStatus.Style = ProgressBarStyle.Marquee;
            prbStatus.MarqueeAnimationSpeed = 60;
            prbStatus.Hide();

            _t.IsBackground = true;
            _t.Start();

            btnClipboardGetText.Click += (o,i) => 
            {
                isStart = !isStart;
                if (isStart)
                {
                    btnClipboardGetText.Text = StatusName.StartText;
                }
                else
                {
                    btnClipboardGetText.Text = StatusName.StopText;
                }
            };

            this.FormClosed += (o, i) =>
            {
                ClipboardMonitor.Stop();
                thisClosed = true;
            };
        }

        void SetClipboard(string data)
        {
            if (string.IsNullOrEmpty(data))
                return;
            if (_CurrentData == data)
                return;

            _CurrentData = data;
            _CurrentRemoveData = RemoveText(data, _RemoveTaget);
            if (lbxLog.InvokeRequired)
                lbxLog.Invoke(new MethodInvoker(() => {
                    lbxLog.Items.Add(string.Format("{0}-catch data : [{1}]", DateTime.Now.ToString("HH:mm:ss"), data));
                    lbxLog.SelectedIndex = lbxLog.Items.Count - 1;
                }));
            else
            {
                lbxLog.Items.Add(string.Format("{0}-catch data : [{1}]", DateTime.Now.ToString("HH:mm:ss"), data));
                lbxLog.SelectedIndex = lbxLog.Items.Count - 1;
            }

            try
            {
                Clipboard.SetText(_CurrentRemoveData);
            }
            catch (ExternalException ex)
            {
                if (lbxLog.InvokeRequired)
                    lbxLog.Invoke(new MethodInvoker(() => {
                        lbxLog.Items.Add(string.Format("{0}-Error : [{1}]", DateTime.Now.ToString("HH:mm:ss"), ex.Message));
                        lbxLog.SelectedIndex = lbxLog.Items.Count - 1;
                    }));
                else
                {
                    lbxLog.Items.Add(string.Format("{0}-Error : [{1}]", DateTime.Now.ToString("HH:mm:ss"), ex.Message));
                    lbxLog.SelectedIndex = lbxLog.Items.Count - 1;
                }
            }
        }

        string RemoveText(string text, string[] oldvalues)
        {
            foreach(string val in oldvalues)
            {
                text = text.Replace(val, string.Empty);
            }
            return text;
        }
    }
}