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
            List<Tuple<string, DateTime, int, string, string>> reports = getReports();
            DateTime now = DateTime.Now;
            foreach ( var report in reports) {
                if ((now.Date - report.Item2.Date).Days % report.Item3 == 0)
                {                
                    List<Tuple<string, string, string>> report_data = new List<Tuple<string, string, string>>();
                    report_data.Add(new Tuple<string, string, string>("location", report.Item4, ""));
                    report_data.Add(new Tuple<string, string, string>("presentation_name", report.Item5, ""));
                    getReportData(report.Item1, report_data);
                    rez.Add(new Tuple<string, List<Tuple<string, string, string>>>(report.Item1, report_data));
                }
            }
            return rez;
        }

        private static List<Tuple<string, DateTime, int, string, string>> getReports()
        {
            var con_string = "Data Source = (localdb)\\ProjectsV13; Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            List<Tuple<string, DateTime, int, string, string>> rez = new List<Tuple<string, DateTime, int, string, string>>();

            using (SqlConnection myConnection = new SqlConnection(con_string))
            {
                string oString = "Select * from dbo.main";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        if (oReader["active"].ToString() == "true")
                        {
                            rez.Add(new Tuple<string, DateTime, int, string, string>(oReader["report"].ToString(), DateTime.Parse(oReader["start"].ToString()), Int32.Parse(oReader["period"].ToString()), oReader["save_to_path"].ToString(), oReader["presentation_title"].ToString()));
                        }
                    }

                    myConnection.Close();
                }
            }

            return rez;
        }

        private static void getReportData(string report, List<Tuple<string, string, string>> data)
        {
            string con_string = "Data Source = (localdb)\\ProjectsV13; Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";

            using (SqlConnection myConnection = new SqlConnection(con_string))
            {
                string oString = "select item, chapter, content from ( select 'chapter' as item, chapter, chapter_name as content from dbo.chapter where report = @report union all select 'source' as item, chapter, source_path as content where report = @report ) as source order by chapter, item";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                oCmd.Parameters.AddWithValue("@report", report);
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        if (oReader["active"].ToString() == "true")
                        {
                            data.Add(new Tuple<string, string, string>(oReader["item"].ToString(), oReader["chapter"].ToString(), oReader["content"].ToString()));
                        }
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
                ISlide slide = ppt.Slides[0];
                SizeF pptSize = ppt.SlideSize.Size;

                string report_name = report_pair.Item1;
                string save_path;
                string presentation_name;

                bool new_chapter = true;

                foreach ( var detail in report_pair.Item2)
                {
                    if (detail.Item1 == "location")
                    {
                        save_path = detail.Item2;
                    }
                    else
                    {
                        if (detail.Item1 == "presentation_name")
                        {

                            presentation_name = detail.Item2;
                        }
                        else
                        {
                            if (detail.Item1 == "chapter")
                            {
                                // nov slajd namestim title
                                new_chapter = true;
                            }
                            if (detail.Item1 == "source")
                            {
                                if (new_chapter)
                                {
                                    new_chapter = false;
                                    // prvu sliku na istu stranu, bez titla, ostalo standard
                                }
                                else
                                {
                                    //standard
                                }
                            }           
                        }
                    }
                }

                /*
                //Set background
                string bgFile = "bg.jpg";
                RectangleF bgRect = new RectangleF(new PointF(0, 0), pptSize);
                //Add title
                RectangleF titleRect = new RectangleF(pptSize.Width / 2 - 200, 10, 400, 50);
                IAutoShape titleShape = slide.Shapes.AppendShape(ShapeType.Rectangle, titleRect);
                titleShape.Fill.FillType = FillFormatType.None;
                titleShape.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph titlePara = titleShape.TextFrame.Paragraphs[0];
                titlePara.Text = "Microsoft PowerPoint";
                titlePara.FirstTextRange.FontHeight = 36;
                titlePara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                titlePara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                titlePara.Alignment = TextAlignmentType.Center;

                //Insert Image
                string logoFile = "logo.png";
                RectangleF logoRect = new RectangleF(pptSize.Width / 2 - 40, 60, 80, 80);
                IEmbedImage image = slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, logoFile, logoRect);
                image.Line.FillType = FillFormatType.None;
                 
                //Save the file
                ppt.SaveToFile("result.pptx", FileFormat.Pptx2010);  
                */   

            }
        }
    }
}
