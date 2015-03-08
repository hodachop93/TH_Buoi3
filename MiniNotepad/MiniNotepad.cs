using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Text;


namespace Bai1
{
    public partial class MiniNotepad : Form
    {
        private string filePath = null;
        bool fileChanged = false;
        public MiniNotepad()
        {
            InitializeComponent();
        }

        private void menuItemNew_Click(object sender, EventArgs e)
        {
            newFile();
        }

        private void toolStripBtnNew_Click(object sender, EventArgs e)
        {
            newFile();
        }

        //
        private void newFile()
        {
            if (!fileChanged)
            {
                this.Text = "New Text Document";
                richTxtBox.Text = "";
                richTxtBox.Visible = true;
                filePath = null;
                richTxtBox.Font = new Font(combFont.Text, float.Parse(combSize.Text), FontStyle.Regular);
                fileChanged = false;
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn chưa lưu thay đổi\nBạn có muốn lưu những thay đổi này không?",
                    "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (result)
                {
                    case DialogResult.Yes:
                        saveFile();
                        newFile();
                        break;
                    case DialogResult.No:
                       
                        fileChanged = false;
                        newFile();
                        break;
                    case DialogResult.Cancel:
                        break;
                }
            }

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private void toolStripBtnOpen_Click(object sender, EventArgs e)
        {
            openFile();
        }

        //Mo file san co
        private void openFile()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Text File (*.txt)|*.txt|Word Document (*.doc)|*.doc|Rich Text File (*.rtf)|*.rtf";
            //Mo lai directory o lan truoc
            open.RestoreDirectory = true;
            if (open.ShowDialog() == DialogResult.OK)
            {
                richTxtBox.Visible = true;
                string fileName = Path.GetFileName(open.FileName);
                this.Text = fileName + " - Mini Notepad";
                //Luu dia chi file
                filePath = open.FileName;
                
                string extension = Path.GetExtension(filePath);
                if (extension.Equals(".txt"))
                    richTxtBox.LoadFile(open.FileName, RichTextBoxStreamType.PlainText);
                else if (extension.Equals(".rtf"))
                    richTxtBox.LoadFile(open.FileName, RichTextBoxStreamType.RichText);
            }
            fileChanged = false;
        }

        private void toolStripBtnSave_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        //Save file
        private void saveFile()
        {
            //Tao hop thoai Save File
            SaveFileDialog save = new SaveFileDialog();
            //Mo lai directory da luu o lan truoc
            save.RestoreDirectory = true;
            save.Filter = "Text File (*.txt)|*.txt|Word Document (*.doc)|*.doc|Rich Text File (*.rtf)|*.rtf";
            if (filePath!=null)
            {
                //Luu file, chuyen san dinh dang moi luon
                //.SaveFile(filePath, RichTextBoxStreamType.RichText); 
                string extension = Path.GetExtension(filePath);
                if (extension.Equals(".txt"))
                    richTxtBox.LoadFile(filePath, RichTextBoxStreamType.PlainText);
                else if (extension.Equals(".rtf"))
                    richTxtBox.LoadFile(filePath, RichTextBoxStreamType.RichText);
            }
            else
            {
                if (save.ShowDialog() == DialogResult.OK)
                {
                    string extension = Path.GetExtension(save.FileName);
                    if (extension.Equals("txt"))
                        richTxtBox.SaveFile(save.FileName, RichTextBoxStreamType.PlainText);
                    else if (extension.Equals("rtf"))
                        richTxtBox.SaveFile(save.FileName, RichTextBoxStreamType.RichText); 
                    filePath = save.FileName;
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    this.Text = fileName + " - Mini Notepad";
                }
            }
            fileChanged = false;
        }

        private void MiniNotepad_Load(object sender, EventArgs e)
        {
            int[] size = { 8, 10, 12, 14, 16, 18, 20, 24, 28, 32, 36, 48, 72 };
            for (int i = 0; i < size.Length; i++)
            {
                combSize.Items.Add(size[i]);
            }
            InstalledFontCollection listFont = new InstalledFontCollection();
            foreach (FontFamily font in listFont.Families)
            {
                combFont.Items.Add(font.Name);
                
            }
        }
        //Xu ly trong rich text box khi font thay doi
        private void combFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            setFontAndSize();
            
        }

        private void combSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            setFontAndSize();
        }

        //Ham set font va size cho selected text
        private void setFontAndSize()
        {
            
            FontStyle fontStyle = richTxtBox.SelectionFont.Style;
            if (btnBold.Checked) fontStyle = fontStyle | FontStyle.Bold;
            else fontStyle = fontStyle & ~FontStyle.Bold;
            if (btnItalic.Checked) fontStyle = fontStyle | FontStyle.Italic;
            else fontStyle = fontStyle & ~FontStyle.Italic;
            if (btnUnderLine.Checked) fontStyle = fontStyle | FontStyle.Underline;
            else fontStyle = fontStyle & ~FontStyle.Underline;
            FontFamily fontfa = new FontFamily(combFont.Text);
            System.Drawing.Font font = new System.Drawing.Font(fontfa, float.Parse(combSize.Text),fontStyle);
            richTxtBox.SelectionFont = font;

            
        }

        private void btnFontColor_Click(object sender, EventArgs e)
        {
            if (colorDialogFont.ShowDialog() == DialogResult.OK)
            {
                richTxtBox.SelectionColor = colorDialogFont.Color;
            }
        }

        private void btnLeftAlign_Click(object sender, EventArgs e)
        {
            richTxtBox.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void btnCenterAlign_Click(object sender, EventArgs e)
        {
            richTxtBox.SelectionAlignment = HorizontalAlignment.Center;

        }

        private void btnRightAlign_Click(object sender, EventArgs e)
        {
            richTxtBox.SelectionAlignment = HorizontalAlignment.Right;

        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            setFontAndSize();
        }

        private void btnBold_Click(object sender, EventArgs e)
        {
            setFontAndSize();
        }

        private void btnUnderLine_Click(object sender, EventArgs e)
        {
            setFontAndSize();
        }

        private void cutMenuItem_Click(object sender, EventArgs e)
        {
            cutAction();
        }

        private void BtnCut_Click(object sender, EventArgs e)
        {
            cutAction();
        }

        private void copyMenuItem_Click(object sender, EventArgs e)
        {
            copyAction();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            copyAction();
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            pasteAction();
        }

        private void pasteMenuItem_Click(object sender, EventArgs e)
        {
            pasteAction();
        }

        private void cutAction()
        {
            richTxtBox.Cut();
        }

        private void copyAction()
        {
            
            Clipboard.SetData(DataFormats.Rtf, richTxtBox.SelectedRtf);
            
        }

        private void pasteAction()
        {
            if (Clipboard.ContainsText(TextDataFormat.Rtf))
            {
                richTxtBox.SelectedRtf = Clipboard.GetData(DataFormats.Rtf).ToString();
            }
        }

        private void btnClipArt_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            //open.Filter = "Bitmap File (*.bmp)|*.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(open.FileName);
                Clipboard.SetDataObject(img);
                DataFormats.Format df = DataFormats.GetFormat(DataFormats.Bitmap);
                if (this.richTxtBox.CanPaste(df))
                {
                    richTxtBox.Paste(df);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnFill_Click(object sender, EventArgs e)
        {
            if (colorDialogBackground.ShowDialog() == DialogResult.OK)
            {
                richTxtBox.BackColor = colorDialogBackground.Color;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            richTxtBox.Undo();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            richTxtBox.Redo();
        }

        private void richTxtBox_TextChanged(object sender, EventArgs e)
        {
            fileChanged = true;
        }
    }
}
