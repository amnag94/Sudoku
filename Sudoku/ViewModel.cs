using System;

namespace Sudoku
{
    class ViewModel
    {
        int size_puzzle;

        /// <summary>
        ///     Constructor that initializes the puzzle size.
        /// </summary>
        /// <param name="size_puzzle">
        ///     Size of puzzle to display
        /// </param>
        public ViewModel(int size_puzzle)
        {
            this.size_puzzle = size_puzzle;
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

    }
}
