using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class MainClass
    {
        public class Logic
        {

            public static int ref_winner = 0;
            public static int x_axis = 15;
            public static int y_axis = 8;     
            public static int[,] gameplayGraph = new int[3, 3] {
                { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }
            };
            public static int[,] winner_color = new int[3, 3] {
                { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }
            };
            public int game_over()
            {
                bool emptyBoxExist = false;
                for(int i = 0; i < 3; i++)
                {
                    if(gameplayGraph[i, 0] == 1 && gameplayGraph[i, 1] == 1 && gameplayGraph[i, 2] == 1) {
                        for(int j = 0; j < 3; j++) { gameplayGraph[i, j] = 3; }
                        return 1; 
                    }
                    else if(gameplayGraph[0, i] == 1 && gameplayGraph[1, i] == 1 && gameplayGraph[2, i] == 1) {
                        for (int j = 0; j < 3; j++) { gameplayGraph[j, i] = 3; }
                        return 1; 
                    }
                    else if (gameplayGraph[i, 0] == 2 && gameplayGraph[i, 1] == 2 && gameplayGraph[i, 2] == 2) {
                        for (int j = 0; j < 3; j++) { gameplayGraph[i, j] = 4; }
                        return 2; 
                    }
                    else if (gameplayGraph[0, i] == 2 && gameplayGraph[1, i] == 2 && gameplayGraph[2, i] == 2) {
                        for (int j = 0; j < 3; j++) { gameplayGraph[j, i] = 4; }
                        return 2; 
                    }
                    else if (gameplayGraph[i, 0] == 0 || gameplayGraph[i, 1] == 0 || gameplayGraph[i, 2] == 0) { emptyBoxExist = true; }
                } 
                if (gameplayGraph[0, 0] == 1 && gameplayGraph[1, 1] == 1 && gameplayGraph[2, 2] == 1) {
                    for (int j = 0; j < 3; j++) { gameplayGraph[j, j] = 3; }
                    return 1;
                } else if (gameplayGraph[0, 0] == 2 && gameplayGraph[1, 1] == 2 && gameplayGraph[2, 2] == 2) {
                    for (int j = 0; j < 3; j++) { gameplayGraph[j, j] = 4; }
                    return 2;
                } else if (gameplayGraph[0, 2] == 1 && gameplayGraph[1, 1] == 1 && gameplayGraph[2, 0] == 1) {
                    gameplayGraph[0, 2] = 3; gameplayGraph[1, 1] = 3; gameplayGraph[2, 0] = 3;
                    return 1;
                } else if (gameplayGraph[0, 2] == 2 && gameplayGraph[1, 1] == 2 && gameplayGraph[2, 0] == 2) {
                    gameplayGraph[0, 2] = 4; gameplayGraph[1, 1] = 4; gameplayGraph[2, 0] = 4;
                    return 2;
                }
                if (!emptyBoxExist) { return 3; }
                return 0;
            }
            public int set_Y_Axis_index(int i) {
                if (i == 3) { return 0; } else if (i == 8) { return 1; } else if (i == 13) { return 2; } return 0;
            }
            public int set_X_Axis_index(int j) {
                if (j == 5) { return 0; } else if (j == 15) { return 1; } else if (j == 25) { return 2; } return 0;
            }
        }
        public class Control : Logic
        {
            public void movement(ref int player)
            {
               switch(Console.ReadKey().Key)
               {
                    case ConsoleKey.RightArrow:
                        if(x_axis + 10 > 25) { break; }
                        x_axis += 10;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (x_axis - 10 < 5) { break; }
                        x_axis -= 10;
                        break;
                    case ConsoleKey.UpArrow:
                        if(y_axis - 5 < 3) { break; }
                        y_axis -= 5;
                        break;
                    case ConsoleKey.DownArrow:
                        if (y_axis + 5 > 13) { break; }
                        y_axis += 5;
                        break;
                    case ConsoleKey.Enter:
                        if(gameplayGraph[set_X_Axis_index(x_axis), set_Y_Axis_index(y_axis)] == 0)
                        {
                            int x = 0;
                            if(player % 2 != 0) { x = 1; } else { x = 2; }
                            gameplayGraph[set_X_Axis_index(x_axis), set_Y_Axis_index(y_axis)] = x;
                            ref_winner = game_over();
                            if (ref_winner == 0) { ++player; }
                        }
                        break;
                    default:
                        break;
               }
            }
        }
        public class Display : Logic
        {
            public void rules()
            {

            }
            public void players_turn_display(int player)
            {
                if (player % 2 != 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\n\t\t\t\t\tPlayer-1(X)<-\t   ");
                    Console.ResetColor();
                    Console.Write("Player-2(O)\n");
                } 
                else
                {
                    Console.Write("\n\t\t\t\t\tPlayer-1(X)\t ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("->Player-2(O)\n");
                    Console.ResetColor();
                }
            }
            public bool display_game_board()
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write("\t\t\t\t\t");
                for (int i = 1; i <= 30; i++) { Console.Write("-"); }
                Console.WriteLine();
                for(int i = 1; i <= 15; i++)
                {
                    for(int j = 1; j <= 30; j++)
                    {
                        if(j == 1) 
                        { 
                            Console.Write("\t\t\t\t\t|");
                        }
                        else if (j == 10 || j == 20 || j == 30)
                        {
                            Console.Write("|");
                        }
                        else
                        {
                            if ((i == 3 || i == 8 || i == 13) && (j == 5 || j == 15 || j == 25))
                            {
                                if (gameplayGraph[set_X_Axis_index(j), set_Y_Axis_index(i)] == 3)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("X");
                                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                                }
                                else if (gameplayGraph[set_X_Axis_index(j), set_Y_Axis_index(i)] == 4)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("O");
                                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                                }
                                else if (x_axis == j && y_axis == i)
                                {
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.Write("#");
                                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                                }
                                else if (gameplayGraph[set_X_Axis_index(j), set_Y_Axis_index(i)] == 1)
                                {
                                    Console.Write("X");
                                }
                                
                                else if (gameplayGraph[set_X_Axis_index(j), set_Y_Axis_index(i)] == 2)
                                {
                                    Console.Write("O");
                                }
                                else
                                {
                                    Console.Write(" ");
                                }
                            }
                            else
                            {
                                Console.Write(" ");
                            }
                            
                        }
                    }
                    if(i == 5 || i == 10 || i == 15)
                    {
                        Console.Write("\n\t\t\t\t\t");
                        for (int ii = 1; ii <= 30; ii++) { Console.Write("-"); }
                    }
                    Console.WriteLine();
                }
                Console.ResetColor();
                if(ref_winner == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\n\n\t\t\t\t\t\tPlayer-1 ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("win!");
                    Console.ResetColor();
                } 
                else if(ref_winner == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\n\n\t\t\t\t\t\tPlayer-2 ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("win!");
                    Console.ResetColor();
                }
                else if (ref_winner == 3)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n\n\t\t\t\t\t\t   Draw!");
                    Console.ResetColor();
                }
                if(ref_winner != 0)
                {
                    Console.WriteLine("\n\nPress any key to continue....");
                    Console.ReadKey();
                    clear_screen();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n\n\n\n\t\t\t\t\t\tG a m e   O v e r!\n\n\n\n\n\n");
                    Console.ResetColor();
                    return true;
                }
                Console.WriteLine("\n\n\t\t\t\t\t\t     Draw!");
                return false;
            }
            public void clear_screen() { Console.Clear(); }
        }

        public static void Main(string[] args)
        {
            int player = 1;
            Display display = new Display();
            Control control = new Control();
            Logic logic = new Logic();
            display.rules();
            while(true)
            {
                display.players_turn_display(player);
                if(display.display_game_board()) { break; }
                control.movement(ref player);
                display.clear_screen();
            }
        }
    }
}
