using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MShell.Core
{
    public ref struct ShellCommand
    {
        private readonly string[] _tokens;
        public string RawLine { get; }

        public string CommandName { get; }

        public bool IsInnerShell { get; }

        public Span<string> Arguments { get; }

        public string ArgumentsAsString { get; }

        public bool IsJob { get; }

        public ShellCommand(string line)
        {
            RawLine = line ?? "";
            var regexp = new Regex(@"(""[^""]+"")|\S+");
            var matches = regexp.Matches(line);
            _tokens = new string[matches.Count];
            for (var idx = 0; idx < matches.Count; idx++)
            {
                _tokens[idx] = matches[idx].Value;
            }
            IsInnerShell = _tokens[0][0] == '$';
            CommandName = IsInnerShell ? _tokens[0].Substring(1) : _tokens[0];
            IsJob = _tokens[_tokens.Length - 1] == "&";
            Arguments = IsJob ?
                _tokens.AsSpan().Slice(1, _tokens.Length - 2) :
                _tokens.AsSpan().Slice(1, _tokens.Length - 1);

            ArgumentsAsString = string.Join(" ", Arguments.ToArray());

        }
    }
}
