using System.Windows;
using Lab02.Tools.DataStorage;
using Lab02.Tools.Managers;
using Lab02.ViewModel;

namespace Lab02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StationManager.Initialize(new SerializedDataStorage());
            DataContext = new PersonsList();
        }
    }
}