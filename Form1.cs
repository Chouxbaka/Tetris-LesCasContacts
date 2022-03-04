
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetTetris
{
    public partial class Form1 : Form
    {

        bool buttonMoveRight = false;
        bool buttonMoveLeft = false;
        bool buttonTurnRight = false;
        bool buttonTurnLeft = false;
        bool buttonMoveDown = false;
        bool buttonHold = false;
        readonly int width = 20, height = 21;
        int[,] grid;
        int[,] gridHold;
        int[,] gridNext;
        Bitmap draw;
        Bitmap draw2;
        Bitmap draw3;
        Graphics canvas;
        Graphics canvas2;
        Graphics canvas3;
        Blocks instantiateBlock;
        Blocks upComingBlock = null;
        Blocks holdedBlock;

        bool blockHolded = false;
        bool startGame = false;
        bool pauseGame = false;
        double nbScore = 0;

        // Creation de blockRepere + hitbox
        int x_blockRepere = 4;
        int y_blockRepere = 0;
        int x_hitbox = 0;
        int y_hitbox = 0;
        int LineDeleted = 0;
        int CountLine = 0;
        bool StillCheck=true;
        int LineYCount = 0;

        public Form1() : base()
        {
            this.KeyPreview = true;
            InitializeComponent();
        }


        void Start(int[,] loadedgrid)
        {
            grid = loadedgrid;
            draw = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            canvas = Graphics.FromImage(draw);
            canvas.FillRectangle(Brushes.Transparent, width, height, width, height);

            draw2 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            canvas2 = Graphics.FromImage(draw2);
            canvas2.FillRectangle(Brushes.Transparent, width, height, width, height);

            draw3 = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            canvas3 = Graphics.FromImage(draw3);
            canvas3.FillRectangle(Brushes.Transparent, width, height, width, height);
            Blocks.Init();

            if (upComingBlock == null) { 
                instantiateBlock = Blocks.GetNextBlock();
                upComingBlock = Blocks.GetNextUpBlock();
            } else
            {
                instantiateBlock = upComingBlock;
                upComingBlock = Blocks.GetNextBlock();
                
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        gridNext[x, y] = 0;
                    }
                }
            }
          
            // debut block hitbox + calcul hitbox
            x_blockRepere = 4;  // Pour start avec le block
            y_blockRepere = 0;
            x_hitbox = instantiateBlock.Width;
            y_hitbox = instantiateBlock.Height;
            instantiateBlock.RotationState = 0;
            upComingBlock.RotationState = 0;
            // ----
            for (int y = 0; y < upComingBlock.Height; y++)
            {
                for (int x = 0; x < upComingBlock.Width; x++)
                {
                    gridNext[x + 2, y + 2] = upComingBlock.SubBlockArr[upComingBlock.RotationState][y, x];
                }
            }
            PaintNext(gridNext);
            // Affiche les blocks en haut (position initiale)
            for (int y = 0; y < instantiateBlock.Height; y++) {
                for (int x = 0; x < instantiateBlock.Width; x++) {
                    grid[x+4, y] = instantiateBlock.SubBlockArr[instantiateBlock.RotationState][y, x];
                }
            }
            //Console.WriteLine("------");
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            CanMoveDown();
            if(nbScore % 1 == 0)
            {
                score.Text = (nbScore).ToString();
            }
            nbScore += 0.5;

        }


        private void Form1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (pauseGame == false) { 
                if (buttonMoveDown == true)
                {
                    this.label15.Text = e.KeyChar.ToString().ToUpper();
                    buttonMoveDown = false;
                    Console.WriteLine();
                } 
            
                if(buttonMoveRight == true)
                {
                    this.label16.Text = e.KeyChar.ToString().ToUpper();
                    buttonMoveRight = false;
                }

                if (buttonMoveLeft == true)
                {
                    this.label14.Text = e.KeyChar.ToString().ToUpper();
                    buttonMoveLeft = false;
                }

                if (buttonTurnRight == true)
                {
                    this.label17.Text = e.KeyChar.ToString().ToUpper();
                    buttonTurnRight = false;
                }

                if (buttonTurnLeft == true)
                {
                    this.label13.Text = e.KeyChar.ToString().ToUpper();
                    buttonTurnLeft = false;
                }

                if (buttonHold == true)
                {
                    this.label19.Text = e.KeyChar.ToString().ToUpper();
                    buttonTurnLeft = false;
                }

                if (startGame) { 

                    if(e.KeyChar.ToString().ToUpper() == this.label14.Text)
                    {
                        CanMoveLeft();
                    }
                    if (e.KeyChar.ToString().ToUpper() == this.label15.Text)
                    {
                        CanMoveDown();
                    }
                    if (e.KeyChar.ToString().ToUpper() == this.label16.Text)
                    {
                        CanMoveRight();
                    }
                    if (e.KeyChar.ToString().ToUpper() == this.label17.Text)
                    {
                        CanRotateCW();
                    }
                    if (e.KeyChar.ToString().ToUpper() == this.label13.Text)
                    {
                        CanRotateNCW();
                    }
                    if (e.KeyChar.ToString().ToUpper() == this.label19.Text)
                    {
                        HoldBlock();
                    }

                }
            }
        }

        private void HoldBlock()
        {
            if (!blockHolded) {
                holdedBlock = instantiateBlock;
                instantiateBlock.RotationState = 0;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        grid[x, y] = gridHold[x,y];
                    }
                }
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        gridHold[x, y] = 0;
                    }
                }
                for (int y = 0; y < instantiateBlock.Height; y++)
                {
                    for (int x = 0; x < instantiateBlock.Width; x++)
                    {
                        gridHold[x+2 , y+2 ] = instantiateBlock.SubBlockArr[instantiateBlock.RotationState][y, x];
                    }
                }
                PaintHold(gridHold);
                CheckLineComplete();
                blockHolded = true;
                Start(grid);
            } else
            {
                if(instantiateBlock.Height >= holdedBlock.Height) {  
                    if(instantiateBlock.Width >= holdedBlock.Width) { 
                        for (int y = 0; y < y_hitbox; y++)
                        {
                            for (int x = 0; x < x_hitbox; x++)
                            {
                                grid[x_blockRepere + x, y_blockRepere + y] = gridHold[x+2, y+2];
                            }
                        }
                    }
                    else
                    {
                        for (int y = 0; y < y_hitbox; y++)
                        {
                            for (int x = 0; x < holdedBlock.Width; x++)
                            {
                                grid[x_blockRepere + x, y_blockRepere + y] = gridHold[x+2,y+2];
                            }
                        }
                    }
                } else
                {
                    if (instantiateBlock.Width >= holdedBlock.Width)
                    {
                        for (int y = 0; y < holdedBlock.Height; y++)
                        {
                            for (int x = 0; x < x_hitbox; x++)
                            {
                                grid[x_blockRepere + x, y_blockRepere + y] = gridHold[x+2,y+2];
                            }
                        }
                    }
                    else
                    {
                        for (int y = 0; y < holdedBlock.Height; y++)
                        {
                            for (int x = 0; x < holdedBlock.Width; x++)
                            {
                                grid[x_blockRepere + x, y_blockRepere + y] = gridHold[x+2,y+2];
                            }
                        }
                    }
                }

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        gridHold[x, y] = 0;
                    }
                }

                blockHolded = false;
                PaintHold(gridHold);
                Paint(grid);
                instantiateBlock = holdedBlock;
                CanMoveDown();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Startgame();
            }

        }


        private void Startgame()
        {
            //IL A MIS PAUSE AU JEU
            if (startGame)
            {
                pauseGame = true;
            }
            else
            {
                this.label7.Visible = false;
                startGame = true;
                this.pictureBox1.Enabled = true;
                this.pictureBox1.Visible = true;
                this.pictureBox3.Enabled = true;
                this.pictureBox3.Visible = true;
                this.timer1.Enabled = true;
                this.score.Visible = true;
                this.label1.Visible = true;
                this.BackgroundImage = null;
                this.BackColor = ColorTranslator.FromHtml("64; 64; 64");

                this.pictureBox2.Enabled = true;
                this.pictureBox2.Visible = true;

                this.label8.Visible = false;
                this.label9.Visible = false;
                this.label10.Visible = false;
                this.label11.Visible = false;
                this.label12.Visible = false;
                this.label13.Visible = false;
                this.label14.Visible = false;
                this.label15.Visible = false;
                this.label16.Visible = false;
                this.label17.Visible = false;
                this.label18.Visible = false;
                this.label19.Visible = false;
                this.label20.Visible = false;
                grid = new int[width, height];
                gridNext = new int[width, height];
                gridHold = new int[width, height];
                Start(grid);
                Paint(grid);
            }

            if (pauseGame)
            {
                if (this.label2.Visible == true)
                {
                    this.label2.Visible = false;
                    this.timer1.Start();
                    pauseGame = false;
                }
                else
                {
                    this.timer1.Stop();
                    this.label2.Visible = true;
                }
            }
        }

        // fonction CanMoveDown
        // execute une batterie de test pour savoir si le blocks peut descendre ou non
        private void CanMoveDown()
        {
            
            // la pièce bouge de base
            bool CanMove = true;
            
            // parcours le x de ma hitbox
            for (int x = 0; x < x_hitbox; x++)
            {
                // parcours le y de ma hitbox
                for (int y = 0; y < y_hitbox; y++)
                {
                    // si dans ma hitbox j'ai une couleur et que j'ai aussi cette couleur dans le bloc d'origine
                    if (grid[x_blockRepere + x, y_blockRepere + y] != 0 && instantiateBlock.SubBlockArr[instantiateBlock.RotationState][y, x] == grid[x_blockRepere + x, y_blockRepere + y])
                    {
                        
                        // si je ne suis pas en bordure d'hitbox
                        if (y < y_hitbox - 1)
                        {
                            // si le bloc en dessous est coloré && ce bloc coloré N'est PAS coloré dans le bloc d'origine
                            if (grid[x_blockRepere + x, y_blockRepere + y + 1] != 0 && instantiateBlock.SubBlockArr[instantiateBlock.RotationState][y + 1, x] != grid[x_blockRepere + x, y_blockRepere + y + 1])
                            {
                                CanMove = false;
                            }
                        }
                        // si je suis en bordure d'hitbox
                        else
                        {
                            // je check juste le block en dessous
                            if (grid[x_blockRepere + x, y_blockRepere + y + 1] != 0)
                            {
                                CanMove = false;
                            }
                        }
                        
                    }
                }
            }
            

            if (y_blockRepere + y_hitbox == 19)
            {
                CanMove = false;
            }
            
            // Déplacer les blocks
            if(CanMove)
            {
                MoveDown();
            } else
            {
                if (y_blockRepere != 0) { 
                    CheckLineComplete();
                    Start(grid);
                }
                else
                {
                    this.timer1.Stop();
                    this.label4.Visible = true;
                    this.label5.Visible = true;

                }
            }

        }

        private void CanMoveRight()
        {
            bool CanMove = true;

            if (x_blockRepere + x_hitbox == 10)
            {
                CanMove = false;

               
            } else {

                for (int x = 0; x < x_hitbox; x++)
                {
                    for (int y = 0; y < y_hitbox; y++)
                    {
                        if (grid[x_blockRepere + x, y_blockRepere + y] != 0 && instantiateBlock.SubBlockArr[instantiateBlock.RotationState][y, x] == grid[x_blockRepere + x, y_blockRepere + y])
                        {
                            if (x < x_hitbox - 1)
                            {
                                if (grid[x_blockRepere + x + 1, y_blockRepere + y] != 0 && instantiateBlock.SubBlockArr[instantiateBlock.RotationState][y, x + 1] != grid[x_blockRepere + x + 1, y_blockRepere + y])
                                {
                                    CanMove = false;
                                }
                            }
                            else
                            {
                                if (grid[x_blockRepere + x + 1, y_blockRepere + y] != 0)
                                {
                                    CanMove = false;
                                }
                            }

                        }
                    }
                }
            }
            if (CanMove)
            {
                MoveRight();
            }
        }

        private void MoveRight()
        {
            for (int x = x_hitbox - 1; x >= 0; x--)
            {
                for (int y = 0; y < y_hitbox; y++)
                {
                    if (grid[x_blockRepere + x, y_blockRepere + y] != 0 && instantiateBlock.SubBlockArr[instantiateBlock.RotationState][y, x] == grid[x_blockRepere + x, y_blockRepere + y])
                    {
                        grid[x_blockRepere + x + 1, y_blockRepere + y] = grid[x_blockRepere + x, y_blockRepere + y];
                        grid[x_blockRepere + x, y_blockRepere + y] = 0;
                    }
                }
            }
            x_blockRepere++;
            Invalidate();
            Paint(grid);
        }

        private void CanMoveLeft()
        {
            bool CanMove = true;

            if (x_blockRepere == 0)
            {
                CanMove = false;


            }
            else
            {
                for (int x = x_hitbox - 1; x >= 0; x--)
                {
                    for (int y = y_hitbox - 1; y >= 0; y--)
                    {
                        if (grid[x_blockRepere + x, y_blockRepere + y] != 0 && instantiateBlock.SubBlockArr[instantiateBlock.RotationState][y, x] == grid[x_blockRepere + x, y_blockRepere + y])
                        {
                            if (x > 0)
                            {
                                if (grid[x_blockRepere + x - 1, y_blockRepere + y] != 0 && instantiateBlock.SubBlockArr[instantiateBlock.RotationState][y, x - 1] != grid[x_blockRepere + x - 1, y_blockRepere + y])
                                {
                                    CanMove = false;
                                }
                            }
                            else
                            {
                                if (grid[x_blockRepere + x - 1, y_blockRepere + y] != 0)
                                {
                                    CanMove = false;
                                }
                            }
                        }
                    }
                }
            }
            if (CanMove)
            {
                MoveLeft();
            }
        }

        private void MoveLeft()
        {
            for (int x = 0; x < x_hitbox; x++)
            {
                for (int y = 0; y < y_hitbox; y++)
                {
                    if (grid[x_blockRepere + x, y_blockRepere + y] != 0 && instantiateBlock.SubBlockArr[instantiateBlock.RotationState][y, x] == grid[x_blockRepere + x, y_blockRepere + y])
                    {
                        grid[x_blockRepere + x - 1, y_blockRepere + y] = grid[x_blockRepere + x, y_blockRepere + y];
                        grid[x_blockRepere + x, y_blockRepere + y] = 0;
                    }
                }
            }
            x_blockRepere--;
            Invalidate();
            Paint(grid);
        }



        // fonction MoveDown
        // déplace tous les blocks de 1 vers le bas, de bas en haut + print
        private void MoveDown()
        {

            for (int y = y_hitbox - 1; y >= 0; y--)
            {
                for (int x = x_hitbox -1 ; x >= 0; x--)
                {
                    if (grid[x_blockRepere + x, y_blockRepere + y] != 0 && instantiateBlock.SubBlockArr[instantiateBlock.RotationState][y, x] == grid[x_blockRepere + x, y_blockRepere + y])
                    {
                        grid[x_blockRepere + x, y_blockRepere + y + 1] = grid[x_blockRepere + x, y_blockRepere + y];
                        grid[x_blockRepere + x, y_blockRepere + y] = 0;
                    }
                }
            }
            y_blockRepere++;
            Invalidate();
            Paint(grid);
        }


        private void CanRotateCW()
        {
            bool CanRotate = true;
            int Rotate;
            int y_h = x_hitbox;
            int x_h = y_hitbox;
            if (instantiateBlock.RotationState == 3)
            {
                Rotate = 0;
                y_h = y_hitbox;
                x_h = x_hitbox;
            }
            else
            {
                Rotate = instantiateBlock.RotationState + 1;
            }

            for (int y = 0; y < y_h; y++)
            {
                for (int x = 0; x < x_h; x++)
                {
                    if (x + x_blockRepere > 9 || x + x_blockRepere <= 0)
                    {
                        CanRotate = false;
                    }
                    if (grid[x + x_blockRepere, y + y_blockRepere] != 0 && grid[x + x_blockRepere, y + y_blockRepere] != instantiateBlock.Id)
                    {
                        CanRotate = false;
                    }

                }
            }
            if (CanRotate)
            {
                RotateCW();
            }
        }
        private void RotateCW()
        {
            for (int y = 0; y < y_hitbox; y++)
            {
                for (int x = 0; x < x_hitbox; x++)
                {
                    grid[x + x_blockRepere, y + y_blockRepere] = 0;
                }
            }
            int third = x_hitbox;
            x_hitbox = y_hitbox;
            y_hitbox = third;
            if (instantiateBlock.RotationState == 3) {
                instantiateBlock.RotationState = 0;
            }
            else { 
                instantiateBlock.RotationState++;   
            }
            for (int y = 0; y < y_hitbox; y++)
            {
                for (int x = 0; x < x_hitbox; x++)
                {
                    grid[x + x_blockRepere, y + y_blockRepere] = instantiateBlock.SubBlockArr[instantiateBlock.RotationState][y, x];
                }
            }
        }

        private void CanRotateNCW()
        {
            bool CanRotate = true;
            int Rotate;
            int y_h = x_hitbox;
            int x_h = y_hitbox;
            if (instantiateBlock.RotationState == 0)
            {
                Rotate = 3;
                y_h = y_hitbox;
                x_h = x_hitbox;
            }
            else
            {
                Rotate = instantiateBlock.RotationState - 1;
            }

            for (int y = 0; y < y_h; y++)
            {
                for (int x = 0; x < x_h; x++)
                {
                    if (x + x_blockRepere > 9 || x + x_blockRepere <= 0)
                    {
                        CanRotate = false;
                    }
                    if (grid[x + x_blockRepere, y + y_blockRepere] != 0 && grid[x + x_blockRepere, y + y_blockRepere] != instantiateBlock.Id)
                    {
                        CanRotate = false;
                    }

                }
            }
            if (CanRotate)
            {
                RotateNCW();
            }
        }

        private void RotateNCW()
        {
            for (int y = 0; y < y_hitbox; y++)
            {
                for (int x = 0; x < x_hitbox; x++)
                {
                    grid[x + x_blockRepere, y + y_blockRepere] = 0;
                }
            }

            int third = x_hitbox;
            x_hitbox = y_hitbox;
            y_hitbox = third;
            if (instantiateBlock.RotationState == 0) { 
                instantiateBlock.RotationState = 3;
            }
            else
            {
                instantiateBlock.RotationState--;
            }
            for (int y = 0; y < y_hitbox; y++)
            {
                for (int x = 0; x < x_hitbox; x++)
                {
                    grid[x + x_blockRepere, y + y_blockRepere] = instantiateBlock.SubBlockArr[instantiateBlock.RotationState][y, x];
                }
            }


        }
        void CheckLineComplete()
        {
            for (int y = 18; y >= 0; y--)
            {
                int IsComplete = 0;
                for (int x = 0; x <= 9; x++)
                {
                    if (grid[x, y] != 0)
                    {
                        IsComplete++;
                    }
                }
                if (IsComplete == 10)
                {
                    for (int x = 0; x <= 9; x++)
                    {
                        grid[x, y] = 0;
                    }
                    for (int yy = y; yy >= 1; yy--)
                    {
                        for (int xx = 0; xx <= 9; xx++)
                        {
                            grid[xx, yy] = grid[xx, yy - 1];
                        }
                    }
                    nbScore += 100;
                    y++;
                }
            }
        }
        // fonction Paint
        // parcours toute notre grille et colorie en fonction des chiffres
        // fait la liaison entre le back (chiffre) et le front (couleur) 
        new void Paint(int[,] grid)
        {
            canvas.Clear(Color.Transparent);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                   
                    if (grid[i, j] == 0)
                    { 
                       canvas.FillRectangle(Brushes.Transparent, i * width, j * height, width, height);
                    }
                    if (grid[i, j] == 1)
                    {
                        canvas.FillRectangle(Brushes.Yellow, i * width, j * height, width, height);
                    }
                    if (grid[i, j] == 2)
                    {
                        canvas.FillRectangle(Brushes.Turquoise, i * width, j * height, width, height);
                    }
                    if (grid[i, j] == 3)
                    {
                        canvas.FillRectangle(Brushes.Purple, i * width, j * height, width, height);
                    }
                    if (grid[i, j] == 4)
                    {
                        canvas.FillRectangle(Brushes.Orange, i * width, j * height, width, height);
                    }
                    if (grid[i, j] == 5)
                    {
                        canvas.FillRectangle(Brushes.Blue, i * width, j * height, width, height);
                    }
                    if (grid[i, j] == 6)
                    {
                        canvas.FillRectangle(Brushes.Red, i * width, j * height, width, height);
                    }
                    if (grid[i, j] == 7)
                    {
                        canvas.FillRectangle(Brushes.Green, i * width, j * height, width, height);
                    }
                    if(grid[i,j]==9)
                    {
                        canvas.FillRectangle(Brushes.Black, i * width, j * height, width, height);
                    }
                }
            }
            pictureBox1.Image = draw;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            startGame = false;
            this.label5.Visible = false;
            this.label4.Visible = false;
            nbScore = 0;
            score.Text = "0";
            Startgame();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.label7.Visible = false;
            this.BackgroundImage = null;
            this.label8.Visible = true;
            this.label9.Visible = true;
            this.label10.Visible = true;
            this.label11.Visible = true;
            this.label12.Visible = true;
            this.label13.Visible = true;
            this.label14.Visible = true;
            this.label15.Visible = true;
            this.label16.Visible = true;
            this.label17.Visible = true;
            this.label18.Visible = true;
            this.label19.Visible = true;
            this.label20.Visible = true;
        }

        private void label13_Click(object sender, EventArgs e)
        {
            buttonTurnLeft = true;
        }

        private void label18_Click(object sender, EventArgs e)
        {
            this.label7.Visible = true;
            this.BackgroundImage = global::ProjetTetris.Properties.Resources.tetris_1_;
            this.label8.Visible = false;
            this.label9.Visible = false;
            this.label10.Visible = false;
            this.label11.Visible = false;
            this.label12.Visible = false;
            this.label13.Visible = false;
            this.label14.Visible = false;
            this.label15.Visible = false;
            this.label16.Visible = false;
            this.label17.Visible = false;
            this.label18.Visible = false;
            this.label19.Visible = false;
            this.label20.Visible = false;
        }

        private void label17_Click(object sender, EventArgs e)
        {
            buttonTurnRight = true;
        }

        private void label14_Click(object sender, EventArgs e)
        {
            buttonMoveLeft= true;
        }

        private void label16_Click(object sender, EventArgs e)
        {
            buttonMoveRight = true;
        }

        private void label15_Click(object sender, EventArgs e)
        {
            buttonMoveDown = true;
        }

        private void label19_Click(object sender, EventArgs e)
        {
            buttonHold = true;
        }

        new void PaintNext(int[,] grid)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {

                    if (grid[i, j] == 0)
                    {
                        canvas2.FillRectangle(Brushes.Transparent, i * width, j * height, width, height);
                    }
                    if (grid[i, j] == 1)
                    {
                        canvas2.FillRectangle(Brushes.Yellow, i * width, j * height, width, height);
                    }
                    if (grid[i, j] == 2)
                    {
                        canvas2.FillRectangle(Brushes.Turquoise, i * width, j * height, width, height);
                    }
                    if (grid[i, j] == 3)
                    {
                        canvas2.FillRectangle(Brushes.Purple, i * width, j * height, width, height);
                    }
                    if (grid[i, j] == 4)
                    {
                        canvas2.FillRectangle(Brushes.Orange, i * width, j * height, width, height);
                    }
                    if (grid[i, j] == 5)
                    {
                        canvas2.FillRectangle(Brushes.Blue, i * width, j * height, width, height);
                    }
                    if (grid[i, j] == 6)
                    {
                        canvas2.FillRectangle(Brushes.Red, i * width, j * height, width, height);
                    }
                    if (grid[i, j] == 7)
                    {
                        canvas2.FillRectangle(Brushes.Green, i * width, j * height, width, height);
                    }
                    if (grid[i, j] == 9)
                    {
                        canvas2.FillRectangle(Brushes.Black, i * width, j * height, width, height);
                    }
                }
            }
            pictureBox2.Image = draw2;
        }

        new void PaintHold(int[,] grid)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {

                    if (grid[i, j] == 0)
                    {
                        canvas3.FillRectangle(Brushes.Transparent, i * width, j * height, width, height);
                    }
                    if (grid[i, j] == 1)
                    {
                        canvas3.FillRectangle(Brushes.Yellow, i * width, j * height, width, height);
                    }
                    if (grid[i, j] == 2)
                    {
                        canvas3.FillRectangle(Brushes.Turquoise, i * width, j * height, width, height);
                    }
                    if (grid[i, j] == 3)
                    {
                        canvas3.FillRectangle(Brushes.Purple, i * width, j * height, width, height);
                    }
                    if (grid[i, j] == 4)
                    {
                        canvas3.FillRectangle(Brushes.Orange, i * width, j * height, width, height);
                    }
                    if (grid[i, j] == 5)
                    {
                        canvas3.FillRectangle(Brushes.Blue, i * width, j * height, width, height);
                    }
                    if (grid[i, j] == 6)
                    {
                        canvas3.FillRectangle(Brushes.Red, i * width, j * height, width, height);
                    }
                    if (grid[i, j] == 7)
                    {
                        canvas3.FillRectangle(Brushes.Green, i * width, j * height, width, height);
                    }
                    if (grid[i, j] == 9)
                    {
                        canvas3.FillRectangle(Brushes.Black, i * width, j * height, width, height);
                    }
                }
            }
            pictureBox3.Image = draw3;
        }
    }
}