namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }

    class Game
    {
        private char[,] board;
        private char currentPlayer;
        private Random random;

        public Game()
        {
            board = new char[3, 3];
            random = new Random();
        }

        public void Start()
        {
            InitializeBoard();
            currentPlayer = random.Next(0, 2) == 0 ? 'X' : 'O';
            Console.WriteLine($"Player {currentPlayer} starts the game.");

            while (true)
            {
                DisplayBoard();
                if (currentPlayer == 'X')
                {
                    PlayerMove();
                }
                else
                {
                    ComputerMove();
                }

                if (CheckWin())
                {
                    DisplayBoard();
                    Console.WriteLine($"Player {currentPlayer} wins!");
                    break;
                }

                if (CheckDraw())
                {
                    DisplayBoard();
                    Console.WriteLine("The game is a draw!");
                    break;
                }

                SwitchPlayer();
            }
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = ' ';
                }
            }
        }

        private void DisplayBoard()
        {
            Console.Clear();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(board[i, j]);
                    if (j < 2) Console.Write("|");
                }
                Console.WriteLine();
                if (i < 2) Console.WriteLine("-----");
            }
        }

        private void PlayerMove()
        {
            int row, col;
            while (true)
            {
                Console.WriteLine("Enter your move (row and column from 1 to 3): ");
                string input = Console.ReadLine();
                string[] parts = input.Split();

                if (parts.Length == 2 &&
                    int.TryParse(parts[0], out row) &&
                    int.TryParse(parts[1], out col) &&
                    row >= 1 && row <= 3 &&
                    col >= 1 && col <= 3 &&
                    board[row - 1, col - 1] == ' ')
                {
                    board[row - 1, col - 1] = currentPlayer;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid move. Make sure to enter two numbers between 1 and 3 for an empty cell.");
                }
            }
        }

        private void ComputerMove()
        {
            int row, col;
            while (true)
            {
                row = random.Next(0, 3);
                col = random.Next(0, 3);

                if (board[row, col] == ' ')
                {
                    board[row, col] = currentPlayer;
                    break;
                }
            }
            Console.WriteLine($"Computer placed {currentPlayer} at ({row + 1}, {col + 1})");
        }

        private void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
        }

        private bool CheckWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer) return true;
                if (board[0, i] == currentPlayer && board[1, i] == currentPlayer && board[2, i] == currentPlayer) return true;
            }

            if (board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer) return true;
            if (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer) return true;

            return false;
        }

        private bool CheckDraw()
        {
            foreach (char spot in board)
            {
                if (spot == ' ') return false;
            }
            return !CheckWin();
        }
    }
}
