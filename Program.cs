namespace ClipboardEnterRemover
{
    internal static class Program
    {
        public static DelegateFunction deFunction = new DelegateFunction();
        public static MainForm main = new MainForm();
        public static Background back = new Background();
        /// <summary>
        /// 제거기 자신이 관리하는 클립보드 저장 데이터
        /// </summary>
        public struct ClipboardData
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
        public static void BacgroundStart()
        {
            back.Start();
        }
        /// <summary>
        /// 메인 종료 여부
        /// </summary>
        public static bool MainFormClosed = false;
        /// <summary>
        /// 백그라운드 실행시작 여부
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