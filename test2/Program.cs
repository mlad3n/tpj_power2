using Spire.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Spire.Presentation.Drawing;
using Spire.Presentation.Drawing.Transition;
using System.IO;

namespace test2
{
    class Program
    {
        static void Main(string[] args)
        {
            Presentation ppt = new Presentation();

            ppt.Slides.Append(); // novi slajd

            //Add title

            ISlide slide = ppt.Slides[ppt.Slides.Count - 1]; // uzmem zadnji slajd
            SizeF pptSize = ppt.SlideSize.Size; // size jebiga

            RectangleF titleRect = new RectangleF(pptSize.Width / 2 - 200, 10, 400, 50);        // TITLE
            IAutoShape titleShape = slide.Shapes.AppendShape(ShapeType.Rectangle, titleRect);
            titleShape.Fill.FillType = FillFormatType.None;
            titleShape.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph titlePara = titleShape.TextFrame.Paragraphs[0];
            titlePara.Text = "Microsoft PowerPoint";
            titlePara.FirstTextRange.FontHeight = 36;
            titlePara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            titlePara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            titlePara.Alignment = TextAlignmentType.Center;

            RectangleF imgtitleRect = new RectangleF(75, 70, pptSize.Width - 200, 30);        // IMGTITLE
            IAutoShape imgtitleShape = slide.Shapes.AppendShape(ShapeType.Rectangle, imgtitleRect);
            imgtitleShape.Fill.FillType = FillFormatType.None;
            imgtitleShape.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph imgtitle = imgtitleShape.TextFrame.Paragraphs[0];
            imgtitle.Text = "Red fox";
            imgtitle.FirstTextRange.FontHeight = 20;
            imgtitle.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            imgtitle.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            imgtitle.Alignment = TextAlignmentType.Left;




            string bgFile = "C:/users/mladen/Desktop/test-images/Red fox.jpg";
            
            RectangleF imgRect = new RectangleF(new PointF(75, 100), new SizeF(pptSize.Width - 150, pptSize.Height - 175));
            slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, bgFile, imgRect);


            slide = ppt.Slides[0]; // uzmem prvi slajd
            pptSize = ppt.SlideSize.Size;

            RectangleF ptitleRect = new RectangleF(pptSize.Width / 2 - 300, pptSize.Height/2 - 50, 600, 100);        // PREZTITLE
            IAutoShape ptitleShape = slide.Shapes.AppendShape(ShapeType.Rectangle, ptitleRect);
            ptitleShape.Fill.FillType = FillFormatType.None;
            ptitleShape.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph ptitlePara = ptitleShape.TextFrame.Paragraphs[0];
            ptitlePara.Text = "Microsoft PowerPoint";
            ptitlePara.FirstTextRange.FontHeight = 50;
            ptitlePara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            ptitlePara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            ptitlePara.Alignment = TextAlignmentType.Center;


            //Save the file
            ppt.SaveToFile("C:/users/mladen/Desktop/test-results/result_new_slide.pptx", FileFormat.Pptx2010);

            string[] files =
              Directory.GetFiles("C:/users/mladen/Desktop/test-images", "*", SearchOption.TopDirectoryOnly);
            foreach (String file in files)
            {
                int index0 = file.IndexOf("\\") + 1;
                int index1 = file.IndexOf(".", index0);
                Console.WriteLine(file);
                int a =file.Length;
                Console.WriteLine(file.Substring(index0, index1 - index0));
            }
            Console.ReadLine();

        }
    }
}
