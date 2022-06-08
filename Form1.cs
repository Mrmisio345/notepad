using System;
using System.IO;
using System.Windows.Forms;

namespace Notepad {
    public partial class Form1 : Form {
        String path = String.Empty;

        public Form1() => InitializeComponent();

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                textBox1.Text = File.ReadAllText(path = openFileDialog1.FileName);
                string[] SplitExtension = openFileDialog1.FileName.Split('.');               
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                File.WriteAllText(path = saveFileDialog1.FileName, textBox1.Text);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            if (!String.IsNullOrWhiteSpace(path)) {
                File.WriteAllText(path, textBox1.Text);
            } else {
                saveAsToolStripMenuItem_Click(sender, e);
            }
        }

        private void exitPrompt() {
            DialogResult = MessageBox.Show("Czy chcesz zapisać bieżący plik?", "Notatnik", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            if (!String.IsNullOrWhiteSpace(textBox1.Text)) {
                exitPrompt();

                if (DialogResult == DialogResult.Yes) {
                    saveToolStripMenuItem_Click(sender, e);
                    textBox1.Text = String.Empty;
                    path = String.Empty;;
                }
                else if (DialogResult == DialogResult.No) {
                    textBox1.Text = String.Empty;;
                    path = String.Empty;;
                }

            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e) => textBox1.SelectAll();

        private void cutToolStripMenuItem_Click(object sender, EventArgs e) => textBox1.Cut();

        private void copyToolStripMenuItem_Click(object sender, EventArgs e) => textBox1.Copy();

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e) => textBox1.Paste();

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) => textBox1.SelectedText = String.Empty;


        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            if (!string.IsNullOrWhiteSpace(textBox1.Text)) {
                exitPrompt();

                if (DialogResult == DialogResult.Yes) {
                    saveToolStripMenuItem_Click(sender, e);
                } else if (DialogResult == DialogResult.Cancel) {
                    e.Cancel = true;
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control) {
                switch (e.KeyCode) {
                    case Keys.A:
                        e.SuppressKeyPress = true;
                        textBox1.SelectAll();
                        break;
                    case Keys.N:
                        e.SuppressKeyPress = true;
                        newToolStripMenuItem_Click(sender, e);
                        break;
                    case Keys.S:
                        e.SuppressKeyPress = true;
                        saveToolStripMenuItem_Click(sender, e);
                        break;
                }
            }
        }
    }
}
