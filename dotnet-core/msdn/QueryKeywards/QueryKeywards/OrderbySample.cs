﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace QueryKeywards
{
    class OrderbySample
    {
        public void Test()
        {
            // Create a delicious data source.
            string[] fruits = { "cherry", "apple", "blueberry" };

            // Query for ascending sort.
            IEnumerable<string> sortAscendingQuery =
                from fruit in fruits
                orderby fruit //"ascending" is default
                select fruit;

            // Query for descending sort.
            IEnumerable<string> sortDescendingQuery =
                from w in fruits
                orderby w descending
                select w;

            // Execute the query.
            Console.WriteLine("Ascending:");
            foreach (string s in sortAscendingQuery)
            {
                Console.WriteLine(s);
            }

            // Execute the query.
            Console.WriteLine(Environment.NewLine + "Descending:");
            foreach (string s in sortDescendingQuery)
            {
                Console.WriteLine(s);
            }

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        public void Test1()
        {
            // Create the data source.
            List<Student> students = GetStudents();

            //Create the query
            var sortedStudents = from student in students
                                 orderby student.Last ascending, student.First ascending
                                 select student;

            // Execute the query.
            Console.WriteLine("sortedStudents:");
            foreach (Student student in sortedStudents)
            {
                Console.WriteLine(student.Last + " " + student.First);
            }

            // Now create groups and sort the groups. The query first sorts the names
            // of all students so that they will be in alphabetical order after they are
            // grouped. The second orderby sorts the group keys in alpha order.
            var sortedGroups = from student in students
                               orderby student.Last, student.First
                               group student by student.Last[0] into newGroup
                               orderby newGroup.Key
                               select newGroup;

            // Execute the query.
            Console.WriteLine(Environment.NewLine + "sortedGroups:");
            foreach (var studentGroup in sortedGroups)
            {
                Console.WriteLine(studentGroup.Key);
                foreach (var student in studentGroup)
                {
                    Console.WriteLine("   {0}, {1}", student.Last, student.First);
                }
            }
        }

        static List<Student> GetStudents()
        {
            // Use a collection initializer to create the data source. Note that each element
            //  in the list contains an inner sequence of scores.
            List<Student> students = new List<Student>
            {
                new Student { First = "Svetlana", Last = "Omelchenko", ID = 111 },
                new Student { First = "Claire", Last = "O'Donnell", ID = 112 },
                new Student { First = "Sven", Last = "Mortensen", ID = 113 },
                new Student { First = "Cesar", Last = "Garcia", ID = 114 },
                new Student { First = "Debra", Last = "Garcia", ID = 115 }
            };

            return students;
        }

        // The element type of the data source.
        class Student
        {
            public string First { get; set; }
            public string Last { get; set; }
            public int ID { get; set; }
        }
    }
}