using System;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagement.Core
{
    /// <summary>
    /// Checkers for register new duties
    /// </summary>
    public static class DutyValidate
    {
        /// <summary>
        /// Check if selected date is between now and upper limit date from duty calendar
        /// </summary>
        /// <param name="selectedDate">The employee selected date to add</param>
        /// <param name="limitDate">The upper limit from calendar</param>
        /// <returns></returns>
        public static bool IsDateRangeCorrect( DateTime selectedDate, DateTime limitDate )
        {
            return selectedDate > DateTime.Now && selectedDate <= limitDate;
        }

        /// <summary>
        /// Check if no exist specialize in the selected date by employee 
        /// </summary>
        /// <param name="selectedDate">The employee selected date to add</param>
        /// <param name="specialize">The employee specialize</param>
        /// <returns>
        ///             If someone have duty with this specialize on the selectedDate
        ///             return false otherwise return true
        /// </returns>
        public static bool IsUniqueSpecialize(DateTime selectedDate, string specialize)
        {
            // Initialize duty items of all employees
            var items = IoC.Duties.Items;

            // Collect all specialize to list with selected by employee date
            var getDateFromItems = items
                .Where ( s => s.StartShift.Date == selectedDate.Date )
                .Select ( s => s.JobName )
                .ToList();
            
            return !getDateFromItems.Contains ( specialize );
        }

        /// <summary>
        /// Check if adding duty is between day before and after with 24 hours 
        /// </summary>
        /// <param name="selectedDate">The selected date for employee</param>
        /// <returns></returns>
        public static bool IsDayAfterOrBefore( DateTime selectedDate )
        {
            // Get employee duties
            var items = IoC.Duties.EmployeeItems;

            return ( 
                from item in items 
                select Math.Abs ( ( selectedDate.Date - item.StartShift.Date ).TotalDays ) )
                .All ( daysDifferent => !( daysDifferent < 2 ) 
                );
        }

        /// <summary>
        /// Get amount of employee duties for month in th selected date
        /// </summary>
        /// <param name="selectedDate">The selected date for employee</param>
        /// <returns></returns>
        public static int AmountDutiesInMoth( DateTime selectedDate )
        {
            // Get employee duties
            var items = IoC.Duties.EmployeeItems;

            return items.Count ( item => item.StartShift.Month == selectedDate.Month );
        }
    }
}