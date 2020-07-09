using FLOWeR_Garden_Ver02.Explorer;
using FLOWeR_Garden_Ver02.FLOWeR.UIDesigner;
using FLOWeR_Garden_Ver02.FLOWeR.UIDesigner.Control;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Xml;

namespace FLOWeR_Garden_Ver02.FLOWeR.Interpreter
{
    public class FlowerInterpreter
    {
        public static FlowerInterpreter Instance
        {
            get;
            private set;
        }
        static FlowerInterpreter()
        {
            Instance = new FlowerInterpreter();
        }
        private FlowerInterpreter()
        {
        }

        static readonly string cscPath = @"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe";

        public string DefaultPath
        {
            get
            {
                if (ProjectManager.Instance.ProjectPath == null || 
                    ProjectManager.Instance.ProjectName == null)
                {
                    return null;
                }

                return string.Format("{0}/{1}/Flower", 
                    ProjectManager.Instance.ProjectPath, ProjectManager.Instance.ProjectName);
            }
        }

        public void GenerateMain()
        {
            StringBuilder builder = new StringBuilder();
            builder
                .AppendLine("using System;")
                .AppendLine("using System.Windows.Forms;")
                .AppendLine(string.Format("namespace {0}", ProjectManager.Instance.ProjectName))
                .AppendLine("{")
                .AppendLine("   static class Program")
                .AppendLine("   {")
                .AppendLine("       [STAThread]")
                .AppendLine("       static void Main()")
                .AppendLine("       {")
                .AppendLine("           Application.EnableVisualStyles();")
                .AppendLine("           Application.SetCompatibleTextRenderingDefault(false);")
                .AppendLine("           Application.Run(new MainForm());")
                .AppendLine("       }")
                .AppendLine("   }")
                .AppendLine("}");

            File.WriteAllText(string.Format("{0}/Program.cs", DefaultPath), builder.ToString());
        }

        public void GenerateForm()
        {
            // 이벤트 생성
            StringBuilder builder = InterpretFlowchartDesigner();

            File.WriteAllText(string.Format("{0}/MainForm.cs", DefaultPath), builder.ToString());
        }


        public void GenerateFormDesigner()
        {
            //디자이너 코드 생성 
            StringBuilder builder = InterpretUIDesigner();
           
            File.WriteAllText(string.Format("{0}/MainForm.Designer.cs", DefaultPath), builder.ToString());
        }

