using System.ComponentModel.DataAnnotations;

namespace OmieVendas.WebApiClientes.Models;

public class LoginModelDto
{
    [Required]
    [EmailAddress]
    public string UserName { get; set; } = string.Empty;
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
