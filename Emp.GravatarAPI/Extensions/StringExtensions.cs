using System.Security.Cryptography;
using System.Text;

namespace Emp.GravatarAPI.Extensions;

public static class StringExtensions
{
    public static string ToMd5(this string s)
    {
        if(string.IsNullOrWhiteSpace(s)) return string.Empty;

        byte[] low = Encoding.Default.GetBytes(s.ToLowerInvariant());
        byte[] buff =  MD5.Create().ComputeHash(low);
        
        StringBuilder builder =  new StringBuilder(buff.Length * 2);

        for (int i = 0; i < buff.Length; i++)
        {
            builder.Append(buff[i].ToString("x2"));
        }
        
        return builder.ToString().ToLowerInvariant();
    }
}