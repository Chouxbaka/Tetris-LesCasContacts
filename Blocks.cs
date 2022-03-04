using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetTetris
{
    class Blocks
    {
        public int Width;
        public int Height;
        public static Blocks[] ArrayOfBlocks;
        public int RotationState;
        public int[][,] SubBlockArr;
        public int Id;
        public static int Random;

        static readonly Blocks Block_O = new Blocks
        {
            Id = 1,
            Width = 2,
            Height = 2,
            RotationState = 0,
            SubBlockArr = new int[][,]
            {
               new int[,]
                {
                    { 1, 1 },
                    { 1, 1 }
                },
               new int[,]
                {
                    { 1, 1 },
                    { 1, 1 }
                },
               new int[,]
                {
                    { 1, 1 },
                    { 1, 1 }
                },
               new int[,]
                {
                    { 1, 1 },
                    { 1, 1 }
                },
            } 
        };

        static readonly Blocks Block_I = new Blocks
        {
            Id = 2,
            Width = 4,
            Height = 1,
            RotationState = 0,
            SubBlockArr = new int[][,]
            {
               new int[,]
                {
                     { 2, 2, 2, 2 }
                },
               new int[,]
                {
                     { 2 },
                     { 2 },
                     { 2 },
                     { 2 }
                },
               new int[,]
                {
                     { 2, 2, 2, 2 }
                },
               new int[,]
                {
                     { 2 },
                     { 2 },
                     { 2 },
                     { 2 }
                },
            }
            
            /*
            Width = 4,
            Height = 4,
            RotationState = 0,
            SubBlockArr = new int[][,]
            {
               new int[,]
                {
                     { 9, 9, 9, 9 },
                     { 2, 2, 2, 2 },
                     { 9, 9, 9, 9 },
                     { 9, 9, 9, 9 }
                },
               new int[,]
                {
                     { 9, 9, 2, 9 },
                     { 9, 9, 2, 9 },
                     { 9, 9, 2, 9 },
                     { 9, 9, 2, 9 }
                },
               new int[,]
                {
                     { 9, 9, 9, 9 },
                     { 9, 9, 9, 9 },
                     { 2, 2, 2, 2 },
                     { 9, 9, 9, 9 }
                },
               new int[,]
                {
                     { 9, 2, 9, 9 },
                     { 9, 2, 9, 9 },
                     { 9, 2, 9, 9 },
                     { 9, 2, 9, 9 }
                },
            } 
            */
        };

        static readonly Blocks Block_T = new Blocks
        {
            Id = 3,
            Width = 3,
            Height = 2,
            RotationState = 0,
            SubBlockArr = new int[][,]
            {
              new int[,]
                {
                     { 0, 3, 0 },
                     { 3, 3, 3 }
                },
              new int[,]
                {
                     { 3, 0 },
                     { 3, 3 },
                     { 3, 0 }
                },
              new int[,]
                {
                     { 3, 3, 3 },
                     { 0, 3, 0 }
                },
              new int[,]
                {
                     { 0, 3 },
                     { 3, 3 },
                     { 0, 3 }
                },
            }
            /*
            Width = 3,
            Height = 3,
            RotationState = 0,
            SubBlockArr = new int[][,]
            {
              new int[,]
                {
                     { 9, 3, 9 },
                     { 3, 3, 3 },
                     { 9, 9, 9 }
                },
              new int[,]
                {
                     { 9, 3, 9 },
                     { 9, 3, 3 },
                     { 9, 3, 9 }
                },
              new int[,]
                {
                     { 9, 9, 9 },
                     { 3, 3, 3 },
                     { 9, 3, 9 }
                },
              new int[,]
                {
                     { 9, 3, 9 },
                     { 3, 3, 9 },
                     { 9, 3, 9 }
                },
            } 
            */
        };

        static readonly Blocks Block_L = new Blocks
        {
            Id = 4,
            Width = 3,
            Height = 2,
            RotationState = 0,
            SubBlockArr = new int[][,]
            {
               new int[,]
               {
                     { 0, 0, 4 },
                     { 4, 4, 4 }
               },
               new int[,]
               {
                     { 4, 0 },
                     { 4, 0 },
                     { 4, 4 }
               },
               new int[,]
               {
                     { 4, 4, 4 },
                     { 4, 0, 0 }
               },
               new int[,]
               {
                     { 4, 4 },
                     { 0, 4 },
                     { 0, 4 }
               },
            }

            /*
            Width = 3,
            Height = 3,
            RotationState = 0,
            SubBlockArr = new int[][,]
            {
               new int[,]
               {
                     { 9, 9, 4 },
                     { 4, 4, 4 },
                     { 9, 9, 9 }
               },
               new int[,]
               {
                     { 9, 4, 9 },
                     { 9, 4, 9 },
                     { 9, 4, 4 }
               },
               new int[,]
               {
                     { 9, 9, 9 },
                     { 4, 4, 4 },
                     { 4, 9, 9 }
               },
               new int[,]
               {
                     { 4, 4, 9 },
                     { 9, 4, 9 },
                     { 9, 4, 9 }
               },
            } 
            */
        };

        static readonly Blocks Block_J = new Blocks
        {
            Id = 5,
            Width = 3,
            Height = 2,
            RotationState = 0,
            SubBlockArr = new int[][,]
            {
               new int[,]
               {
                     { 5, 0, 0 },
                     { 5, 5, 5 }
               },
               new int[,]
               {
                     { 5, 5 },
                     { 5, 0 },
                     { 5, 0 }
               },
               new int[,]
               {
                     { 5, 5, 5 },
                     { 0, 0, 5 }
               },
               new int[,]
               {
                     { 0, 5 },
                     { 0, 5 },
                     { 5, 5 }
               },
            }

            /*
            Width = 3,
            Height = 3,
            RotationState = 0,
            SubBlockArr = new int[][,]
            {
               new int[,]
               {
                     { 5, 9, 9 },
                     { 5, 5, 5 },
                     { 9, 9, 9 }
               },
               new int[,]
               {
                     { 9, 5, 5 },
                     { 9, 5, 9 },
                     { 9, 5, 9 }
               },
               new int[,]
               {
                     { 9, 9, 9 },
                     { 5, 5, 5 },
                     { 9, 9, 5 }
               },
               new int[,]
               {
                     { 9, 5, 9 },
                     { 9, 5, 9 },
                     { 5, 5, 9 }
               },
            } 
            */
        };

        static readonly Blocks Block_Z = new Blocks
        {
            Id = 6,
            Width = 3,
            Height = 2,
            RotationState = 0,
            SubBlockArr = new int[][,]
            {
               new int[,]
                {
                    { 6, 6, 0 },
                    { 0, 6, 6 }
                },
               new int[,]
                {
                    { 0, 6 },
                    { 6, 6 },
                    { 6, 0 }
                },
               new int[,]
                {
                    { 6, 6, 0 },
                    { 0, 6, 6 }
                },
               new int[,]
                {
                    { 0, 6 },
                    { 6, 6 },
                    { 6, 0 }
                },
            }

            /*
            Width = 3,
            Height = 3,
            RotationState = 0,
            SubBlockArr = new int[][,]
            {
               new int[,]
                {
                    { 6, 6, 9 },
                    { 9, 6, 6 },
                    { 9, 9, 9 }
                },
               new int[,]
                {
                    { 9, 9, 6 },
                    { 9, 6, 6 },
                    { 9, 6, 9 }
                },
               new int[,]
                {
                    { 9, 9, 9 },
                    { 6, 6, 9 },
                    { 9, 6, 6 }
                },
               new int[,]
                {
                    { 9, 6, 9 },
                    { 6, 6, 9 },
                    { 6, 9, 9 }
                },
            } 
            */
        };

        static readonly Blocks Block_S = new Blocks
        {
            Id = 7,
            Width = 3,
            Height = 2,
            RotationState = 0,
            SubBlockArr = new int[][,]
            {
               new int[,]
                {
                    { 0, 7, 7 },
                    { 7, 7, 0}
                },
               new int[,]
                {
                    { 7, 0 },
                    { 7, 7 },
                    { 0, 7 }
                },
               new int[,]
                {
                    { 0, 7, 7 },
                    { 7, 7, 0 }
                },
               new int[,]
                {
                    { 7, 0 },
                    { 7, 7 },
                    { 0, 7 }
                },
            }
            /*
            Width = 3,
            Height = 3,
            RotationState = 0,
            SubBlockArr = new int[][,]
            {
               new int[,]
                {
                    { 9, 7, 7 },
                    { 7, 7, 9 },
                    { 9, 9, 9 }
                },
               new int[,]
                {
                    { 9, 7, 9 },
                    { 9, 7, 7 },
                    { 9, 9, 7 }
                },
               new int[,]
                {
                    { 9, 9, 9 },
                    { 9, 7, 7 },
                    { 7, 7, 9 }
                },
               new int[,]
                {
                    { 7, 9, 9 },
                    { 7, 7, 9 },
                    { 9, 7, 9 }
                },
            } 
            */
        };

        /*  1 = JAUNE
            2 = TURQUOISE
            3 = VIOLET
            4 = ORANGE
            5 = BLEU
            6 = ROUGE
            7 = VERT */

        public static void Init()
        {
            ArrayOfBlocks = new Blocks[]{
                Block_O,
                Block_I,
                Block_T,
                Block_L,
                Block_J,
                Block_Z,
                Block_S
            };
           Random = new Random().Next(ArrayOfBlocks.Length);
        }

        public static Blocks GetNextBlock()
        {
            var nextBlock = ArrayOfBlocks[Random];
            return nextBlock;
        }
        public static Blocks GetNextUpBlock()
        {
            Blocks nextBlock ;
            if (Random > 0) { 
                nextBlock = ArrayOfBlocks[Random-1];
            } else {
                return ArrayOfBlocks[2];
            }

            return nextBlock;
        }
    }


}