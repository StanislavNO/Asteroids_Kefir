using System;
using System.Collections.Generic;

namespace Assets.Source.Code_base
{
    public class PauseController
    {
        private List<IPause> _pauses;

        public PauseController()
        {
            _pauses = new List<IPause>();
        }

        public void Add(IPause pause)
        {
            if (pause == null)
                throw new ArgumentNullException(nameof(pause));

            _pauses.Add(pause);
        }

        public void Pause()
        {
            foreach (IPause pause in _pauses)
                pause.Pause(true);
        }

        public void Play()
        {
            foreach (IPause pause in _pauses)
                pause.Pause(false);
        }
    }
}
