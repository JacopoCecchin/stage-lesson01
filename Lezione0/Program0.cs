using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione0
{
    class DummySerializer<T>
    {
        private List<T> _list= new List<T>();
        
        public DummySerializer()
        {

        }

        public void AddItem(T item)
        {
            _list.Add(item);
        }

        public string ToStringData()
        {
            string data = string.Empty;
            for (int i = 0; i < _list.Count; i++)
            {
                T x = _list[i];
                string k = x.ToString();
                data += k;
                if (i < _list.Count - 1)
                    data += "|";
            }
            return data;
        }
    }


    class Program0
    {

        static void Main(string[] args)
        {
            //Program0 First = new Program0();
            //First.Run();

            Program2 p = new Program2();
            p.Run();                     
        }

        public void Run()
        {
            DummySerializer<int> a = new DummySerializer<int>();
            for (int i = 0; i < 11; i++)
                a.AddItem(i);

            Console.WriteLine(a.ToStringData());

            DummySerializer<double> b = new DummySerializer<double>();
            for (int j = 0; j < 21; j++)            
                b.AddItem(j * 1.23248);
            
            Console.WriteLine(b.ToStringData());

            DummySerializer<string> c = new DummySerializer<string>();
            for (int k = 0; k < 31; k++)            
                c.AddItem(Convert.ToString(k));
            
            c.ToStringData();
            Console.WriteLine(c.ToStringData());


        }
    }
}





