using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseWorkTetris
{
    class StandartShape : Shape
    {
        // варинаты тела фигур
        public int[,] figure1 = new int[2, 2]
        {
             { 1, 1 },
             { 1, 1 }
        };

        public int[,] figure2 = new int[3, 3]
        {
             { 1, 0, 0 },
             { 1, 1, 1 },
             { 0, 0, 0 }
        };

        public int[,] figure3 = new int[3, 3]
        {
             { 0, 1, 0 },
             { 1, 1, 1 },
             { 0, 0, 0 }
        };

        public int[,] figure4 = new int[3, 3]
         {
             { 0, 1, 1 },
             { 1, 1, 0 },
             { 0, 0, 0 }
         };

        public int[,] figure5 = new int[4, 4]
        {
            { 1, 0, 0, 0 },
            { 1, 0, 0, 0 },
            { 1, 0, 0, 0 },
            { 1, 0, 0, 0 }
        };

        public StandartShape(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.body = GenetateBody();
        }

        public void ResetHape(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // метод генерации тела фигуры
        public int[,] GenetateBody()
        {
            int[,] matrix = body;
            switch (new Random().Next(1, 6))
            {
                case 1:
                    matrix = figure1;
                    break;
                case 2:
                    matrix = figure2;
                    break;
                case 3:
                    matrix = figure3;
                    break;
                case 4:
                    matrix = figure4;
                    break;
                case 5:
                    matrix = figure5;
                    break;
            }
            return matrix;
        }

        // попытка вертеть фигуру вертит фигуру только в 2 -е позиций
        public void Rotate()
        {
            int[,] tempMatrix = new int[body.GetLength(0), body.GetLength(1)];
            for (int i = 0; i < body.GetLength(0); i++)
            {
                for (int j = 0; j < body.GetLength(0); j++)
                {
                    tempMatrix[i, j] = body[j, (body.GetLength(0) - 1) - i];
                }
            }
            body = tempMatrix;
            int offset1 = (8 - (x + body.GetLength(0)));
            if (offset1 < 0)
            {
                for (int i = 0; i < Math.Abs(offset1); i++)
                    Leght();
            }

            if (x < 0)
            {
                for (int i = 0; i < Math.Abs(x) + 1; i++)
                    Right();
            }
        }
    }
}