        private StringBuilder InterpretFlowchartDesigner()
        {
            UIDesigner.UIDesigner uiDesigner = UIManager.Instance.FlowerUIDesigner;
            List<FWControl> fwControls = uiDesigner.FWControlList;

            StringBuilder builder = new StringBuilder();
            builder
                .AppendLine("using System;")
                .AppendLine("using System.Windows.Forms;")
                .AppendLine(string.Format("namespace {0}", ProjectManager.Instance.ProjectName))
                .AppendLine("{")
                .AppendLine("   public partial class MainForm : Form")
                .AppendLine("   {")
                .AppendLine("       public MainForm()")
                .AppendLine("       {")
                .AppendLine("           InitializeComponent();")
                .AppendLine("       }");

            foreach (FWControl fwControl in fwControls)
            {
                FWEventList fwEventList = fwControl.FWEventList;

                foreach (Event ev in fwEventList.Events)
                {
                    if (!string.IsNullOrWhiteSpace(ev.EventValue))
                    {
                        builder.AppendLine(string.Format("private void {0}(object sender, EventArgs e)", ev.EventValue));
                        builder.AppendLine("{");

                        //To Do
                        string upperDirectory = string.Format("{0}/{1}", ProjectManager.Instance.ProjectPath, ProjectManager.Instance.ProjectName);
                        string xmlPath = string.Format("{0}/{1}/{2}.xml", upperDirectory, "Flowchart Designer", ev.EventValue);

                        using (XmlReader xmlReader = XmlReader.Create(xmlPath))
                        {
                            xmlReader.MoveToContent();

                            bool isPrevDecision = false;
                            bool isPrevIf = false;

                            while (xmlReader.Read())
                            {
                                builder.Append('\t');

                                if (xmlReader.IsStartElement("Ready"))
                                {
                                    if (xmlReader.HasAttributes)
                                    {
                                        string varType = xmlReader.GetAttribute("Type");
                                        string varName = xmlReader.GetAttribute("Name");

                                        if (varType.Equals("정수"))
                                        {
                                            varType = "int";
                                        }
                                        else if (varType.Equals("실수"))
                                        {
                                            varType = "double";
                                        }
                                        else if (varType.Equals("문자열"))
                                        {
                                            varType = "string";
                                        }

                                        builder.AppendLine(string.Format("{0} {1};", varType, varName));
                                    }
                                }
                                else if (xmlReader.IsStartElement("Decision") || xmlReader.Name.Equals("Decision"))
                                {
                                    if (isPrevDecision)
                                    {
                                        isPrevDecision = false;

                                        builder.AppendLine("}");
                                    }
                                    else
                                    {
                                        isPrevDecision = true;

                                        if (xmlReader.HasAttributes)
                                        {
                                            string cond = xmlReader.GetAttribute("Condition");

                                            if (!isPrevIf)
                                            {
                                                isPrevIf = true;
                                                builder.AppendLine(string.Format("if ({0})", cond));
                                                builder.AppendLine("{");
                                            }
                                            else
                                            {
                                                isPrevIf = false;
                                                builder.AppendLine("else");
                                                builder.AppendLine("{");
                                            }
                                        }
                                    }
                                }
                                else if (xmlReader.IsStartElement("Process"))
                                {
                                    if (isPrevDecision)
                                    {
                                        builder.Append('\t');
                                    }

                                    if (xmlReader.HasAttributes)
                                    {
                                        string processName = xmlReader.GetAttribute("ProcessName");

                                        if (processName.Equals("MessageBox"))
                                        {
                                            builder.AppendLine(string.Format("System.Windows.Forms.MessageBox.Show({0});",
                                                xmlReader.GetAttribute("Message")));
                                        }
                                        else if (processName.Equals("CodeInjection"))
                                        {
                                            builder.AppendLine(xmlReader.GetAttribute("Code"));
                                        }
                                        else if (processName.Equals("InputValue"))
                                        {
                                            builder.AppendLine(string.Format("{0} = {1};",
                                                xmlReader.GetAttribute("VarName"), xmlReader.GetAttribute("VarValue")));
                                        }
                                        else if (processName.Equals("ListBoxAdd"))
                                        {
                                            builder.AppendLine(string.Format("{0}.Items.Add({1});",
                                                xmlReader.GetAttribute("ListBoxName"), xmlReader.GetAttribute("ListBoxItem")));
                                        }
                                        else if (processName.Equals("ListBoxRemove"))
                                        {
                                            builder.AppendLine(string.Format("{0}.Items.Remove({1});",
                                                xmlReader.GetAttribute("ListBoxName"), xmlReader.GetAttribute("ListBoxItem")));
                                        }
                                    }
                                }
                            }
                        }

                        builder.AppendLine("}");
                    }
                }
            }

            builder
                .AppendLine("   }")
                .AppendLine("}");

            return builder;
        }

