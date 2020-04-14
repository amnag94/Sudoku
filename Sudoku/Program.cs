using System;

namespace Sudoku
{ 

    class Program
    {
        const string HARD_STRING = "hard";
        const string MEDIUM_STRING = "medium";
        const string EASY_STRING = "easy";

        const double HARD = 0.75;
        const double MEDIUM = 0.55;
        const double EASY = 0.40;

        static void Main(string[] args)
        {
            int size_puzzle = Int32.Parse(args[0]);
            string hardness = args[1];

            double difficulty;
            switch(hardness)
            {
                case HARD_STRING:
                    difficulty = HARD;
                    break;
                case MEDIUM_STRING:
                    difficulty = MEDIUM;
                    break;
                case EASY_STRING:
                    difficulty = EASY;
                    break;
                default:
                    difficulty = MEDIUM;
                    break;
            }

            ViewModel viewModel = new ViewModel(size_puzzle);

            viewModel.ShowSudoku((1 - difficulty));

        }
    }
}
