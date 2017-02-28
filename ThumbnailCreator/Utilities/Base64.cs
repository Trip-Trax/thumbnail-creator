using System;

namespace ThumbnailCreator.Utilities {
    class Base64 {
        public static byte[] Decode(string data) {
            return Convert.FromBase64String(data);
        }
    }
}
