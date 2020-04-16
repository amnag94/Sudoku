using System;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections;

namespace Sudoku
{
    class Cell : INotifyPropertyChanged
    {
        public int digit = 0;
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string color = "Black";

        /// <summary>
        ///     Property to bind to color
        ///     of text for each item
        /// </summary>
        public string Color
        {
            get
            {
                return color;
            }
            set
            {
                if(value != color)
                {
                    color = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        ///     Property for digit to enable
        ///     binding to each grid value.
        /// </summary>
        public string Digit
        {
            get
            {
                return digit == 0 ? " " : digit + "";
            }
            set
            {
                int result;
                if (Int32.TryParse(value, out result) && result != digit)
                {
                    digit = result;
                    NotifyPropertyChanged();
                }
            }
        }
    }

    class SudokuModel : INotifyPropertyChanged, IEnumerable<bool>
    {
        int size_puzzle;
        double difficulty;
        public Cell[][] puzzle;
        private bool[][] positions;
        static SudokuModel model_instance;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Model constructor to initialize the puzzle
        ///     size and difficulty.
        /// </summary>
        /// <param name="size_puzzle">
        ///     Size of puzzle to be generated
        /// </param>
        /// <param name="difficulty">
        ///     Amount of puzzle to be shown
        ///     0.6 : Easy, 0.45 : Medium, 0.25 : Hard
        /// </param>
        private SudokuModel(int size_puzzle, double difficulty)
        {
            InitializeModel(size_puzzle, difficulty, this);
        }

        private static void InitializeModel(int size_puzzle, double difficulty, SudokuModel model)
        {
            model.size_puzzle = size_puzzle;
            model.difficulty = difficulty;

            model.puzzle = new Cell[size_puzzle][];
            model.positions = new bool[size_puzzle][];

            // To avoid NullReferenceException
            for (int puzzle_row = 0; puzzle_row < size_puzzle; puzzle_row++)
            {
                model.puzzle[puzzle_row] = new Cell[size_puzzle];
                model.positions[puzzle_row] = new bool[size_puzzle];

                for (int puzzle_column = 0; puzzle_column < size_puzzle; puzzle_column++)
                {
                    model.puzzle[puzzle_row][puzzle_column] = new Cell();
                }
            }
        }

        /// <summary>
        ///     Generate and return the instance
        ///     of the SudokuModel that will enable
        ///     a Singleton Design Pattern. 
        /// </summary>
        /// <param name="size_puzzle">
        ///     Number of rows and columns for puzzle
        /// </param>
        /// <param name="difficulty">
        ///     Amount of puzzle to be shown
        ///     0.6 : Easy, 0.45 : Medium, 0.25 : Hard
        /// </param>
        /// <returns>
        ///     Single instance of Sudoku Model that
        ///     exists.
        /// </returns>
        public static SudokuModel GetInstance(int size_puzzle, double difficulty)
        {
            if (!(model_instance is SudokuModel))
            {
                model_instance = new SudokuModel(size_puzzle, difficulty);
            }
            else
            {
                InitializeModel(size_puzzle, difficulty, model_instance);
            }

            return model_instance;
        }

        private bool ValidatePuzzle(int row, int column, int value)
        {

            // Check row
            if(puzzle[row].Count(x => x.digit == value) > 0)
            {
                return false;
            }

            // Check column
            for (int index = 0; index < size_puzzle; index++)
            {
                if (puzzle[index][column].digit == value)
                {
                    return false;
                }
            }

            // Check box
            var box_size = (int)Math.Sqrt(size_puzzle);

            var box_row_start = row - (row % box_size);
            var box_col_start = column - (column % box_size);

            for (int row_index = 0; row_index < box_size; row_index++)
            {
                for (int col_index = 0; col_index < box_size; col_index++)
                {
                    if (puzzle[box_row_start + row_index][box_col_start + col_index].digit == value)
                    {
                        return false;
                    }
                }
            }

            return true;

        }

        private void SetRandomStart(int[] available_values)
        {
            Random random = new Random();
            int start = random.Next(1, size_puzzle);

            var temp = available_values[start - 1];
            available_values[start - 1] = available_values[0];
            available_values[0] = temp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="available_values"></param>
        /// <param name="used"></param>
        /// <returns></returns>
        public bool FormSolution(int row, int column, int[] available_values)
        {
            if (row >= size_puzzle)
            {
                return true;
            }

            // Pick random start to adding a value
            SetRandomStart(available_values);

            for (int value_index = 0; value_index < size_puzzle; value_index++)
            {

                var current_value = available_values[value_index];

                if (ValidatePuzzle(row, column, current_value)) 
                {
                    puzzle[row][column].digit = current_value;

                    int next_row = (column + 1) % size_puzzle == 0 ? row + 1 : row;
                    int next_column = (column + 1) % size_puzzle;

                    int[] available_array = available_values;
                    var success = FormSolution(next_row, next_column, available_values);

                    if (success)
                    {
                        return success;
                    }

                    available_values = available_array;
                    puzzle[row][column].digit = 0;

                }
            }

            return false;
        }

        private void FormPuzzle()
        {
            for(int row = 0; row < size_puzzle; row++)
            {
                for(int column = 0; column < size_puzzle; column++)
                {
                    if (!positions[row][column])
                    {
                        puzzle[row][column].digit = 0;
                        puzzle[row][column].color = "Blue";
                    }
                }
            }
        }

        /// <summary>
        /// Calculate an update value using difficulty
        /// and hence number of values to display and
        /// set values in positions.
        /// </summary>
        public void CalculatePuzzleDisplay()
        {
            int num_to_display = (int)(Math.Round(size_puzzle * size_puzzle * difficulty));

            int update = (int)(Math.Round(size_puzzle * difficulty));

            // To avoid displaying only in one row
            if (update < Math.Sqrt(size_puzzle))
            {
                update *= 2;
            }

            int row = 0, column = 0;
            while (num_to_display > 0)
            {
                positions[row][column] = true;

                row = (column + update) >= size_puzzle ? row + 1 : row;
                column = (column + update) % size_puzzle;

                // Wrap around in case num_to_display left
                row = row >= size_puzzle ? 0 : row;

                num_to_display--;
            }

            FormPuzzle();
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Enumerator for positions array to
        ///     check which puzzle positions were
        ///     crosses on the grid.
        ///     Enables iterator design pattern.
        /// </summary>
        /// <returns>
        ///     Current value in the array positions
        /// </returns>
        public IEnumerator<bool> GetEnumerator()
        {
            for(int iterable_row = 0; iterable_row < size_puzzle; iterable_row++)
            {
                for(int iterable_column = 0; iterable_column < size_puzzle; iterable_column++)
                {
                    yield return positions[iterable_row][iterable_column];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Puzzle property to enable binding.
        /// </summary>
        public Cell[][] Puzzle {
            get
            {
                return puzzle;
            }
            set
            {
                if(value != puzzle)
                {
                    puzzle = value;
                    NotifyPropertyChanged();
                }
            }
        }

    }
}
