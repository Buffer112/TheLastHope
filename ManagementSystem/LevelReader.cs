using System.IO;

namespace TheLastHope.Management
{
    /// <summary>
    /// Is used for getting level's map from file.
    /// </summary>
    public class LevelReader
    {
        string levelToRead = string.Empty;
        string[] level;
		private int levelLength;
        char divider;

        /// <summary>
        /// Reads map from file.
        /// </summary>
        /// <param name="levelToRead">Name of map-file without extension.</param>
        /// <param name="divider">Separator of elements in one string of map.</param>
        public LevelReader(string levelToRead, char divider)
        {
            this.levelToRead = levelToRead;
            this.divider = divider;

            if (File.Exists(levelToRead))
            {
                level = File.ReadAllLines(levelToRead);
				levelLength = level.Length;
            }
        }

		public int LevelLength { get => levelLength; }

		/// <summary>
		/// Returns array of code of gameObject for each horizontal line.
		/// </summary>
		/// <param name="lineNumber">Number of current vertical line.</param>
		/// <returns></returns>
		public string[] GetLine(int lineNumber)
        {
            if (level != null)
            {
                return level[lineNumber].Split(divider);
            }
            else
            {
                return new string[15]; //MAGIC NUMBERS
            }
        }

    }
}

