using FLOWeR_Garden_Ver02.Explorer;
using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Action;
using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Action.CodeInjection;
using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Action.InputValue;
using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Action.ListBoxs;
using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Action.MessageBox;
using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Component;
using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;

namespace FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner
{
    /// <summary>
    /// FlowchartDesigner.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FlowchartDesigner : UserControl
    {
        public FlowchartDesigner(string flowName, string flowValue)
        {
            InitializeComponent();

            FWSymbolList = new List<FWSymbol>();

            FlowName = flowName;
            FlowValue = flowValue;

            string upperDirectory = string.Format("{0}/{1}", ProjectManager.Instance.ProjectPath, ProjectManager.Instance.ProjectName);

            XmlPath = string.Format("{0}/{1}/{2}.xml", upperDirectory, "Flowchart Designer", flowValue);
        }

        public Window DockedWindow
        {
            get;
            set;
        }

        public List<FWSymbol> FWSymbolList
        {
            get;
            set;
        }

        public string FlowName
        {
            get;
            private set;
        }

        public string FlowValue
        {
            get;
            private set;
        }

        public string XmlPath
        {
            get;
            private set;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {

        }

        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Type type = FlowManager.Instance.FlowTool.SelectedFWSymbol;
                Point point = e.GetPosition(this);

                if (type != null)
                {
                    DrawSymbol(type, point);
                }
                else
                {
                    // 액션에 있을것인가.
                    DrawAction(point);
                }
            }

            e.Handled = true;
        }

        private void DrawAction(Point point)
        {
            ActionTool actionTool = FlowManager.Instance.ActionTool;
            if (actionTool.SelectedAction != null)
            {
                Type actionType = actionTool.SelectedAction.GetType();
                FlowComponent flowComponent = null;
                FWProcess fwProcess = null;

                if (actionType.Equals(typeof(ActionInputValue)))
                {
                    fwProcess = new ActionInputValue();
                }
                else if (actionType.Equals(typeof(ActionCodeInjection)))
                {
                    fwProcess = new ActionCodeInjection();
                }
                else if (actionType.Equals(typeof(ActionMessageBox)))
                {
                    fwProcess = new ActionMessageBox();
                }
                else if (actionType.Equals(typeof(ActionListBoxAdd)))
                {
                    fwProcess = new ActionListBoxAdd();
                }
                else if (actionType.Equals(typeof(ActionListBoxRemove)))
                {
                    fwProcess = new ActionListBoxRemove();
                }
                // 앞으로 추가.
                else if (false)
                {

                }

                if (fwProcess != null)
                {
                    fwProcess.Left = Math.Round(point.X);
                    fwProcess.Top = Math.Round(point.Y);
                    fwProcess.Width = 100;
                    fwProcess.Height = 30;

                    if (!FWSymbolList.Contains(fwProcess))
                    {
                        FWSymbolList.Add(fwProcess);
                    }

                    flowComponent = new FlowComponent(fwProcess, this);

                    flowComponent.PropertyChanged += FlowComponent_PropertyChanged;
                    flowComponent.PreviewMouseDown += FlowComponent_PreviewMouseDown;
                    flowComponent.PreviewMouseUp += FlowComponent_PreviewMouseUp;

                    canvasArea.Children.Add(flowComponent);

                    FlowManager.Instance.ActionTool.DeselectListBox();
                }
            }
        }

        private void DrawSymbol(Type type, Point point)
        {
            FlowComponent flowComponent = null;
            FWSymbol fwSymbol = null;

            if (type.Equals(typeof(FWTerminal)))
            {
                fwSymbol = new FWTerminal();
            }
            else if (type.Equals(typeof(FWReady)))
            {
                fwSymbol = new FWReady();
            }
            else if (type.Equals(typeof(FWProcess)))
            {
                fwSymbol = new FWProcess();
            }
            else if (type.Equals(typeof(FWDecision)))
            {
                fwSymbol = new FWDecision();
            }

            if (fwSymbol != null)
            {
                fwSymbol.Left = Math.Round(point.X);
                fwSymbol.Top = Math.Round(point.Y);
                fwSymbol.Width = 100;
                fwSymbol.Height = 30;

                if (!FWSymbolList.Contains(fwSymbol))
                {
                    FWSymbolList.Add(fwSymbol);
                }

                flowComponent = new FlowComponent(fwSymbol, this);

                flowComponent.PropertyChanged += FlowComponent_PropertyChanged;
                flowComponent.PreviewMouseDown += FlowComponent_PreviewMouseDown;
                flowComponent.PreviewMouseUp += FlowComponent_PreviewMouseUp;

                canvasArea.Children.Add(flowComponent);

                FlowManager.Instance.FlowTool.Deselection();
            }
        }

