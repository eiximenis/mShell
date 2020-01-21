using System;
using System.Diagnostics;

namespace MShell.Core
{
    public class ShellJob
    {
        public int Id { get;  }

        private Process _process;

        public DateTime? EndAt { get; private set; }
        public int ExitCode { get; private set; }

        public string ProcessName { get; }
        public bool Running { get; private set; }

        public ShellJob(Process process)
        {
            _process = process;
            _process.EnableRaisingEvents = true;
            _process.Exited += OnProcessExited;
            _process.Start();
            Running = true;
            Id = _process.Id;
            ProcessName = _process.ProcessName;
        }

        private void OnProcessExited(object sender, EventArgs e)
        {
            Running = false;
            EndAt = _process.ExitTime;
            ExitCode = _process.ExitCode;
            _process.Exited -= OnProcessExited;
            _process = null;
        }
    }
}