using System;
using System.Collections.Generic;
using System.Text;
//Serializable;
namespace ConsoleApp1
{
    class shi
    {
        public shi() { }
        public shi(string name)
        {
            this.name = name;
        }
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
        }
       public void SayHi()
        {
            Console.WriteLine("プレイヤ{0}胜利!!!", name);
        }
    }
}
