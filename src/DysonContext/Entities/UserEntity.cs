
using Microsoft.AspNetCore.Identity;

namespace DysonContext.Entities;

public class UserEntity
{
    private static readonly PasswordHasher<UserEntity> _passwordHasher = new();

    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    // Senha criptografada armazenada no banco
    public string PasswordHash { get; private set; } = string.Empty;

    public ICollection<FileNodeEntity> FileNodes { get; set; } = [];

    // 🔒 Define a senha e gera o hash
    public void SetPassword(string password)
    {
        PasswordHash = _passwordHasher.HashPassword(this, password);
    }

    // ✅ Compara senha enviada com a armazenada
    public bool ComparePass(string password)
    {
        var result = _passwordHasher.VerifyHashedPassword(this, PasswordHash, password);
        return result == PasswordVerificationResult.Success;
    }
}
