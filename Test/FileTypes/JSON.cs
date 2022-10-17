using System;

namespace Test.FileTypes
{
    public class Json: IFileTypes
    {
        public void Type()
        {
            Console.WriteLine("I'm a JSON file!");
        }
    }
}