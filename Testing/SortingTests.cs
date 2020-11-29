using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class SortingTests
    {
        // Constants used for testing:
        // Input
        private const string InputString =
            "Th1s 1s 4 t3st! Th3 5tr1ng 5h0uldn't h4v3 numb3r5 4ft3r th15. D1d 1t w0rk? 1f n0t, call m3 0n 0123456789 or email me at treshpillay@gmail.com.";
        
        // Output
        private const string ExpectedOutputStringNoSpecialCharacters = "Th1s1s4t3stTh35tr1ng5h0uldnth4v3numb3r54ft3rth15D1d1tw0rk1fn0tcallm30n0123456789oremailmeattreshpillaygmailcom";
        private const string ExpectedOutputStringNoNumbers = "Thsstst!Thtrnghuldn'thvnumbrftrth.Ddtwrk?fnt,callmnoremailmeattreshpillay@gmail.com.";
        private const string ExpectedOutputStringOnlyLetters = "ThsststThtrnghuldnthvnumbrftrthDdtwrkfntcallmnoremailmeattreshpillaygmailcom";
        private const string ExpectedOutputStringLowerCase =
            "th1s 1s 4 t3st! th3 5tr1ng 5h0uldn't h4v3 numb3r5 4ft3r th15. d1d 1t w0rk? 1f n0t, call m3 0n 0123456789 or email me at treshpillay@gmail.com.";
        private const string ExpectedSortedLowerCaseOutput =
            "aaaaabccdddeeeffgghhhhhhiiiklllllllmmmmmmnnnnnooprrrrrrssssttttttttttttuuvwy";

        // Testing that only special characters gets removed from a string - numbers and letters will remain
        [TestMethod]
        public void FilterOut_SpecialCharacters_RemoveSpecialCharactersOnly()
        {
            var sorting = new Sorting.Sorting(InputString);
            var regexFilter = @"[^A-Za-z0-9]";
            
            sorting.FilteredString = sorting.FilterOut(regexFilter);
            
            Assert.AreEqual(ExpectedOutputStringNoSpecialCharacters, sorting.FilteredString);
        }

        // Testing that only numbers gets removed from a string - special characters and letters will remain
        [TestMethod]
        public void FilterOut_Numbers_RemoveNumbersOnly()
        {
            var sorting = new Sorting.Sorting(InputString);
            var regexFilter = @"[^A-Za-z\p{P}\p{S}]";
            
            sorting.FilteredString = sorting.FilterOut(regexFilter);
            
            Assert.AreEqual(ExpectedOutputStringNoNumbers, sorting.FilteredString);
        }

        // Testing that numbers AND special characters get removed from a string - only letters will remain
        [TestMethod]
        public void FilterOut_NonLetters_RemovesAnyNonLetters()
        {
            var sorting = new Sorting.Sorting(InputString);
            var regexFilter = @"[^A-Za-z]";
            
            sorting.FilteredString = sorting.FilterOut(regexFilter);
            
            Assert.AreEqual(ExpectedOutputStringOnlyLetters, sorting.FilteredString);
        }
        
        // Testing that all letters of a given string will be converted to lower case using built-in ToLower() method
        [TestMethod]
        public void Convert_ToLowerCase_ConvertGivenStringToLowerCase()
        {
            var sorting = new Sorting.Sorting(InputString);
            
            var lowerCaseString = sorting.InputString.ToLower();
            
            Assert.AreEqual(ExpectedOutputStringLowerCase, lowerCaseString);
        }

        // Testing that all letters of a given string will be converted to lower case using built-in ToLower() method
        [TestMethod]
        public void Sort_Alphabetically_SortGivenStringAlphabeticallyAndReturnSortedArray()
        {
            var sorting = new Sorting.Sorting("thisisatest");
            string[] expectedSortedStringArray = {"a", "e", "h", "i", "i", "s", "s", "s", "t", "t", "t"};
            
            var actualSortedStringArray = sorting.SortLowerCaseFilteredString(sorting.InputString);
            
            CollectionAssert.AreEqual(expectedSortedStringArray, actualSortedStringArray);
        }

        // Testing that all letters of a given string will be converted to lower case using built-in ToLower() method
        [TestMethod]
        public void Concatenate_StringArrayIntoToString_ConcatenateTheGivenStringArrayIntoSingleString()
        {
            var sorting = new Sorting.Sorting();
            string[] sortedStringArray = { "a", "e", "h", "i", "i", "s", "s", "s", "t", "t", "t" };
            var expectedConcatenatedString = "aehiisssttt";
            
            var actualConcatenatedString = sorting.ConcatenateGivenString(sortedStringArray);
            
            Assert.AreEqual(expectedConcatenatedString, actualConcatenatedString);
        }

        // Testing that input string is filtered out to have only letters, is converted to lowercase and then sorted alphabetically
        [TestMethod]
        public void Sort_FilteredLowerCaseString_SortsLowerCaseStringInAlphabeticOrder()
        {
            var sorting = new Sorting.Sorting(InputString);
            var regexFilter = @"[^A-Za-z]";

            sorting.FilteredString = sorting.FilterOut(regexFilter);
            
            sorting.FilteredString = sorting.FilteredString.ToLower();
            
            var sortedStringArray = sorting.SortLowerCaseFilteredString(sorting.FilteredString);
            
            sorting.OutputString = sorting.ConcatenateGivenString(sortedStringArray);
            
            Assert.AreEqual(ExpectedSortedLowerCaseOutput, sorting.OutputString);
        }
    }
}
