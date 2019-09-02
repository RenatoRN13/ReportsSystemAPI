using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace ReportsSystemApi.Domain
{
    public class SigningConfig
    {
        public SecurityKey key { get; }
        public SigningCredentials signingCredentials { get; }
        public SigningConfig(){
            using (var provider = new RSACryptoServiceProvider(2048)){
                key = new RsaSecurityKey(provider.ExportParameters(true));
            }
            signingCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}
