using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.IO;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing.Layout;
using ProjektWSB.Models;

namespace ProjektWSB.Controllers
{
    public class PDFController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: PDF
        public ActionResult Index()
        {
            if (Session["userName"] == null)
                return RedirectToAction("Login", "Users");
            else
            {
                //database
                int userId = Int32.Parse(Session["userId"].ToString());
                var menu = db.Menus.Where(x => x.UserId == userId).ToList();
                foreach (Menu m in menu)
                {
                    m.Dish = db.Dishes.Where(x => x.Id == m.DishId).FirstOrDefault();
                }

                menu.Sort((p, q) => p.Dish.Type.CompareTo(q.Dish.Type));

                // Create a new PDF document
                PdfDocument document = new PdfDocument();
                document.Info.Title = "Created with PDFsharp";

                // Page Options
                PdfPage pdfPage = document.AddPage();
                pdfPage.Height = 842;//842
                pdfPage.Width = 590;

                // Get an XGraphics object for drawing
                XGraphics graph = XGraphics.FromPdfPage(pdfPage);

                // Text format
                XStringFormat format = new XStringFormat();
                format.LineAlignment = XLineAlignment.Near;
                format.Alignment = XStringAlignment.Near;
                var tf = new XTextFormatter(graph);

                XFont fontDish = new XFont("Segoe Print", 12, XFontStyle.Regular);
                XFont fontDishType = new XFont("Segoe Print", 14, XFontStyle.Bold);
                XFont fontTitle = new XFont("Segoe Print", 25, XFontStyle.Bold);

                // Row elements
                int el1_width = 80;
                int el2_width = 450;

                // page structure options
                double lineHeight = 25;
                int marginLeft = 20;
                int marginTop = 20;

                int el_height = 30;
                int rect_height = 20;

                int interLine_X_1 = 2;
                int interLine_X_2 = 2 * interLine_X_1;

                int offSetX_1 = el1_width;
                int offSetX_2 = el2_width;

                XSolidBrush rect_style1 = new XSolidBrush(XColors.White);

                string currentType = "";
                string oldType = "";
                int typeCounter = 0;

                graph.DrawString("Menu", fontTitle, XBrushes.Black,
                    new XRect(0, 50, pdfPage.Width, 0),
                    XStringFormats.Center);

                for (int i = 0; i < menu.Count; i++)
                {
                    double dist_Y = 50 + lineHeight * (i + 1 + typeCounter);
                    double dist_Y2 = dist_Y - 2;

                    currentType = menu[i].Dish.Type;
                    if (oldType != currentType)
                    {
                        oldType = currentType;

                        tf.DrawString(
                            menu[i].Dish.Type,
                            fontDishType,
                            XBrushes.Black,
                            new XRect(marginLeft + interLine_X_1, dist_Y + marginTop, el1_width + el2_width, el_height),
                            format);
                        typeCounter++;
                        i--;
                    }
                    else
                    {
                        tf.DrawString(
                            menu[i].Dish.Name,
                            fontDish,
                            XBrushes.Black,
                            new XRect(marginLeft * 2 + interLine_X_1, marginTop + dist_Y, el2_width, el_height),
                            format);

                        tf.DrawString(
                            menu[i].Dish.Price.ToString() + " zł",
                            fontDish,
                            XBrushes.Black,
                            new XRect(marginLeft + offSetX_2 + 2 * interLine_X_2, marginTop + dist_Y, el1_width, el_height),
                            format);
                    }
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    document.Save(ms, false);
                    return File(ms.ToArray(), "application/pdf");
                }
            }
        }
    }
}