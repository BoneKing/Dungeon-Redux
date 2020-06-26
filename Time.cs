//returns the ingame time
using System;
using System.Threading;

namespace Dungeon_Redux
{
    class Time
    {
        int day, hour, minute, second;
        public void initTime()
        {
            day = 1;
            hour = 0;
            minute = 0;
            second = 0;
            Console.WriteLine("Day: " + day + " at " + hour + ":" + minute + ":" + second); 
        }
        public void runTime()
        {
            while(day <=7 ){
                if(minute > 59) {
                    hour = hour + 1;
                    minute = 0;
                }
                if(hour > 23) {
                    day = day +1;
                    hour = 0;
                    if (day > 7) {
                        day = 1;
                    }
                }
                if(second > 59) {
                    minute = minute + 1;
                    second = 0;
                }
                else if(second < 60) {
                    Thread.Sleep(1000);
                    second++;
                } 
                //Console.WriteLine("Day: " + day + " at " + hour + ":" + minute + ":" + second);
            }   
            if(day > 7) {
                day = 1;
                hour = 0;
                minute = 0;
                second = 0;
            }
        }
        public void printTime(){
            Console.WriteLine("Day {0} at {1}:{2}:{3}",day,hour, minute, second);
        }
    }
}
