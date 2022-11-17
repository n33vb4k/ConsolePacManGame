using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



namespace NEA2
{
    class Maze
    {
        private int rowcount;
        private int colcount;
        private string mazefile;
        private Pacman pac;
        private Blinky blink;
        private Clyde cly;
        private Inky ink;
        private Pinky pink;
        private KillBerry kb1 = new KillBerry();
        private KillBerry kb2 = new KillBerry();
        private KillBerry kb3 = new KillBerry();
        private KillBerry kb4 = new KillBerry();
        public int finalscore;

        private Cell[,] Cells;
        public Maze(int noofrows, int noofcols, string mazef, Pacman P, Blinky B, Clyde C, Inky I, Pinky Pi)
        {
            pac = P;
            blink = B;
            cly = C;
            ink = I;
            pink = Pi;
            mazefile = mazef;
            rowcount = noofrows;
            colcount = noofcols;
            Cells = new Cell[rowcount, colcount];
            kb1.row = 1;
            kb1.col = 1;
            kb2.row = 1;
            kb2.col = 26;
            kb3.row = 29;
            kb3.col = 1;
            kb4.row = 29;
            kb4.col = 26;



            for (int row = 0; row < rowcount; row++)
            {
                for (int col = 0; col < colcount; col++)
                {
                    Cells[row, col] = new Cell();
                }
            }
            LoadMazeIntoCellArray();
        }

