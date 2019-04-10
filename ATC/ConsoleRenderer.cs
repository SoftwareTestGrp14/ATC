using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC
{
    public class ConsoleRenderer : IConsoleRenderer
    {
        private ILogger _logger;
        public ConsoleRenderer(ILogger logger)
        {
            _logger = logger;
        }

        public ConsoleRenderer()
        {
            _logger = new ConsoleLog();
        }
        public void Render(List<ITrack> tracks)
        {
            _logger.Write("All tracks in airspace:");
            foreach (var track in tracks)
            {
                string formattedTrack =
                    $"Tag: {track._tag} - (X,Y): ({track._xCord}, {track._yCord}) - Alt: {track._alt} - Vel: {track._velocity}M/s - Course: {track._course}";
                _logger.Write(formattedTrack);
            }
            _logger.Write("");
        }

        public void Render(List<ISeparationCondition> separations)
        {
            _logger.Write("All separations:");
            foreach (var separation in separations)
            {
                string formattedSeparation =
                    $"Separation between: {separation._track1._tag} and {separation._track2._tag}";
                _logger.Write(formattedSeparation);
            }
        }
    }
}
