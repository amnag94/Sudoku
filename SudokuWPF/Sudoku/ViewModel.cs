using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace Sudoku
{
    class ViewModel : INotifyPropertyChanged
    {
        int size_puzzle;

        public ObservableCollection<ObservableCollection<Cell>> puzzle;

        public ObservableCollection<ObservableCollection<Cell>> solution;

        // Maintain originally displayed positions
        public bool[][] displayedPositions;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Constructor that initializes the puzzle size and puzzle collection.
        /// </summary>
        /// <param name="size_puzzle">
        ///     Size of puzzle to display
        /// </param>
        public ViewModel(int size_puzzle)
        {
            this.size_puzzle = size_puzzle;

            // Initialize the puzzle with all blanks 
            // so that we only modify it later to 
            // make binding work
            this.puzzle = new ObservableCollection<ObservableCollection<Cell>>();

            for (int row = 0; row < size_puzzle; row++)
            {
                this.puzzle.Add(new ObservableCollection<Cell>());
                for (int column = 0; column < size_puzzle; column++)
                {
                    this.puzzle[row].Add(new Cell { Digit = "0" });
                }
            }

            this.displayedPositions = new bool[size_puzzle][];
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
            // Modify puzzle using generated puzzle

            for (int row = 0; row < size_puzzle; row++)
            {
                this.displayedPositions[row] = new bool[size_puzzle];
                for(int column = 0; column < size_puzzle; column++)
                {
                    this.puzzle[row][column].Digit = model.puzzle[row][column].Digit;
                    this.puzzle[row][column].Color = model.puzzle[row][column].Color;

                    // Displayed in original puzzle or not
                    this.displayedPositions[row][column] = this.puzzle[row][column].digit > 0;
                }
            }
        }

        private void StoreSolution(SudokuModel model)
        {
            this.solution = new ObservableCollection<ObservableCollection<Cell>>();

            for (int row = 0; row < size_puzzle; row++)
            {
                this.solution.Add(new ObservableCollection<Cell>());
                for (int column = 0; column < size_puzzle; column++)
                {
                    this.solution[row].Add(new Cell { Digit = model.puzzle[row][column].Digit });
                }
            }
        }

        /// <summary>
        ///     Modify the value in puzzle using solution to 
        ///     reveal the answer for that item using 2 way binding.
        ///     We have set Digit of item to be revealed as -1
        ///     using binding from view to source.
        /// </summary>
        public void Reveal()
        {
            for(int row = 0; row < size_puzzle; row++)
            {
                for(int column = 0; column < size_puzzle; column++)
                {
                    if(Puzzle[row][column].Digit == "-1")
                    {
                        puzzle[row][column].Digit = solution[row][column].Digit;
                    }
                }
            }
        }

        /// <summary>
        ///     Validates the currently solved puzzle stored in
        ///     Puzzle with binding from view to source by comparing
        ///     with solution.
        /// </summary>
        /// <returns>
        ///     True if validated as correctly solved else, False.
        /// </returns>
        public bool Validate()
        {
            bool flag = true;

            for(int row = 0; row < size_puzzle; row++)
            {
                for(int column = 0; column < size_puzzle; column++)
                {
                    // Check only for originally blank values
                    if (!displayedPositions[row][column])
                    {
                        if (Puzzle[row][column].Digit != solution[row][column].Digit)
                        {
                            flag = false;
                            Puzzle[row][column].Color = "Red";
                        }
                        else
                        {
                            Puzzle[row][column].Color = "Green";
                        }
                    }
                }
            }

            return flag;
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

            // Store solution
            StoreSolution(model);

            //Solution
            //DisplayPuzzle(model);

            //Console.WriteLine();

            // Puzzle
            model.CalculatePuzzleDisplay();

            StorePuzzle(model);

            //DisplayPuzzle(model);
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

            for (int row = 0; row < this.puzzle.Count; row++)
            {
                for (int column = 0; column < this.puzzle.Count; column++)
                {
                    string value = this.puzzle[row][column].Digit;

                    if (!this.displayedPositions[row][column])
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
            /*for(int row = 0; row < size_puzzle; row++)
            {
                for(int column = 0; column < size_puzzle; column++)
                {
                    Console.Write(Puzzle[row][column] + " ");
                }
                Console.WriteLine();
            }*/
            
        }

        public ObservableCollection<ObservableCollection<Cell>> Puzzle
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
