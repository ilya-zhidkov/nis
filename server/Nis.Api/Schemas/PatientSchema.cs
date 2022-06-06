using System.ComponentModel.DataAnnotations;

namespace Nis.Api.Schemas;

public class Patient
{
    /// <summary>
    /// Unique ID of a patient
    /// </summary>
    [Required]
    public Guid Id { get; set; }
    /// <summary>
    /// First name of the patient
    /// </summary>
    /// <example>Jiří</example>
    public string FirstName { get; set; }
    /// <summary>
    /// Last name of the patient
    /// </summary>
    /// <example>Procházka</example>
    public string LastName { get; set; }
}
