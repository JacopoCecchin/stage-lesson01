using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione0
{
    class Program1
    {
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

            public Point2D GetCoordinates()
            {
                Point2D p = new Point2D();
                p.Move(_x, _y);
                return p;
            }
        }

        class Shape2D : Point2D
        {
            private int _width;
            private int _height;

            public void SetSize(int width, int height)
            {
                _width = width;
                _height = height;
            }

            protected int GetWidth()
            {
                return _width;
            }

            protected int GetHeight()
            {
                return _height;
            }
        }

        class Sprite2D : Shape2D
        {
            protected string _image;

            public void SetImage(string image)
            {
                _image = image;
            }
        }

        public void Run()
        {
            Sprite2D sprite = new Sprite2D();
            sprite.SetSize(100, 100);
            sprite.SetImage(@"C:\Develop\MyGame\Textures\sprite.png");
            sprite.Move(50, 50);                                                                  
        }

        public void MySpecialMethod()
        {
            List<Point2D> points = new List<Point2D>();
            points.Add(new Point2D());
            points.Add(new Shape2D());
            points.Add(new Sprite2D());
            points.Add(new Shape2D());
            points.Add(new Point2D());

            Random random = new Random();
            foreach (Point2D p in points)
            {
                int x=random.Next(1,11);
                int y= random.Next(1, 11);
                p.Move(x, y);
            }
        }
    }
}
