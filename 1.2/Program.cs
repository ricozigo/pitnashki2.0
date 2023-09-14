using System;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();
        int[,] matrix = new int[4, 4];
        int[] numbers = new int[16];

        // Инициализируем массив числами от 1 до 15
        for (int i = 0; i < 15; i++)
        {
            numbers[i] = i + 1;
        }

        // Перемешиваем массив
        for (int i = 0; i < 14; i++)
        {
            int j = random.Next(i, 15);
            int temp = numbers[i];
            numbers[i] = numbers[j];
            numbers[j] = temp;
        }

        // Заполняем двумерный массив случайными неповторяющимися числами по вертикали и горизонтали
        int index = 0;
        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                matrix[row, col] = numbers[index];
                index++;
            }
        }

        // Заменяем нули на -1 для представления пустого места
        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                if (matrix[row, col] == 0)
                {
                    matrix[row, col] = -1;
                }
            }
        }

        // Выводим начальное состояние игрового поля
        PrintMatrix(matrix);

        int movesLeft = 1000000; // Количество доступных перемещений

        while (movesLeft > 0)
        {
            // Считываем ввод пользователя
            Console.WriteLine("что куда зачем (0 если слабак): ");
            int blockNumber = int.Parse(Console.ReadLine());

            if (blockNumber == 0)
            {
                int zeroRow = -1;
                int zeroCol = -1;

                // Находим позицию блока "ноль" (пустой ячейки) в матрице
                for (int row = 0; row < 4; row++)
                {
                    for (int col = 0; col < 4; col++)
                    {
                        if (matrix[row, col] == -1)
                        {
                            zeroRow = row;
                            zeroCol = col;
                            break;
                        }
                    }
                    if (zeroRow != -1)
                    {
                        break;
                    }
                }

                if (zeroRow == -1)
                {
                    Console.WriteLine("нет пустой ячейки");
                    continue; // Пропускаем итерацию цикла и продолжаем ввод
                }

                // Находим позицию блока, который нужно переместить
                int blockRow = zeroRow;
                int blockCol = zeroCol;

                // Перемещаем блок "ноль" на пустую ячейку
                matrix[zeroRow, zeroCol] = matrix[blockRow, blockCol];
                matrix[blockRow, blockCol] = -1;
            }
            else
            {
                // Находим позицию блока в матрице
                int blockRow = -1;
                int blockCol = -1;

                for (int row = 0; row < 4; row++)
                {
                    for (int col = 0; col < 4; col++)
                    {
                        if (matrix[row, col] == blockNumber)
                        {
                            blockRow = row;
                            blockCol = col;
                            break;
                        }
                    }
                    if (blockRow != -1)
                    {
                        break;
                    }
                }

                if (blockRow == -1)
                {
                    Console.WriteLine("ты какой такой блок хочешь переместить");
                    continue; // Пропускаем итерацию цикла и продолжаем ввод
                }

                // Перемещаем блок на пустую ячейку (блок "ноль")
                int zeroRow = -1;
                int zeroCol = -1;

                // Находим позицию блока "ноль" (пустой ячейки) в матрице
                for (int row = 0; row < 4; row++)
                {
                    for (int col = 0; col < 4; col++)
                    {
                        if (matrix[row, col] == -1)
                        {
                            zeroRow = row;
                            zeroCol = col;
                            break;
                        }
                    }
                    if (zeroRow != -1)
                    {
                        break;
                    }
                }

                if (zeroRow == -1)
                {
                    Console.WriteLine("нет пустой ячейки");
                    continue; // Пропускаем итерацию цикла и продолжаем ввод
                }

                // Перемещаем блок на пустую ячейку
                matrix[zeroRow, zeroCol] = blockNumber;
                matrix[blockRow, blockCol] = -1;
            }

            movesLeft--;

            // Выводим обновленное состояние игрового поля
            PrintMatrix(matrix);

            // Проверяем на выигрыш
            if (CheckWinCondition(matrix))
            {
                Console.WriteLine("Поздравляем! Ты выиграли!");
                break;
            }
        }

        Console.WriteLine("ну лох чё");
    }

    static void PrintMatrix(int[,] matrix)
    {
        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                if (matrix[row, col] == -1)
                {
                    Console.Write("0\t");
                }
                else
                {
                    Console.Write(matrix[row, col] + "\t");
                }
            }
            Console.WriteLine();
        }
    }

    static bool CheckWinCondition(int[,] matrix)
    {
        int expectedValue = 1;
        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                if (matrix[row, col] != -1)
                {
                    if (matrix[row, col] != expectedValue)
                    {
                        return false; // Не все числа идут по порядку
                    }
                    expectedValue++;
                }
            }
        }
        return true; // Все числа идут по порядку
    }
}
