namespace ClipboardEnterRemover
{
    internal static class Program
    {
        public static Background back = new();
        /// <summary>
        /// ���ű� �ڽ��� �����ϴ� Ŭ������ ���� ������
        /// </summary>
        public struct ClipboardData
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
        /// ���� ���� ����
        /// </summary>
        public static bool MainFormClosed = false;
        /// <summary>
        /// ��׶��� ������� ����
        /// </summary>
        public static bool BackgrndRun { get; set; }
        /// <summary>
        /// ���α׷��� Ÿ��Ʋ��
        /// </summary>
        public static string ProgramTitle
        {
            get
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
                return string.Format("{0} [v{1}]", "Ŭ������ ���� ���ű�", fvi.FileVersion);
            }
        }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}