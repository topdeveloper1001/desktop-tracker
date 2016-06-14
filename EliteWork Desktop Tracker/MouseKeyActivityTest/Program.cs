using EWActivityCatcher;
using System;
using System.Collections.Generic;
using System.Text;

namespace MouseKeyActivityTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ActivityProcessor.GetInstance().SubscribeToMouseKeyEvents();
            Console.Read();
        }
    }
}
