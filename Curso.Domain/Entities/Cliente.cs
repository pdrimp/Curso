using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace Curso.Domain.Entities
{
    public class Cliente
    {
        public Guid Id { get; private set; }

        public string Nombre { get; private set; }

        public string Correo { get; private set; }

        public string PasswordHash { get; private set; }

        public Guid TipoClienteId { get; private set; }
        public TipoCliente TipoCliente { get; private set; }

        public ICollection<Interes> Intereses { get; private set; } = new List<Interes>();

        protected Cliente() { }

        public Cliente(string nombre, string correo, string plainPassword, Guid tipoClienteId)
        {
            SetNombre(nombre);
            SetCorreo(correo);
            SetPassword(plainPassword);
            TipoClienteId = tipoClienteId;
            Id = Guid.NewGuid();
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("Nombre es requerido", nameof(nombre));
            if (nombre.Length < 2 || nombre.Length > 80) throw new ArgumentException("Nombre debe tener entre 2 y 80 caracteres", nameof(nombre));
            Nombre = nombre.Trim();
        }

        public void SetCorreo(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo)) throw new ArgumentException("Correo es requerido", nameof(correo));
            var emailAttr = new EmailAddressAttribute();
            if (!emailAttr.IsValid(correo)) throw new ArgumentException("Correo no tiene formato válido", nameof(correo));
            Correo = correo.Trim().ToLowerInvariant();
        }

        public void SetPassword(string plainPassword)
        {
            if (string.IsNullOrEmpty(plainPassword) || plainPassword.Length < 8)
                throw new ArgumentException("La contraseña debe tener al menos 8 caracteres", nameof(plainPassword));

            // PBKDF2 hashing
            using var rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[16];
            rng.GetBytes(salt);

            using var deriveBytes = new Rfc2898DeriveBytes(plainPassword, salt, 10000, HashAlgorithmName.SHA256);
            var hash = deriveBytes.GetBytes(32);

            PasswordHash = $"10000.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        public void SetTipoCliente(Guid tipoClienteId)
        {
            if (tipoClienteId == Guid.Empty) throw new ArgumentException("TipoClienteId es requerido", nameof(tipoClienteId));
            TipoClienteId = tipoClienteId;
        }

        public void SetIntereses(IEnumerable<Interes> intereses)
        {
            Intereses = new List<Interes>(intereses ?? Array.Empty<Interes>());
        }
    }
}
