using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task345
{
    class Program
    {
        public static void Main(string[] args)
        {
            int[] integers = new[] { 1, 2, 2, 2, 3, 3, 4, 5 };
            //string[] strings =   ...  linq  expression  ...
            //  strings [0] = Broj 1 ponavlja  se 1 puta
            //  strings [1] = Broj 2 ponavlja  se 3 puta
            //  strings [2] = Broj 3 ponavlja  se 2 puta
            //  strings [3] = Broj 4 ponavlja  se 1 puta
            //  strings [4] = Broj 5 ponavlja  se 1 puta
        }

        void Example1()
        {
            var list = new List<Student>()
            {
                new  Student("Ivan", jmbag:"001234567")
            };
            var ivan = new Student("Ivan", jmbag: "001234567");

            // false :(
            bool anyIvanExists = list.Any(s => s == ivan);
        }

        void Example2()
        {
            var list = new List<Student>()
            {
                new  Student("Ivan", jmbag:"001234567"),
                new  Student("Ivan", jmbag:"001234567")
            };

            // 2 :(
            var distinctStudents = list.Distinct().Count();
        }

        void Example3()
        {
            //University[] universities = GetAllCroatianUniversities();
            //Student[] allCroatianStudents =   // ...
            //Student[]  croatianStudentsOnMultipleUniversities = // ...
            //Student[]  studentsOnMaleOnlyUniversities = // ...
        }
    }
}
