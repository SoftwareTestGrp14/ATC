using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace ATC
{
    public class PlaneTracker : IPlaneTracker
    {
        
        private List<ITrack> tracks = new List<ITrack>();
        private IAirSpaceTracker airSpaceTracker;
        private IAirSpace airSpace;
        private List<string[]> tempDataList = new List<string[]>();
        private List<SeparationCondition> currentSeparations = new List<SeparationCondition>();
        ConsoleLog cLog = new ConsoleLog();

        public PlaneTracker()
        {
            airSpaceTracker= new AirSpaceTracker();
            airSpace = new AirSpace();
        }


        public void Update(string data)
        {
            Console.Clear();
            cLog.Write(data);
            cLog.Write("");
            //initializing temps
            double vel=0;
            double course=0;
            //Converts data to string array
            string[] newData = ConvertTransponderData(data);
            //Flag that checks if 
            bool dataExists = false;

            //Creating tracks
            int i = 0;
            foreach (var AircraftName in tempDataList)
            {
                //checks if there is data for this aircraft already
                
                if (AircraftName[0] == newData[0])
                {
                    //If there the aircraft is already registered the new velocity and course is calculated and the data is overwritten
                    vel = Calculator.CalcVelocity(int.Parse(AircraftName[1]), int.Parse(newData[1]), int.Parse(AircraftName[2]), int.Parse(newData[2]), DateTime.Parse(AircraftName[4]), DateTime.Parse(newData[4]));
                    course = Calculator.CalcCourse(int.Parse(AircraftName[1]), int.Parse(newData[1]), int.Parse(AircraftName[2]), int.Parse(newData[2]));
                    for (int j = 0; j < newData.Length; j++)
                    {
                        AircraftName[j] = newData[j];
                    }
                    dataExists = true;
                }

                i++;
            }

            if (!dataExists)
            {
                //If the data did not exist then it is added to the list.
                tempDataList.Add(newData);
            }
                
                //The track is then created for the new data
                ITrack newTrack = new Track(newData[0], int.Parse(newData[1]), int.Parse(newData[2]), int.Parse(newData[3]), vel , course, DateTime.Parse(newData[4]));



                
                //checks if the new track is in the airspace
                if (airSpaceTracker.IsInAirSpace(airSpace, newTrack) && !tracks.Exists(x=>x._tag==newTrack._tag))
                {
                    //The track is in the airspace and it is not in the list already, it will be added
                    tracks.Add(newTrack);
                }
                else if (!airSpaceTracker.IsInAirSpace(airSpace,newTrack) && tracks.Exists(x => x._tag == newTrack._tag))
                {
                //The track is not in airspace but it is in the list already, it will be removed   
                int index = tracks.FindIndex(x => x._tag == newTrack._tag);
                tracks.RemoveAt(index);

                }
                else if (airSpaceTracker.IsInAirSpace(airSpace, newTrack) && tracks.Exists(x => x._tag == newTrack._tag))
                {
                    //The track is in the airspace and is already in the list, it will be overwritten
                    int index = tracks.FindIndex(x=>x._tag == newTrack._tag);
                    tracks.RemoveAt(index);
                    tracks.Add(newTrack);

                }

            
            //Handles separation
            foreach (var curTrack in tracks)
                {
                    if (curTrack != newTrack)
                    {


                        SeparationCondition newSeparationCondition = new SeparationCondition(curTrack, newTrack);

                        if (Calculator.IsSeparation(curTrack, newTrack))
                        {
                            //Separation detected on the two tracks
                            if (!currentSeparations.Exists(x => x == newSeparationCondition))
                            {
                                //This separation was not previously registered and will be inserted in list
                                currentSeparations.Add(newSeparationCondition);
                            }
                            else
                            {
                                //This separation was previously registered and will overwrite existing

                                int index = currentSeparations.FindIndex(x => x == newSeparationCondition);
                                currentSeparations.RemoveAt(index);
                                currentSeparations.Add(newSeparationCondition);
                            }

                        }
                        else
                        {
                            //Separation not detected on the two tracks
                            if (currentSeparations.Exists(x => x == newSeparationCondition))
                            {
                                //Sepration was previously registered and will be removed
                                int index = currentSeparations.FindIndex(x => x == newSeparationCondition);
                                currentSeparations.RemoveAt(index);
                            }

                            //If it was not registered then nothing needs to be done.
                        }

                    }

                }
            cLog.Write("");
            cLog.Write("All tracks in airspace :");
            foreach (var track in tracks)
            {
                cLog.Write(track._tag);
            }
            cLog.Write("");
            cLog.Write("");

            cLog.Write("");
            cLog.Write("All separations:");
            foreach (var sep in currentSeparations)
            {
                cLog.Write($"Separation between: {sep._track1._tag} and {sep._track2._tag}");
            }
            cLog.Write("");
            cLog.Write("");


        }

        public string[] ConvertTransponderData(string data)
        {
            Debug.WriteLine("Data her:");
            Debug.WriteLine(data);

            string[] separatedData = data.Split(new string[] { ";" }, StringSplitOptions.None);

           
            //Rearranging the date and time to correct format
            string year = separatedData[4].Substring(0, 4);
            string month = separatedData[4].Substring(4, 2);
            string day = separatedData[4].Substring(6, 2);
            string hour = separatedData[4].Substring(8, 2);
            string minute = separatedData[4].Substring(10, 2);
            string second = separatedData[4].Substring(12, 2);

            string dateTime = $"{year}-{month}-{day} {hour}:{minute}:{second}";

            separatedData[4] = dateTime;

            return separatedData;

        }

    }
}
