using System.Runtime.InteropServices;

namespace ClipboardEnterRemover
{
    public partial class MainForm : Form
    {
        static readonly string[] _RemoveTaget = {"\r","\n","-\r","-\n"};
        
        /// <summary>
        /// 제거기 자신이 관리하는 클립보드 저장 데이터
        /// </summary>
        struct ClipboardData
        {
            /// <summary>
            /// 원본 클립보드 데이터
            /// </summary>
            public static string RawData = string.Empty;
            /// <summary>
            /// 엔터가 제거된 클립보드 데이터
            /// </summary>
            public static string EnterRemoveData = string.Empty;
        }
        /// <summary>
        /// 버튼텍스트
        /// </summary>
        struct ButtonName
        {
            public const string StartText = "중지";
            public const string StopText = "시작하기";
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
                if(MessageBox.Show("클립보드 엔터 제거기를 종료하시겠습니까?","close", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

            //클립보드 엔터제거기 실행 상태 디스플레이용 쓰레드
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

            //프로그레스바 설정
            prbStatus.Style = ProgressBarStyle.Marquee;
            prbStatus.MarqueeAnimationSpeed = 60;
            prbStatus.Hide();

            //최소화 시 보이지 않게 처리.
            this.Resize += (o, i) =>
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.Hide();
                }
            };
        }

        // "X" 버튼 비활성화
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
        /// 클립보드의 DATA를 엔터 값을 제거한 값으로 대체시킨다.
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
        /// 엔터값 등 제거할 값을 제거하고 반환한다.
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