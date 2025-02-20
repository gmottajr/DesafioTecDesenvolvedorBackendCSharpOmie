using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omie.WebApiAuthorization.Models
{
    using System.ComponentModel.DataAnnotations;

    public record LoginRequestDto
    {
        [Required(ErrorMessage = "Username is required.")]
        [MinLength(3, ErrorMessage = "Username must be at least 3 characters.")]
        public string Username { get; init; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Password must be at least 4 characters.")]
        public string Password { get; init; }
    }
}