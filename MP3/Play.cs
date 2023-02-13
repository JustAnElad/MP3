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

        //Song thread execution
        public static async Task PlaySong()
        {
            Console.WriteLine("task started");
            soundPlayer.PlaySync();
        }


        //Creates a song looping
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
        public static async void Menu(string[] songsList)
        {
            while (true)
            {

                switch (Console.ReadLine())
                {
                    case "/play":

                        await Play.SongLoop(songsList);
                        break;

                    case "/skip":

                        Play.SkipSong(songsList);
                        break;

                    case "/stop":
                        Play.StopSong();
                        break;

                    default:
                        Console.WriteLine("No songs to play.");
                        break;
                }
            }
        }
        public static void StopSong()
        { 
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
