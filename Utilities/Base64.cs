﻿using System;

namespace Utilities {
    public static class Base64 {
        public static byte[] Decode(string data) {
            return Convert.FromBase64String(data);
        }
    }
}
