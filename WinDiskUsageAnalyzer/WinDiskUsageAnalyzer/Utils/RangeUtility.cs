using FoldersAndFilesSizeAnalyzer.Entities;
using System.Text.RegularExpressions;

namespace WinDiskUsageAnalyzer.Utils
{
    public class RangeUtility
    {
        /// <summary>
        /// Gets 0-based indexes of the first and the last object in a range.
        /// </summary>
        /// <param name="text">Input text that user has entered in GUI.</param>
        /// <returns>0-based indexes of the first and the last item in a range.</returns>
        public static (int?, int?) ParseRange(string text)
        {
            // Format: from - to
            // - from and to are integers
            //  -one or more whitespaces can be before/after from, to, and '-'
            Regex regex = new Regex(@"^\s*(?<from>\d+)\s*-\s*(?<to>\d+)\s*$");

            Match match = regex.Match(text);
            if (match?.Success == true)
            {
                // Read from and to indexes.
                // Note: User will enter 1-based indexes, so converted them to 0-based indexes, becase this method returns 0-based indexes.
                bool fromSuccessfullyParsed = int.TryParse(match.Groups["from"]?.Value, out int fromBase1Index);
                bool toSuccessfullyParsed = int.TryParse(match.Groups["to"]?.Value, out int toBase1Index);

                if (fromSuccessfullyParsed && toSuccessfullyParsed)
                    return (fromBase1Index - 1, toBase1Index - 1); // Return 0-based indexes.
            }

            return (null, null);
        }

        public static void MakeSureRangeNotToBig(ref int? from, ref int? to, Folder? folder)
        {
            int maxOneBasedIndexInRange = folder?.Items?.Length ?? 0;

            MaxSureOneBasedIndexNotToBig(ref from, maxOneBasedIndexInRange);
            MaxSureOneBasedIndexNotToBig(ref to, maxOneBasedIndexInRange);
        }

        private static void MaxSureOneBasedIndexNotToBig(ref int? index, int maxOneBasedIndexInRange)
        {
            if (index > maxOneBasedIndexInRange)
                index = maxOneBasedIndexInRange;
        }
    }
}
