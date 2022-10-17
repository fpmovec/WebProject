using System;

namespace Test.FileTypes
{
    public class Txt: IFileTypes
    {
        public void Type()
        {
            Console.WriteLine("I'm a TXT file!");
        }
    }
}