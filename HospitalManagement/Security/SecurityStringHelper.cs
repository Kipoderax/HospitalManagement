using System;
using System.Runtime.InteropServices;
using System.Security;

namespace HospitalManagement
{
    /// <summary>
    /// Helpers for the <see cref="SecureString"/> class
    /// </summary>
    public static class SecurityStringHelper
    {
        /// <summary>
        /// Unsecures a <see cref="SecureString"/> to plain text
        /// </summary>
        /// <param name="secureString">The secure string</param>
        /// <returns></returns>
        public static string UnSecure(this SecureString secureString)
        {
            // Make sure we have a secure string
            if (secureString == null)
                return string.Empty;

            // Get a pointer for an unsecure string in memory
            var unmangedString = IntPtr.Zero;

            try
            {
                // Unsecure string
                unmangedString = Marshal.SecureStringToGlobalAllocUnicode( secureString );
                return Marshal.PtrToStringUni( unmangedString );
            }
            finally
            {
                // Clean up any memory allocation
                Marshal.ZeroFreeGlobalAllocUnicode( unmangedString );
            }
        }
    }
}