        private void FlowComponent_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ConvertFlowToXml();

            GenerateCode();
        }

        private void GenerateCode()
        {
            FlowDocument flowDocument = new FlowDocument
            {
                LineHeight = 1
            };
            rtbCode.Document = flowDocument;

            string eventName = FlowName;
            string eventValue = FlowValue;

            //StringBuilder builder = new StringBuilder();

            //builder.AppendLine(string.Format("private void {0}(object sender, EventArgs e)", eventValue));
            //builder.AppendLine("{");

            AddColorStringRichTextBox(rtbCode, "private void ", Brushes.Blue);
            AddColorStringRichTextBox(rtbCode, eventValue + "(", Brushes.Black);
            AddColorStringRichTextBox(rtbCode, "object", Brushes.Blue);
            AddColorStringRichTextBox(rtbCode, " sender,", Brushes.Black);
            AddColorStringRichTextBox(rtbCode, " EventArgs", Brushes.Blue);
            AddColorStringRichTextBox(rtbCode, " e)\n", Brushes.Black);
            AddColorStringRichTextBox(rtbCode, "{\n", Brushes.Black);

            //To Do
            string upperDirectory = string.Format("{0}/{1}", ProjectManager.Instance.ProjectPath, ProjectManager.Instance.ProjectName);
            string xmlPath = string.Format("{0}/{1}/{2}.xml", upperDirectory, "Flowchart Designer", eventValue);

            using (XmlReader xmlReader = XmlReader.Create(xmlPath))
            {
                try
                {
                    xmlReader.MoveToContent();
                }
                catch
                {
                    return;
                }

                bool isPrevDecision = false;
                bool isPrevIf = false;

                while (xmlReader.Read())
                {
                    //builder.Append('\t');

                    AddColorStringRichTextBox(rtbCode, "\t", Brushes.Black);

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

                            //builder.AppendLine(string.Format("{0} {1};", varType, varName));

                            AddColorStringRichTextBox(rtbCode, varType, Brushes.Blue);
                            AddColorStringRichTextBox(rtbCode, " ", Brushes.White);
                            AddColorStringRichTextBox(rtbCode, varName, Brushes.Black);
                            AddColorStringRichTextBox(rtbCode, ";\n", Brushes.Black);
                        }
                    }
                    else if (xmlReader.IsStartElement("Decision") || xmlReader.Name.Equals("Decision"))
                    {
                        if (isPrevDecision)
                        {
                            isPrevDecision = false;

                            //builder.AppendLine("}");

                            AddColorStringRichTextBox(rtbCode, "}", Brushes.Black);
                            AddColorStringRichTextBox(rtbCode, "\n", Brushes.Black);
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
                                    //builder.AppendLine(string.Format("if ({0})", cond));
                                    //builder.Append('\t');
                                    //builder.AppendLine("{");

                                    AddColorStringRichTextBox(rtbCode, "if", Brushes.Blue);
                                    AddColorStringRichTextBox(rtbCode, " (", Brushes.Black);
                                    AddColorStringRichTextBox(rtbCode, cond, Brushes.Black);
                                    AddColorStringRichTextBox(rtbCode, ")\n", Brushes.Black);
                                    AddColorStringRichTextBox(rtbCode, "\t{", Brushes.Black);
                                    AddColorStringRichTextBox(rtbCode, "\n", Brushes.Black);
                                }
                                else
                                {
                                    isPrevIf = false;
                                    //builder.AppendLine("else");
                                    //builder.Append('\t');
                                    //builder.AppendLine("{");

                                    AddColorStringRichTextBox(rtbCode, "else\n", Brushes.Blue);
                                    AddColorStringRichTextBox(rtbCode, "\t{", Brushes.Black);
                                    AddColorStringRichTextBox(rtbCode, "\n", Brushes.Black);
                                }
                            }
                        }
                    }
                    else if (xmlReader.IsStartElement("Process"))
                    {
                        if (isPrevDecision)
                        {
                            //builder.Append('\t');

                            AddColorStringRichTextBox(rtbCode, "\t", Brushes.Black);
                        }

                        if (xmlReader.HasAttributes)
                        {
                            string processName = xmlReader.GetAttribute("ProcessName");

                            if (processName.Equals("MessageBox"))
                            {
                               // builder.AppendLine(string.Format("System.Windows.Forms.MessageBox.Show({0});",
                                    //xmlReader.GetAttribute("Message")));

                                AddColorStringRichTextBox(rtbCode, "System.Windows.Forms.", Brushes.Black);
                                AddColorStringRichTextBox(rtbCode, "MessageBox.", Brushes.Blue);
                                AddColorStringRichTextBox(rtbCode, string.Format("Show({0});", xmlReader.GetAttribute("Message"))
                                    , Brushes.Black);
                                AddColorStringRichTextBox(rtbCode, "\n", Brushes.Black);
                            }
                            else if (processName.Equals("CodeInjection"))
                            {
                               // builder.AppendLine(xmlReader.GetAttribute("Code"));

                                AddColorStringRichTextBox(rtbCode, xmlReader.GetAttribute("Code"), Brushes.Black);
                                AddColorStringRichTextBox(rtbCode, "\n", Brushes.Black);
                            }
                            else if (processName.Equals("InputValue"))
                            {
                                string text = string.Format("{0} = {1};",
                                    xmlReader.GetAttribute("VarName"), xmlReader.GetAttribute("VarValue"));

                               // builder.AppendLine(string.Format("{0} = {1};",
                               //     xmlReader.GetAttribute("VarName"), xmlReader.GetAttribute("VarValue")));

                                AddColorStringRichTextBox(rtbCode, text, Brushes.Black);
                                AddColorStringRichTextBox(rtbCode, "\n", Brushes.Black);
                            }
                            else if (processName.Equals("ListBoxAdd"))
                            {
                                string text = string.Format("{0}.Items.Add({1});",
                                    xmlReader.GetAttribute("ListBoxName"), xmlReader.GetAttribute("ListBoxItem"));

                               // builder.AppendLine(string.Format("{0}.Items.Add({1});",
                               //     xmlReader.GetAttribute("ListBoxName"), xmlReader.GetAttribute("ListBoxItem")));

                                AddColorStringRichTextBox(rtbCode, text, Brushes.Black);
                                AddColorStringRichTextBox(rtbCode, "\n", Brushes.Black);
                            }
                            else if (processName.Equals("ListBoxRemove"))
                            {
                                string text = string.Format("{0}.Items.Remove({1});",
                                    xmlReader.GetAttribute("ListBoxName"), xmlReader.GetAttribute("ListBoxItem"));

                                //builder.AppendLine(string.Format("{0}.Items.Remove({1});",
                               //     xmlReader.GetAttribute("ListBoxName"), xmlReader.GetAttribute("ListBoxItem")));

                                AddColorStringRichTextBox(rtbCode, text, Brushes.Black);
                                AddColorStringRichTextBox(rtbCode, "\n", Brushes.Black);
                            }
                        }
                    }
                }

               // builder.Append('\r');
              //  builder.AppendLine("}");

                AddColorStringRichTextBox(rtbCode, "\r}", Brushes.Black);
            }
            //paragraph.Inlines.Add(new Bold(new Run(builder.ToString())));
           // flowDocument.Blocks.Add(paragraph);
        }

        private void AddColorStringRichTextBox(RichTextBox richTextBox, string text, SolidColorBrush color)
        {
            TextRange textRange = new TextRange(richTextBox.Document.ContentEnd, richTextBox.Document.ContentEnd)
            {
                Text = text
            };

            textRange.ApplyPropertyValue(TextElement.ForegroundProperty, color);
        }

        private bool isDrag = false;
        private Point? startPoint = null;
        private Point? endPoint = null;
        private Line hintLine = null;

        private FlowComponent startComponent = null;
        private FlowComponent endComponent = null;

        private void FlowComponent_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Type type = FlowManager.Instance.FlowTool.SelectedFWSymbol;

            if (type != null)
            {
                if (type.Equals(typeof(FWLine)))
                {
                    if (e.ChangedButton == MouseButton.Left)
                    {
                        isDrag = true;

                        if (startPoint == null)
                        {
                            startPoint = e.GetPosition(this);
                            Point pt = startPoint.Value;

                            hintLine = new Line
                            {
                                Stroke = Brushes.Black,
                                StrokeThickness = 4,
                                X1 = pt.X,
                                Y1 = pt.Y,
                                X2 = pt.X,
                                Y2 = pt.Y
                            };

                            canvasArea.Children.Add(hintLine);

                            if (sender is FlowComponent flowComponent)
                            {
                                if (startComponent == null)
                                {
                                    startComponent = flowComponent;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            // 힌트 라인
            if (isDrag)
            {
                Point currentPoint = e.GetPosition(this);

                Vector vector = Point.Subtract(currentPoint, startPoint.Value);
                if (vector.Length > 0.0)
                {
                    vector.Normalize();
                }

                hintLine.X2 = currentPoint.X - vector.X;
                hintLine.Y2 = currentPoint.Y - vector.Y;
            }

            // 기존 연결된 선들 다시 연결
            ReconnectLine();
        }

        private void FlowComponent_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            //// 마지막 클릭
            isDrag = false;

            if (endPoint == null)
            {
                endPoint = e.GetPosition(this);

                canvasArea.Children.Remove(hintLine);

                // 선 연결 하자.
                if (sender is FlowComponent flowComponent)
                {
                    if (startComponent != null)
                    {
                        endComponent = flowComponent;

                        ConnectLine(startComponent.FWSymbol, endComponent.FWSymbol);

                        startComponent = endComponent = null;
                    }
                }

                startPoint = endPoint = null;
            }

            FlowManager.Instance.FlowTool.Deselection();
        }

        public void ReconnectLine()
        {
            foreach (FWSymbol fwSymbol in FWSymbolList)
            {
                if (fwSymbol is FWLine fwLine)
                {
                    FWSymbol startConnector = fwLine.StartConnector;
                    FWSymbol endConnector = fwLine.EndConnector;

                    DrawFlowPath(fwLine, startConnector, endConnector);
                }
            }
        }

        private void ConnectLine(FWSymbol startConnector, FWSymbol endConnector)
        {
            if (startConnector.EndConnector != null)
            {
                return;
            }

            FWLine fwLine = new FWLine
            {
                StartConnector = startConnector,
                EndConnector = endConnector
            };

            if (startConnector is FWDecision fwDecision)
            {
                if (fwDecision.EndConnector[0] == null)
                {
                    fwDecision.EndConnector[0] = fwLine;
                }
                else if (fwDecision.EndConnector[1] == null)
                {
                    fwDecision.EndConnector[1] = fwLine;
                }
                else
                {
                    return;
                }

                endConnector.StartConnector = fwLine;
            }
            else
            {
                startConnector.EndConnector = fwLine;
                endConnector.StartConnector = fwLine;
            }

            FWSymbolList.Add(fwLine);

            DrawFlowPath(fwLine, startConnector, endConnector);

            FlowComponent flowComponent = new FlowComponent(fwLine, this);
            canvasArea.Children.Add(flowComponent);
        }
        
        private void ConvertFlowToXml()
        {
            FWSymbol terminalStart = null;
            terminalStart = FindTerminalSymbol();

            if (terminalStart == null)
            {
                return;
            }

            FWSymbol currentSymbol = terminalStart;

            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
                NewLineOnAttributes = true
            };

            using (XmlWriter xmlWriter = XmlWriter.Create(XmlPath, xmlWriterSettings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Terminal");

                StepIntoFlow(currentSymbol, xmlWriter);

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }
        }

        private void StepIntoFlow(FWSymbol currentSymbol, XmlWriter xmlWriter)
        {
            while ((currentSymbol = currentSymbol.EndConnector) != null)
            {
                if (currentSymbol is FWLine)
                {
                    continue;
                }

                if (currentSymbol is FWReady fwReady)
                {
                    xmlWriter.WriteStartElement("Ready");
                    xmlWriter.WriteAttributeString("Type", fwReady.VarType);
                    xmlWriter.WriteAttributeString("Name", fwReady.VarName);
                    xmlWriter.WriteEndElement();
                }
                else if (currentSymbol is FWProcess fwProcess)
                {
                    // 액션 종류 여러개임
                    if (fwProcess is ActionCodeInjection actionCodeInjection)
                    {
                        xmlWriter.WriteStartElement("Process");
                        xmlWriter.WriteAttributeString("ProcessName", "CodeInjection");
                        xmlWriter.WriteAttributeString("Code", actionCodeInjection.Code);
                        xmlWriter.WriteEndElement();
                    }
                    else if (fwProcess is ActionInputValue actionInputValue)
                    {
                        xmlWriter.WriteStartElement("Process");
                        xmlWriter.WriteAttributeString("ProcessName", "InputValue");
                        xmlWriter.WriteAttributeString("VarName", actionInputValue.VarName);
                        xmlWriter.WriteAttributeString("VarValue", actionInputValue.VarValue);
                        xmlWriter.WriteEndElement();
                    }
                    else if (fwProcess is ActionMessageBox actionMessageBox)
                    {
                        xmlWriter.WriteStartElement("Process");
                        xmlWriter.WriteAttributeString("ProcessName", "MessageBox");
                        xmlWriter.WriteAttributeString("Message", actionMessageBox.Message);
                        xmlWriter.WriteEndElement();
                    }
                    else if (fwProcess is ActionListBoxAdd actionListBoxAdd)
                    {
                        xmlWriter.WriteStartElement("Process");
                        xmlWriter.WriteAttributeString("ProcessName", "ListBoxAdd");
                        xmlWriter.WriteAttributeString("ListBoxName", actionListBoxAdd.ListBoxName);
                        xmlWriter.WriteAttributeString("ListBoxItem", actionListBoxAdd.ListBoxItem.ToString());
                        xmlWriter.WriteEndElement();
                    }
                    else if (fwProcess is ActionListBoxRemove actionListBoxRemove)
                    {
                        xmlWriter.WriteStartElement("Process");
                        xmlWriter.WriteAttributeString("ProcessName", "ListBoxRemove");
                        xmlWriter.WriteAttributeString("ListBoxName", actionListBoxRemove.ListBoxName);
                        xmlWriter.WriteAttributeString("ListBoxItem", actionListBoxRemove.ListBoxItem.ToString());
                        xmlWriter.WriteEndElement();
                    }
                    // 추가 예정
                    else if (false)
                    {

                    }
                }
                else if (currentSymbol is FWDecision fwDecision)
                {
                    //True
                    if (fwDecision.DecisionTrue != null)
                    {
                        xmlWriter.WriteStartElement("Decision");
                        xmlWriter.WriteAttributeString("Condition", fwDecision.ConditionalExpression);

                        StepIntoFlow(fwDecision.DecisionTrue, xmlWriter);

                        xmlWriter.WriteEndElement();
                    }

                    //False
                    if (fwDecision.DecisionFalse != null)
                    {
                        xmlWriter.WriteStartElement("Decision");
                        xmlWriter.WriteAttributeString("Condition", "!(" + fwDecision.ConditionalExpression + ")");

                        StepIntoFlow(fwDecision.DecisionFalse, xmlWriter);

                        xmlWriter.WriteEndElement();
                    }
                }
            }
        }

        private FWSymbol FindTerminalSymbol()
        {
            FWSymbol terminalStart = null;

            foreach (FWSymbol fwSymbol in FWSymbolList)
            {
                if (fwSymbol is FWTerminal fwTerminal)
                {
                    if (fwTerminal.StartConnector == null
                        && fwTerminal.EndConnector != null)
                    {
                        terminalStart = fwTerminal;
                        break;
                    }
                }
            }

            return terminalStart;
        }

        private void DrawFlowPath(FWLine fwLine, FWSymbol startConnector, FWSymbol endConnector)
        {
            Point[] startPoints = startConnector.RectPoints;
            Point[] endPoints = endConnector.RectPoints;
            
            double minDistance = double.MaxValue;
            double distance;
            int shortestI = -1, shortestJ = -1;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    distance = GetDistance(startPoints[i], endPoints[j]);
                    if (minDistance > distance)
                    {
                        minDistance = distance;
                        shortestI = i;
                        shortestJ = j;
                    }
                }
            }

            fwLine.DrawLine(startPoints[shortestI], endPoints[shortestJ]);
        }

        private double GetDistance(Point pt1, Point pt2)
        {
            return Math.Sqrt(Math.Pow((pt1.X - pt2.X), 2) + Math.Pow((pt1.Y - pt2.Y), 2));
        }

        private void Grid_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Deselect();
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isDrag)
            {
                isDrag = false;

                startPoint = endPoint = null;

                startComponent = null;
                endComponent = null;

                canvasArea.Children.Remove(hintLine);

                FlowManager.Instance.FlowTool.Deselection();

                Deselect();
            }
        }

        private void Deselect()
        {
            foreach (UIElement control in canvasArea.Children)
            {
                if (control is FlowComponent selectedComponent)
                {
                    if (selectedComponent.FWSymbol is FWLine fwLine)
                    {
                        fwLine.IsSelected = false;
                        fwLine.Thickness = 2;
                    }
                    selectedComponent.IsSelected = false;
                }
            }
        }

        public void DeleteSymbol(FlowComponent flowComponent)
        {
            FWSymbol fwSymbol = flowComponent.FWSymbol;

            if (fwSymbol.StartConnector != null)
            {
                fwSymbol.StartConnector.EndConnector = null;
            }

            if (fwSymbol.EndConnector != null)
            {
                fwSymbol.EndConnector.StartConnector = null;
            }

            FWSymbolList.Remove(fwSymbol);

            canvasArea.Children.Remove(flowComponent);
        }
    }
}
