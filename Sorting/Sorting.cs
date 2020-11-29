using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Sorting
{
    public class Sorting
    {
        // Regex pattern to only filter letters from a given string (lower- and upper-case) 
        private const string RegexPattern = @"[^A-Za-z]";

        public string InputString { get; set; }
        public string FilteredString { get; set; }
        public string OutputString { get; set; }

        public Sorting()
        {
        }
        
        public Sorting(string inputString)
        {
            InputString = inputString;
        }

        // Filters out a string based off the given regex pattern
        public string FilterOut(string regexPattern)
        {
            var regex = new Regex(regexPattern);
            var filteredString = regex.Replace(InputString, "");
            
            return filteredString;
        }

        // Sorts the given string alphabetically and returns single sorted string
        public string[] SortLowerCaseFilteredString(string lowerCaseFilteredString)
        {
            // Split the string into a string array
            var inputStringArray = Regex.Split(lowerCaseFilteredString, string.Empty);

            // Remove extra empty string characters
            inputStringArray = inputStringArray.Where(s => s != string.Empty).ToArray();

            // Sort the array
            Array.Sort(inputStringArray);

            return inputStringArray;
        }

        // Concatenates a given string array into one string
        public string ConcatenateGivenString(string[] sortedStringArray)
        {
            var sortedOutputAsOneString = sortedStringArray.Aggregate("", (current, s) => current + s);
            return sortedOutputAsOneString;
        }

        public static void Main()
        {
            Console.WriteLine("Please enter a sentence:");
            //var input = "thisisatest";//"Th1s 1s 4 t3st! Th3 5tr1ng 5h0uldn't h4v3 numb3r5 4ft3r th15. D1d 1t w0rk? 1f n0t, call m3 0n 0123456789 or email me at treshpillay@gmail.com.";
            var input = Console.ReadLine();
            
            var sorting = new Sorting(input);
            
            sorting.FilteredString = sorting.FilterOut(RegexPattern);
            sorting.FilteredString = sorting.FilteredString.ToLower();

            var sortedStringArray = sorting.SortLowerCaseFilteredString(sorting.FilteredString);

            sorting.OutputString = sorting.ConcatenateGivenString(sortedStringArray);

            Console.Clear();

            Console.WriteLine($"Input:\n------\n{input}\n");
            Console.WriteLine($"Output:\n------\n{sorting.OutputString}");
            
            Console.WriteLine("\nPress any key to quit.");
            Console.ReadKey();
        }
    }
}
