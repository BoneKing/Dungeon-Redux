//returns the ingame time
using System;
using System.Threading;

namespace Dungeon_Redux
{
    class Time
    {
        public int day, hour, minute, second;
        public void initTime()
        {
            day = 1;
            hour = 1;
            minute = 0;
            //second = 0;
            Console.WriteLine("Day: " + day + " at " + hour + ":" + minute); 
        }
        public void runTime()
        {
            while(day < 8 ) {
                if(minute > 59) {
                    hour = hour + 1;
                    //Console.WriteLine("\nNEW HOUR: {0}", hour);
                    minute = 0;
                }
                if(hour > 23) {
                    day = day +1;
                    Console.WriteLine("\nStart of Day: {0}\n", day);
                    hour = 0;
                    if (day > 7) {
                        Console.WriteLine("larger than 7");
                        Console.WriteLine("going to endTime");
                        endTime();
                    }
                }
                if(second > 59) {
                    minute = minute + 1;
                    second = 0;
                    //Console.WriteLine("Day: "+ day+ " at "+hour+":"+minute);
                }
                else if(second < 60) {
                    Thread.Sleep(10000);
                    second++;
                } 
                //Console.WriteLine("Day: " + day + " at " + hour + ":" + minute + ":" + second);
            } 
            //return 0;  
        }
        public void printTime(){
            Console.WriteLine("Day {0} at {1}:{2}",day,hour, minute);
        }
        public bool endTime(){
            if(day > 7){
                Console.WriteLine("End of Day 7, Mission Accomplished!");
                return true;
            }
            else{
                return false;
            }
        }
        public int getDay(){
            return day;
        }
        public int getHour(){
            return hour;
        }
        public int getMinute(){
            return minute;
        }
    }
}