        public string GetMazeFile()
        {
            return mazefile;
        }
        public string[,] LoadMazeIntoAccurateGrid()
        {
            string filename = mazefile;
            string[,] grid = new string[31, 29];
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                int rowcount = 0;
                while (sr.EndOfStream == false)
                {
                    line = sr.ReadLine();
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] == ' ')
                        {
                            grid[rowcount, i] = " ";
                        }
                        else if (line[i] == '#')
                        {
                            grid[rowcount, i] = "#";
                        }
                        else if (line[i] == '-')
	                    {
                            grid[rowcount, i] = "-";
	                    }

                    }
                    rowcount++;
                }
            }
            return grid;
        }
        public string[,] LoadMazeIntoGrid()  //loads maze from text file into an array of strings
        {
            string filename = mazefile;
            string[,] grid = new string[31, 29];
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                int rowcount = 0;
                while (sr.EndOfStream == false)
                {
                    line = sr.ReadLine();
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] == ' ' || line[i] == '-')
                        {
                            grid[rowcount, i] = " ";
                        }
                        else if (line[i] == '#')
                        {
                            grid[rowcount, i] = "#";
                        }

                    }
                    rowcount++;
                }
            }
            return grid;
        }
        public void LoadMazeIntoCellArray() //formats the array of cells created to correct walldown/wallright, this is where the maze is actually created
        {           
            string[,] grid = LoadMazeIntoGrid();

            for (int row = 1; row < rowcount - 1; row++)
            {
                for (int col = 1; col < colcount - 1; col++)
                {
                    if (grid[row, col] == " ")
                    {
                        if (grid[row, col + 1] == " ")
                        {
                            Cells[row, col].WallRight = false;
                        }
                        if (grid[row, col - 1] == " ")
                        {
                            Cells[row, col - 1].WallRight = false;
                        }
                        if (grid[row + 1, col] == " ")
                        {
                            Cells[row, col].WallDown = false;
                        }
                        if (grid[row - 1, col] == " ")
                        {
                            Cells[row - 1, col].WallDown = false;
                        }
                    }

                }
            }
            //ghosts can pass through only
            Cells[11, 13].WallDown = true;
            Cells[11, 14].WallDown = true;

        }

        public void BreakWall(int row1, int col1, int row2, int col2) //if a wall needs to be broken
        {
            if (row1 < row2 && row1 < rowcount - 1)
            {
                Cells[row1, col1].WallDown = false;
            }
            else if (row2 < row1 && row2 < rowcount - 1)
            {
                Cells[row2, col2].WallDown = false;
            }
            if (col1 < col2 && col1 < colcount - 1)
            {
                Cells[row1, col1].WallRight = false;
            }
            else if (col2 < col1 && col2 < colcount - 1)
            {
                Cells[row2, col2].WallRight = false;
            }
        }


        //♠
        //♦
        //◕
        //ᗧ
        //ᗣ
        //displays maze to console
        public void Display2() 
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.SetCursorPosition(0, 0);
            string output = "";
            for (int row = 0; row < rowcount; row++)
            {
                for (int col = 0; col < colcount; col++)
                {
                    if (col == pac.GetCol() && row == pac.GetRow()) //output pacman character at his position
                    {
                        output += "ᗧ";
                    }
                    else if (col == blink.GetCol() && row == blink.GetRow() || col == cly.GetCol() && row == cly.GetRow() || col == ink.GetCol() && row == ink.GetRow() || col == pink.GetCol() && row == pink.GetRow()) //output ghost character at their positions
                    {
                        output += "ᗣ";
                    }
                    else if (col == kb1.col && row == kb1.row && kb1.visible == true || col == kb2.col && row == kb2.row && kb2.visible == true || col == kb3.col && row == kb3.row && kb3.visible == true || col == kb4.col && row == kb4.row && kb4.visible == true)
                    {
                        output += "◕";
                    }
                    else
                    {
                        if (Cells[row, col].WallDown) //if theres a wall down a floor will be displayed
                        {
                            output += "_";
                        }
                        else
                        {
                            output += " ";
                        }
                    }
                    if (Cells[row, col].WallRight) //if there is a wall to the right a wall will be displayed
                    {
                        output += "║";
                    }
                    else
                    {
                        output += " ";
                    }
                }
                output += "\n";
            }
            
            output += $"score: {pac.score}";
            output += "\n";
            output += $"lives remaining: {pac.lives}";
            output += "\n";
            output += "use wasd to move, e to exit";

            WriteStringWithColoredLetter(output, 'ᗧ');
            
        }

        public void WriteStringWithColoredLetter(string maze, char p) //allows to write certain things with colour
        {
            int o = maze.IndexOf(p);
            o = maze.IndexOf(p);
            o = maze.IndexOf(p);
            Console.Write(maze.Substring(0, o));
            if (pac.kill == true)
	        {
                Console.ForegroundColor = ConsoleColor.Blue;
	        }
            else
	        {
                Console.ForegroundColor = ConsoleColor.Yellow;
	        }          
            Console.Write(maze[o]);
            Console.ResetColor();
            Console.WriteLine(maze.Substring(o + 1));
        }

        static int kbtimer = 0;

        public bool ValidMoveAndTP(char wasd) //Checks if the move pacman is trying to make is actually valid and if he is going through a teleport hole
        {
            //teleport hole in maze
            if (mazefile == "mazewalls1.txt")
            {
                if (pac.GetRow() == 14 && pac.GetCol() == 0)
                {
                    if (wasd == 'a')
                    {
                        pac.TpRight();
                        return false;
                    }
                }
                if (pac.GetRow() == 14 && pac.GetCol() == 27)
                {
                    if (wasd == 'd')
                    {
                        pac.TpLeft();
                        return false; ;
                    }
                }
            }
            else
            {
                if ((pac.GetRow() == 13 && pac.GetCol() == 0) || (pac.GetRow() == 16 && pac.GetCol() == 0))
                {
                    if (wasd == 'a')
                    {
                        pac.TpRight();
                        return false;
                    }
                }
                if ((pac.GetRow() == 13 && pac.GetCol() == 27) || (pac.GetRow() == 16 && pac.GetCol() == 27))
                {
                    if (wasd == 'd')
                    {
                        pac.TpLeft();
                        return false; ;
                    }
                }

            }

            //checking if berry has been eaten
            if (pac.GetCol() == kb1.col && pac.GetRow() == kb1.row && kb1.visible)
            {
                kbEaten(ref kb1);
            }
            else if (pac.GetCol() == kb2.col && pac.GetRow() == kb2.row && kb2.visible)
            {
                kbEaten(ref kb2);
            }
            else if (pac.GetCol() == kb3.col && pac.GetRow() == kb3.row && kb3.visible)
            {
                kbEaten(ref kb3);
            }
            else if (pac.GetCol() == kb4.col && pac.GetRow() == kb4.row && kb4.visible)
            {
                kbEaten(ref kb4);
            }

            if (pac.kill == true)
            {
                if (kbtimer == 30)
                {
                    Revertkb();
                }              
            }

            //checking if move is actually valid 
            switch (wasd)
            {
                case 'w':
                    if (pac.GetRow() - 1 >= 0 && !Cells[pac.GetRow() - 1, pac.GetCol()].WallDown) return true;
                    break;
                case 's':
                    if (pac.GetRow() + 1 < rowcount && !Cells[pac.GetRow(), pac.GetCol()].WallDown) return true;
                    break;
                case 'd':
                    if (pac.GetCol() + 1 < colcount && !Cells[pac.GetRow(), pac.GetCol()].WallRight) return true;
                    break;
                case 'a':
                    if (pac.GetCol() - 1 >= 0 && !Cells[pac.GetRow(), pac.GetCol() - 1].WallRight) return true;
                    break;
            }
            return false;
        }
        
        public void Move(char wasd)
        {
            kbtimer++;
            if (ValidMoveAndTP(wasd))
            {
                //responding to the user input and moving pacman if its possible
                switch (wasd)
                {
                    case 'w':
                        pac.MoveUp();
                        break;
                    case 's':
                        pac.MoveDown();
                        break;
                    case 'a':
                        pac.MoveLeft();
                        break;
                    case 'd':
                        pac.MoveRight();
                        break;
                }
            }
        }
        public void Revertkb()
        {
            pac.kill = false;
            pink.scatter = false;
            ink.scatter = false;
            cly.scatter = false;
            blink.scatter = false;
        }
        
        public void kbEaten(ref KillBerry kb)
        {
            kbtimer = 0;
            kb.visible = false;
            pac.kill = true;
            pink.scatter = true;
            ink.scatter = true;
            cly.scatter = true;
            blink.scatter = true;              
        }

        public bool CheckHit() //Checking if pacman has been hit by a ghost or if he has killed a ghost
        {
            if (pac.kill == true)
            {
                if (pac.GetRow() == blink.row && pac.GetCol() == blink.col)
                {
                    blink.ResetPos();
                    pac.score += 100;
                }
                else if (pac.GetRow() == cly.row && pac.GetCol() == cly.col)
                {
                    cly.ResetPos();
                    pac.score += 100;
                }
                else if (pac.GetRow() == ink.row && pac.GetCol() == ink.col)
                {
                    ink.ResetPos();
                    pac.score += 100;
                }
                else if (pac.GetRow() == pink.row && pac.GetCol() == pink.col)
                {
                    pink.ResetPos();
                    pac.score += 100;
                }
                return false;
            }
            
            if ((pac.GetRow() == blink.row && pac.GetCol() == blink.col) || (pac.GetRow() == cly.row && pac.GetCol() == cly.col) || (pac.GetRow() == ink.row && pac.GetCol() == ink.col) || (pac.GetRow() == pink.row && pac.GetCol() == pink.col))
            {
                
                pac.ResetPos();
                blink.ResetPos();
                cly.ResetPos();
                ink.ResetPos();
                pink.ResetPos();
                pac.score -= 100;
                pac.lives--;              
                
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public bool CheckEnd() //checking if the game is over
        {
            if (!pac.kill && ((pac.GetRow() == blink.row && pac.GetCol() == blink.col) || (pac.GetRow() == cly.row && pac.GetCol() == cly.col) || (pac.GetRow() == ink.row && pac.GetCol() == ink.col) || (pac.GetRow() == pink.row && pac.GetCol() == pink.col)))
            {
                if (pac.lives < 2 )
                {
                    finalscore = pac.score;
                    return true;
                }               
            }
            return false;
            
        }

        
        public List<Cell> WalkableCells(Cell current, Cell target) //creates a list of cells that the current cell can move too
        {
            string[,] grid = LoadMazeIntoGrid();
            List<Cell> Possible = new List<Cell>()
            {
                new Cell {Col = current.Col, Row = current.Row-1, parent = current, G = current.G++},
                new Cell {Col = current.Col, Row = current.Row+1, parent = current, G = current.G++},
                new Cell {Col = current.Col-1, Row = current.Row, parent = current, G = current.G++},
                new Cell {Col = current.Col+1, Row = current.Row, parent = current, G = current.G++}
            };

            for (int i = 0; i < Possible.Count; i++)
            {
                Possible[i].CalculateH(target.Col, target.Row);
            }

            foreach (Cell cells in Possible.ToList())
            {
                if (cells.Col > colcount || cells.Col < 0)
                {
                    Possible.Remove(cells); //remove cells outside maze
                }
                else if (cells.Row > rowcount || cells.Row < 0)
                {
                    Possible.Remove(cells); //remove cells outside maze
                }
                else if (grid[cells.Row, cells.Col] != " ")
                {
                    Possible.Remove(cells); //remove cells which are a wall
                }
            }
            return Possible;
        }
        
                   
        public int clycount = 0;
       //add so that when clycount mod something then clys target changes
        public Cell GetTarget(Ghost ghost)
        {
            
            pac.score += 1; //pacman score goes up 1 every ghost move
            clycount++;
            Random rnd = new Random();
            Cell target = new Cell();
            int c = 0;
            int r = 0;
            string[,] grid = LoadMazeIntoAccurateGrid();
            if (pac.kill == false)
            {
                if (ghost == blink) //blink pathfinds straigh to pacman
                {
                    c = pac.GetCol();
                    r = pac.GetRow();
                }
                else if (ghost == cly) //cly moves randomly
                {
                    if (clycount % 5 != 0)
                    {
                        do
                        {
                            r = rnd.Next(1, 31);
                            c = rnd.Next(1, 28);
                        } while (grid[r, c] == "#" || grid[r, c] == "-");
                    }
                    else
                    {
                        c = pac.GetCol();
                        r = pac.GetRow();
                    }
                }
                else if (ghost == ink) //pathfinds to the right of pacman
                {
                    int upperb;
                    if (pac.GetCol() + 8 > 28)
                    {
                        upperb = 28;
                    }
                    else
                    {
                        upperb = pac.GetCol() + 8;
                    }
                    r = pac.GetRow();
                    do
                    {
                        c = rnd.Next(pac.GetCol(), upperb);
                    } while (grid[r, c] == "#" || grid[r, c] == "-");

                }
                else if (ghost == pink) //pathfinds below pacman
                {
                    int lowerb;
                    if (pac.GetRow() - 8 < 1)
                    {
                        lowerb = 1;
                    }
                    else
                    {
                        lowerb = pac.GetRow() - 8;
                    }
                    c = pac.GetCol();
                    do
                    {
                        r = rnd.Next(lowerb, pac.GetRow() + 1);
                    } while (grid[r, c] == "#" || grid[r, c] == "-");
                }
            }
            else
            {
                if (ghost == blink)
                {
                    c = 1;
                    r = 1;
                }
                else if (ghost == ink)
                {
                    r = 1;
                    c = 26;
                }
                else if (ghost == pink)
                {
                    r = 29;
                    c = 1;
                }
                else if (ghost == cly)
                {
                    r = 29;
                    c = 26;                   
                }
            }

            

            target.Row = r;
            target.Col = c;
            return target;
        }

        //add target cell as parameter
        public void MoveGhostTrial(Ghost ghost, Cell t) //movement for pathfinding ghosts
        {
            Cell g = new Cell
            {
                Col = ghost.GetCol(),
                Row = ghost.GetRow()
            };
            
            g.CalculateH(t.Col, t.Row);
            List<Cell> open = new List<Cell>(); //list of nodes that are being visited/need to be visited
            List<Cell> closed = new List<Cell>(); //list of nodes already visited
            open.Add(g);
            while (open.Count != 0)
            {
                Cell check = open.OrderBy(x => x.F).First();
                if (check.Col == t.Col && check.Row == t.Row)
                {
                    Cell path = check;
                    while (path.parent != null)
                    {
                        if (path.Col != t.Col || path.Row != t.Row)
                        {
                            
                            g.Row = path.Row;
                            g.Col = path.Col;
                            ghost.col = path.Col;
                            ghost.row = path.Row;                           
                        }
                        path = path.parent;
                    }
                    
                    break;
                }
                closed.Add(check);
                open.Remove(check);
                List<Cell> Walkable = WalkableCells(check, t);
                foreach (Cell WalkableCell in Walkable)
                {
                    if (closed.Any(x => x.Col == WalkableCell.Col && x.Row == WalkableCell.Row)) continue; //ignores it if its in the closed list
                    if (open.Any(x => x.Col == WalkableCell.Col && x.Row == WalkableCell.Row)) //if node is in list of visiting
                    {
                        Cell existing = open.First(x => x.Col == WalkableCell.Col && x.Row == WalkableCell.Row);
                        if (existing.F > check.F) //if current node has a lower F value than the same node on a different path
                        {
                            open.Remove(existing);
                            open.Add(WalkableCell);
                        }
                    }
                    else
                    {
                        open.Add(WalkableCell);
                    }
                }
            }

            if ((g.Col + 1 == t.Col && g.Row == t.Row) || (g.Col - 1 == t.Col && g.Row == t.Row) || (g.Col == t.Col && g.Row == t.Row + 1) || (g.Col == t.Col && g.Row == t.Row - 1))
            {
                ghost.col = t.Col;
                ghost.row = t.Row;
                g.Col = t.Col;
                g.Row = t.Row;                
            }

        }

        
        
        

            
        
    }
}
