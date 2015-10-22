using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Security.Token
{
    [Serializable]
    public class TokenGenerator
    {
        public const string PrivateKey = "<RSAKeyValue><Modulus>oichIUDy8vhCW47Aaf+/nEqw3qIv+SP9gxQHnQBWnXNU2eZ81qwtjQ7bi855cJbbw11rp5mRDG97KEcg5vKGihbikN+R037k50f1FRiw3/tXULkAbovxW3dIyLaslZmrYCodwOYV89+EPixvSjxPR3+qUBSERYiGIgfQ6fOX1FM=</Modulus><Exponent>AQAB</Exponent><P>2gxqTOXo5YK+5dgCg27cN+MdXMSKrZkYg8JsCQN3T8CzWCS0iDUFVibrf4l4ZRzrvwpmISowQHDwmJMAN/YEeQ==</P><Q>vmAsHr3nVc3kZzOSnSs21YnZPLIb7I6qFxA8/QZfkwcT4vkv1QBHwctGycdAVHw/DmTkzN94mEF41UuwWyC0Kw==</Q><DP>ZxBROnG9iKYpFzjnzzoaSyxFl7Cqn+1qQfUm3YfO4FqEKtiGoI73K8aPr6PJzXlDEPSYW3q2fe8kOenZw5m8eQ==</DP><DQ>OJnlZDp39jXh66EUvS/k/LYgZYBa9wkvnu3QBDaJ4e/fxMLrqruLmh2y7TkVckkCgmgS8qqac0I8B8aaJ23gww==</DQ><InverseQ>QCAyH0+NOP5G8/Zy0K+620VSkK+CMCjlnrFb1w1UUIPGLRUjnb4M45oeUj123jdFalqwaV5Y4p79OFpwEyXPzQ==</InverseQ><D>FhUYGOw8acRPXSKip4zRrlopnhgeSmKRWrF2m/X41bZoHsiZxa5rAaO9WFvFBAU3ZnKpf4iVTUk9T23oWCs9EQePz4zMJBum7F8WP3wstrnLyXw0CkuuNagIWiZ281v6wNADfigR2ASjqgTDG/PeEyhei0QDPKVhQumMKC4xRvE=</D></RSAKeyValue>";

        public readonly string Value;
        public readonly DateTime Expires;
        public readonly byte[] Data;
        public byte[] Signature { private set; get; }

        public TokenGenerator(string value, DateTime expires)
        {
            Value = value;
            Expires = expires;
            using (var ms = new MemoryStream())
            using (var writer = new BinaryWriter(ms))
            {
                writer.Write(Expires.Ticks);
                writer.Write(Value);
                Data = ms.ToArray();
            }
        }

        private void Sign(string privateKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            using (var sha1 = new SHA1CryptoServiceProvider())
            {
                rsa.FromXmlString(privateKey);
                Signature = rsa.SignData(Data, sha1);
            }
        }

        public string GetTokenString(string privateKey)
        {
            if (Signature == null)
            {
                Sign(privateKey);
            }
            return Convert.ToBase64String(Data.Concat(Signature).ToArray());
        }
        /* sometimes has issue */
        public string GetTokenEncodedString(string privateKey)
        {
            if (Signature == null)
            {
                Sign(privateKey);
            }
            return System.Web.HttpServerUtility.UrlTokenEncode(Data.Concat(Signature).ToArray());
        }
        public static TokenGenerator FromTokenString(string tokenString, string key)
        {
            var buffer = Convert.FromBase64String(tokenString);
            var data = buffer.Take(buffer.Length - 128).ToArray();
            var sig = buffer.Skip(data.Length).Take(128).ToArray();
            using (var rsa = new RSACryptoServiceProvider())
            using (var sha1 = new SHA1CryptoServiceProvider())
            {
                rsa.FromXmlString(key);
                if (rsa.VerifyData(data, sha1, sig))
                {
                    using (var ms = new MemoryStream(data))
                    using (var reader = new BinaryReader(ms))
                    {
                        var ticks = reader.ReadInt64();
                        var value = reader.ReadString();
                        var expires = new DateTime(ticks);
                        return new TokenGenerator(value, expires);
                    }
                }
            }
            return null;
        }

    }
}
