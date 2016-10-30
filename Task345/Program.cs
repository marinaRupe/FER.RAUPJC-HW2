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
            Task3();
            Task4();
            Task5();
        }

        public static void Task3()
        {
            int[] integers = new[] {1, 2, 2, 2, 3, 3, 4, 5};

            string[] strings = integers.GroupBy(i => i)
                                        .Select(group => $"Broj {group.Key} ponavlja se {group.Count()} puta")
                                        .ToArray();

            Console.WriteLine(strings[0]);
            Console.WriteLine(strings[1]);
            Console.WriteLine(strings[2]);
            Console.WriteLine(strings[3]);
            Console.WriteLine(strings[4]);
        }


        public static void Task4()
        {
            Example1();
            Example2();
        }


        public static void Task5()
        {
            University[] universities = GetAllCroatianUniversities();

            Student [] allCroatianStudents = universities.SelectMany(u => u.Students)
                                                          .Distinct()
                                                          .OrderBy(s => s.Jmbag)
                                                          .ToArray();

            //Student[]  croatianStudentsOnMultipleUniversities = universities.

            University[] femaleOnlyOrMixedGenderUniversities = universities.Where(u => u.Students.Any(s => s.Gender.Equals(Gender.Female))).ToArray();

            Student[] studentsOnMaleOnlyUniversities = universities.Except(femaleOnlyOrMixedGenderUniversities)
                .SelectMany(u => u.Students)
                .Distinct()
                .OrderBy(s => s.Jmbag)
                .ToArray();
        }


        public static void Example1()
        {
            var list = new List<Student>()
            {
                new Student("Ivan", jmbag: "001234567")
            };
            var ivan = new Student("Ivan", jmbag: "001234567");

            bool anyIvanExists = list.Any(s => s == ivan);
            Console.WriteLine(anyIvanExists);
        }


        public static void Example2()
        {
            var list = new List<Student>()
            {
                new Student("Ivan", jmbag: "001234567"),
                new Student("Ivan", jmbag: "001234567")
            };

            var distinctStudents = list.Distinct().Count();
            Console.WriteLine(distinctStudents);
        }


        public static University[] GetAllCroatianUniversities()
        {
            string[] universityNames = {"Fakultet elektrotehnike i računarstva",
                                        "Prirodoslovno-matematički fakultet",
                                        "Katolički bogoslovni fakultet",
                                        "Fakultet strojarstva i brodogradnje",
                                        "Kineziološki fakultet",
                                        "Medicinski fakultet u Zagrebu",
                                        "Fakultet kemijskog inženjerstva i tehnologije u Zagrebu",
                                        "Fakultet političkih znanosti u Zagrebu",
                                        "Fakultet prometnih znanosti u Zagrebu",
                                        "Veterinarski fakultet u Zagrebu",
                                        "Učiteljski fakultet"
            };

            University[] universities = new University[universityNames.Length];
            Student[] students = GenerateStudents();

            for (var i = 0; i < universityNames.Length; i++)
            {
                universities[i] = new University
                {
                    Name = universityNames[i],
                    Students = GetRandomStudents(students, 4)
                };

                /*
                Console.WriteLine(universities[i].Name);
                foreach (Student t in universities[i].Students)
                {
                    Console.WriteLine(t.Name + ", " + t.Gender);
                }
                Console.WriteLine();
                */
            }

            return universities;
        }


        static Student[] GetRandomStudents(Student[] studentList, int numberOfStudents)
        {
            var rnd = new Random();
            var randomStudents = studentList.OrderBy(user => rnd.Next()).Take(numberOfStudents).ToArray();

            return randomStudents;
        }


        public static Student[] GenerateStudents()
        {
            string[] listOfStudents =
            {
                "Dalija, ž",
                "Mak, m",
                "Dunja, ž",
                "Ivona, ž",
                "Ema, ž",
                "Ena, ž",
                "Luka, m",
                "Sergej, m",
                "Marijana, ž",
                "Arijana, ž",
                "Danilo, m",
                "Ana, ž",
                "Arian, m",
                "Isak, m",
                "Jelena, ž",
                "Marko, m",
                "Marija, ž",
                "Milica, ž",
                "Arijan, m",
                "Alisa, ž",
                "Nikola, m",
                "Leon, m",
                "Ivana, ž",
                "Filip, m",
                "Anika, ž",
                "Mia, ž",
                "Dimitrije, m",
                "Jakša, m",
                "Mara, ž",
                "Marina, ž",
                "Petar, m",
                "Ivan, m",
                "Leon, ž",
                "Mateo, m",
                "Katarina, m",
                "Marin, m",
                "Matej, m",
                "Damir, m",
                "Lovorka, ž",
                "Tanja, ž",
                "Vanja, ž",
                "Jasmina, ž",
                "Dimitra, ž",
                "Boris, m",
                "Petra, ž",
                "Magdalena, ž",
                "Nina, ž",
                "Nika, ž",
                "Nela, ž",
                "David, m",
                "Lana, ž",
                "Maris, m",
                "Aleksandar, m",
                "Maja, ž",
                "Stefan, m",
                "Elvira, ž",
                "Ada, ž",
                "Milan, m",
                "Marta, ž",
                "Alen, m",
                "Lara, ž",
                "Neda, ž",
                "Matija, m",
                "Kira, ž",
                "Andrej, m",
                "Maša, ž",
                "Tamara, ž",
                "Dejan, m",

            };

            Student[] students = new Student[listOfStudents.Length];

            for (var i = 0; i < listOfStudents.Length; i++)
            {
                var name = listOfStudents[i].Split(',')[0].Trim();
                var jmbag = (i + 10000).ToString();
                var gender = listOfStudents[i].Split(',')[1].Trim();

                students[i] = new Student(name, jmbag)
                {
                    Gender = gender == "ž" ? Gender.Female : Gender.Male
                };
            }
            return students;
        }
        
    }
}
