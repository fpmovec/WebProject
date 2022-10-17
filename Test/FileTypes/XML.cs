using System;

namespace Test.FileTypes
{
    public class Xml: IFileTypes
    {
        public void Type()
        {
            Console.WriteLine("I'm a XML file!");
        }
    }
}