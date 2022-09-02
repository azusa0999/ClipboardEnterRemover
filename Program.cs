namespace ClipboardEnterRemover
{
    internal static class Program
    {
        public static DelegateFunction deFunction = new DelegateFunction();
        public static MainForm main = new MainForm();
        public static Background back = new Background();
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
        public static void BacgroundStart()
        {
            back.Start();
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
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(main);
        }
    }
}