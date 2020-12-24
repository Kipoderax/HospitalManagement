using System;

namespace HospitalManagement.Web.Server
{
    /// <summary>
    /// Validate employee properties before save to database
    /// </summary>
    public static class EmployeeValidate
    {
        public static bool PeselValidate ( string pesel )
        {
            // Check pesel length
            if (pesel.Length != 11)
                return false;

            // Check if pesel contain only digits
            foreach (var p in pesel)
            {
                if (!char.IsDigit( p ))
                    return false;
            }

            // Check month, day
            if (int.Parse( pesel.Substring( 2, 2 ) ) > 12 ||
                int.Parse( pesel.Substring( 4, 2 ) ) > DateTime.DaysInMonth(
                    int.Parse( string.Concat( "19", pesel.Substring( 0, 2 ) ) ),
                    int.Parse( pesel.Substring( 2, 2 ) ) ))
                return false;

            // Get control sum
            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3, 1 };
            int sum = 0;
            for (int i = 0; i < weights.Length; i++)
                sum += int.Parse( pesel[i].ToString() ) * weights[i];

            // Check control sum
            if (sum % 10 != 0)
                return false;

            return true;
        }
    }
}
