namespace ClipboardEnterRemover
{
    internal static class Program
    {
        public static Background back = new();
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

        /// <summary>
        /// 메인 종료 여부
        /// </summary>
        public static bool MainFormClosed = false;
        /// <summary>
        /// 백그라운드 실행시작 여부
        /// </summary>
        public static bool BackgrndRun { get; set; }
        /// <summary>
        /// 프로그램의 타이틀명
        /// </summary>
        public static string ProgramTitle
        {
            get
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
                return string.Format("{0} [v{1}]", "클립보드 엔터 제거기", fvi.FileVersion);
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