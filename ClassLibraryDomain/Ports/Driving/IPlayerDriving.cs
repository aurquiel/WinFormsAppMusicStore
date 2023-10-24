using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDomain.Ports.Driving
{
    public interface IPlayerDriving
    {
        public Task<bool> Play(string filePath);

        public bool IsPlaying();

        public void Stop();

        public void Pause();

        public void SetVolume(double value);

        public float GetVolume();

        public long GetLength();

        public long GetPosition();

        public void Seek(long value);

        public TimeSpan CurrentTime();

        public TimeSpan TotalTime();

        public float GetWindowsVolume();
    }
}
