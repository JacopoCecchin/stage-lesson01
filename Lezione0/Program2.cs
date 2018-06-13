using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione0
{
    class Program2
    {
        #region Inner Classes

        class Point2D
        {
            private int _x;
            private int _y;

            public int X
            {
                get { return _x; }
            }

            public int Y
            {
                get { return _y; }
            }

            public void Move(int x, int y)
            {
                _x = x;
                _y = y;
            }
        }

        class Entity : Point2D
        {
            #region Constants and Fields

            private int _speedX = 1;
            private int _speedY = 1;

            private int _width = 1;
            private int _height = 1;

            private Entity _parent;

            private string _symbol;

            #endregion

            #region Properties

            public int Width
            {
                get { return _width; }
            }

            public int Height
            {
                get { return _height; }
            }            

            public int SpeedX
            {
                //set { _speedX = value; }
                get { return _speedX; }
            }

            public int SpeedY
            {
                get { return _speedY; }
            }

            public Entity Parent
            {
                get { return _parent; }
                set { _parent = value; }
            }

            #endregion

            #region Methods

            public void SetSize(int width, int height)
            {
                _width = width;
                _height = height;
            }
            
            public void SetSpeed(int sx, int sy)
            {
                _speedX = sx;
                _speedY = sy;
            }

            public void SetSymbol(string symbol)
            {
                _symbol = symbol;
            }

            public  virtual void Update()
            {
                // Getting current coords
                int x = this.X;
                int y = this.Y;
                int aw = this.Parent.X + this.Parent.Width - 1;
                int ah = this.Parent.Y + this.Parent.Height - 1;
                // Moving logic X

                if (x + _speedX <= this.Parent.X || x + _speedX >= aw)
                {

                    if (x + _speedX >= aw)
                    {
                        x = (aw - 1) + _speedX;
                        _speedX *= -1;
                    }
                    if (x + _speedX <= this.Parent.X)
                    {
                        x = this.Parent.X + 1 + _speedX;
                        _speedX *= -1;
                    }
                }

                // Moving logic Y

                if (y + _speedY <= this.Parent.Y || y + _speedY >= ah)
                {
                    if (y + _speedY >= ah)
                    {
                        y = (ah - 1) + _speedY;
                        _speedY *= -1;
                    }
                    if (y + _speedY <= this.Parent.Y)
                    {
                        y = this.Parent.Y + 1 + _speedY;
                        _speedY *= -1;
                    }
                }

                x += _speedX;
                y += _speedY;

                // Update current coords
                Move(x, y);
            }

            public virtual void Draw()
            {
                Console.SetCursorPosition(this.X, this.Y);
                Console.Write(_symbol);
            }

            #endregion
        }       

        class Arena : Entity
        {
            public Arena(int width, int height)
            {
                if (width < 10)
                    throw new InvalidOperationException("Invalid width, use at least 10.");                

                if (height < 10)
                    throw new InvalidOperationException("Invalid height, use at least 10.");

                SetSize(width, height);
            }

            public override void Draw()
            {
                int x = this.X;
                int y = this.Y;                

                string horizontalBoundLine = new String('▓', this.Width);
                string verticalBoundLine = "▓" + new String(' ', this.Width - 2) + "▓";


                for (int i = 0; i < this.Height; i++)
                {
                    Console.SetCursorPosition(x, y);

                    if (i == 0 || i == this.Height - 1)
                        Console.WriteLine(horizontalBoundLine);
                    else
                        Console.WriteLine(verticalBoundLine);

                    y += 1;
                }
            }
        }

        class Player : Entity
        {

            public override void Update()
            {
                int x = this.X;
                int y = this.Y;
                int aw = this.Parent.X + this.Parent.Width - 1;
                int ah = this.Parent.Y + this.Parent.Height - 1;

                if (Console.KeyAvailable)
                {                    
                    var input = Console.ReadKey();

                    if (x + SpeedX <= this.Parent.X )
                    {
                        if (input.Key == ConsoleKey.UpArrow)
                        {
                            if (y - 1 > this.Parent.Y)
                                y -= this.SpeedY;
                        }

                        if (input.Key == ConsoleKey.RightArrow)
                        {
                            if (x + 1 < aw)
                                x += this.SpeedX;
                        }

                        if (input.Key == ConsoleKey.DownArrow)
                        {
                            if (y + 1 < ah)
                                y += this.SpeedY;
                        }
                    }

                    else if(x + SpeedX >= aw)
                    {
                        if (input.Key == ConsoleKey.UpArrow)
                        {
                            if (y - 1 > this.Parent.Y)
                                y -= this.SpeedY;

                            if (input.Key == ConsoleKey.DownArrow)
                            {
                                if (y + 1 < ah)
                                    y += this.SpeedY;
                            }

                            if (input.Key == ConsoleKey.LeftArrow)
                            {
                                if (x - 1 > this.Parent.X)
                                    x -= this.SpeedX;
                            }
                        }
                    }

                    else if(y + SpeedY <= this.Parent.Y)
                    {
                        if (input.Key == ConsoleKey.RightArrow)
                        {
                            if (x + 1 < aw)
                                x += this.SpeedX;
                        }

                        if (input.Key == ConsoleKey.DownArrow)
                        {
                            if (y + 1 < ah)
                                y += this.SpeedY;
                        }

                        if (input.Key == ConsoleKey.LeftArrow)
                        {
                            if (x - 1 > this.Parent.X)
                                x -= this.SpeedX;
                        }
                    }

                    else if(y + SpeedY >= ah)
                    {
                        if (input.Key == ConsoleKey.UpArrow)
                        {
                            if (y - 1 > this.Parent.Y)
                                y -= this.SpeedY;
                        }

                        if (input.Key == ConsoleKey.RightArrow)
                        {
                            if (x + 1 < aw)
                                x += this.SpeedX;
                        }

                        if (input.Key == ConsoleKey.LeftArrow)
                        {
                            if (x - 1 > this.Parent.X)
                                x -= this.SpeedX;
                        }
                    }

                    else
                    { 
                        //Move Player Up
                        if (input.Key == ConsoleKey.UpArrow)
                        {
                            if (y - 1 > this.Parent.Y)
                                y -= this.SpeedY;
                        }


                        //Move Player Right
                        if (input.Key == ConsoleKey.RightArrow)
                        {
                            if (x + 1 < aw)
                                x += this.SpeedX;
                        }

                        //Move Player Down
                        if (input.Key == ConsoleKey.DownArrow)
                        {
                            if (y + 1 < ah)
                                y += this.SpeedY;
                        }

                        //Move Player Left
                        if (input.Key == ConsoleKey.LeftArrow)
                        {
                            if (x - 1 > this.Parent.X)
                                x -= this.SpeedX;
                        }
                    }
                }

                // Update current coords
                Move(x, y);

            }            
            public override void Draw()
            {
                var color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                base.Draw();
                Console.ForegroundColor = color;
            }
        }

        class Enemy : Entity
        {

        }

        class MessageDialog : Entity
        {
            private int _screenWidth = Console.LargestWindowWidth - 20;
            private int _screenHeight = Console.LargestWindowHeight - 20;

            private string _message;

            public bool ShowMessage(string message, ConsoleKey positiveAnswer, ConsoleKey negativeAnswer)
            {
                _message = message;

                Console.SetCursorPosition((_screenWidth - _message.Length) / 2, _screenHeight / 2);
                Console.WriteLine(_message);
                
                while(true)
                {
                    var input = Console.ReadKey();

                    if (input.Key == positiveAnswer)
                        return true;

                    if (input.Key == negativeAnswer)
                        return false;
                }
            }
        }

        #endregion

        #region Constants and Fields

        private Arena _arena;
        private List<Enemy> _enemies;
        private Player _player;

        private bool _gameEnded = false;

        private int _screenWidth = Console.LargestWindowWidth - 20;
        private int _screenHeight = Console.LargestWindowHeight - 20;
        private int count = 0;

        #endregion

        #region Methods

        public void Run()
        {           
            Console.CursorVisible = false;            
            Console.SetWindowSize(_screenWidth, _screenHeight);
            Console.SetWindowPosition(0, 0);

            // Initialize arena
            _arena = new Arena(_screenWidth - 2, _screenHeight - 2);
            _arena.Move(1, 1);

            // Initialize player
            _player = new Player();            
            _player.Parent = _arena;
            _player.Move(20, 20);
            _player.SetSymbol("Ü");

            // Initialize entities
            _enemies = new List<Enemy>();
         //   _enemies.AddRange(CreateEnemies(_arena, 20, -1, 2));

            // Infinite loop            
            while (!_gameEnded)
            {
                if (count == 0)
                {
                    SetGame();
                }

                Update();

                Draw();
                                
                System.Threading.Thread.Sleep(16);

                count++;
            }            
        }

        private void Update()
        {
            // Update player
            _player.Update();

            // Update enemies
            for (int i = 0; i < _enemies.Count; i++)
                _enemies[i].Update();

            // Game logic
            bool isPlayerDead = false;
            foreach (Enemy enemy in _enemies)
            {
                if (enemy.X == _player.X && enemy.Y == _player.Y)
                {
                    isPlayerDead = true;
                    break;
                }                
            }

            if(isPlayerDead)
            {
                MessageDialog dialog = new MessageDialog();
                _gameEnded = !dialog.ShowMessage("Vuoi Continuare a Giocare? (y/n)", ConsoleKey.Y, ConsoleKey.N);
            }
        }

        private void Draw()
        {
            _arena.Draw();

            _player.Draw();

            for (int i = 0; i < _enemies.Count; i++)           
                _enemies[i].Draw();            
        }   

        private void CreateEnemies(Arena arena, int count, int minspeed, int maxspeed)
        {
            Random randomX = new Random(DateTime.Now.Second);
            Random randomY = new Random(DateTime.Now.Millisecond);

            List<int> xs = new List<int>();
            List<int> ys = new List<int>();

           // List<Enemy> enemies = new List<Enemy>();
            for (int i = 0 ; i < count ; i++)
            {
                Enemy enemy = new Enemy();

                // Set parent
                enemy.Parent = arena;

                // Set symbol
                enemy.SetSymbol("■");

                // Set first position
                int newX = randomX.Next(2, 79);
                int newY = randomY.Next(2, 25);
                bool isTaken = IsPositionTaken(newX, newY, xs, ys);
                if(isTaken)
                {
                    while(isTaken)
                    {
                        newX = randomX.Next(2, 79);
                        newY = randomY.Next(2, 25);
                        isTaken = IsPositionTaken(newX, newY, xs, ys);
                    }
                }
                
                enemy.Move(newX, newY);
                
                xs.Add(enemy.X);
                ys.Add(enemy.Y);

                //Set Speed
                enemy.SetSpeed(randomX.Next(minspeed, maxspeed), randomY.Next(minspeed, maxspeed));


                _enemies.Add(enemy);

            }

           // return enemies.ToArray();
        }

        private bool IsPositionTaken(int x, int y, List<int>xs, List<int>ys)
        {           
            for (int i = 0; i < xs.Count; i++)
            {
                if (xs[i] == x || ys[i] == y)
                {
                    return true;
                }
            }
            return false;
        }

        private void SetGame()
        {
            #region Costants and Fields
            string _message = "Scegli il livello di difficoltà(1-5)";
            Console.SetCursorPosition((_screenWidth - _message.Length) / 2, _screenHeight / 2);
            Console.WriteLine(_message);

            var input = Console.ReadKey();
            
            #endregion

            if (input.Key==ConsoleKey.D1)
            {
                CreateEnemies(_arena, 10, -1, 1);
            }

            if (input.Key == ConsoleKey.D2)
            {
                CreateEnemies(_arena, 12, -1, 1);
            }

            if (input.Key == ConsoleKey.D3)
            {
                CreateEnemies(_arena, 15, -1, 2);
                _player.SetSpeed(2, 2);
            }

            if (input.Key == ConsoleKey.D4)
            {
                CreateEnemies(_arena, 18, -1, 2);
                _player.SetSpeed(3, 3);
            }

            if (input.Key == ConsoleKey.D5)
            {
                CreateEnemies(_arena, 22, -2, 2);
                _player.SetSpeed(4, 4);
            }
        }
        #endregion
    }
}
