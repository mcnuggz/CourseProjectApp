using System;

namespace CourseProjectApp.Models.CSharp
{
    public class FirstClass
    {
        public string Value;
        private string _value;

        //properties 
        public string MainValue {
            get {return _value;}
            set { _value = value; }
        }

        public FirstClass()
        {

        }

        public FirstClass(string value)
        {
            Value = value;
        }

        //return method
        public bool TrueFalse(int number)
        {
            if (number > 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //void method

        public void NoReturn(string value)
        {
            Console.WriteLine($"This is my return value: {value}");
        }
    }
}
