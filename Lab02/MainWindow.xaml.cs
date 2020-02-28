using Lab02.ViewModel;

namespace Lab02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new Lab02ViewModel();
        }
    }
}