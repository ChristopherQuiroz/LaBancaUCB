using LaBancaUCB.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Core.DTOs
{
    public class SecurityDto
    {
        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Name { get; set; } = null!;

        public RoleType? Role { get; set; }
    }
}
