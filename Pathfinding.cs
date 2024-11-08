using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace michaelsparty
{
    public class PathFinder
    {

        public string OpenFileDialog()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select a Grid ";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
                else
                {
                    return null; 
                }
            }
        }

        public List<List<char>> LoadGridFromFile(string filePath)
        {
            try
            {
                var lines = File.ReadAllLines(filePath);
                var gridData = new List<List<char>>(); 

                foreach (var line in lines)
                {
                    var row = new List<char>(line);
                    gridData.Add(row);
                }

                return gridData;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load the grid");
                return null;
            }
        }


    }
}
