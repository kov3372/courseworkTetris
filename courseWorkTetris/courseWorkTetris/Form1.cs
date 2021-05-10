using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace courseWorkTetris
{
    public partial class Form1 : Form
    {
        // масив который представляет игровое поле
        private int[,] gameMape = new int[16, 8];

        // размер  одного квадратика квадратика
        private int sizeOfPixel = 35;

        // количество убраных линий
        int removedLine = 0;

        // очки
        int score = 0;

        // обычный интервал падения
        int normalInterval = 1000;

        // ускореный интервал падения
        int fastInterval = 500;

        // экземпляр стандартной фигуры
        StandartShape kvadrat = new StandartShape(3, 0);

        // экземпляр жидкой фигуры фигуры
        LiquidShape linqshape = new LiquidShape(3, 0);


        // 




        // рисуем сетку(работает)
        public void DrawGrid(Graphics graf)
        {
            for (int i = 0; i <= gameMape.GetLength(0); i++)
            {
                graf.DrawLine(new Pen(Color.Black), new Point(0, i * sizeOfPixel), new Point(280, i * sizeOfPixel));
            }
            for (int j = 0; j <= gameMape.GetLength(1); j++)
            {
                graf.DrawLine(new Pen(Color.Black), new Point(j * sizeOfPixel, 0), new Point(j * sizeOfPixel, 560));
            }
        }

        // рисуем фигурку(работает)
        public void Drawfigure(Graphics g)
        {
            //  Rectangle kvadrat = new Rectangle(x * sizeOfPixel, y * sizeOfPixel, sizeOfPixel, sizeOfPixel);
            for (int i = 0; i < gameMape.GetLength(0); i++)
            {
                for (int j = 0; j < gameMape.GetLength(1); j++)
                {
                    if (gameMape[i, j] == 1)
                    {
                        g.FillRectangle(Brushes.Green, j * sizeOfPixel + 1, i * sizeOfPixel + 1, sizeOfPixel - 1, sizeOfPixel - 1);
                    }                  
                }
            }
        }

        // метод синхронизацыи масива фигуры и масива игрового поля(работает)
        public void Sunchro()
        {
            for (int i = kvadrat.y; i < kvadrat.y + kvadrat.body.GetLength(0); i++)
            {
                for (int j = kvadrat.x; j < kvadrat.x + kvadrat.body.GetLength(1); j++)
                {
                    if (kvadrat.body[i - kvadrat.y, j - kvadrat.x] != 0)
                    {
                        gameMape[i, j] = kvadrat.body[i - kvadrat.y, j - kvadrat.x];
                    }
                }
            }
        }

        // очистка тереторий (работает)
        public void ResetArea()
        {
            for (int i = kvadrat.y; i < kvadrat.y + kvadrat.body.GetLength(1); i++)
            {
                for (int j = kvadrat.x; j < kvadrat.x + kvadrat.body.GetLength(0); j++)
                {
                    if (i >= 0 && j >= 0 && i < 16 && j < 8)
                    {
                        if (kvadrat.body[i - kvadrat.y, j - kvadrat.x] != 0)
                        {
                            gameMape[i, j] = 0;
                        }                          
                    }
                      
                }
            }
        }

        // проверка на то что не выходит ли фигура за правую гарницу карты или не лежит какая то фигура с права (работает )
        public bool CheckRightside()
        {
            for (int i = kvadrat.y; i < kvadrat.y + kvadrat.body.GetLength(1); i++)
            {
                for (int j = kvadrat.x; j < kvadrat.x + kvadrat.body.GetLength(0); j++)
                {
                    int g1 = i - kvadrat.y;
                    int g2 = j - kvadrat.x;

                    if (kvadrat.body[g1, g2] != 0)
                    {
                        if (j + 1 > 7 || j + 1 < 0)
                            return true;

                        if (gameMape[i, j + 1] != 0)
                        {
                          
                           if (g2 + 1 >= kvadrat.body.GetLength(0) || g2 + 1 < 0)
                               return true;

                          if (kvadrat.body[g1, g2 + 1] == 0)
                               return true;                                                            
                        }
                    }
                }
            }
            return false;
        }
  
       // метод для проверки возможна ли ротация (работате )
        public bool RoteshenIsPossible()
        {
            for (int i = kvadrat.y; i < kvadrat.y + kvadrat.body.GetLength(1); i++)
            {
                for (int j = kvadrat.x; j < kvadrat.x + kvadrat.body.GetLength(0); j++)
                {
                    

                    if(j >= 0 && j <= gameMape.GetLength(1)-1)
                    {
                        if(gameMape[i,j] != 0 && kvadrat.body[i- kvadrat.y, j- kvadrat.x] == 0)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
          return false;
        }

       // метод на проверку левой стороны ( работатет)
        public bool CheckLefttside()
        {
            for (int i = kvadrat.y; i < kvadrat.y + kvadrat.body.GetLength(1); i++)
            {
                for (int j = kvadrat.x; j < kvadrat.x + kvadrat.body.GetLength(0) ; j++)
                {
                    int g1 = i - kvadrat.y;
                    int g2 = j - kvadrat.x;

                    if (kvadrat.body[g1, g2] != 0)
                    {
                        if (j - 1 > 7 || j - 1 < 0)
                            return true;

                        if (gameMape[i, j - 1] != 0  )
                        {
                           if(g2 - 1 < 0 || g2 - 1 > kvadrat.body.GetLength(0))                         
                               return true;
                           

                            if (kvadrat.body[g1, g2 - 1] == 0)
                                return true;

                        }
                    }
                }
            }
            return false;
        }

        // вариант метода из видео проверки право и лево (работает)
        public bool CollideHor(int dir)
        {
            for (int i = kvadrat.y; i < kvadrat.y + kvadrat.body.GetLength(0); i++)
            {
                for (int j = kvadrat.x; j < kvadrat.x + kvadrat.body.GetLength(0); j++)
                {
                    if (kvadrat.body[i - kvadrat.y, j - kvadrat.x] != 0)
                    {
                        if (j + 1 * dir > 7 || j + 1 * dir < 0)
                            return true;

                        if (gameMape[i, j + 1 * dir] != 0)
                        {
                            if (j - kvadrat.x + 1 * dir >= kvadrat.body.GetLength(0) || j - kvadrat.x + 1 * dir < 0)
                            {
                                return true;
                            }
                            if (kvadrat.body[i - kvadrat.y, j - kvadrat.x + 1 * dir] == 0)
                                return true;
                        }
                    }
                }
            }
            return false;
        }

        // функция для проверки не выходим ли мы за границу карты или не лежит под фигурой какоя то другая фигурка (работает отлично)
        public bool ChecknextStep()
        {
            for (int i = kvadrat.y + kvadrat.body.GetLength(0) - 1; i >= kvadrat.y; i--)
            {
                for (int j = kvadrat.x; j < kvadrat.x + kvadrat.body.GetLength(0); j++)
                {
                    int g1 = i - kvadrat.y;
                    int g2 = j - kvadrat.x;

                    if (kvadrat.body[g1, g2] != 0)
                    {
                        if (i + 1 == 16)
                        {
                            return true;
                        }

                        if (gameMape[i + 1, j] != 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        // метод очистки карты( работает)
        public void ClearMap()
        {
            for (int i = 0; i < gameMape.GetLength(0); i++)
            {
                for (int j = 0; j < gameMape.GetLength(1); j++)
                {
                    gameMape[i, j] = 0;
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            label1.Text = "линий  " + removedLine;
            label2.Text = "очки  " + score;


            // реализация таймера 
            timer1.Interval = normalInterval;
            timer1.Tick += new EventHandler(update);
            timer1.Start();

            // реализация движение вправо и влево
            this.KeyUp += new KeyEventHandler(key);
        }



        // очистка заполненых рядов (работате)
        public void DeleteFilledRows()
        {                               
            for (int i = 0; i < gameMape.GetLength(0); i++)
            {
                int count = 0;
                for (int j = 0; j < gameMape.GetLength(1); j++)
                {
                   
                    if(gameMape[i,j] != 0)                    
                        count++;                   
                }
                if(count == 8)
                {
                    removedLine++;
                    for (int g = i; g >= 1; g--)
                    {
                        for (int e = 0; e < gameMape.GetLength(1); e++)
                        {                     
                            gameMape[g , e] = gameMape[g - 1, e];
                        }
                    }
                }
            }
            for(int i =0; i < removedLine; i++)
            {
                score += 10 * (i+1);
            }

            label1.Text = "линий  " + removedLine;
            label2.Text = "очки  " + score;
        }

        // метод для проверки клавиш отвечающих за право и лево
        private void key(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                // верчение фигуры пока что не реализовано
                case Keys.Space:

                    if(!RoteshenIsPossible())
                    {
                        ResetArea();
                        kvadrat.Rotate();
                        Sunchro();
                        Invalidate();
                    }
                  
                    break;

                // движение фиугры влево
                case Keys.Left:

                   if(!CheckLefttside())
                    {
                        // очистили 
                        ResetArea();
                        // подвинули
                        kvadrat.Leght();
                        // синхронизировали
                        Sunchro();
                        Invalidate();
                    }
                                                          
                    break;

                // движение фигуры вправо
                case Keys.Right:

                   if (!CheckRightside())               
                    {
                        // очистили 
                        ResetArea();
                        // подвинули
                        kvadrat.Right();
                        // синхронизировали
                        Sunchro();
                        Invalidate();
                    }                 
                    break;
            }
        }

        // метод отчающий за падение вниз
        private void update(object sender, EventArgs e)
        {
            ResetArea();
            if(!ChecknextStep())
            {
                kvadrat.Movedown();
            }
            else
            {
                Sunchro();
                DeleteFilledRows();             
               kvadrat = new StandartShape(3, 0);
            }           
            Sunchro();
            Invalidate();
        }

        // функция отвечающая за отрисовку всего
        private void DrawAllObject(object sender, PaintEventArgs e)
        {
            // отрисовка сетки
            DrawGrid(e.Graphics);

            // отрисовка фиугры
            Drawfigure(e.Graphics);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
