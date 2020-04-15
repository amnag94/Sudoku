using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace Sudoku
{
    class ViewModel : INotifyPropertyChanged
    {
        int size_puzzle;

        public string[][] puzzle;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Constructor that initializes the puzzle size.
        /// </summary>
        /// <param name="size_puzzle">
        ///     Size of puzzle to display
        /// </param>
        public ViewModel(int size_puzzle)
        {
            this.size_puzzle = size_puzzle;
            puzzle = new string[size_puzzle][];
        }

        private int[] GetAllValues()
        {
            int[] available_values = new int[this.size_puzzle];

            for (int number = 1; number <= size_puzzle; number++)
            {
                available_values[number - 1] = number;
            }

            return available_values;
        }

        private void StorePuzzle(SudokuModel model)
        {
            for(int row = 0; row < size_puzzle; row++)
            {
                this.puzzle[row] = new string[size_puzzle];
                for(int column = 0; column < size_puzzle; column++)
                {
                    this.puzzle[row][column] = model.puzzle[row][column].Digit;
                }
            }
        }

        private SudokuModel SetUpPuzzle(double difficulty)
        {
            SudokuModel model = SudokuModel.GetInstance(size_puzzle, difficulty);

            int[] available_values = GetAllValues();
            model.FormSolution(0, 0, available_values);

            return model;
        }

        private void ShowDashes(int dash_count)
        {
            for (int dash = 1; dash <= dash_count; dash++)
            {
                Console.Write("- ");
            }
        }

        private void DisplayDivision()
        {
            Console.Write("+ ");
            for (int box_index = 1; box_index <= (int)Math.Sqrt(size_puzzle); box_index++)
            {
                ShowDashes((int)Math.Sqrt(size_puzzle));
                Console.Write("+ ");
            }
        }

        /// <summary>
        ///     Display a 2-d matrix displaying puzzle only at places
        ///     mentioned in positions and a X at other places as well
        ///     as the solution for it.
        /// </summary>
        /// <param name="difficulty">
        ///     Fraction of puzzle to be shown
        /// </param>
        public void ShowSudoku(double difficulty)
        {
            SudokuModel model = SetUpPuzzle(difficulty);

            //Solution
            DisplayPuzzle(model);

            Console.WriteLine();

            // Puzzle
            model.CalculatePuzzleDisplay();

            StorePuzzle(model);

            DisplayPuzzle(model);
        }

        private void DisplayPuzzle(SudokuModel model)
        {
            var box_size = (int)Math.Sqrt(size_puzzle);

            for (int row = 0; row < size_puzzle; row++)
            {
                if (row % box_size == 0)
                {
                    DisplayDivision();
                    Console.WriteLine();
                }

                for (int column = 0; column < size_puzzle; column++)
                {
                    if (column % box_size == 0)
                    {
                        Console.Write("| ");
                    }

                    string value = model.puzzle[row][column].digit + " ";
                    if(model.puzzle[row][column].digit == 0)
                    {
                        value = "X ";
                    }

                    Console.Write(value);
                }

                Console.Write("| ");
                Console.WriteLine();

            }

            DisplayDivision();
            Console.WriteLine();

        }

        public void SavePuzzle()
        {
            string[] linesToWrite = new string[size_puzzle];
            string saved_puzzle = "";

            for (int row = 0; row < size_puzzle; row++)
            {
                for (int column = 0; column < size_puzzle; column++)
                {
                    string value = this.puzzle[row][column];

                    if (value == " ")
                    {
                        saved_puzzle += "X ";
                    }
                    else
                    {
                        saved_puzzle += value + " ";
                    }
                }

                // Avoid extra space added
                linesToWrite[row] = saved_puzzle;
                saved_puzzle = "";
            }

            File.WriteAllLines(@"../Saved_Puzzle.txt", linesToWrite);
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            for(int row = 0; row < size_puzzle; row++)
            {
                for(int column = 0; column < size_puzzle; column++)
                {
                    Console.Write(Puzzle[row][column] + " ");
                }
                Console.WriteLine();
            }
            
        }

        public string[][] Puzzle
        {
            get
            {
                return puzzle;
            }
            set
            {
                if (value != puzzle)
                {
                    puzzle = value;
                    NotifyPropertyChanged();
                }
            }
        }

    }
}
