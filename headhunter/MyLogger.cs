namespace headhunter
{
    public class MyLogger
    {
        private static MyLogger _instance;
        public ILogger Logger { get; private set; }

        private MyLogger() { }

        public static MyLogger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MyLogger
                    {
                        Logger = new ServiceCollection()
                            .AddLogging(builder => builder.AddConsole())
                            .BuildServiceProvider()
                            .GetService<ILogger<MyLogger>>()
                    };
                }
                return _instance;
            }
        }
    }
}
