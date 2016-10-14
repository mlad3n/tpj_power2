using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Drawing;
using Spire.Presentation;
using Spire.Presentation.Drawing;
using Spire.Presentation.Drawing.Transition;
using System.IO;

namespace generate_ppt
{

    /*
      public Person SomeMethod(string fName)
        {
            var con = ConfigurationManager.ConnectionStrings["Yourconnection"].ToString();

            Person matchingPerson = new Person();
            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = "Select * from Employees where FirstName=@fName";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                oCmd.Parameters.AddWithValue("@Fname", fName);           
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {    
                        matchingPerson.firstName = oReader["FirstName"].ToString();
                        matchingPerson.lastName = oReader["LastName"].ToString();                       
                    }

                    myConnection.Close();
                }               
            }
            return matchingPerson;
        }
        */
    class Program
    {

        static void Main(string[] args)
        {
            List<Tuple<string, List<Tuple<string, string, string>>>> data = getData();

            generate_reports(data);
        }


        static List<Tuple<string, List<Tuple<string, string, string>>>> getData()
        {
            List<Tuple<string, List<Tuple<string, string, string>>>> rez = new List<Tuple<string, List<Tuple<string, string, string>>>>();
            List<Tuple<string, DateTime, int, string, string, string>> reports = getReports();
            DateTime now = DateTime.Now;
            foreach ( var report in reports) {
                if (((now.Date - report.Item2.Date).Days >= 0) && ((now.Date - report.Item2.Date).Days % report.Item3 == 0))
                {                
                    List<Tuple<string, string, string>> report_data = new List<Tuple<string, string, string>>();
                    report_data.Add(new Tuple<string, string, string>("location", report.Item4, ""));
                    report_data.Add(new Tuple<string, string, string>("background", report.Item6, ""));
                    report_data.Add(new Tuple<string, string, string>("presentation_name", report.Item5, ""));
                    getReportData(report.Item1, report_data);
                    rez.Add(new Tuple<string, List<Tuple<string, string, string>>>(report.Item1, report_data));
                }
            }
            return rez;
        }

        private static List<Tuple<string, DateTime, int, string, string, string>> getReports()
        {
            var con_string = "Data Source =(localdb)\\ProjectsV13; Initial Catalog = Database1; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            List<Tuple<string, DateTime, int, string, string, string>> rez = new List<Tuple<string, DateTime, int, string, string, string>>();

            using (SqlConnection myConnection = new SqlConnection(con_string))
            {
                string oString = "select * from dbo.main";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        if (oReader["active"].ToString() == "True")
                        {
                            rez.Add(new Tuple<string, DateTime, int, string, string, string>(oReader["report"].ToString(), DateTime.Parse(oReader["start"].ToString()), Int32.Parse(oReader["period"].ToString()), oReader["save_to_path"].ToString(), oReader["presentation_title"].ToString(), oReader["design_template_path"].ToString()));
                        }
                    }

                    myConnection.Close();
                }
            }

