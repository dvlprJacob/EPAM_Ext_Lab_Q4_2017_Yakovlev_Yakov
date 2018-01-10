namespace Task09_2
{
    using System.Text.RegularExpressions;

    public static class ExtensionMethodTask2
    {
        public static bool IsPositiveIntegerNumber(this string line)
        {
            Regex reg = new Regex(@"^[1-9]\d*$");
            return reg.IsMatch(line);
        }
    }
}