using DrawingApplication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assign_3_BS_19011519_006
{
    public partial class Shape : Form
    {
        public Shape()
        {
            InitializeComponent();

            this.Width = 1150;
            this.Height = 700;
            bm = new Bitmap(pic.Width, pic.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            pic.Image = bm;
           // fractal.start_x = pic.Width / 2;
           // fractal.start_y = pic.Height / 2;
        }

        Bitmap bm;
        Graphics g;
        bool paint = false;
        Point px, py;
        Pen p = new Pen(Color.Black, 1);
        int index;
        Pen erase = new Pen(Color.White, 20);
        public int x, y, sX, sY, cX, cY, x2, y2, startAngle, sweepAngle;
        //SmileyFace smileyFace = new SmileyFace();
        //Fractal fractal = new Fractal();

        /*
         Create a variable names for Color dialogbox & new color
         */
        ColorDialog cd = new ColorDialog();
        Color new_color;


        private void btn_rect_Click(object sender, EventArgs e)
        {
            /*
             if rect btn is pressed then active index==4
            to draw Rectangle
             */
            index = 4;
        }
       

        private void btn_line_Click(object sender, EventArgs e)
        {
            /*
            if Line btn is pressed then active index==5
            to draw straight Line
            */
            index = 5;
        }

        private void pic_paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (paint)
            {
                if (index == 3)
                {
                    g.DrawEllipse(p, cX, cY, sX, sY);
                }
                /*
                 Method to draw Rectangle if mouse is up &
                paint bool value is false & index==4
                 */
                if (index == 4)
                {
                    g.DrawRectangle(p, cX, cY, sX, sY);
                }
                /*
                 Method to draw straight Line if mouse is up
                & paint bool value is false & index==5
                 */
                if (index == 5)
                {
                    g.DrawLine(p, cX, cY, x, y);
                }
                if (index == 8)
                {
                    sX = 50;
                    sY = 50;
                    x = sX + 40;
                    y = sY + 40;
                    cX = 40;
                    cY = 40;
                    e.Graphics.DrawEllipse(p, sX, sY, cX, cY);
                    x2 = x + 8;
                    y2 = y + 15;
                    x = 24;
                    y = 20;
                    startAngle = 0;
                    sweepAngle = 180;
                    e.Graphics.DrawArc(p, x2, y2, x, y, startAngle, sweepAngle);
                    x = x + 8;
                    y = y + 10;
                    cX = 8;
                    cY = 8;
                    e.Graphics.DrawEllipse(p, sX, sY, cX, cY);
                    sX = sY + 16;
                    e.Graphics.DrawEllipse(p, sX, sY, cX, cY);
                }
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            pic.Image = bm;
            index = 0;
        }

        private void btn_color_Click(object sender, EventArgs e)
        {
            /*
             if color btn is pressed then open color dialogbox and set
            the selected color to the new_color, pen color and pic_color....
             */
            cd.ShowDialog();
            new_color = cd.Color;
            pic_color.BackColor = cd.Color;
            p.Color = cd.Color;
        }

        private void btn_ellipse_Click(object sender, EventArgs e)
        {
            index = 3;
        }

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            /*
             Method for pencil to draw free-form line 
            if the bool paint value is true and index == 1,
            user click & move the mouse...
             */
            if (paint)
            {
                if(index == 1)
                {
                    px = e.Location;
                    g.DrawLine(p, px, py);
                    py = px;
                }
                if (index == 2)
                {
                    px = e.Location;
                    g.DrawLine(erase, px, py);
                    py = px;
                }

            }
            pic.Refresh();
            /*
             If mouse is moving then set the start and end points
            to get the height and width
             */
            x = e.X;
            y = e.Y;
            sX = e.X - cX;
            sY = e.Y - cY;
        }

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            //if mouse is up then set the bool paint value as false
            paint = false;
            sX = x - cX;
            sY = y - cY;

            /*
             Method to draw the ellipse if mouse is up &
            paint bool value is false & index==3
                      */
            if (index == 3)
            {
                g.DrawEllipse(p, cX, cY, sX, sY);   
            }
            /*
             Method to draw Rectangle if mouse is up &
            paint bool value is false & index==4
             */
            if(index == 4)
            {
                g.DrawRectangle(p, cX, cY, sX,sY);
            }
            /*
             Method to draw straight Line if mouse is up
            & paint bool value is false & index==5
             */
            if (index == 5)
            {
                g.DrawLine(p, cX, cY, x, y);
            }
            if (index == 8)
            {
               // DrawingP
            }

        }

        private void btn_pencil_Click(object sender, EventArgs e)
        {
            /*
             if pencil btn is pressed then active 
            index==1 code to draw lines..
             */
            index = 1;
        }

        private void pic_MouseClick(object sender, MouseEventArgs e)
        {
            if(index == 7)
            {
                Point point = set_point(pic, e.Location);
                Fill(bm, point.X, point.Y, new_color);
            }
        }

        private void btn_fill_Click(object sender, EventArgs e)
        {
            index = 7;
        }

      
        private void btn_eraser_Click(object sender, EventArgs e)
        {
            index = 2;
        }
        

        private void btnSmiley_Click(object sender, PaintEventArgs e)
        {
            
            index = 8;
        }

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            /*if user click on the pic canvas then set the paint bool 
             value as true & assign the click point to py...
             */
            paint = true;
            py = e.Location;
            /*
             if mouse is doen then set X<Y co=ordinates to draw from
             */
            cX = e.X;
            cY = e.Y;
        }
        static Point set_point(PictureBox pb, Point pt)
        {
            float pX = 1f * pb.Image.Width / pb.Width;
            float pY = 1f * pb.Image.Height / pb.Height;
            return new Point((int)(pt.X * pX), (int)(pt.Y * pY));
        }
        private void color_picker_MouseClick(object sender, MouseEventArgs e)
        {
            Point point = set_point(color_picker, e.Location);
            pic_color.BackColor = ((Bitmap)color_picker.Image).GetPixel(point.X, point.Y);
            new_color = pic_color.BackColor;
            p.Color = pic_color.BackColor;
        }

        private void validate(Bitmap bm, Stack<Point>sp, int x, int y, Color old_color, Color new_color)
        {
            /*
             Creating method to validate pixel old_color before
            filling the shape to the new_color...
             */
            Color cx = bm.GetPixel(x, y);
            if (cx == old_color)
            {
                sp.Push(new Point(x, y));
                bm.SetPixel(x, y, new_color);
            }
        }

        public void Fill(Bitmap bm, int x, int y, Color new_clr)
        {
            /*
             Creating FloodFill function using validate method....
             */
            Color old_color = bm.GetPixel(x, y);
            Stack<Point> pixel = new Stack<Point>();
            pixel.Push(new Point(x, y));
            pixel.Push(new Point(x, y));
            bm.SetPixel(x, y, new_clr);
            if (old_color == new_clr)
                return;
            while(pixel.Count > 0)
            {
                /*
                 This method will get the old pixel color and fill new_color
                from the clicked point till the stack count > 0 else if the old_color
                is equal to new_color then return....do nothing...
                 */
                Point pt = (Point)pixel.Pop();
                if(pt.X>0 && pt.Y>0 && pt.X<bm.Width-1 && pt.Y < bm.Height - 1)
                {
                    /*Here first it will validate then fill the stack point*/
                    validate(bm, pixel, pt.X - 1, pt.Y, old_color, new_color);
                    validate(bm, pixel, pt.X, pt.Y-1, old_color, new_color);
                    validate(bm, pixel, pt.X+1, pt.Y, old_color, new_color);
                    validate(bm, pixel, pt.X, pt.Y+1, old_color, new_color);
                }

            }

        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Image(*.jpg)|*.jpg|(*.*|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Bitmap btm = bm.Clone(new Rectangle(0, 0, pic.Width, pic.Height), bm.PixelFormat);
                btm.Save(sfd.FileName, ImageFormat.Jpeg);
                MessageBox.Show("Image Saved Successfully!");
            }
        }


    }
}
/*  private void drawLine()
        {
            Random random = new Random();
            p.Color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            fractal.angle = fractal.angle + fractal.point_angle;
            fractal.length = fractal.length + fractal.point_increment;

            fractal.end_x = (int)(fractal.start_x + Math.Cos(fractal.angle * .017453292519) * fractal.length);
            fractal.end_y = (int)(fractal.start_y + Math.Sin(fractal.angle * .017453292519) * fractal.length);

            Point[] points =
            {
                    new Point(fractal.start_x, fractal.start_y),
                    new Point(fractal.end_x, fractal.end_y),
               };

            fractal.start_x = fractal.end_x;
            fractal.start_y = fractal.end_y;

            g.DrawLines(p, points);
        }
       
        private void btnfractal_Click(object sender, EventArgs e)
        {
            fractal.length = fractal.point_length;
            fractal.increment = fractal.point_increment;
            fractal.angle = fractal.point_angle;

            fractal.start_x = pic.Width / 2;
            fractal.start_y = pic.Height / 2;

            pic.Refresh();

            p.Width = 1;
            fractal.length = fractal.point_length;
            g = pic.CreateGraphics();
            for (int i = 0; i < fractal.point_lines; i++)
                drawLine();
        }
       */