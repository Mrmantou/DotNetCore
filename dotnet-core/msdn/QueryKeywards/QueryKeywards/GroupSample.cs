using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace QueryKeywards
{
    class GroupSample
    {
        // The element type of the data source.
        public class Student
        {
            public string First { get; set; }
            public string Last { get; set; }
            public int ID { get; set; }
            public List<int> Scores;
        }

        public static List<Student> GetStudents()
        {
            // Use a collection initializer to create the data source. Note that each element
            //  in the list contains an inner sequence of scores.
            List<Student> students = new List<Student>
            {
                new Student { First = "Svetlana", Last = "Omelchenko", ID = 111, Scores = new List<int> { 97, 72, 81, 60 } },
                new Student { First = "Claire", Last = "O'Donnell", ID = 112, Scores = new List<int> { 75, 84, 91, 39 } },
                new Student { First = "Sven", Last = "Mortensen", ID = 113, Scores = new List<int> { 99, 89, 91, 95 } },
                new Student { First = "Cesar", Last = "Garcia", ID = 114, Scores = new List<int> { 72, 81, 65, 84 } },
                new Student { First = "Debra", Last = "Garcia", ID = 115, Scores = new List<int> { 97, 89, 85, 82 } }
            };

            return students;
        }

        public void Test()
        {
            // Obtain the data source.
            List<Student> students = GetStudents();

            // Query variable is an IEnumerable<IGrouping<char, Student>>
            var studentQuery1 = from student in students
                                group student by student.Last[0];

            // Group students by the first letter of their last name
            // Query variable is an IEnumerable<IGrouping<char, Student>>
            var studentQuery2 = from student in students
                                group student by student.Last[0] into g
                                orderby g.Key
                                select g;

            // Iterate group items with a nested foreach. This IGrouping encapsulates
            // a sequence of Student objects, and a Key of type char.
            // For convenience, var can also be used in the foreach statement.
            foreach (IGrouping<char, Student> studentGroup in studentQuery2)
            {
                Console.WriteLine(studentGroup.Key);
                // Explicit type for student could also be used here.
                foreach (var student in studentGroup)
                {
                    Console.WriteLine($"    {student.Last}, {student.First}");
                }
            }

            // Same as previous example except we use the entire last name as a key.
            // Query variable is an IEnumerable<IGrouping<string, Student>>
            var studentQuery3 = from student in students
                                group student by student.Last;

            // Group by true or false.
            // Query variable is an IEnumerable<IGrouping<bool, Student>>
            var booleanGroupQuery = from student in students
                                    group student by student.Scores.Average() >= 80; //pass or fail!

            // Execute the query and access items in each group
            foreach (var studentGroup in booleanGroupQuery)
            {
                Console.WriteLine(studentGroup.Key == true ? "High averages" : "Low averages");
                foreach (var student in studentGroup)
                {
                    Console.WriteLine($"    {student.Last}, {student.First}:{student.Scores.Average()}");
                }
            }

            // This method groups students into percentile ranges based on their
            // grade average. The Average method returns a double, so to produce a whole
            // number it is necessary to cast to int before dividing by 10.
            var studentQuery4 = from student in students
                                let avg = (int)student.Scores.Average()
                                group student by (avg / 10) into g
                                orderby g.Key
                                select g;

            foreach (var studentGroup in studentQuery4)
            {
                int temp = studentGroup.Key * 10;
                Console.WriteLine($"Students with an average between {temp} and {temp + 10}");
                foreach (var student in studentGroup)
                {
                    Console.WriteLine($"    {student.Last}, {student.First}:{student.Scores.Average()}");
                }
            }

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        public void Test1()
        {
            // Create a data source.
            string[] words = { "blueberry", "chimpanzee", "abacus", "banana", "apple", "cheese" };

            // Create the query.
            var wordGroups = from w in words
                             group w by w[0];

            // Execute the query.
            foreach (var wordGroup in wordGroups)
            {
                Console.WriteLine("Words that start with the letter '{0}':", wordGroup.Key);
                foreach (var word in wordGroup)
                {
                    Console.WriteLine(word);
                }
            }

            // Create the data source.
            string[] words2 = { "blueberry", "chimpanzee", "abacus", "banana", "apple", "cheese", "elephant", "umbrella", "anteater" };

            // Create the query.
            var wordGroups2 = from w in words2
                              group w by w[0] into grps
                              where grps.Key == 'a' || grps.Key == 'e' || grps.Key == 'i' || grps.Key == 'o' || grps.Key == 'u'
                              select grps;

            // Execute the query.
            foreach (var wordGroup in wordGroups2)
            {
                Console.WriteLine("Groups that start with a vowel: {0}", wordGroup.Key);
                foreach (var word in wordGroup)
                {
                    Console.WriteLine("   {0}", word);
                }
            }
        }
    }
}
