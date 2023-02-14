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

        //Creates a song looping so a song will start after the other ends
        public static async Task<int> SongLoop(string[] songsList, CancellationTokenSource CancellationTokenSource)
        {
           
            //prints songs length
            Console.WriteLine("songs amount = " + songsList.Length);

            for (songIndex = 0; songIndex < songsList.Length; songIndex++)
            {
                soundPlayer.SoundLocation = songsList[songIndex];
                //prints the index of the songs array for debugging
                Console.WriteLine("Song number: " + songIndex);

                await Task.Run(() =>
                {
                    if (!CancellationTokenSource.Token.IsCancellationRequested)
                    {
                        Console.WriteLine("Task started");
                        soundPlayer.PlaySync();
                    }
                    else if (CancellationTokenSource.Token.IsCancellationRequested)
                    {
                        CancellationTokenSource.Dispose();
                        Console.WriteLine("cancelled");
                        Menu(songsList);
                    }
                    
                });
               
            }

            return songIndex;
        }
        //Menu function
        public static void Menu(string[] songsList)
        {
            var CancellationTokenSource = new CancellationTokenSource();



            while (true)
            {
                Console.WriteLine("Reached Menu");
                switch (Console.ReadLine())
                {
                    case "/play":

                        _ = Task.Run(() => SongLoop(songsList, CancellationTokenSource)); 
                        break;

                    case "/skip":

                        SkipSong(songsList);
                        break;

                    case "/stop":
                        Console.WriteLine("reached stop case");
                        CancellationTokenSource.Cancel();
                       
                        break;

                    default:
                        Console.WriteLine("No songs to play.");
                        break;
                }
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
