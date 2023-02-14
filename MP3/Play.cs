using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace MP3
{
    public static class Play
    {
        //Creates a SoundPlayer field
        public static SoundPlayer soundPlayer = new SoundPlayer();

        //Creates a songIndex variable
        public static int songIndex;

        //Create a new task which is async so the Menu can continue while it's running in the background
        public static Task PlaySong()
        {
            Console.WriteLine("Task started");
            soundPlayer.PlaySync();
            return Task.CompletedTask;
        }


        //Creates a song looping so a song will start after the other ends
        public static async Task<int> SongLoop(string[] songs)
        {
           
            //prints songs length
            Console.WriteLine("songs amount = " + songs.Length);

            for (songIndex = 0; songIndex < songs.Length; songIndex++)
            {
                soundPlayer.SoundLocation = songs[songIndex];
                //prints the index of the songs array for debugging
                Console.WriteLine("Song number: " + songIndex);

                await PlaySong();
            }

            return songIndex;
        }
        //Menu function
        public static async Task Menu(string[] songsList)
        {
            while (true)
            {

                switch (Console.ReadLine())
                {
                    case "/play":

                        _ = Task.Run(() => SongLoop(songsList)); //Should continue without waiting for SongLoop task
                        break;

                    case "/skip":

                        SkipSong(songsList);
                        break;

                    case "/stop":
                        StopSong();
                        break;

                    default:
                        Console.WriteLine("No songs to play.");
                        break;
                }
                Console.WriteLine("got out of the switch");
            }
        }
        public static void StopSong()
        {
            Console.WriteLine("reached the stop function");
            soundPlayer.Stop();
        }
        public static void SkipSong(string[] songs)
        {
            /*int songCounter;

            for(songCounter = 0; songCounter < songs.Length; songCounter++)
            {
                
            }
            */

            if (songIndex++ > songs.Length)
            {
               soundPlayer.SoundLocation = songs[0];
               songIndex = 0;
                Console.WriteLine("Reset");
            }
            else if (songIndex++ == songs.Length)
            {
                songIndex = 0;
                soundPlayer.SoundLocation = songs[0];
                
            }
            else
            {
                soundPlayer.SoundLocation = songs[songIndex++];
                Console.WriteLine("Sound location = " + songIndex);
            }
            
            soundPlayer.Play();

            /*soundPlayer.Stop();
            soundPlayer.SoundLocation = songs[songIndex++];
            soundPlayer.Play();
            */
        }
    }
}
