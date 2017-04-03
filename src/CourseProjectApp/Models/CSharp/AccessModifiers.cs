using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectApp.Models.CSharp
{
    public class AccessModifiers
    {
        //public modifier
        public string PublicString { get; set; }
        
        public void PublicMethod()
        {

        }

        //private
        private string privateString { get; set; }
        private void PrivateMethod()
        {

        }

        //protected
        protected int Number { get; set; }
        protected void ProtectedMethod()
        {

        }
    }

    public class Access : AccessModifiers
    {
        public Access()
        {
            Number = 15;
            ProtectedMethod();
        }
    }
}
