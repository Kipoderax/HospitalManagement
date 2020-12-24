using System;

namespace HospitalManagement.Web.Server
{
    /// <summary>
    /// Validate employee properties before save to database
    /// </summary>
    public static class EmployeeValidate
    {
        /// <summary>
        /// Pesel checking for persons born between 1900-1999
        /// </summary>
        /// <param name="pesel">Employee pesel to check</param>
        /// <returns></returns>
        public static bool PeselValidate ( string pesel )
        {
            // Check pesel length
            if (pesel.Length != 11)
                return false;

            // Check if pesel contain only digits
            foreach (var p in pesel)
                if (!char.IsDigit( p ))
                    return false;

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

        /// <summary>
        /// Check if number pwz is correct input with country standard
        /// </summary>
        /// <param name="numberPwz">employee number pwz</param>
        /// <returns></returns>
        public static bool NumberPwzValidate( string numberPwz )
        {
            // Check number length
            if (numberPwz.Length != 7)
                return false;
           
            // Check if number pwz contain only digits
            foreach (var p in numberPwz)
                if (!char.IsDigit( p ))
                    return false;

            // Get control sum
            int sum = 0;
            for (int i = 0; i < numberPwz.Length - 1; i++)
                sum += int.Parse( numberPwz[i+1].ToString() ) * (i + 1);

            // Check sum control with first digit in number pwz
            if (sum % 11 != int.Parse( numberPwz[0].ToString() ))
                return false;

            return true;
        }
    }
}