            return rez;
        }

        private static void getReportData(string report, List<Tuple<string, string, string>> data)
        {
            string con_string = "Data Source =(localdb)\\ProjectsV13; Initial Catalog = Database1; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";

            using (SqlConnection myConnection = new SqlConnection(con_string))
            {
                string oString = "select item, chapter, content from ( select 'chapter' as item, chapter, chapter_name as content from dbo.chapter where report = @report union all select 'source' as item, chapter,source_path as content from dbo.script where report = @report ) as source order by chapter, item";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                oCmd.Parameters.AddWithValue("@report", report);
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        data.Add(new Tuple<string, string, string>(oReader["item"].ToString(), oReader["chapter"].ToString(), oReader["content"].ToString()));
                    }

                    myConnection.Close();
                }
            }
        }

        private static void generate_reports(List<Tuple<string, List<Tuple<string, string, string>>>> data)
        {
            foreach (var report_pair in data)
            {
                Presentation ppt = new Presentation();

                string report_name = report_pair.Item1;
                string save_path = "";
                string presentation_name = "";
                string background = "";

                bool new_chapter = true;

                foreach ( var detail in report_pair.Item2)
                {
                    if (detail.Item1 == "location" || detail.Item1 == "background")
                    {
                        if (detail.Item1 == "location")
                            save_path = detail.Item2;
                        else
                            background = detail.Item2;
                    }
                    else
                    {
                        if (detail.Item1 == "presentation_name")
                        {
                            
                            presentation_name = detail.Item2;

                            ISlide slide = ppt.Slides[0]; // uzmem prvi slajd
                            SizeF pptSize = ppt.SlideSize.Size;

                            RectangleF bgRect = new RectangleF(new PointF(0, 0), pptSize);
                            slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, background, bgRect);

                            RectangleF ptitleRect = new RectangleF(pptSize.Width / 2 - 300, pptSize.Height / 2 - 50, 600, 100);        // PREZTITLE
                            IAutoShape ptitleShape = slide.Shapes.AppendShape(ShapeType.Rectangle, ptitleRect);
                            ptitleShape.Fill.FillType = FillFormatType.None;
                            ptitleShape.ShapeStyle.LineColor.Color = Color.Empty;
                            TextParagraph ptitlePara = ptitleShape.TextFrame.Paragraphs[0];
                            ptitlePara.Text = presentation_name;
                            ptitlePara.FirstTextRange.FontHeight = 50;
                            ptitlePara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                            ptitlePara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                            ptitlePara.Alignment = TextAlignmentType.Center;


                        }
                        else
                        {
                            if (detail.Item1 == "chapter")
                            {
                                ppt.Slides.Append();

                                ISlide slide = ppt.Slides[ppt.Slides.Count - 1]; // uzmem zadnji slajd
                                SizeF pptSize = ppt.SlideSize.Size;

                                RectangleF bgRect = new RectangleF(new PointF(0, 0), pptSize);
                                slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, background, bgRect);

                                RectangleF titleRect = new RectangleF(pptSize.Width / 2 - 200, 20, 400, 50);        // TITLE
                                IAutoShape titleShape = slide.Shapes.AppendShape(ShapeType.Rectangle, titleRect);
                                titleShape.Fill.FillType = FillFormatType.None;
                                titleShape.ShapeStyle.LineColor.Color = Color.Empty;
                                TextParagraph titlePara = titleShape.TextFrame.Paragraphs[0];
                                titlePara.Text = detail.Item3;
                                titlePara.FirstTextRange.FontHeight = 36;
                                titlePara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                                titlePara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                                titlePara.Alignment = TextAlignmentType.Center;

                                new_chapter = true;
                            }
                            if (detail.Item1 == "source")
                            {

                                string[] files = Directory.GetFiles(detail.Item3, "*", SearchOption.TopDirectoryOnly);
                                foreach (String file in files)
                                {
                                    if (!new_chapter)
                                    {
                                        ppt.Slides.Append();

                                        ISlide slide2 = ppt.Slides[ppt.Slides.Count - 1]; // uzmem zadnji slajd
                                        SizeF pptSize2 = ppt.SlideSize.Size;

                                        RectangleF bgRect = new RectangleF(new PointF(0, 0), pptSize2);
                                        slide2.Shapes.AppendEmbedImage(ShapeType.Rectangle, background, bgRect);
                                    }
                                    else
                                    {
                                        new_chapter = false;
                                    }

                                    ISlide slide = ppt.Slides[ppt.Slides.Count - 1]; // uzmem zadnji slajd
                                    SizeF pptSize = ppt.SlideSize.Size;

                                    String image = file;
                                    int index0 = file.LastIndexOf("\\") + 1;
                                    int index1 = file.IndexOf(".", index0);
                                    String image_name = file.Substring(index0, index1 - index0);

                                    RectangleF imgtitleRect = new RectangleF(75, 70, pptSize.Width - 200, 30);        // IMGTITLE
                                    IAutoShape imgtitleShape = slide.Shapes.AppendShape(ShapeType.Rectangle, imgtitleRect);
                                    imgtitleShape.Fill.FillType = FillFormatType.None;
                                    imgtitleShape.ShapeStyle.LineColor.Color = Color.Empty;
                                    TextParagraph imgtitle = imgtitleShape.TextFrame.Paragraphs[0];
                                    imgtitle.Text = image_name;
                                    imgtitle.FirstTextRange.FontHeight = 20;
                                    imgtitle.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                                    imgtitle.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                                    imgtitle.Alignment = TextAlignmentType.Left;

                                    RectangleF imgRect = new RectangleF(new PointF(75, 100), new SizeF(pptSize.Width - 150, pptSize.Height - 175));
                                    slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, image, imgRect);

                                }
                            }           
                        }
                    }
                }

                // SAVE
                ppt.SaveToFile(save_path + "\\" + presentation_name + ".pptx", FileFormat.Pptx2010);

            }
        }
    }
}
