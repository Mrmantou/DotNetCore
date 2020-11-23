using System;
using System.Collections.Generic;
using System.Linq;

namespace QueryKeywards
{
    /// <summary>
    /// The data source referenced in the from clause must have a type of IEnumerable, IEnumerable<T>, or a derived type such as IQueryable<T>.
    /// </summary>
    class FromAndWhereSample
    {
        public void WhereSample()
        {
            // A simple data source.
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            // Simple query with one predicate in where clause.
            var queryLowNums =
                from num in numbers
                where num < 5
                select num;

            // Execute the query.
            foreach (var s in queryLowNums)
            {
                Console.Write(s.ToString() + " ");
            }

            // Create the query with two predicates in where clause.
            var queryLowNums2 =
                from num in numbers
                where num < 5 && num % 2 == 0
                select num;

            // Execute the query
            foreach (var s in queryLowNums2)
            {
                Console.Write(s.ToString() + " ");
            }
            Console.WriteLine();

            // Create the query with two where clause.
            var queryLowNums3 =
                from num in numbers
                where num < 5
                where num % 2 == 0
                select num;

            // Execute the query
            foreach (var s in queryLowNums3)
            {
                Console.Write(s.ToString() + " ");
            }

            // Create the query with a method call in the where clause.
            // Note: This won't work in LINQ to SQL unless you have a
            // stored procedure that is mapped to a method by this name.
            var queryEvenNums =
                from num in numbers
                where IsEven(num)
                select num;

            // Execute the query.
            foreach (var s in queryEvenNums)
            {
                Console.Write(s.ToString() + " ");
            }

            // Method may be instance method or static method.
            static bool IsEven(int i)
            {
                return i % 2 == 0;
            }
        }

        public void CompoundFromClauses()
        {
            // Use a collection initializer to create the data source. Note that
            // each element in the list contains an inner sequence of scores.
            List<Student> students = new List<Student>
            {
                new Student { LastName = "Omelchenko", Scores = new List<int> { 97, 72, 81, 60 } },
                new Student { LastName = "O'Donnell", Scores = new List<int> { 75, 84, 91, 39 } },
                new Student { LastName = "Mortensen", Scores = new List<int> { 88, 94, 65, 85 } },
                new Student { LastName = "Garcia", Scores = new List<int> { 97, 89, 85, 82 } },
                new Student { LastName = "Beebe", Scores = new List<int> { 35, 72, 91, 70 } }
            };

            // Use a compound from to access the inner sequence within each element.
            // Note the similarity to a nested foreach statement.
            var scoreQuery = from student in students
                             from score in student.Scores
                             where score > 90
                             select new { Last = student.LastName, score };

            // Rest the mouse pointer on scoreQuery in the following line to
            // see its type. The type is IEnumerable<'a>, where 'a is an
            // anonymous type defined as new {string Last, int score}. That is,
            // each instance of this anonymous type has two members, a string
            // (Last) and an int (score).
            foreach (var student in scoreQuery)
            {
                Console.WriteLine("{0} Score: {1}", student.Last, student.score);
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            // The element type of the data source.

        }

        public void UsingMultipleFromClausesToPerformJoins()
        {
            char[] upperCase = { 'A', 'B', 'C' };
            char[] lowerCase = { 'x', 'y', 'z' };

            // The type of joinQuery1 is IEnumerable<'a>, where 'a
            // indicates an anonymous type. This anonymous type has two
            // members, upper and lower, both of type char.
            var joinQuery1 = from upper in upperCase
                             from lower in lowerCase
                             select new { upper, lower };

            // The type of joinQuery2 is IEnumerable<'a>, where 'a
            // indicates an anonymous type. This anonymous type has two
            // members, upper and lower, both of type char.
            var joinQuery2 = from lower in lowerCase
                             where lower != 'x'
                             from upper in upperCase
                             select new { lower, upper };

            // Execute the queries.
            Console.WriteLine("Cross join:");
            // Rest the mouse pointer on joinQuery1 to verify its type.
            foreach (var pair in joinQuery1)
            {
                Console.WriteLine("{0} is matched to {1}", pair.upper, pair.lower);
            }

            Console.WriteLine("Filtered non-equijoin:");
            // Rest the mouse pointer over joinQuery2 to verify its type.
            foreach (var pair in joinQuery2)
            {
                Console.WriteLine("{0} is matched to {1}", pair.lower, pair.upper);
            }

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        class Student
        {
            public string LastName { get; set; }
            public List<int> Scores { get; set; }
        }
    }
}
