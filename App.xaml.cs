namespace RCWA_Sweep_Tool
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new MainPage()) 
            { 
                Title = "RCWA_Sweep_Tool",
                Width = 1080,
                Height = 1100
            };
        }
    }
}
