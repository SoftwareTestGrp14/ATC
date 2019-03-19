using System;
using System.Collections.Generic;
using System.Text;
using TransponderReceiver;

namespace ATC
{
    public class ATM
    {
        private IPlaneTracker planeTracker;


        public ATM(IPlaneTracker plane)
        {
            planeTracker = plane;

            ITransponderReceiver receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();

            receiver.TransponderDataReady += Receiver_TransponderDataReady;
        }

        private void Receiver_TransponderDataReady(object sender, global::TransponderReceiver.RawTransponderDataEventArgs e)
        {
            var lst = e.TransponderData;

            foreach (var item in lst)
            {
                planeTracker.Update(item);
            }
            
        }
    }
}
