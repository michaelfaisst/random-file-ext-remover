using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomFileExtRemover
{
    public partial class Form1 : Form
    {

        private string _selectedPath;

        public Form1()
        {
            InitializeComponent();
            btnGo.Enabled = false;
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            var result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                _selectedPath = folderBrowserDialog.SelectedPath;
                txtPath.Text = _selectedPath;
                btnGo.Enabled = true;
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            var result =
                MessageBox.Show(
                    "Es wird empfohlen vor Ausführung der Umbenennung eine Sicherungskopie des Ordners zu machen. Fortfahren?",
                    "", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {

                var allFilesPaths = Directory.GetFiles(_selectedPath, "*.*", SearchOption.AllDirectories);

                lbxOutput.Items.Clear();
                foreach (var filePath in allFilesPaths)
                {
                    var lastDotIndex = filePath.LastIndexOf(".", StringComparison.InvariantCulture);

                    if (filePath.Length - lastDotIndex - 1 == 6)
                    {
                        var newFileName = filePath.Substring(0, lastDotIndex);
                        lbxOutput.Items.Add("Datei gefunden: " + filePath);
                        lbxOutput.Items.Add("Neuer Dateiname: " + newFileName);

                        File.Move(filePath, newFileName);
                        lbxOutput.Items.Add("Erfolgreich umbenannt");

                        lbxOutput.Items.Add("");
                    }
                }

                MessageBox.Show("Alle Dateien erfolgreich umbenannt");
            }
        }
    }
}
