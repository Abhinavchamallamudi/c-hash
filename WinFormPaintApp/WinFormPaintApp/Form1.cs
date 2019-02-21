/* Name : Aravind Muvva and Abhinav Chamallamudi
 * ZID : Z1835959 and Z1826541
 * Course : CSCI 504
 * Due date : 11-01-2018
 * Purpose : to create a windows form to develop paint app with features like pencil,eraser etc
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormPaintApp
{
    public partial class Form1 : Form
    {
        Graphics g;
        bool startPaint = false;
        //nullable int for storing Null value
        int? initX = null;
        int? initY = null;
        Pen pen;
        float penWidth = 1;
        bool drawLine = false;

        bool startingPoint = true;

        int xStartValue;
        int yStartValue;

        int xEndValue;
        int yEndValue;
        bool isAnyChanges = false;
        string filePath = string.Empty;
        bool isEraserClicked = false;

        bool firstClickOnUndo = false;

        Stack<Bitmap> undo ;

        Stack<Bitmap> redo;

        public Form1()
        {
            InitializeComponent();
            g = PnlDrawBoard.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pen = new Pen(Color.Black, 1);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            cmbPencilSize.Enabled = false;
            cmbPaintBrush.Enabled = false;
            undo = new Stack<Bitmap>();
            redo = new Stack<Bitmap>();
        }

        

        private void btnEditCoulors_Click(object sender, EventArgs e)
        {
            //Open Color Dialog and Set BackColor of btn_PenColor if user click on OK
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
            {
                btnPenColor.BackColor = c.Color;
            }
        }

        private void PnlDrawBoard_Paint(object sender, PaintEventArgs e)
        {

        }


        //Event fired when the mouse pointer is moved over the Panel(pnl_Draw).
        private void PnlDrawBoard_MouseMove(object sender, MouseEventArgs e)     //To draw on the panel when the color is selected and when we try to draw or erase
        {
            
            if (startPaint && !drawLine && !isEraserClicked)
            {
                isAnyChanges = true;
                //Setting the Pen BackColor and line Width
                Pen p = new Pen(btnPenColor.BackColor, penWidth);  //making a pen
                //Drawing the line.
                g.DrawLine(p, new Point(initX ?? e.X, initY ?? e.Y), new Point(e.X, e.Y));
                //g.DrawLine(p, initX ?? e.X, initY ?? e.Y, e.X, e.Y);
                initX = e.X;
                initY = e.Y;
            }
            if (startPaint && drawLine)
            {
                isAnyChanges = true;
                if (startingPoint)
                {
                    xStartValue = e.X;
                    yStartValue = e.Y;
                    startingPoint = false;
                }

            }
            if (startPaint && isEraserClicked)
            {
                isAnyChanges = true;
                //Setting the Pen BackColor and line Width
                Pen p = new Pen(PnlDrawBoard.BackColor, 50);
                //Drawing the line.
                g.DrawLine(p, new Point(initX ?? e.X, initY ?? e.Y), new Point(e.X, e.Y));
                //g.DrawLine(p, initX ?? e.X, initY ?? e.Y, e.X, e.Y);
                initX = e.X;
                initY = e.Y;
            }
        }


        private void DrawALine()   //functionality for draw line
        {
            if (xStartValue != 0 && yStartValue != 0)
            {
                Pen p = new Pen(btnPenColor.BackColor, penWidth);
                g.DrawLine(p, xStartValue, yStartValue, xEndValue, yEndValue);
                drawLine = false;
                startingPoint = true;

            }
           
        }

        //Fired when the mouse pointer is over the pnl_Draw and a mouse button is released.
        private void PnlDrawBoard_MouseUp(object sender, MouseEventArgs e)
        {

            
            if (drawLine)
            {
                xEndValue = e.X;
                yEndValue = e.Y;
                DrawALine();
            }


            Bitmap graphicSurface = new Bitmap(900, 300);
            Graphics gp = Graphics.FromImage(graphicSurface);
            Rectangle rect = PnlDrawBoard.RectangleToScreen(PnlDrawBoard.ClientRectangle);
            gp.CopyFromScreen(rect.Location, Point.Empty, PnlDrawBoard.Size);
            gp.Dispose();
            undo.Push(graphicSurface);

            isEraserClicked = startPaint = false;
            initX = null;
            initY = null;
            xStartValue = yStartValue = xEndValue = yEndValue = 0;

            
        }

        //Event Fired when the mouse pointer is over Panel and a mouse button is pressed
        private void PnlDrawBoard_MouseDown(object sender, MouseEventArgs e)
        {
            startPaint = true;           

        }

        private void btnPenColor_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            btnPenColor.BackColor = p.BackColor;
            
        }

        private void btnPencil_Click(object sender, EventArgs e)   //functionality for pencil
        {
            Cursor.Current = Cursors.Default;
            penWidth = float.Parse(cmbPencilSize.Text);
            cmbPencilSize.Enabled = true;
            cmbPaintBrush.Enabled = false;

        }

        private void cmbPencilSize_SelectedIndexChanged(object sender, EventArgs e)  //assigning the pencil size
        {
            penWidth = float.Parse(cmbPencilSize.Text);

        }

        private void btnPaintBrush_Click(object sender, EventArgs e)  //action of selecting paint brush
        {
            Cursor.Current = Cursors.Cross;
            cmbPencilSize.Enabled = false;
            cmbPaintBrush.Enabled = true;
            penWidth = float.Parse(cmbPaintBrush.Text);
        }

        private void cmbPaintBrush_SelectedIndexChanged(object sender, EventArgs e)  //for paint brush
        {
            penWidth = float.Parse(cmbPaintBrush.Text);
        }

        private void btnEraser_Click(object sender, EventArgs e)  //action for eraser
        {
            isEraserClicked = true;
            // g.Clear(PnlDrawBoard.BackColor);
        }

        private void btnDrawLine_Click(object sender, EventArgs e)  //action for draw line
        {
            drawLine = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)    //to create a new option in our file menu
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to save changes", "WinFormPaint",
                MessageBoxButtons.YesNoCancel);

            if (dialogResult == DialogResult.Yes)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "Images (*.png)|*.png";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                Bitmap graphicSurface = new Bitmap(900, 300);
                Graphics gp = Graphics.FromImage(graphicSurface);
                Rectangle rect = PnlDrawBoard.RectangleToScreen(PnlDrawBoard.ClientRectangle);
                gp.CopyFromScreen(rect.Location, Point.Empty, PnlDrawBoard.Size);
                gp.Dispose();

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    graphicSurface.Save(saveFileDialog1.FileName, ImageFormat.Png);
                    filePath = saveFileDialog1.FileName;
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                g.Clear(PnlDrawBoard.BackColor);
            }          

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)  //to create a save option in our file menu
        {
            if (string.IsNullOrEmpty(filePath))
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "Images (*.png)|*.png";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                Bitmap graphicSurface = new Bitmap(900, 300);
                Graphics gp = Graphics.FromImage(graphicSurface);
                Rectangle rect = PnlDrawBoard.RectangleToScreen(PnlDrawBoard.ClientRectangle);
                gp.CopyFromScreen(rect.Location, Point.Empty, PnlDrawBoard.Size);
                gp.Dispose();

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    graphicSurface.Save(saveFileDialog1.FileName, ImageFormat.Png);
                    filePath = saveFileDialog1.FileName;
                }
            }
            else
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "Images (*.png)|*.png";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                Bitmap graphicSurface = new Bitmap(900, 300);
                Graphics gp = Graphics.FromImage(graphicSurface);
                Rectangle rect = PnlDrawBoard.RectangleToScreen(PnlDrawBoard.ClientRectangle);
                gp.CopyFromScreen(rect.Location, Point.Empty, PnlDrawBoard.Size);
                gp.Dispose();
                graphicSurface.Save(filePath, ImageFormat.Png);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)  //to create a save as option in our menu
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Images (*.png)|*.png";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            Bitmap graphicSurface = new Bitmap(900, 300);
            Graphics gp = Graphics.FromImage(graphicSurface);
            Rectangle rect = PnlDrawBoard.RectangleToScreen(PnlDrawBoard.ClientRectangle);
            gp.CopyFromScreen(rect.Location, Point.Empty, PnlDrawBoard.Size);
            gp.Dispose();

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                graphicSurface.Save(saveFileDialog1.FileName, ImageFormat.Png);
                filePath = saveFileDialog1.FileName;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)  //to create open option in our file menu
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PnlDrawBoard.BackgroundImage = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)  //undo make changes go back by one step
        {

            if (undo.Any())
            {
                var a = undo.Pop();
                redo.Push(a);
                PnlDrawBoard.BackgroundImage = a;
            }


        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)   //redo make changes go up by 1 step if any undo is done
        {
            if (redo.Any())
            {

                var a = redo.Pop();
                undo.Push(a);
                PnlDrawBoard.BackgroundImage = a;
            }
        }
    }
}
