using System.Runtime.InteropServices;

namespace ClipboardEnterRemover
{
    public partial class MainForm : Form
    {
        public static progressBarVisible? MainProgressBarVisible;
        public static listLogging? ListLogging;
        /// <summary>
        /// ��ư�ؽ�Ʈ
        /// </summary>
        struct ButtonName
        {
            public const string StartText = "����";
            public const string StopText = "�����ϱ�";
        }

        readonly NotifyIcon _notifyIcon = new();

        public MainForm()
        {
            InitializeComponent();

            ListLogging = (title, content) => 
            {
                // ����Ʈ �ڽ��� �α׸� ǥ���Ѵ�.
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
                // ���α׷����� ������� ǥ�� ����
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

            this.exitToolStripMenuItem.Click += (o, i) =>
            {
                if (MessageBox.Show("Ŭ������ ���� ���ű⸦ �����Ͻðڽ��ϱ�?", "close", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ClipboardMonitor.Stop();
                    Program.MainFormClosed = true;
                    this.Close();
                }
            };

            //Program.BacgroundStart();
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
    }
}