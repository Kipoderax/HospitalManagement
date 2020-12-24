using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Web.Server
{
    /// <summary>
    /// Simply object of our employee entity for use in view application
    /// </summary>
    public class EmployeeRegisterDto
    {
        #region Public Properties

        [Required(ErrorMessage ="Nie wpisano imienia pracownika")]
        [StringLength(25, MinimumLength = 3, ErrorMessage ="Długość imienia musi mieścić się w zakresie 3-25")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="Nie wpisano nazwisko pracownika")]
        [StringLength( 25, MinimumLength = 3, ErrorMessage = "Długość nazwiska musi mieścić się w zakresie 3-25" )]
        public string LastName { get; set; }

        [StringLength(11, MinimumLength = 11, ErrorMessage ="Pesel nie ma 11 znaków")]
        public string Pesel { get; set; }

        [Required(ErrorMessage ="Posada pracownika jest wymagana")]
        public string Type { get; set; }

        [Required(ErrorMessage ="Specjalizacja pracownika jest wymagana")]
        public string Specialize { get; set; }

        [StringLength(7, MinimumLength =7, ErrorMessage = "Numer PWZ nie ma 7 znaków")]
        public string NumberPwz { get; set; }

        #endregion
    }
}
