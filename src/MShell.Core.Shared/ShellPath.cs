using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace MShell.Core.Files
{
    public readonly struct ShellPath
    {
        public bool HasDriveLetter { get; }
        public bool IsAbsolute { get; }
        public char DriveLetter { get; }
        public string Value { get; }

        public bool IsVirtual { get; }

        public ShellPath(string name)
        {
            Value = name.Trim();
            Value = Value.Replace('/', '\\');

            Value = ProcessVirtualPath(Value);
            IsVirtual = Value.StartsWith(@"\\");        // Paths starting with // or \\ are considered virtual and allow the use of virtual file drivers
            if (IsVirtual)
            {
                Value = Value.Substring(2);
                DriveLetter = ' ';
                HasDriveLetter = false;
            }
            else
            {

                if (((Value[0] >= 'A' && Value[0] <= 'Z') ||
                     (Value[0] >= 'a' && Value[0] <= 'z')) &&
                     Value[1] == ':')
                {
                    HasDriveLetter = true;
                    DriveLetter = Value[0];
                }
                else
                {
                    HasDriveLetter = false;
                    DriveLetter = ' ';
                }
            }

            IsAbsolute = IsVirtual || HasDriveLetter || Value[0] == '\\';
        }

        private static string ProcessVirtualPath(string value)
        {
            var regexp = new Regex(@"\\\\([aA-zZ]{1})\\");       // Regexp that captures \\[drive-letter\
            var maches = regexp.Matches(value);
            if (maches.Count == 1 && maches[0].Groups.Count == 2)
            {
                return $"{maches[0].Groups[1].Value}:" + value.Substring(3);
            }
            return value;
        }

        public static ShellPath Combine(in ShellPath first, in ShellPath second)
        {
            if (second.IsAbsolute)
            {
                throw new ArgumentException("second path can't be absolute");
            }

            return new ShellPath(Path.Combine(first.Value, second.Value));
        }
    }
}
