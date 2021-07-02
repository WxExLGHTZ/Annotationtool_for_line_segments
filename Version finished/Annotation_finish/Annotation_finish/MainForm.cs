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

        private Drawing _drawing = new Drawing(new Curve[0]);                                        // Zwischenspeicher
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
        public int zahler = 2;
        public int zwischen = 2;


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
            //bitmapList.Clear();
            //pathList.Clear();
            //_savedDrawing.Clear();
            //_drawing.Clear();
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

                pictureBox1.Image = (Image)bitmapList[cur_pic]; // ausgabe des ersten Elements
                path = pathList[cur_pic];

                IsSaved = true;


                foreach (Curve curve in _savedDrawing.Curves)
                {
                    if (curve.ImagePath == path)
                    {
                        _drawing.AddCurve(curve);
                    }
                }
                MessageBox.Show("Press curve button to place a curve", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }


        //Shortcuts input
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) //Emirhan
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

                    pictureBox1.Image = (Image)bitmapList[cur_pic]; // ausgabe des ersten Elements
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

                //int letztecurv = _drawing.Curves.Count - 1;

                //Pen = new Pen(Color.FromArgb(r.Next(0, 256),r.Next(0, 256), r.Next(0, 256)), 10f);

                Pen.Color = penColor;

                colorToolStripMenuItem1.BackColor = penColor;

                // _drawing.Draw(GetPictureBoxGraphics());
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
                    //_savedDrawing = _drawing;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }



        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

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

                    _savedDrawing.AddCurve(_currentCurve); // zwischenspeicher für alle Curves

                    _drawing.AddCurve(_currentCurve);



                    _currentCurve.Draw(GetPictureBoxGraphics());
                    //_drawing.Draw(GetPictureBoxGraphics());


                    IsSaved = false;

                    _currentCurve = null;
                }
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.M)
            {
                _currentCurve = null;
                _clickHandler = Curve.CurveClickHandler;
                StatusManager.Instance.SetStatus(Curve.StartMessage);
            }
        }

        private void pictureBox1_Paint_1(object sender, PaintEventArgs e)
        {
            SetGraphicsTransformToWorld(e.Graphics);
            _drawing.Draw(e.Graphics);

        }


        private void pictureBox1_Resize_1(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        //Curve Button
        private void curveToolStripMenuItem1_Click_1(object sender, EventArgs e) //Emirhan
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
        private void stopDrawingToolStripMenuItem1_Click(object sender, EventArgs e) //Emirhan
        {

            if (_clickHandler == Curve.CurveClickHandler)
            {
                _clickHandler = null;
                StatusManager.Instance.SetStatus("Finished Drawing");
            }
        }


        private void nextToolStripMenuItem1_Click_1(object sender, EventArgs e) //Emirhan
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

        private void backToolStripMenuItem1_Click(object sender, EventArgs e) //Emirhan
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

            //int letztecurv = _drawing.Curves.Count - 1;

            //Pen = new Pen(Color.FromArgb(r.Next(0, 256),r.Next(0, 256), r.Next(0, 256)), 10f);

            Pen.Color = penColor;

            colorToolStripMenuItem1.BackColor = penColor;

            // _drawing.Draw(GetPictureBoxGraphics());
            _currentCurve = null;
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (_drawing.Curves.Count > 0)
            {
                _drawing.RemoveCurve(_drawing.Curves.Count - 1);
                _savedDrawing.RemoveCurve(_savedDrawing.Curves.Count - 1);
                _drawing.Draw(GetPictureBoxGraphics());
                //_savedDrawing = _drawing;
            }
        }

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


                if (MessageBox.Show("Do you want to save changes to your text?", "My Application", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Cancel the Closing event from closing the form.
                    e.Cancel = true;
                    saveToolStripMenuItem1_Click(sender, e);
                    // Call method to save file...
                    //test
                }
            }
        }
    }
}


