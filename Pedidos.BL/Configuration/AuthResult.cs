using System.Collections.Generic;

namespace Pedidos.BL.Configuration
{
    public class AuthResult
    {
        public string Token { get; set; }
        public bool Exitoso { get; set; }
        public List<string> Errores { get; set; }
    }
}