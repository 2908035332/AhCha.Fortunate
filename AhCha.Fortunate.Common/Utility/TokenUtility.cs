using System.Text;
using System.Security.Claims;
using System.Security.Principal;
using AhCha.Fortunate.Common.Const;
using AhCha.Fortunate.Common.Global;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace AhCha.Fortunate.Common.Utility
{
    public class TokenUtility
    {
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public static string GetToken(ClaimEntity entity)
        {
            Claim[] claims = {
                new Claim(ClaimConst.CLAINM_USERID,entity.Id),
                new Claim(ClaimConst.CLAINM_NAME,entity.Name),
                new Claim(ClaimConst.CLAINM_ACCOUNT,entity.Account),
                new Claim(ClaimConst.CLAINM_ROLE_ID,entity.RoleId),
                new Claim(ClaimConst.CLAINM_ROLE_Name,entity.RoleName),
                new Claim(ClaimConst.CLAINM_DEVICE_ID,entity.DeviceId),
            };

            // 2. 从 appsettings.json 中读取SecretKey
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AhChaFortunateGlobalContext.JwtSettings.SecretKey));
            // 3. 选择加密算法
            var algorithm = SecurityAlgorithms.HmacSha256;
            // 4. 生成Credentials
            var signingCredentials = new SigningCredentials(secretKey, algorithm);
            // 5. 根据以上，生成token
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: AhChaFortunateGlobalContext.JwtSettings.Issuer,//Issuer
                audience: AhChaFortunateGlobalContext.JwtSettings.Audience,//Audience
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(1),//过期时间
                signingCredentials: signingCredentials//Credentials
            );
            // 6. 将token变为string
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;

        }

        /// <summary>
        /// 解析Principal
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static ClaimsPrincipal GetPrincipal(string? token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken? jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken != null)
            {
                TokenValidationParameters Parameters = new()
                {
                    //token是否包含有效期 
                    RequireExpirationTime = true,
                    //是否对Key进行验证
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AhChaFortunateGlobalContext.JwtSettings.SecretKey)),

                    //验证秘钥的接受人
                    ValidateAudience = true,
                    ValidAudience = AhChaFortunateGlobalContext.JwtSettings.Audience,

                    //验证秘钥的发行人
                    ValidateIssuer = true,
                    ValidIssuer = AhChaFortunateGlobalContext.JwtSettings.Issuer,

                    //验证令牌是否过期
                    ValidateLifetime = true,

                    //时间验证允许的偏差
                    ClockSkew = TimeSpan.Zero
                };
                try
                {
                    var pincipal = tokenHandler.ValidateToken(token, Parameters, out SecurityToken securityToken);
                    return pincipal;
                }
                catch (Exception ex)
                {
                    throw new Exception($"token出现异常为{ex.Message}");
                }
            }
            throw new Exception("token出现异常为null");
        }

        /// <summary>
        /// 验证Principal
        /// </summary>
        /// <param name="token"></param>
        public static bool ValidateToken(string token, out IPrincipal LoginPrincipal)
        {
            var principal = GetPrincipal(token);

            LoginPrincipal = null;
            if (principal == null) return false;

            var identity = principal.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return false;
            }
            if (!identity.IsAuthenticated)
            {
                return false;
            }
            LoginPrincipal = principal;

            return true;
        }
    }
}
