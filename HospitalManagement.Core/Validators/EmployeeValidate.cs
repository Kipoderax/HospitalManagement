using System;
using System.Linq;

namespace HospitalManagement.Core
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
            if( pesel.Any ( p => !char.IsDigit( p ) ) )
                return false;

            // Check month, day
            if (int.Parse( pesel.Substring( 2, 2 ) ) > 12 ||
                int.Parse( pesel.Substring( 4, 2 ) ) > DateTime.DaysInMonth(
                    int.Parse( string.Concat( "19", pesel.Substring( 0, 2 ) ) ),
                    int.Parse( pesel.Substring( 2, 2 ) ) ))
                return false;
            
            // Get control sum
            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3, 1 };
            var sum = weights.Select ( ( t, i ) => int.Parse ( pesel[i].ToString() ) * t ).Sum();

            // Check control sum
            return sum % 10 == 0;
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
            if( numberPwz.Any ( p => !char.IsDigit( p ) ) )
                return false;
            

            // Get control sum
            var sum = 0;
            for (var i = 0; i < numberPwz.Length - 1; i++)
                sum += int.Parse( numberPwz[i+1].ToString() ) * (i + 1);

            // Return true if sum control is the first digit in number pwz
            return sum % 11 == int.Parse( numberPwz[0].ToString() );
        }
    }
}
