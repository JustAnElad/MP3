using System.Media;
using System.IO;



namespace MP3
{
    public class Program
    {
        static void Main(string[] args)
        {


            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string folder = desktop + "/nice_ass_songs/";

            //create directory
            if (!Directory.Exists(folder))
            {
                DirectoryInfo directoryInfo = Directory.CreateDirectory(folder);
                directoryInfo.Attributes = FileAttributes.Directory;
            }

            FileInfo[] files = new DirectoryInfo(folder).GetFiles();

            // Check if the array is empty
            if (files.Length == 0)
            {
                Console.WriteLine("The folder is empty.");

            }
            
            Console.Clear();
            //creates songs' array
            string[] songsList = new string[files.Length];
            //fills the array with the songs' name
            for( int i = 0; i < songsList.Length; i++)
            {
                songsList[i] = folder + files[i].Name;
                Console.WriteLine(songsList[i]);
            }
             Play.Menu(songsList);
            
        }
    }
}