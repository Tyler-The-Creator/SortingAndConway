using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class SortingTests
    {
        private const string InputString =
            "Th1s 1s 4 t3st! Th3 5tr1ng 5h0uldn't h4v3 numb3r5 4ft3r th15. D1d 1t w0rk? 1f n0t, call m3 0n 0123456789 or email me at treshpillay@gmail.com.";
        private const string ExpectedOutputStringNoPunctuation = "Th1s1s4t3stTh35tr1ng5h0uldnth4v3numb3r54ft3rth15D1d1tw0rk1fn0tcallm30n0123456789oremailmeattreshpillaygmailcom";
        private const string ExpectedOutputStringNoNumbers = "Thsstst!Thtrnghuldn'thvnumbrftrth.Ddtwrk?fnt,callmnoremailmeattreshpillay@gmail.com.";
        private const string ExpectedOutputStringOnlyLetters = "ThsststThtrnghuldnthvnumbrftrthDdtwrkfntcallmnoremailmeattreshpillaygmailcom";

        // Test type: Functional
        [TestMethod]
        public void FilterOut_Punctuation_RemovesOnlyPunctuation()
        {
            var sorting = new Sorting.Sorting(InputString);
            var regexFilter = @"[^A-Za-z0-9]";
            //var outputString = sorting.FilterOut(regexFilter);
            //Assert.AreEqual<string>(ExpectedOutputStringNoPunctuation, outputString);
        }

        [TestMethod]
        public void FilterOut_Numbers_RemovesOnlyNumbers()
        {
            var sorting = new Sorting.Sorting(InputString);
            //var regexForStringToRemove = @"[^A-Za-z]";
            var regexFilter = @"[^A-Za-z\p{P}\p{S}]";
            //var outputString = sorting.FilterOut(regexFilter);
            //Assert.AreEqual<string>(ExpectedOutputStringNoNumbers, outputString);
        }

        [TestMethod]
        public void FilterOut_NonLetters_RemovesAllNonLetters()
        {
            var sorting = new Sorting.Sorting(InputString);
            var regexFilter = @"[^A-Za-z]";
            //var outputString = sorting.FilterOut(regexFilter);
            //Assert.AreEqual<string>(ExpectedOutputStringOnlyLetters, outputString);
        }

        [TestMethod]
        public void Convert_ToLowerCase_ConvertGivenStringToLowerCase()
        {
            var sorting = new Sorting.Sorting(InputString);
        }

        // Test type: Negative
    }
}
