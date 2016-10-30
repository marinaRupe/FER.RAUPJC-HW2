using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class DuplicateTodoItemException : Exception
    {
        public DuplicateTodoItemException()
        {   
        }
        public DuplicateTodoItemException(string message) : base(message)
        {
        }
    }
}
