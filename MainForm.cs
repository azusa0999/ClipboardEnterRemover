using System.Runtime.InteropServices;

namespace ClipboardEnterRemover
{
    public partial class MainForm : Form
    {
        public static progressBarVisible? MainProgressBarVisible;
        public static listLogging? ListLogging;
        /// <summary>
        /// 버튼텍스트
        /// </summary>
        struct ButtonName
        {
            public const string StartText = "중지";
            public const string StopText = "시작하기";
        }

        readonly NotifyIcon _notifyIcon = new();

        public MainForm()
        {
            InitializeComponent();

            ListLogging = (title, content) => 
            {
                // 리스트 박스에 로그를 표시한다.
                lbxLog.Invoke(() => 
                {
                    lbxLog.Items.Add(string.Format("{0}-{1} : [{2}]", DateTime.Now.ToString("HH:mm:ss"), title, content));
                    if (lbxLog.Items.Count > 30)
                        lbxLog.Items.RemoveAt(0);
                    lbxLog.SelectedIndex = lbxLog.Items.Count - 1;
                });
            };
            MainProgressBarVisible = (visible) =>
            {
                // 프로그레스바 진행상태 표시 여부
                prbStatus.Invoke(() => 
                {
                    if (visible)
                        prbStatus.Show();
                    else
                        prbStatus.Hide();
                });
            };
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = Program.ProgramTitle;

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

            btnStart.Click += (o, i) =>
            {
                Program.BackgrndRun = !Program.BackgrndRun;
                if (Program.BackgrndRun)
                {
                    btnStart.Text = ButtonName.StartText;
                }
                else
                {
                    btnStart.Text = ButtonName.StopText;
                }
            };

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

            this.exitToolStripMenuItem.Click += (o, i) =>
            {
                if (MessageBox.Show("클립보드 엔터 제거기를 종료하시겠습니까?", "close", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ClipboardMonitor.Stop();
                    Program.MainFormClosed = true;
                    this.Close();
                }
            };

            //Program.BacgroundStart();
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
    }
}