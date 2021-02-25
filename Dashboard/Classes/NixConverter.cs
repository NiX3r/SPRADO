using OpenHtmlToPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dashboard.Classes
{
    class NixConverter
    {

        public static bool ConvertHtmlToPdf(DataGridView table, String path, String filter)
        {

            String HTML = "";
            String Footer, Header, Body = "";
            Header = "<!doctype html><html><head><meta charset=\"utf-8\"><link rel=\"stylesheet\" href=\"https://sprado.cz/inc/css/revision_1.5.css\"><title>\"NameFile\"</title>	 <BR><BR><p class=\"date\">\"Date\"</p>	<h1>\"NameFile\"</h1>	<p class=\"revisions\">Počet revizí: <a class=\"number\">\"Number\"</a> | Filtr: <a class=\"text\">\"Text\"</a></p></head><body>	<div class=\"body\">			<div class=\"table\">			<BR /><table id=\"information\">  				<tr>    				<th style=\"width: 10%\">Datum</th>    				<th style=\"width: 10%\">Splatnost</th>   	 				<th style=\"width: 20%\">Dům</th>					<th style=\"width: 10%\">Revizák</th>					<th style=\"width: 10%\">Typ</th>					<th style=\"width: 40%\">Info</th> 				</tr>";
            Footer = "			</table>		</div>	</div>	<div class=\"triangle2\"></div>	<div class=\"foot\">		<p>Created by <a class=\"name\" href=\"https://iliev.dev\">Daniel Iliev</a> | Designe by <a class=\"name\" href=\"https://orim0.eu\">Jaroslav Maňo</a></p>	</div></body></html>";

            Header = Header.Replace("\"NameFile\"", path.Substring(path.LastIndexOf('\\') + 1));
            Header = Header.Replace("\"Text\"", filter);
            Header = Header.Replace("\"Date\"", DateTime.Now.ToString());
            Header = Header.Replace("\"Number\"", (table.Rows.Count).ToString());

            foreach (DataGridViewRow row in table.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null && row.Cells[2].Value != null && row.Cells[3].Value != null && row.Cells[4].Value != null && row.Cells[5].Value != null)
                {
                    String record = "<tr>    				<td>%DATE1%</td>   				<td>%DATE2%</td>   				<td>%STREET%</td>					<td>%NAME%</td>					<td>%TYPE%</td>					<td>%INFO%</td>  				</tr>";

                    record = record.Replace("%DATE1%", row.Cells[0].Value + "");
                    record = record.Replace("%DATE2%", row.Cells[1].Value + "");
                    record = record.Replace("%STREET%", row.Cells[2].Value + "");
                    record = record.Replace("%NAME%", row.Cells[3].Value + "");
                    record = record.Replace("%TYPE%", row.Cells[4].Value + "");
                    record = record.Replace("%INFO%", row.Cells[5].Value + "");

                    Body += record;
                }

            }

            HTML = Header + Body + Footer;

            var pdf = Pdf.From(HTML)
                         .WithoutOutline()
                         .WithMargins(0.01.Centimeters())
                         .WithObjectSetting("web.*", "true")
                         .Content();

            File.WriteAllBytes(path, pdf);

            return true;

            /*try
            {

                String HTML = "";
                String Footer, Header, Body = "";
                Header = "<!doctype html><html><head><meta charset=\"utf-8\"><link rel=\"stylesheet\" href=\"https://sprado.cz/inc/css/revision_1.1.css\"><title>\"NameFile\"</title>	<p class=\"date\">\"Date\"</p>	<h1>\"NameFile\"</h1>	<p class=\"revisions\">Počet revizí: <a class=\"number\">\"Number\"</a> | Filtr: <a class=\"text\">\"Text\"</a></p></head><body>	<div class=\"body\">		<div class=\"triangle\"></div>		<div class=\"table\">			<table id=\"information\">  				<tr>    				<th style=\"width: 10%\">Datum</th>    				<th style=\"width: 10%\">Splatnost</th>   	 				<th style=\"width: 20%\">Dům</th>					<th style=\"width: 10%\">Revizák</th>					<th style=\"width: 10%\">Typ</th>					<th style=\"width: 40%\">Info</th> 				</tr>";
                Footer = "			</table>		</div>	</div>	<div class=\"triangle2\"></div>	<div class=\"foot\">		<p>Created by <a class=\"name\" href=\"https://iliev.dev\">Daniel Iliev</a> | Designe by <a class=\"name\" href=\"https://orim0.eu\">Jaroslav Maňo</a></p>	</div></body></html>";

                Header = Header.Replace("\"NameFile\"", path);
                Header = Header.Replace("\"Text\"", filter);
                Header = Header.Replace("\"Date\"", DateTime.Now.ToString());
                Header = Header.Replace("\"Numer\"", table.Rows.Count.ToString());

                foreach(DataGridViewRow row in table.Rows)
                {
                    String record = "<tr>    				<td>%DATE1%</td>   				<td>%DATE2%</td>   				<td>%STREET%</td>					<td>%NAME%</td>					<td>%TYPE%</td>					<td>%INFO%</td>  				</tr>";

                    record = record.Replace("%DATE1%", row.Cells[0].Value.ToString());
                    record = record.Replace("%DATE2%", row.Cells[1].Value.ToString());
                    record = record.Replace("%STREET%", row.Cells[2].Value.ToString());
                    record = record.Replace("%NAME%", row.Cells[3].Value.ToString());
                    record = record.Replace("%TYPE%", row.Cells[4].Value.ToString());
                    record = record.Replace("%INFO%", row.Cells[5].Value.ToString());

                    Body += record;

                }

                HTML = Header + Body + Footer;

                var pdf = Pdf.From(HTML).Content();

                File.WriteAllBytes(path, pdf);

                return true;

            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;

            }*/

        }

    }
}
