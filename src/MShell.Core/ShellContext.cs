using MShell.Core.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace MShell.Core
{
    public class ShellContext
    {
        private ShellPath _shellPath;
        private readonly List<ShellJob> _jobs;
        public ref readonly ShellPath CurrentPath
        {
            get { return ref _shellPath; }
        }
        public FileProcessor FileProcessor { get; }
        public IEnumerable<ShellJob> Jobs { get => _jobs; }

        public ShellContext(string startupPath)
        {
            _shellPath = new ShellPath(startupPath);
            FileProcessor = new FileProcessor();
            _jobs = new List<ShellJob>();
        }

        public void UpdateCurrentPath(ShellPath newPath)
        {
            if (newPath.IsAbsolute)
            {
                _shellPath = newPath;
            }
            else
            {
                _shellPath = ShellPath.Combine(CurrentPath, newPath);
            }
        }

        public void AddJob(ShellJob job)
        {
            _jobs.Add(job);
        }


    }
}
