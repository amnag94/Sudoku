using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Sudoku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Cell> SudokuDisplay { get; set; }
        ViewModel viewmodel;
        Button btn_clicked;


        public MainWindow()
        {
            viewmodel = Program.Setup();
            InitializeComponent();

            SudokuDisplay = new List<Cell>();

            /*for(int row = 0; row < viewmodel.Puzzle.Length; row++)
            {
                for(int column = 0; column < viewmodel.Puzzle[0].Length; column++)
                {
                    SudokuDisplay.Add(viewmodel.Puzzle[row][column]);
                }
            }*/

            SelectValue.ItemsSource = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            this.DataContext = viewmodel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SelectValue.Visibility = Visibility.Visible;
            btn_clicked = (Button)e.Source;
        }

        private void SelectValue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(btn_clicked != null)
            {
                btn_clicked.Content = SelectValue.SelectedItem.ToString();
            }

            SelectValue.Visibility = Visibility.Hidden;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Reveal_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Validate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
