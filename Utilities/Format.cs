namespace Utilities {
    public static class Format {
        public static string Bytes(ulong bytes) {
            if (bytes < 1024) {
                return $"{bytes} Bytes";
            } else if (bytes < 1048576) {
                return $"{(bytes / 1024).ToString("N3")} KB";
            } else if (bytes < 1073741824) {
                return $"{(bytes / 1048576).ToString("N3")} MB";
            }

            return $"{(bytes / 1073741824).ToString("N3")} GB";
        }
    }
}
