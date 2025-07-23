using TicTacToe.Api.Models;

namespace TicTacToe.Api.Service;

public class GameLogic
{
    public bool CheckWin(List<List<Player?>> board, Player player, int winLength)
    {
        int size = board.Count;
        // Проверка по строкам
        for (int i = 0; i < size; i++)
        {
            int count = 0;
            for (int j = 0; j < size; j++)
            {
                if (board[i][j] == player)
                {
                    count++;
                    if (count == winLength) return true;
                }
                else
                {
                    count = 0;
                }
            }
        }
        // Проверка по столбцам
        for (int j = 0; j < size; j++)
        {
            int count = 0;
            for (int i = 0; i < size; i++)
            {
                if (board[i][j] == player)
                {
                    count++;
                    if (count == winLength) return true;
                }
                else
                {
                    count = 0;
                }
            }
        }
        // Проверка по диагоналям
        for (int i = 0; i <= size - winLength; i++)
        {
            for (int j = 0; j <= size - winLength; j++)
            {
                int count = 0;
                for (int k = 0; k < winLength; k++)
                {
                    if (board[i + k][j + k] == player)
                        count++;
                    else
                        break;
                }
                if (count == winLength) return true;
            }
        }
        // Проверка по обратным диагоналям
        for (int i = 0; i <= size - winLength; i++)
        {
            for (int j = winLength - 1; j < size; j++)
            {
                int count = 0;
                for (int k = 0; k < winLength; k++)
                {
                    if (board[i + k][j - k] == player)
                        count++;
                    else
                        break;
                }
                if (count == winLength) return true;
            }
        }
        return false;
    }
}
