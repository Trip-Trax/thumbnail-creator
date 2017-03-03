using System;

namespace Thumbnail.Utilities {
    class Base64 {
        public static byte[] Decode(string data) {
            return Convert.FromBase64String(data);
        }
    }
}
