using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows;
using System.Diagnostics.Eventing.Reader;

namespace GrEditor
{
    public partial class FrmGrEditor : Form
    {
        Graphics mobjCanvas;
        int mintMouseX, mintMouseY;
        Pen lobjActivePen;
        Brush lobjActiveBrush;

        //deklarace promenz ne vytvoreni
        Bitmap mobjBitmapInMemory;
        Graphics mobjGraphicsInMemory;
        
        //Drawing Tools
        enum enTool
        {
            Line,
            Rectangle,
            Ellipse,
            Triangle,
            RightAngleTriangle,
            Curve
        }
        //Tool that is used
        enTool memActiveTool;


        public FrmGrEditor()
        {
            InitializeComponent();

            // creation of drawing graphic
            mobjCanvas = pbCanvas.CreateGraphics();

            //canvas a picture box maji stejne rozmery
            mobjBitmapInMemory = new Bitmap(pbCanvas.Width, pbCanvas.Height, mobjCanvas);
            //z bit mapy vytvoreni grafiky
            mobjGraphicsInMemory = Graphics.FromImage(mobjBitmapInMemory); 

            // init of tool
            memActiveTool = enTool.Line;

            mobjGraphicsInMemory.Clear(Color.White);
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            Brush lobjBrush;

            mobjCanvas.DrawLine(Pens.Red, 15, 15, 100, 100);

            lobjBrush = new SolidBrush(Color.DarkOliveGreen);

            mobjCanvas.FillRectangle(lobjBrush, 50, 50, 400, 200);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mobjCanvas.Clear(Color.White);
        }

        private void statusStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pbCanvas_Click(object sender, EventArgs e)
        {

        }

        private void pbCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            tsCursorCoordinations.Text = "x:" + e.X + " y:" + e.Y;

            if (e.Button == MouseButtons.None) return;

            //na platno se lepi puvodni obrazek
            mobjCanvas.Clear(Color.White);
            mobjCanvas.DrawImage(mobjBitmapInMemory, 0, 0);

            //nakresleni na platno
            GraphicsDraw(mobjCanvas, e);          
        }

        private void pbCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            lobjActivePen = new Pen(pbColorBorder.BackColor);
            lobjActiveBrush = new SolidBrush(pbColorBackground.BackColor);

            
            if (e.Button == MouseButtons.Left)
            {
                //nakresleni do pameti
                GraphicsDraw(mobjGraphicsInMemory, e);
            }
        }
        private void GraphicsDraw(Graphics objGrafika, MouseEventArgs e)
        {
            Pen lobjActivePen = new Pen(pbColorBorder.BackColor);

            switch (memActiveTool)
            {
                // line
                case enTool.Line:
                    objGrafika.DrawLine(lobjActivePen, mintMouseX, mintMouseY, e.X, e.Y);
                    break;
                case enTool.Rectangle:
                    objGrafika.DrawRectangle(lobjActivePen, mintMouseX, mintMouseY, e.X - mintMouseX, e.Y - mintMouseY);
                    break;
                case enTool.Ellipse:
                    objGrafika.DrawEllipse(lobjActivePen, mintMouseX, mintMouseY, e.X - mintMouseX, e.Y - mintMouseY);
                    break;
            }
        }

        private void pbColorSelectRed_MouseDown(object sender, MouseEventArgs e)
        {             
            PictureBox lobjpictureBox;

            lobjpictureBox = (PictureBox)sender;

            if(e.Button == MouseButtons.Left)
                pbColorBorder.BackColor = lobjpictureBox.BackColor;
            else if (e.Button == MouseButtons.Right)
                pbColorBackground.BackColor = lobjpictureBox.BackColor;

        }

        private void rbLine_Click(object sender, EventArgs e)
        {
            RadioButton lrbTool;

            lrbTool = (RadioButton)sender;

            switch (lrbTool.Text)
            {
                case "Line": memActiveTool = enTool.Line; break;
                case "Rectangle": memActiveTool = enTool.Rectangle; break;
                case "Ellipse": memActiveTool = enTool.Ellipse; break;
                case "Triangle": memActiveTool = enTool.Triangle; break;
                case "Right angle triangle": memActiveTool = enTool.RightAngleTriangle; break;
                case "Curve": memActiveTool = enTool.Curve; break;
            }
        }

        private void FrmGrEditor_Load(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }

        private void ulozitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mobjBitmapInMemory.Save("c:\\Temp\\Kreslenicko.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private void konecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void nacistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mobjBitmapInMemory = new Bitmap(@"c:\\Temp\\Kreslenicko.jpeg");
        }

        private void pbCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            mintMouseX = e.X;
            mintMouseY = e.Y;
        }
    }
}