        private StringBuilder InterpretUIDesigner()
        {
            UIDesigner.UIDesigner uiDesigner = UIManager.Instance.FlowerUIDesigner;
            List<FWControl> fwControls = uiDesigner.FWControlList;

            StringBuilder builder = new StringBuilder();
            builder
                .AppendLine(string.Format("namespace {0}", ProjectManager.Instance.ProjectName))
                .AppendLine("{")
                .AppendLine("   partial class MainForm")
                .AppendLine("   {")
                .AppendLine("       private System.ComponentModel.IContainer components = null;")
                .AppendLine("       protected override void Dispose(bool disposing)")
                .AppendLine("       {")
                .AppendLine("           if (disposing && (components != null))")
                .AppendLine("           {")
                .AppendLine("               components.Dispose();")
                .AppendLine("           }")
                .AppendLine("           base.Dispose(disposing);")
                .AppendLine("       }")
                .AppendLine("       private void InitializeComponent()")
                .AppendLine("       {");

            foreach (FWControl fwControl in fwControls)
            {
                builder.AppendLine("this." + fwControl.Name + " = new System.Windows.Forms." + fwControl.TypeName + "();");
            }

            builder
                .AppendLine("           this.SuspendLayout();");

            int tabIndex = 0;
            foreach (FWControl fwControl in fwControls)
            {
                if (fwControl is FWButton fwButton)
                {
                    builder.AppendLine(string.Format("this.{0}.Text = \"{1}\";", fwButton.Name, fwButton.Text));
                    builder.AppendLine(string.Format("this.{0}.BackColor = System.Drawing.Color.Transparent;", fwButton.Name));
                    builder.AppendLine(string.Format("this.{0}.UseVisualStyleBackColor = true;", fwButton.Name));
                    builder.AppendLine(string.Format("this.{0}.FlatStyle = System.Windows.Forms.FlatStyle.Flat;", fwButton.Name));
                    builder.AppendLine(string.Format("this.{0}.FlatAppearance.BorderSize = 0;", fwButton.Name));
                    builder.AppendLine(string.Format("this.{0}.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0,255,255,255);", fwButton.Name));
                    //builder.AppendLine(string.Format("this.{0}.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;;", fwButton.Name));
                    builder.AppendLine(string.Format("this.{0}.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;;", fwButton.Name));
                }
                else if (fwControl is FWTextBox fwTextBox)
                {
                    builder.AppendLine(string.Format("this.{0}.BorderStyle = System.Windows.Forms.BorderStyle.None;", fwTextBox.Name));
                    builder.AppendLine(string.Format("this.{0}.Text = \"{1}\";", fwTextBox.Name, fwTextBox.Text));
                    builder.AppendLine(string.Format("this.{0}.Font = new System.Drawing.Font(\"굴림\", {1}F);", fwTextBox.Name, (int)fwTextBox.Height/2 - 1));
                }
                else if (fwControl is FWListBox fwListBox)
                {
                    builder.AppendLine(string.Format("this.{0}.BorderStyle = System.Windows.Forms.BorderStyle.None;", fwListBox.Name));
                    builder.AppendLine(string.Format("this.{0}.FormattingEnabled = true;", fwListBox.Name));
                    builder.AppendLine(string.Format("this.{0}.ItemHeight = {1};", fwListBox.Name, fwListBox.ItemHeight));
                }

                builder.AppendLine(string.Format("this.{0}.Location = new System.Drawing.Point({1}, {2});", fwControl.Name, (int)fwControl.Left, (int)fwControl.Top));
                builder.AppendLine(string.Format("this.{0}.Name = \"{0}\";", fwControl.Name));
                builder.AppendLine(string.Format("this.{0}.Size = new System.Drawing.Size({1}, {2});", fwControl.Name, (int)fwControl.Width, (int)fwControl.Height));
                builder.AppendLine(string.Format("this.{0}.TabIndex = {1};", fwControl.Name, tabIndex));

                FWEventList fwEventList = fwControl.FWEventList;
                foreach (Event ev in fwEventList.Events)
                {
                    if (!string.IsNullOrWhiteSpace(ev.EventValue))
                    {
                        builder.AppendLine(string.Format("this.{0}.{1} += new System.EventHandler(this.{2});", fwControl.Name, ev.EventName, ev.EventValue));
                    }
                }
                tabIndex++;
            }

            builder
                .AppendLine("           this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);")
                .AppendLine("           this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;")
                .AppendLine("           this.ClientSize = new System.Drawing.Size(1280, 800);")
                .AppendLine("           this.MaximizeBox = false;")
                .AppendLine("           this.MinimizeBox = false;")
                .AppendLine("           this.MaximumSize = new System.Drawing.Size(1280, 800);")
                .AppendLine("           this.MinimumSize = new System.Drawing.Size(1280, 800);");

            if (!string.IsNullOrWhiteSpace(UIManager.Instance.BackgroundImage))
            {
                builder
                    .AppendLine("           this.BackgroundImage = System.Drawing.Image.FromFile(@\"" + UIManager.Instance.BackgroundImage + "\");")
                    .AppendLine("           this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;");
            }

            foreach (FWControl fwControl in fwControls)
            {
                builder.AppendLine("this.Controls.Add(this." + fwControl.Name + ");");
            }

            builder
                .AppendLine("           this.Text = \"MainForm\";")
                .AppendLine("           this.Name = \"MainForm\";")
                .AppendLine("           this.ResumeLayout(false);")
                .AppendLine("           this.PerformLayout();")
                .AppendLine("       }");

            foreach (FWControl fwControl in fwControls)
            {
                builder.AppendLine("private System.Windows.Forms." + fwControl.TypeName + " " + fwControl.Name + ";");
            }

            builder
                .AppendLine("   }")
                .AppendLine("}");
            

            return builder;
        }

        public void CompileFlower()
        {            
            string argument = string.Format("-target:winexe -out:{0} {1} {2} {3}", 
                string.Format(@"{0}\{1}.exe", DefaultPath, ProjectManager.Instance.ProjectName),
                string.Format(@"{0}\Program.cs", DefaultPath),
                string.Format(@"{0}\MainForm.cs", DefaultPath),
                string.Format(@"{0}\MainForm.Designer.cs", DefaultPath));

            Process process = Process.Start(cscPath, argument);
            process.WaitForExit();

            //File.Delete(string.Format("{0}/Program.cs", DefaultPath));
            //File.Delete(string.Format("{0}/MainForm.cs", DefaultPath));
            //File.Delete(string.Format("{0}/MainForm.Designer.cs", DefaultPath));
        }
    }
}
