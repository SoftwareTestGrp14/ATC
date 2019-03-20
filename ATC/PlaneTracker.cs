﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ATC
{
    public class PlaneTracker : IPlaneTracker
    {

        public List<ITrack> _tracks { get; }
        public IAirSpaceTracker _airSpaceTracker { get; }
        public IAirSpace _airSpace { get; }
        public List<string[]> tempDataList = new List<string[]>();
        public List<ISeparationCondition> _currentSeparations { get; }
        public ConsoleLog _cLog { get;}
        public FileLog _fLog { get; }

        public PlaneTracker(IAirSpace airSpace, IAirSpaceTracker airSpaceTracker,List<ISeparationCondition> currentSeparations, List<ITrack> tracks, ConsoleLog cLog, FileLog fLog)
        {
            _cLog = cLog;
            _fLog = fLog;
            _tracks = tracks;
            _currentSeparations = currentSeparations;
            _airSpaceTracker = airSpaceTracker;
            _airSpace = airSpace;
        }


        public void Update(string data)
        {
           // Console.Clear();
            _cLog.Write(data);
            _cLog.Write("");
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
            else
            {
               
                
                //The track is then created for the new data
                ITrack newTrack = new Track(newData[0], int.Parse(newData[1]), int.Parse(newData[2]), int.Parse(newData[3]), vel, course, DateTime.Parse(newData[4]));
                
                //checks if the new track is in the airspace
                if (_airSpaceTracker.IsInAirSpace(_airSpace, newTrack) && !_tracks.Exists(x => x._tag == newTrack._tag))
                {
                   
                    //The track is in the airspace and it is not in the list already, it will be added
                    _tracks.Add(newTrack);
                }
                else if (!_airSpaceTracker.IsInAirSpace(_airSpace, newTrack) && _tracks.Exists(x => x._tag == newTrack._tag))
                {
                    
                    //The track is not in airspace but it is in the list already, it will be removed   
                    int index = _tracks.FindIndex(x => x._tag == newTrack._tag);
                    _tracks.RemoveAt(index);

                }
                else if (_airSpaceTracker.IsInAirSpace(_airSpace, newTrack) && _tracks.Exists(x => x._tag == newTrack._tag))
                {
                    
                    //The track is in the airspace and is already in the list, it will be overwritten
                    int index = _tracks.FindIndex(x => x._tag == newTrack._tag);
                    _tracks.RemoveAt(index);
                    _tracks.Add(newTrack);

                }
              


                //Handles separation
                foreach (var curTrack in _tracks)
                {
                    if (curTrack != newTrack)
                    {


                        SeparationCondition newSeparationCondition = new SeparationCondition(curTrack, newTrack);

                        if (Calculator.IsSeparation(curTrack, newTrack))
                        {
                            //Separation detected on the two tracks
                            if (!_currentSeparations.Exists(x => x == newSeparationCondition))
                            {
                                //This separation was not previously registered and will be inserted in list
                                _currentSeparations.Add(newSeparationCondition);
                                _fLog.Write($"Separation condition detected at {newSeparationCondition._track1._tag} and {newSeparationCondition._track2._tag} at timestamp: {newSeparationCondition.Timestamp}");

                            }
                            else
                            {
                                //This separation was previously registered and will overwrite existing

                                int index = _currentSeparations.FindIndex(x => x == newSeparationCondition);
                                _currentSeparations.RemoveAt(index);
                                _currentSeparations.Add(newSeparationCondition);
                            }

                        }
                        else
                        {
                            //Separation not detected on the two tracks
                            if (_currentSeparations.Exists(x => x == newSeparationCondition))
                            {
                                //Sepration was previously registered and will be removed
                                int index = _currentSeparations.FindIndex(x => x == newSeparationCondition);
                                _currentSeparations.RemoveAt(index);
                            }

                            //If it was not registered then nothing needs to be done.
                        }

                    }

                }

                //Writes to log
                _cLog.Write("");
                _cLog.Write("All tracks in airspace :");
                foreach (var track in _tracks)
                {
                    _cLog.Write(track._tag);
                }
                _cLog.Write("");
                _cLog.Write("");

                _cLog.Write("");
                _cLog.Write("All separations:");
                foreach (var sep in _currentSeparations)
                {
                    _cLog.Write($"Separation between: {sep._track2} and {sep._track2._tag}");
                }
                _cLog.Write("");
                _cLog.Write("");
            }





                



        }

        public string[] ConvertTransponderData(string data)
        {
            Debug.WriteLine("Data her:");
            Debug.WriteLine(data);

            string[] separatedData = data.Split(new string[] { ";" }, StringSplitOptions.None);

           
            //Rearranging the date and time to correct format
            string year = separatedData[4].Substring(0, 5);
            string month = separatedData[4].Substring(5, 2);
            string day = separatedData[4].Substring(7, 2);
            string hour = separatedData[4].Substring(9, 2);
            string minute = separatedData[4].Substring(11, 2);
            string second = separatedData[4].Substring(13, 2);

            string dateTime = $"{year}-{month}-{day} {hour}:{minute}:{second}";

            separatedData[4] = dateTime;

            return separatedData;

        }

    }
}
