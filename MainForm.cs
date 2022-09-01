using System.Runtime.InteropServices;

namespace ClipboardEnterRemover
{
    public partial class MainForm : Form
    {
        static readonly string[] _RemoveTaget = {"\r","\n","-\r","-\n"};
        
        /// <summary>
        /// ���ű� �ڽ��� �����ϴ� Ŭ������ ���� ������
        /// </summary>
        struct ClipboardData
        {
            /// <summary>
            /// ���� Ŭ������ ������
            /// </summary>
            public static string RawData = string.Empty;
            /// <summary>
            /// ���Ͱ� ���ŵ� Ŭ������ ������
            /// </summary>
            public static string EnterRemoveData = string.Empty;
        }
        /// <summary>
        /// ��ư�ؽ�Ʈ
        /// </summary>
        struct ButtonName
        {
            public const string StartText = "����";
            public const string StopText = "�����ϱ�";
        }

        System.Threading.Thread? _t;
        bool _isStart = false;
        bool _thisFormClosed = false;

        NotifyIcon _notifyIcon = new NotifyIcon();
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _notifyIcon.Icon = this.Icon;
            _notifyIcon.Visible = true;
            _notifyIcon.Text = this.Text;
            _notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            _notifyIcon.DoubleClick += (o, i) => {
                this.ShowInTaskbar = true;
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
            };
            this.showToolStripMenuItem.Click += (o, i) => 
            {
                this.ShowInTaskbar = true;
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
            };
            this.exitToolStripMenuItem.Click += (o, i) =>
            {
                if(MessageBox.Show("Ŭ������ ���� ���ű⸦ �����Ͻðڽ��ϱ�?","close", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ClipboardMonitor.Stop();
                    _thisFormClosed = true;
                    this.Close();
                }
            };

            btnStart.Click += (o, i) =>
            {
                _isStart = !_isStart;
                if (_isStart)
                {
                    btnStart.Text = ButtonName.StartText;
                }
                else
                {
                    btnStart.Text = ButtonName.StopText;
                }
            };

            //Ŭ������ �������ű� ���� ���� ���÷��̿� ������
            _t = new Thread(new ThreadStart(() => {
                while (true)
                {
                    if (_thisFormClosed)
                        return;

                    if (_isStart)
                    {
                        if (ClipboardMonitor.MonitorStatus == ClipboardMonitor.Status.Stop)
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
                        if (ClipboardMonitor.MonitorStatus == ClipboardMonitor.Status.Start)
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
            _t.IsBackground = true;
            _t.Start();

            //���α׷����� ����
            prbStatus.Style = ProgressBarStyle.Marquee;
            prbStatus.MarqueeAnimationSpeed = 60;
            prbStatus.Hide();

            //�ּ�ȭ �� ������ �ʰ� ó��.
            this.Resize += (o, i) =>
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.Hide();
                }
            };
        }

        // "X" ��ư ��Ȱ��ȭ
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        /// <summary>
        /// Ŭ�������� DATA�� ���� ���� ������ ������ ��ü��Ų��.
        /// </summary>
        /// <param name="data"></param>
        void SetClipboard(string data)
        {
            if (string.IsNullOrEmpty(data))
                return;
            if (ClipboardData.RawData == data)
                return;

            ClipboardData.RawData = data;
            ClipboardData.EnterRemoveData = RemoveText(data, _RemoveTaget);
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
                Clipboard.SetText(ClipboardData.EnterRemoveData);
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

        /// <summary>
        /// ���Ͱ� �� ������ ���� �����ϰ� ��ȯ�Ѵ�.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="oldvalues"></param>
        /// <returns></returns>
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