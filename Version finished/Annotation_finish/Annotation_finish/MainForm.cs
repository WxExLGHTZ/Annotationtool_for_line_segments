using Annotation;
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

namespace Annotation_finish
{
    public partial class MainForm : Form
    {

        private Drawing _drawing = new Drawing(new Curve[0]);         // Zwischenspeicher
        private Drawing _savedDrawing = new Drawing(new Curve[0]);    // gesamtheit (Drawing) aller Curven
        private bool IsSaved = true;

        private CurveClickHandler _clickHandler = null;
        private Curve _currentCurve = null;

        List<Bitmap> bitmapList = new List<Bitmap>(); // liste der Bilder
        List<string> pathList = new List<string>();   // lister der Adressen der Bilder
        public string path;                           // platzhalter für EINE Adresse EINES Bildes
        private int cur_pic;

        public Color penColor = Color.BlueViolet;


        public Pen Pen = new Pen(Color.BlueViolet, 10f);

        Graphics pictureBoxGraphics = null;
 



        public MainForm()
        {
            InitializeComponent();
            _drawing.Redraw += OnRedraw;
            StatusManager.Instance.StatusMessageChanged += (s, e) => toolStripStatusLabel1.Text = e.Message;
        }

        private void SetGraphicsTransformToWorld(Graphics g)
        {
            g.ResetTransform();
            g.ScaleTransform(1f, -1f);
            g.TranslateTransform(0f, -pictureBox1.Height);
        }

        private Graphics GetPictureBoxGraphics()
        {
            if (pictureBoxGraphics == null)
            {
                pictureBoxGraphics = pictureBox1.CreateGraphics();
                pictureBoxGraphics.ScaleTransform(1f, -1f);
                pictureBoxGraphics.TranslateTransform(0f, -pictureBox1.Height);
            }
            return pictureBoxGraphics;
        }



        private void OnRedraw(Object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }


        /// <summary>
        /// Öffnen einer Datei (.drw, .bmp, .jpg, .png)
        /// </summary>
  
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                AddExtension = true,
                CheckFileExists = true,
                Filter = "Drawing files(*.drw)|*.DRW|Image Files(*.BMP)|*.BMP|Image Files(*-JPG)|*.JPG|Image Files(.PNG)|*.PNG",
                Title = "Please select a drawing file or image to open.",

            };

            ofd.Multiselect = true; 

            cur_pic = 0;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                bitmapList.Clear();
                pathList.Clear();
                _savedDrawing.Clear();
                _drawing.Clear();


                foreach (string f in ofd.FileNames)
                {
                    string ext = Path.GetExtension(f);

                    if (ext == ".drw")
                    {
                        _savedDrawing.Load(f);
                    }
                    else
                    {
                        pathList.Add(f);
                    }

                }

                foreach (Curve curve in _savedDrawing.Curves)
                {
                    if (!pathList.Contains(curve.ImagePath))
                    {
                        pathList.Add(curve.ImagePath);
                    }
                }

                foreach (string p in pathList)
                {
                    bitmapList.Add(new Bitmap(p));
                }

                pictureBox1.Image = (Image)bitmapList[cur_pic];
                path = pathList[cur_pic];

                IsSaved = true;


                foreach (Curve curve in _savedDrawing.Curves)
                {
                    if (curve.ImagePath == path)
                    {
                        _drawing.AddCurve(curve);
                    }
                }
                MessageBox.Show("Press *New Curve* or *N* to place a new curve", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }


