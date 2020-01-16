using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MShell.Core
{
    public class ShellCommand
    {
        private readonly string[] _tokens;
        public string RawLine { get;  }

        public string CommandName { get; }

        public bool IsInnerShell { get; }

        public string ProcessedValue { get; }

        public IEnumerable<string> Tokens => _tokens;

        public ShellCommand(string line)
        {
            RawLine = line ?? "";
            var regexp = new Regex(@"(""[^""]+"")|\S+");
            var matches = regexp.Matches(line);
            _tokens = new string[matches.Count];
            for (var idx =0; idx < matches.Count; idx++)
            {
                _tokens[idx] = matches[idx].Value;
            }

            IsInnerShell = _tokens[0][0] == '$';
            CommandName = IsInnerShell ? _tokens[0].Substring(1) : _tokens[0];
        }
    }
}
