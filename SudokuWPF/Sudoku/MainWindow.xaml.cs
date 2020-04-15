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
        String EDITABLE = "Editable";


        public MainWindow()
        {
            viewmodel = Program.Setup();
            InitializeComponent();

            SelectValue.ItemsSource = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            this.DataContext = viewmodel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // If there is a previous button then set it's background to normal
            if(btn_clicked != null)
            {
                btn_clicked.Background = Brushes.AliceBlue;
            }

            Button btn_current = (Button)e.Source;

            // Only if editable space then perform the following actions
            // Either blank or Tag set to EDITABLE on first update
            if (btn_current.Content.ToString() == " " || (btn_current.Tag != null && btn_current.Tag.ToString() == EDITABLE))
            {
                SelectValue.Visibility = Visibility.Visible;
                SelectValue.SelectedItem = null;
                btn_clicked = btn_current;

                // Highlight selected item
                btn_clicked.Background = Brushes.Coral;
            }
            else
            {
                // Hide Combo box if non editable button clicked
                SelectValue.Visibility = Visibility.Hidden;
            }
        }

        private void SelectValue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(btn_clicked != null)
            {
                btn_clicked.Content = SelectValue.SelectedItem.ToString();

                // Indicate as an editable button
                btn_clicked.Tag = EDITABLE;
                btn_clicked.BorderBrush = Brushes.Black;
                btn_clicked.BorderThickness = new Thickness { Bottom = 2, Top = 2, Left = 2, Right = 2 };

                // Set background to normal
                btn_clicked.Background = Brushes.AliceBlue;

                // Prevents using current click in future for another button
                btn_clicked = null;
            }

            SelectValue.Visibility = Visibility.Hidden;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            viewmodel.SavePuzzle();
        }

        private void Reveal_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Validate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