        //Shortcuts input
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Right)
            {

                if (cur_pic < bitmapList.Count - 1)
                {

                    cur_pic++;

                    path = pathList[cur_pic];

                    _drawing.Clear();

                    foreach (Curve curve in _savedDrawing.Curves)
                    {
                        if (curve.ImagePath == path)
                        {
                            _drawing.AddCurve(curve);
                        }
                    }

                    pictureBox1.Image = (Image)bitmapList[cur_pic];

                }

            }
            if (keyData == Keys.Left)
            {

                if (cur_pic > 0)
                {
                    cur_pic--;

                    path = pathList[cur_pic];

                    _drawing.Clear();

                    foreach (Curve curve in _savedDrawing.Curves)
                    {
                        if (curve.ImagePath == path)
                        {
                            _drawing.AddCurve(curve);
                        }
                    }

                    pictureBox1.Image = (Image)bitmapList[cur_pic];
                }


            }
            if (keyData == Keys.O)
            {
                OpenFileDialog ofd = new OpenFileDialog
                {
                    AddExtension = true,
                    CheckFileExists = true,
                    Filter = "Drawing files(*.drw)|*.DRW|Image Files(*.BMP)|*.BMP|Image Files(*-JPG)|*.JPG|Image Files(.PNG)|*.PNG",
                    Title = "Please select a drawing file or image to open.",

                };

                ofd.Multiselect = true;
                bitmapList.Clear();
                pathList.Clear();
                _savedDrawing.Clear();
                _drawing.Clear();
                cur_pic = 0;



                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (string f in ofd.FileNames)
                    {
                        string ext = Path.GetExtension(f);

                        if (ext == ".drw")
                        {
                            _savedDrawing.Load(f);
                        }
                        else
                        {
                            pathList.Add(f);
                        }

                    }

                    foreach (Curve curve in _savedDrawing.Curves)
                    {
                        if (!pathList.Contains(curve.ImagePath))
                        {
                            pathList.Add(curve.ImagePath);
                        }
                    }

                    foreach (string p in pathList)
                    {
                        bitmapList.Add(new Bitmap(p));
                    }

                    pictureBox1.Image = (Image)bitmapList[cur_pic];
                    path = pathList[cur_pic];



                    foreach (Curve curve in _savedDrawing.Curves)
                    {
                        if (curve.ImagePath == path)
                        {
                            _drawing.AddCurve(curve);
                        }
                    }

                }
            }
            if (keyData == Keys.N)
            {
                if (bitmapList.Count != 0)
                {
                    _currentCurve = null;
                    _clickHandler = Curve.CurveClickHandler;
                    StatusManager.Instance.SetStatus(Curve.StartMessage);

                }
                else
                {
                    MessageBox.Show("load image first", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (keyData == Keys.T)
            {
                if (_clickHandler == Curve.CurveClickHandler)
                {
                    _clickHandler = null;
                    StatusManager.Instance.SetStatus("Finished Drawing");
                }
            }
            if (keyData == Keys.S)
            {

                if (_savedDrawing.Curves.Count != 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog
                    {
                        AddExtension = true,
                        DefaultExt = ".drw",
                        CheckPathExists = true,
                        Filter = "Drawing files (*.drw)|",
                        Title = "Please select an annotation file to save."
                    };

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        _savedDrawing.Save(sfd.FileName);
                    }
                    IsSaved = true;
                }
                else
                {
                    MessageBox.Show("no curves placed ", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            if (keyData == Keys.C)
            {
                penColor = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));

                Pen.Color = penColor;

                colorToolStripMenuItem1.BackColor = penColor;

                _currentCurve = null;
            }
            if (keyData == Keys.X)
            {
                _drawing.Clear();
                _savedDrawing.Clear();
                pictureBox1.Invalidate();
            }
            if (keyData == Keys.B)
            {
                if (_drawing.Curves.Count > 0)
                {
                    _drawing.RemoveCurve(_drawing.Curves.Count - 1);
                    _savedDrawing.RemoveCurve(_savedDrawing.Curves.Count - 1);
                    _drawing.Draw(GetPictureBoxGraphics());

                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }



    

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (_clickHandler != null)
            {
                string statusMessage;

                ClickResult result = _clickHandler(e.Location, e.Button, pictureBox1.Height, ref _currentCurve, out statusMessage);
                StatusManager.Instance.SetStatus(statusMessage);

                if (result == ClickResult.Canceled)
                {
                    _currentCurve = null;
                }
                else if (result == ClickResult.Finished)
                {
                    _currentCurve = _currentCurve.CurveGenerator(_currentCurve.xCoordinates(), _currentCurve.yCoordinates());

                    _currentCurve.ImagePath = path;

                    _currentCurve.penColor = penColor;


                    if (Pen != null)
                    {
                        _currentCurve.DrawPen = new Pen(_currentCurve.penColor, 10f);
                    }

                    _savedDrawing.AddCurve(_currentCurve); 

                    _drawing.AddCurve(_currentCurve);


                    _currentCurve.Draw(GetPictureBoxGraphics());
                    
                    IsSaved = false;

                    _currentCurve = null;
                }
            }
        }
        

        private void pictureBox1_Paint_1(object sender, PaintEventArgs e)
        {
            SetGraphicsTransformToWorld(e.Graphics);
            _drawing.Draw(e.Graphics);

        }
        
    

        //Curve Button
        private void curveToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            if (bitmapList.Count != 0)
            {
                _currentCurve = null;
                _clickHandler = Curve.CurveClickHandler;
                StatusManager.Instance.SetStatus(Curve.StartMessage);

            }
            else
            {
                MessageBox.Show("load image first", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Stop Button
        private void stopDrawingToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            if (_clickHandler == Curve.CurveClickHandler)
            {
                _clickHandler = null;
                StatusManager.Instance.SetStatus("Finished Drawing");
            }
        }


        private void nextToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            if (cur_pic < bitmapList.Count - 1)
            {

                cur_pic++;

                path = pathList[cur_pic];

                _drawing.Clear();

                foreach (Curve curve in _savedDrawing.Curves)
                {
                    if (curve.ImagePath == path)
                    {
                        _drawing.AddCurve(curve);
                    }
                }

                pictureBox1.Image = (Image)bitmapList[cur_pic];

            }
        }

        private void backToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            if (cur_pic > 0)
            {
                cur_pic--;

                path = pathList[cur_pic];

                _drawing.Clear();

                foreach (Curve curve in _savedDrawing.Curves)
                {
                    if (curve.ImagePath == path)
                    {
                        _drawing.AddCurve(curve);
                    }
                }

                pictureBox1.Image = (Image)bitmapList[cur_pic];
            }
        }

        private void clearToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _drawing.Clear();
            _savedDrawing.Clear();
            pictureBox1.Invalidate();
        }

        private Random r = new Random();
        private void colorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            penColor = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));

            Pen.Color = penColor;

            colorToolStripMenuItem1.BackColor = penColor;

            _currentCurve = null;
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (_drawing.Curves.Count > 0)
            {
                _drawing.RemoveCurve(_drawing.Curves.Count - 1);
                _savedDrawing.RemoveCurve(_savedDrawing.Curves.Count - 1);
                _drawing.Draw(GetPictureBoxGraphics());
            }
        }


        /// <summary>
        /// speichern einer Datei (.drw)
        /// </summary>
       
        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (_savedDrawing.Curves.Count != 0)
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    AddExtension = true,
                    DefaultExt = ".drw",
                    CheckPathExists = true,
                    Filter = "Drawing files (*.drw)|",
                    Title = "Please select an annotation file to save."
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    _savedDrawing.Save(sfd.FileName);
                }
                IsSaved = true;
            }
            else
            {
                MessageBox.Show("no curves placed ", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void MainForm_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (IsSaved == false)
            {


                if (MessageBox.Show("Do you want to save changes bofore closing?", "My Application", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    e.Cancel = true;
                    saveToolStripMenuItem1_Click(sender, e);
                    
                }
            }
        }

        private void helpShowToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To start work with this application:\n\npress *File* and open a file you want to work with.\n" +
                            "press *Edit*.\npress *New Curve* and start\n\nShortcuts you can use:\n\nO : Open\nS : Save\nX : Clear All\n" +
                            "N : New Curve\nT : Stop Drawing\nB : Delete Curve\nC : New Color\n--> : Next Image:\n<-- : Previous Image", "Information",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

           
        }
    }
}


