using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumentCentre
{
    public partial class Form1 : Form
    {
        int counter = 0;
        int currentPage;
        FontDialog fd = new FontDialog();

        public Form1()
        {
            InitializeComponent();

            rtbx_TextPrint.Font = fd.Font;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string currentLine;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            float rightMargin = e.MarginBounds.Right;
            float bottomMargin = e.MarginBounds.Bottom;

            float yPos = 0;
            int nPages;
            int nLines;
            int i;

            nLines = (int)(e.MarginBounds.Height / fd.Font.GetHeight(e.Graphics));
            nPages = (rtbx_TextPrint.Lines.Length - 1) / nLines + 1;



            i = 0;
            while ((i < nLines) && (counter < rtbx_TextPrint.Lines.Length))
            {
                currentLine = rtbx_TextPrint.Lines[counter];

                yPos = topMargin + i * fd.Font.GetHeight(e.Graphics);
                e.Graphics.DrawString(currentLine, fd.Font, new SolidBrush(rtbx_TextPrint.SelectionColor), new RectangleF(leftMargin, yPos, rightMargin, bottomMargin), new StringFormat());

                counter++;
                i++;
            }

            e.HasMorePages = false;
            if (currentPage < nPages)
            {
                ++currentPage;
                e.HasMorePages = true;
            }
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            counter = 0;
            currentPage = 1;
        }

        private void настройкаСтраницыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.ShowDialog();
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void предпромотрПечатиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }

        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fd.ShowDialog() == DialogResult.OK)
            {
                rtbx_TextPrint.Font = fd.Font;
            }
        }
    }
}
