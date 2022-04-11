using SolarPark.DrawSolarPark;
using SolarPark.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SolarPark
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            _viewModel = (InputViewModel)DataContext;

            InitializeComponent();
            Initiliaze_Or_Clear_Canvas();

        }

        InputViewModel _viewModel;
        public Path solarParkShape { get; set; }
        public Path restrictionParkShape { get; set; }

        private void Button_Click_Send_Data(object sender, RoutedEventArgs e)
        {
            _viewModel = (InputViewModel)DataContext;

            var panel = new Rectangle();
            panel.Width = _viewModel.PanelLength;
            panel.Height = _viewModel.PanelWidth;

            var listOfPanels = DrawPark.Draw_Panels_On_Park(SolarParkCanvas, solarParkShape, restrictionParkShape, panel, _viewModel.ColumnSpacing, _viewModel.RowSpacing, _viewModel.TiltAngle);

            TestLabel.Content = $"Generated panels: {listOfPanels.Count}";
        }

        private void Button_Click_Clear_Canvas(object sender, RoutedEventArgs e)
        {
            Initiliaze_Or_Clear_Canvas();
        }

        private void Initiliaze_Or_Clear_Canvas()
        {
            SolarParkCanvas.Children.Clear();
            var parkCoordinates = DrawPark.Return_Park_Coordinates();
            var restrictionCoordinates = DrawPark.Return_Restriction_Coordinates();

            solarParkShape = DrawPark.Create_Polygon_Park_Shape_Geometries(SolarParkCanvas, parkCoordinates, Brushes.Yellow);
            restrictionParkShape = DrawPark.Create_Polygon_Park_Shape_Geometries(SolarParkCanvas, restrictionCoordinates, Brushes.Red);

            TestLabel.Content = "Generated panels:";
        }

        private void LimitInputToNumbers(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TiltAngle_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tiltAngleInput = sender as TextBox;
            GenerateButton.IsEnabled = !string.IsNullOrEmpty(tiltAngleInput.Text) ? (int.Parse(tiltAngleInput.Text) <= 60) : false;
        }
    }
}
