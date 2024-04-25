namespace IdentityUtility.Token;

public interface ITokenService
{
    string GenerateToken(GenerateTokenPayload payload);
}


