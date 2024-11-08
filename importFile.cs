using System.IO;
using System.Windows.Forms;

namespace michaelsparty
{
    public class ImportFile
    {
        public string fileToString;

        public string FileToString()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.Title = "Select a File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    fileToString = File.ReadAllText(filePath);
                    return fileToString;
                }
                else
                {
                    MessageBox.Show("No file selected.", "File Import", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }
            }
        }
    }
}
