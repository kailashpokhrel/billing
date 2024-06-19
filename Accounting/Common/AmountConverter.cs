namespace Accounting.Common
{
    public static class AmountConverter
    {
        private static readonly string[] Ones = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
        private static readonly string[] Teens = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        private static readonly string[] Tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        public static string ConvertToWords(decimal amount)
        {
            if (amount == 0)
                return "Zero";

            string words = ConvertToWordsRecursive((long)amount);
            return words;
        }

        private static string ConvertToWordsRecursive(long number)
        {
            if (number < 10)
                return Ones[number];
            if (number < 20)
                return Teens[number - 10];
            if (number < 100)
                return Tens[number / 10] + " " + Ones[number % 10];
            if (number < 1000)
                return Ones[number / 100] + " Hundred " + ConvertToWordsRecursive(number % 100);
            if (number < 1000000)
                return ConvertToWordsRecursive(number / 1000) + " Thousand " + ConvertToWordsRecursive(number % 1000);
            if (number < 1000000000)
                return ConvertToWordsRecursive(number / 1000000) + " Million " + ConvertToWordsRecursive(number % 1000000);

            return "Number is too large to convert";
        }
    }

}
