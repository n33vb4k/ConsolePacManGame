using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace NEA2
{
    class Program
    {
        static int Ghostspeed = 250;
        
        //add highscore loaded onto start screeen from a text file
        //freeze ghosts powerup (potentialy if i have time)
        //make pacman move without having to repeatedly press button
        static void Main(string[] args)
        {
            startmenu();
            //rows = 31;
            //cols = 28;
            string mazeoption = selectmaze();
            Pacman pac = new Pacman();
            Blinky blink = new Blinky();
            Clyde cly = new Clyde();
            Pinky pink = new Pinky();
            Inky ink = new Inky();
            Maze maze1 = new Maze(31, 28, mazeoption, pac, blink, cly, ink, pink); 
            maze1.Display2();
            Task updateghostsBnC = new Task(() => MoveGhostsBnC(blink, cly, maze1)); //Creates thread that will keep moivng the ghosts without relying on the user input
            Task updateghostsInP = new Task(() => MoveGhostsInP(ink, pink, maze1));
            Task updatedisplay = new Task(() => UpdateDisp(maze1)); //Creates thread that will keep updatin the display
            char wasd = Console.ReadKey(true).KeyChar;
            updatedisplay.Start(); //starts the threads after pacman has entered the first key
            updateghostsBnC.Start();
            
            updateghostsInP.Start();
            while (wasd != 'e')
            {                  
                wasd = Console.ReadKey(true).KeyChar; //pacman input from user
                maze1.Move(wasd);
                Thread.Sleep(50);          
            }
            
        }
        static void UpdateDisp(Maze m)
        {
            while (m.CheckEnd() == false)
            {
                m.Display2();
                Thread.Sleep(75);
            }
        }

        static void MoveGhostsInP(Ghost ink, Ghost pink, Maze m)
        {

            while (m.CheckEnd() == false)
            {
                if (m.CheckHit() == true)
                {
                    Thread.Sleep(1000);
                }

                m.MoveGhostTrial(ink, m.GetTarget(ink));
                m.MoveGhostTrial(pink, m.GetTarget(pink));

                Thread.Sleep(Ghostspeed);
            }
        }
        //add counter for how many times this runs, after 7 moves start the other ghost movement inside this thread but with a bool to stay if it has started or not to see if it doesnt crash
        static void MoveGhostsBnC(Ghost blink, Ghost cly, Maze m)
        {
            
            while (m.CheckEnd() == false)
            {
                if (m.CheckHit() == true)
                {
                    Thread.Sleep(1000);
                }                
                m.MoveGhostTrial(blink, m.GetTarget(blink));
                m.MoveGhostTrial(cly, m.GetTarget(cly));
                
                               
                Thread.Sleep(Ghostspeed);
            }
            
        }
        static string selectmaze()  //Allows user to select the maze they want to play at the start
        {
            Console.Clear();
            int option = 1;
            string maze1 = File.ReadAllText("mazewalls1.txt");
            string maze2 = File.ReadAllText("mazewalls2.txt");
            Console.WriteLine(maze1);
            Console.WriteLine("Use left and right arrow keys then enter to choose a maze <>");
            ConsoleKeyInfo choice;
            do
            {
                Console.SetCursorPosition(0, 0);
                choice = Console.ReadKey(true);
                if (choice.Key == ConsoleKey.RightArrow)
                {
                    Console.WriteLine(maze2);
                    Console.WriteLine("Use left and right arrow keys then enter to choose a maze <>");
                    option = 2;
                }               
                else if (choice.Key == ConsoleKey.LeftArrow)
                {
                    Console.WriteLine(maze1);
                    Console.WriteLine("Use left and right arrow keys then enter to choose a maze <>");
                    option = 1;
                }                

            } while (choice.Key != ConsoleKey.Enter);
            Console.Clear();
            if (option == 1)
            {
                return "mazewalls1.txt";
            }
            else
            {
                return "mazewalls2.txt";
            }


        }

        static void startmenu()
        {
            Console.WriteLine(@" _    _      _                            _        
| |  | |    | |                          | |       
| |  | | ___| | ___ ___  _ __ ___   ___  | |_ ___  
| |/\| |/ _ \ |/ __/ _ \| '_ ` _ \ / _ \ | __/ _ \ 
\  /\  /  __/ | (_| (_) | | | | | |  __/ | || (_) |
 \/  \/ \___|_|\___\___/|_| |_| |_|\___|  \__\___/ 
                                                   
                                                   
  ______  ___  _____ ___  ___  ___   _   _         
  | ___ \/ _ \/  __ \|  \/  | / _ \ | \ | |        
  | |_/ / /_\ \ /  \/| .  . |/ /_\ \|  \| |        
  |  __/|  _  | |    | |\/| ||  _  || . ` |        
  | |   | | | | \__/\| |  | || | | || |\  |        
  \_|   \_| |_/\____/\_|  |_/\_| |_/\_| \_/        
                                                   
                                                   ");
            Console.WriteLine();
            Console.WriteLine("---Please enter full screen---");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Use arrow keys to choose a option and press enter...");
            Console.WriteLine("  Start New Game");
            Console.WriteLine("  Game Rules");
            Console.WriteLine("  Settings");
            Console.WriteLine("  Exit");
            Console.CursorLeft = 0;
            Console.CursorTop = 1;
            Console.Write(">");
            int option = 1;
            bool exit = false;
            do
            {
                ConsoleKeyInfo choice = Console.ReadKey(true);
                if (choice.Key == ConsoleKey.DownArrow && option < 4)
                {
                    Console.CursorTop = option;
                    Console.CursorLeft = 0;
                    Console.Write(" ");
                    option++;
                    Console.CursorTop = option;
                    Console.CursorLeft = 0;
                    Console.Write(">");
                }
                else if (choice.Key == ConsoleKey.UpArrow && option > 1)
                {
                    Console.CursorTop = option;
                    Console.CursorLeft = 0;
                    Console.Write(" ");
                    option--;
                    Console.CursorTop = option;
                    Console.CursorLeft = 0;
                    Console.Write(">");
                }
                else if (choice.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    if (option == 1)
                    {
                        Console.WriteLine("Loading new game...");                      
                        Thread.Sleep(100);
                        return;
                        
                    }
                    else if (option == 2)
                    {
                        Console.WriteLine("Loading Rules...");
                        Thread.Sleep(100);
                        Console.Clear();
                        Console.WriteLine(@"1. Survive to gain points
2. Lose 100 points when you lose a life
3. You only have 3 lives
4. Kill Ghosts by eating the power ups in the corners (you will turn blue when you can kill)
5. Gain 100 points when you kill a ghost
6. Select game difficulty in settings menu
enter a key to start game");
                        Console.ReadKey();
                        option = 1;
                    }
                    else if (option == 3)
                    {
                        Console.WriteLine("Settings menu...");
                        Thread.Sleep(250);
                        Console.Clear();
                        SettingsMenu();
                        option = 1;
                    }
                    else if (option == 4)
                    {
                        exit = true;
                        Environment.Exit(0);
                    }

                }


            } while (exit != true);

            Console.ReadKey();
        }

        

        static void SettingsMenu()
        {
            Console.WriteLine("Choose difficulty...");
            Console.WriteLine("  Easy");
            Console.WriteLine("  Normal");
            Console.WriteLine("  Hard");
            Console.WriteLine("  Return to Main menu");
            Console.CursorLeft = 0;
            Console.CursorTop = 1;
            Console.Write(">");
            int option = 1;
            bool exit = false;
            do
            {
                ConsoleKeyInfo choice = Console.ReadKey(true);
                if (choice.Key == ConsoleKey.DownArrow && option < 4)
                {
                    Console.CursorTop = option;
                    Console.CursorLeft = 0;
                    Console.Write(" ");
                    option++;
                    Console.CursorTop = option;
                    Console.CursorLeft = 0;
                    Console.Write(">");
                }
                else if (choice.Key == ConsoleKey.UpArrow && option > 1)
                {
                    Console.CursorTop = option;
                    Console.CursorLeft = 0;
                    Console.Write(" ");
                    option--;
                    Console.CursorTop = option;
                    Console.CursorLeft = 0;
                    Console.Write(">");
                }
                else if (choice.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    if (option == 1)
                    {
                        Console.WriteLine("Difficulty set to Easy");
                        Ghostspeed = 350;
                        Thread.Sleep(100);
                        return;
                        
                        
                    }
                    else if (option == 2)
                    {
                        Console.WriteLine("Difficulty set to Normal");
                        Ghostspeed = 250;
                        Thread.Sleep(100);
                        return;
                    }
                    else if (option == 3)
                    {
                        Console.WriteLine("Difficulty set to Hard");
                        Ghostspeed = 100;
                        Thread.Sleep(100);
                        return;
                    }
                    else if (option == 4)
                    {
                        exit = true;
                        return;
                    }

                }


            } while (exit != true);

            Console.ReadKey();
        }

    }
}
