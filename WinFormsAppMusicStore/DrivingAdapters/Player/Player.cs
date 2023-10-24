using ClassLibraryDomain.Ports.Driving;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppMusicStoreAdmin.DrivingAdapters.Player
{
    public class Player : IPlayerDriving
    {
        private Mp3FileReader _audioFile;
        private WaveOutEvent _outputDevice = new WaveOutEvent(); // or WaveOutEvent()
        private EventHandler<(bool, string)> _raiseRichTextInsertMessage;
        private EventHandler _playNextAudio;

        public Player()
        {
            _outputDevice.Volume = 1;
            _outputDevice.PlaybackStopped += _outputDevice_PlaybackStopped;
        }

        public void SetRaiseRichTextInsertMessage(EventHandler<(bool, string)> raiseRichTextInsertMessage)
        {
            _raiseRichTextInsertMessage = raiseRichTextInsertMessage;
        }

        public void SetPlayNextAudio(EventHandler playNextAudio)
        {
            _playNextAudio = playNextAudio;

        }

        private void _outputDevice_PlaybackStopped(object? sender, StoppedEventArgs e)
        {
            if (e.Exception != null)
            {
                _playNextAudio?.Invoke(this, null);
                _raiseRichTextInsertMessage?.Invoke(this, (false, "Excepcion audio detenido: " + e.Exception.Message));
            }

            //Si termino la cancion
            if (_outputDevice.GetPosition() >= _audioFile.TotalTime.TotalMilliseconds)
            {
                _playNextAudio?.Invoke(this, null);
            }
        }

        public async Task<bool> Play(string filePath)
        {
            try
            {
                if (_outputDevice.PlaybackState == PlaybackState.Paused)
                {
                    _outputDevice.Play();
                }
                else if (_outputDevice.PlaybackState != PlaybackState.Playing)
                {
                    _audioFile = new Mp3FileReader(filePath);
                    _outputDevice.Init(_audioFile);
                    _outputDevice.Play();
                }

                return true;
            }
            catch (Exception ex)
            {
                await Task.Delay(100);
                _raiseRichTextInsertMessage?.Invoke(this, (false, "Excepcion al reproducir audio: " + ex.Message));
                _playNextAudio?.Invoke(this, null);
                return false;
            }
        }

        public bool IsPlaying()
        {
            return _outputDevice.PlaybackState == PlaybackState.Playing;
        }

        public void Stop()
        {
            _outputDevice.Stop();
            if (_audioFile != null)
            {
                _audioFile.Close();
            }
        }

        public void Pause()
        {
            _outputDevice.Pause();
        }

        public void SetVolume(double value)
        {
            _outputDevice.Volume = (float)value;
        }

        public float GetVolume()
        {
            return _outputDevice.Volume;
        }

        public long GetLength()
        {
            return _audioFile.Length;
        }

        public long GetPosition()
        {
            return _audioFile.Position;
        }

        public void Seek(long value)
        {
            _audioFile.Position = value;
        }

        public TimeSpan CurrentTime()
        {
            return _audioFile.CurrentTime;
        }

        public TimeSpan TotalTime()
        {
            return _audioFile.TotalTime;
        }

        public float GetWindowsVolume()
        {
            var enumerator = new MMDeviceEnumerator();
            var device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            return device.AudioEndpointVolume.MasterVolumeLevelScalar;
        }
    }
}
