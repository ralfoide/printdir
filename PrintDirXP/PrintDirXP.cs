/*
 * Project:		PrintDirXP
 * Language:	C#
 * Author:		RM, 20020901
 * 
 * 
 * .Net printing help:
 * ms-help://MS.VSCC/MS.MSDNVS/cpref/html/frlrfsystemdrawingprintingprintdocumentclasstopic.htm
*/


using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;				// RM 20020901 for DirectoryInfo
using System.Drawing.Printing;	// RM 20020901 for printing
using System.Diagnostics;		// RM 20020901 for Debug.Assert

namespace PrintDirXP
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class PrintDirForm : System.Windows.Forms.Form
	{
		// printing -- RM 20020901
		private ArrayList		mDirectoryText;
		private string			mHeaderDirText;
		private PrintDocument	mPrintDoc;
		private PageSettings	mPageSettings;
		private int				mPrintCurrentCount;

		// UI components
		private System.Windows.Forms.GroupBox GroupSelection;
		private System.Windows.Forms.GroupBox GroupContentPreview;
		private System.Windows.Forms.GroupBox GroupPrint;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox mEditDirPath;
		private System.Windows.Forms.Button mButtonBrowseDir;
		private System.Windows.Forms.TextBox mEditDirContent;
		private System.Windows.Forms.Button mButtonPrint;
		private System.Windows.Forms.Button mButtonPrintSetup;
		private System.Windows.Forms.Button mButtonPrintPreview;
		private System.Windows.Forms.Button mButtonQuit;
		private System.Windows.Forms.PageSetupDialog mPageSetupDialog;
		private System.Windows.Forms.PrintPreviewDialog mPrintPreviewDialog;
		private System.Windows.Forms.PrintDialog mPrintDialog;
		private System.Windows.Forms.FontDialog mPrintFontDialog;

		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PrintDirForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// constructor code after InitializeComponent call
			//
			GetLastUsedPath();
			ParseSelectedDirectory();

			// init settings for printing
			mPrintDoc = new PrintDocument();
			mPrintDoc.PrintPage += new PrintPageEventHandler(this.printDocPage);

			mPageSettings = new PageSettings();

			mPrintDoc.DefaultPageSettings = mPageSettings;

			mPageSetupDialog.Document = mPrintDoc;
			mPageSetupDialog.PageSettings = mPageSettings;

			mPrintPreviewDialog.Document = mPrintDoc;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintDirForm));
            this.mEditDirPath = new System.Windows.Forms.TextBox();
            this.mButtonBrowseDir = new System.Windows.Forms.Button();
            this.GroupSelection = new System.Windows.Forms.GroupBox();
            this.GroupContentPreview = new System.Windows.Forms.GroupBox();
            this.mEditDirContent = new System.Windows.Forms.TextBox();
            this.GroupPrint = new System.Windows.Forms.GroupBox();
            this.mButtonPrint = new System.Windows.Forms.Button();
            this.mButtonPrintSetup = new System.Windows.Forms.Button();
            this.mButtonPrintPreview = new System.Windows.Forms.Button();
            this.mButtonQuit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.mPageSetupDialog = new System.Windows.Forms.PageSetupDialog();
            this.mPrintPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.mPrintDialog = new System.Windows.Forms.PrintDialog();
            this.mPrintFontDialog = new System.Windows.Forms.FontDialog();
            this.GroupSelection.SuspendLayout();
            this.GroupContentPreview.SuspendLayout();
            this.GroupPrint.SuspendLayout();
            this.SuspendLayout();
            // 
            // mEditDirPath
            // 
            resources.ApplyResources(this.mEditDirPath, "mEditDirPath");
            this.mEditDirPath.Name = "mEditDirPath";
            this.mEditDirPath.TextChanged += new System.EventHandler(this.mEditDirPath_TextChanged);
            // 
            // mButtonBrowseDir
            // 
            resources.ApplyResources(this.mButtonBrowseDir, "mButtonBrowseDir");
            this.mButtonBrowseDir.Name = "mButtonBrowseDir";
            this.mButtonBrowseDir.Click += new System.EventHandler(this.ButtonBrowseDir_Click);
            // 
            // GroupSelection
            // 
            resources.ApplyResources(this.GroupSelection, "GroupSelection");
            this.GroupSelection.Controls.Add(this.mEditDirPath);
            this.GroupSelection.Controls.Add(this.mButtonBrowseDir);
            this.GroupSelection.Name = "GroupSelection";
            this.GroupSelection.TabStop = false;
            // 
            // GroupContentPreview
            // 
            resources.ApplyResources(this.GroupContentPreview, "GroupContentPreview");
            this.GroupContentPreview.Controls.Add(this.mEditDirContent);
            this.GroupContentPreview.Name = "GroupContentPreview";
            this.GroupContentPreview.TabStop = false;
            // 
            // mEditDirContent
            // 
            resources.ApplyResources(this.mEditDirContent, "mEditDirContent");
            this.mEditDirContent.Name = "mEditDirContent";
            this.mEditDirContent.ReadOnly = true;
            // 
            // GroupPrint
            // 
            resources.ApplyResources(this.GroupPrint, "GroupPrint");
            this.GroupPrint.Controls.Add(this.mButtonPrint);
            this.GroupPrint.Controls.Add(this.mButtonPrintSetup);
            this.GroupPrint.Controls.Add(this.mButtonPrintPreview);
            this.GroupPrint.Name = "GroupPrint";
            this.GroupPrint.TabStop = false;
            // 
            // mButtonPrint
            // 
            resources.ApplyResources(this.mButtonPrint, "mButtonPrint");
            this.mButtonPrint.Name = "mButtonPrint";
            this.mButtonPrint.Click += new System.EventHandler(this.mButtonPrint_Click);
            // 
            // mButtonPrintSetup
            // 
            resources.ApplyResources(this.mButtonPrintSetup, "mButtonPrintSetup");
            this.mButtonPrintSetup.Name = "mButtonPrintSetup";
            this.mButtonPrintSetup.Click += new System.EventHandler(this.ButtonPrintSetup_Click);
            // 
            // mButtonPrintPreview
            // 
            resources.ApplyResources(this.mButtonPrintPreview, "mButtonPrintPreview");
            this.mButtonPrintPreview.Name = "mButtonPrintPreview";
            this.mButtonPrintPreview.Click += new System.EventHandler(this.mButtonPrintPreview_Click);
            // 
            // mButtonQuit
            // 
            resources.ApplyResources(this.mButtonQuit, "mButtonQuit");
            this.mButtonQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mButtonQuit.Name = "mButtonQuit";
            this.mButtonQuit.Click += new System.EventHandler(this.ButtonQuit_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // mPrintPreviewDialog
            // 
            resources.ApplyResources(this.mPrintPreviewDialog, "mPrintPreviewDialog");
            this.mPrintPreviewDialog.Name = "mPrintPreviewDialog";
            // 
            // mPrintFontDialog
            // 
            this.mPrintFontDialog.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // PrintDirForm
            // 
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.mButtonQuit;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mButtonQuit);
            this.Controls.Add(this.GroupPrint);
            this.Controls.Add(this.GroupContentPreview);
            this.Controls.Add(this.GroupSelection);
            this.Name = "PrintDirForm";
            this.GroupSelection.ResumeLayout(false);
            this.GroupSelection.PerformLayout();
            this.GroupContentPreview.ResumeLayout(false);
            this.GroupContentPreview.PerformLayout();
            this.GroupPrint.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new PrintDirForm());
		}


		// -------------------------------------------------------------------


		/// <summary>
		/// Parses directory and prepare text output
		/// </summary>
		private void ParseSelectedDirectory()
		{
			string dir_path = mEditDirPath.Text;
			string result = "";
			bool can_print = false;

			if (mDirectoryText == null)
				mDirectoryText = new ArrayList();
			else
				mDirectoryText.Clear();

			const int max_name_length = 42;
			string format1 = "{0,-45} {1,9}    {2,-10} {3,-8}\r\n";
			string format2 = "{0,-45} {1,9}    {2,-10} {3,8}\r\n";
			mHeaderDirText = String.Format(format1, "Nom", "Taille", "Date", "");
			result = mHeaderDirText;

			if (dir_path != "")
			{
				try
				{
					DirectoryInfo info = new DirectoryInfo(dir_path);

					DirectoryInfo[] dir_list = info.GetDirectories();
					foreach(DirectoryInfo dir in dir_list)
					{
						string line;

						string name = dir.Name;
						name = name.Substring(0, Math.Min(name.Length, max_name_length));


						line = String.Format(format2,
							name,
							"[Rép]",
							dir.LastWriteTime.ToShortDateString(),
							dir.LastWriteTime.ToShortTimeString());

						mDirectoryText.Add(line);
						result += line;
						can_print = true;
					}

					FileInfo[] file_list = info.GetFiles();
					foreach(FileInfo file in file_list)
					{
						string line;
						string kb;
						
						if (file.Length < 1024)
							kb = String.Format("{0}", file.Length);
						else
							kb = String.Format("{0} ko", file.Length / 1024);

						string name = file.Name;
						name = name.Substring(0, Math.Min(name.Length, max_name_length));

						line = String.Format(format2,
							name,
							kb,
							file.LastWriteTime.ToShortDateString(),
							file.LastWriteTime.ToShortTimeString());

						mDirectoryText.Add(line);
						result += line;
						can_print = true;
					}

				}
				catch(DirectoryNotFoundException /* e */)
				{
					result = "(Sélectionner un dossier d'abord)";
				}
				catch(Exception e)
				{
					result = "(Erreur: Impossible d'accéder à ce dossier. Sélectionnez en un autre.)";
					MessageBox.Show(result,
									"Erreur acces disque: " + e.Message,
									MessageBoxButtons.OK,
									MessageBoxIcon.Exclamation);
				}
			}

			// affect new result to the content edit box
			mEditDirContent.Text = result;

			// validate the preview and print button if there is something to print
			mButtonPrintPreview.Enabled = can_print;
			mButtonPrint.Enabled = can_print;
		}

		// -------------------------------------------------------------------

		/// <summary>
		/// Let the user select the directory to be printed
		/// </summary>
		private void ButtonBrowseDir_Click(object sender, System.EventArgs e)
		{
            FolderBrowser fb = new FolderBrowser("Sélection du dossier à imprimer:");
            fb.IncludeFiles = false;
            if (fb.ShowDialog() == DialogResult.OK) {
				this.mEditDirPath.Text = fb.SelectedPath;
				// ParseSelectedDirectory();
			}
		}

		/// <summary>
		/// Closes the application
		/// </summary>
		private void ButtonQuit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void ButtonPrintSetup_Click(object sender, System.EventArgs e)
		{
			if (mPageSetupDialog.ShowDialog() == DialogResult.OK)
			{
				if (!Object.ReferenceEquals(mPageSetupDialog.PageSettings, mPageSettings))
					mPageSettings = mPageSetupDialog.PageSettings;
				mPrintDoc.DefaultPageSettings = mPageSettings;
			}
		}

		private void mButtonPrintPreview_Click(object sender, System.EventArgs e)
		{
			mPrintCurrentCount = 0;
			mPrintPreviewDialog.ShowDialog();
		}

		private void mButtonPrint_Click(object sender, System.EventArgs e)
		{
			try 
			{
				mPrintCurrentCount = 0;
				mPrintDoc.Print();
			}  
			catch(Exception ex) 
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void printDocPage(object sender, PrintPageEventArgs e)
		{
			string line;
			int count = 0;
			float y_pos = 0;
			float left_margin   = e.MarginBounds.Left;
			float right_margin  = e.MarginBounds.Right;
			float top_margin    = e.MarginBounds.Top;
			float bottom_margin = e.MarginBounds.Bottom;


			// the list of lines might be empty, but it must be allocated
			Debug.Assert(mDirectoryText != null);

			// nb lines
			int nb_lines = mDirectoryText.Count;

			// the font used to print, the height of the font
			Font print_font = mPrintFontDialog.Font;
			float font_height = print_font.GetHeight(e.Graphics);

			// calculate the number of lines per page.
			int lines_per_page = (int)(e.MarginBounds.Height / print_font.GetHeight(e.Graphics));
			// the first line is always the table header
			lines_per_page -= 1;

			// continue printing where left before or start again at begining of document
			if (mPrintCurrentCount < nb_lines)
				count = mPrintCurrentCount;
			else
				count = 0;

			// --- print the header ---

			Font header_font = new Font("Arial", 10);
			y_pos = top_margin - 0.10f * font_height;

			e.Graphics.DrawLine(Pens.Black, left_margin, y_pos, right_margin, y_pos);
			y_pos -= 1.5f * font_height;

			line = mEditDirPath.Text;
			e.Graphics.DrawString(line, header_font, Brushes.Black, left_margin, y_pos, new StringFormat());
			
			// --- print the footer ---

			y_pos = bottom_margin + 0.10f * font_height;

			e.Graphics.DrawLine(Pens.Black, left_margin, y_pos, right_margin, y_pos);
			y_pos += 0.5f * font_height;

			line = String.Format("Imprimé le {0}, {1}",
								 DateTime.Now.ToShortDateString(),
								 DateTime.Now.ToShortTimeString());
			e.Graphics.DrawString(line, header_font, Brushes.Black, left_margin, y_pos, new StringFormat());

			int nb_pages  = (int) Math.Ceiling((nb_lines / lines_per_page) + 0.5);
			int curr_page = (int) Math.Ceiling((count / lines_per_page) + 0.5);

			line = String.Format("Page {0}/{1}", curr_page, nb_pages);
			RectangleF rectangle = new RectangleF(left_margin, y_pos, right_margin, y_pos + font_height);
			StringFormat str_format = new StringFormat();
			str_format.Alignment = StringAlignment.Center;
			e.Graphics.DrawString(line, header_font, Brushes.Black, rectangle, str_format);


			// --- print the actual content ---
			
			// the table starts at the top margin
			y_pos = top_margin;

			// the first line is always the table header
			line = mHeaderDirText;
			e.Graphics.DrawString(line, print_font, Brushes.Black, left_margin, y_pos, new StringFormat());
			y_pos += font_height;

			e.Graphics.DrawLine(Pens.Black, left_margin, y_pos, right_margin, y_pos);


			// print each line for this page
			for(lines_per_page += count; count < lines_per_page && count < nb_lines; count++)
			{
				line = mDirectoryText[count].ToString();
				e.Graphics.DrawString(line, print_font, Brushes.Black, left_margin, y_pos, new StringFormat());
				// positiony for next line
				y_pos += font_height;
			}

			// if more lines exist, print another page.
			mPrintCurrentCount = count;
			e.HasMorePages = (count < nb_lines);
		}

		private void mEditDirPath_TextChanged(object sender, System.EventArgs e)
		{
			ParseSelectedDirectory();
		}


		private void GetLastUsedPath()
		{
		}


		private void SetLastUsedPath()
		{
		}
	
	
	}
}
