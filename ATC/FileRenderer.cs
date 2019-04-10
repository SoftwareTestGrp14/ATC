using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC
{
    public class FileRenderer : IFileRenderer
    {
        private ILogger _logger;
        public FileRenderer(ILogger logger)
        {
            _logger = logger;
        }
        public void Render(ISeparationCondition separation)
        {
            var formattedSeparation =
                $"Separation condition detected at {separation._track1._tag} and {separation._track2._tag} at timestamp: {separation.Timestamp}";
            _logger.Write(formattedSeparation);
        }
    }
}
