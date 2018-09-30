using System;

namespace ElectionCalculatorView.Helpers
{
    public static class PeselValidator
    {
        private static readonly int[] multiplicators = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };

        public static DateTime? GetDateFromPesel(string pesel)
        {
            int year = int.Parse(pesel.Substring(0, 2));
            int month = int.Parse(pesel.Substring(2, 2));
            int day = int.Parse(pesel.Substring(4, 2));
            int century;

            if (month >= 1 && month <= 12)
            {
                century = 19;
            }
            else if (month >= 21 && month <= 32)
            {
                century = 20;
            }
            else if (month >= 81 && month <= 92)
            {
                century = 18;
            }
            else { return null; }

            DateTime date;

            try
            {
                date = new DateTime(100 * century + year, month % 20, day);
            }
            catch (Exception)
            {
                return null;
            }

            return date;
        }

        public static bool IsPeselValid(string pesel)
        {
            if (pesel.Length != 11) { return false; }

            bool isControlSumValid = IsControlSumValid(pesel);

            if (!isControlSumValid) { return false; }

            bool isDateValid = IsDateValid(pesel);

            return isDateValid;
        }

        private static bool IsControlSumValid(string pesel)
        {
            int sum = 0;
            for (int i = 0; i < multiplicators.Length; i++)
            {
                if (!int.TryParse(pesel[i].ToString(), out int digit)) { return false; }

                sum += multiplicators[i] * digit;
            }

            int rest = (1000 - sum) % 10;

            if (!int.TryParse(pesel[10].ToString(), out int tenthDigit)) { return false; }

            return rest == tenthDigit;
        }

        private static bool IsDateValid(string pesel)
        {
            return GetDateFromPesel(pesel).HasValue;
        }
    }
}