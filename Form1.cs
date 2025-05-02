using System;
using System.Windows.Forms;
using System.Drawing;

namespace CoI_Clac_2._0
{
    public partial class Form1 : Form
    {
        string version = "0.1.0";
        bool toogleSurplusColors = false;

        // Array: Name, Fuelusage, TotalUsage
        private readonly object[,] VehicleData =
        {
            {"T1 Truck", 0.4f,0.0f},
            {"T2 Truck", 1.0f,0.0f},
            {"T3 Truck", 2.3f,0.0f},
            {"T1 Excavator", 1f,0.0f},
            {"T2 Excavator", 2.3f,0.0f},
            {"T3 Excavator", 5f,0.0f},
            {"T1 Harvester", 0.8f,0.0f},
            {"T2 Harvester", 1.8f,0.0f}
        };

        private readonly object[,] ShipData =
{
            {"Cargoship 2(s)", 40f, 0f},
            {"Cargoship 2(fs)", 14f, 0f},
            {"Cargoship 4(s)", 60f , 0f},
            {"Cargoship 4(fs)", 21f, 0f},
            {"Cargoship 6(s)", 80f , 0f},
            {"Cargoship 6(fs)", 28f , 0f},
            {"Cargoship 8(s)", 100f , 0f},
            {"Cargoship 8(fs)", 35f, 0f}
        };

        private readonly object[,] BuildingData_Usage =
        {
            {"T1 Diesel Pow.", 3f , 0f},
            {"T2 Diesel Pow.", 18f , 0f},
            {"T1 Rub.Syn.C", 8f , 0f},
            {"T2 Rub.Syn.S", 12f , 0f},
            {"Cracking Unit(U)", 24f, 0f }

        };

        // Array: Name, Fuel production, TotalProduction 
        private readonly object[,] BuildingData_Production =
        {
            {"Basic Distiller", 24f,0f },
            {"Dist. (Stage II)", 36f,0f },
            {"T1 Chem.Plant", 27f,0f  },
            {"T2 Chem.Plant", 54f,0f  },
            {"Cracking Unit(P)", 24f,0f  }
        };

        // // Array: Name, Fuel safing value 
        private object[,] EdictsFuelData =
        {
            {"Vehicles Fuel Saver I", 0.15f, false },
            {"Vehicles Fuel Saver II", 0.15f ,false},
            {"Ships Fuel Saver I", 0.1f,false },
        };

        private object[,] SummaryData =
        {
            {"Vehicles:",0f },
            {"Ships:",0f },
            {"Buildings:",0f },
            {"Production:", 0f },
            {"Surplus:",0f },
        };

        Point upLeftStart = new Point(30, 10);
        void BuildGUI_NumericUD(object[,] Data, string GroupBoxName, int posOffsetRight = 0)
        {
            Point p = new Point(upLeftStart.X + posOffsetRight, upLeftStart.Y);
            
            GroupBox groupBox = new GroupBox();
            groupBox.Text = GroupBoxName;
            groupBox.Location = p;
            
            for (int i = 0; i < Data.GetLength(0);i++)
            {
                
                Label labelData = new Label(); 
                labelData.Name = "Lbl_" + (string)Data[i,0];
                labelData.Location = new Point (groupBox.Location.X + 10, groupBox.Location.Y + 25 + (i * 25));
                labelData.Text = (string)Data [i , 0];
                labelData.AutoSize = true;
                this.Controls.Add(labelData);

                NumericUpDown numericUD = new NumericUpDown();
                numericUD.Name = "NumUD" + (string)Data[i, 0];
                numericUD.Location = new Point(groupBox.Location.X + 130, groupBox.Location.Y + 25 + (i * 25));
                numericUD.Maximum = 9999m;
                numericUD.Size = new Size(70,25);
                this.Controls.Add(numericUD);

            }
            groupBox.Size = new Size (220,(Data.GetLength(0) * 25) + 35);
            this.Controls.Add(groupBox);
        }

        void BuildGUI_Buttons()
        {
            Button btnClear = new Button();
            btnClear.Text = "Clear All";
            btnClear.AutoSize = true;
            btnClear.Location = new Point(880, 405);
            btnClear.Click += new EventHandler(ClearAllBtnClicked);
            this.Controls.Add (btnClear);

            Button btnToggleColormode = new Button();
            btnToggleColormode.Text = "Colormode";
            btnToggleColormode.AutoSize = true;
            btnToggleColormode.Location = new Point(650, 365);
            btnToggleColormode.Click += new EventHandler(ColorMode);
            this.Controls.Add(btnToggleColormode);

            Button btnExit = new Button();
            btnExit.Text = "Exit";
            btnExit.AutoSize = true;
            btnExit.Location = new Point(35, 405);
            btnExit.Click += new EventHandler(Exit);
            this.Controls.Add(btnExit);

        }

        void Exit(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }


        void ColorMode(object sender, EventArgs e)
        {
            toogleSurplusColors = !toogleSurplusColors;
        }

        void ClearAllBtnClicked(object sender, EventArgs e)
        {
          
            foreach (Control c in Controls)
            {
                if (c is NumericUpDown numUD)
                {
                    numUD.Value = 0;
                }
                if (c is CheckBox ChB)
                {
                    ChB.Checked = false;
                }
            }
        }

        void BuildGUI_CheckBoxes(object[,] Data, int offsetY = 0)
        {
            GroupBox groupBox = new GroupBox();
            groupBox.Location = new Point(30,250);
            groupBox.Text = "Fuel saving edicts";

            for (int i = 0; i < Data.GetLength(0); i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Name = (string)Data[i, 0];
                checkBox.Text = (string)Data[i, 0];
                checkBox.Checked = (bool)Data[i,2];
                checkBox.AutoSize = true;   
                checkBox.Location = new Point(groupBox.Location.X + 10, groupBox.Location.Y + 25 + (i * 25));
                this.Controls.Add(checkBox);
            }
            groupBox.Size = new Size(220, (Data.GetLength(0) * 25) + 35);
            this.Controls.Add(groupBox);
        }

        void BuildGUI_Summary(object[,] Data)
        {
            GroupBox groupBox = new GroupBox();
            groupBox.Text = "Summary:";
            groupBox.Location = new Point(750, 250);

            for (int i = 0; i < Data.GetLength(0); i++ )
            {
                Label labelName = new Label();
                TextBox Value = new TextBox(); 
                labelName.Name = (string)Data[i, 0];
                Value.Name = "Value" + (string)Data[i, 0];
                labelName.Text = (string)Data[i, 0];
                Value.Text = $"{Data[i, 1]}";
                Value.TextAlign = HorizontalAlignment.Right;
                labelName.AutoSize = true;
                Value.AutoSize = true;
                Value.ReadOnly = true;
                labelName.Location = new Point(groupBox.Location.X + 10, groupBox.Location.Y + 20 + (i * 25));
                Value.Location = new Point(groupBox.Location.X + 105, groupBox.Location.Y + 15 + +(i * 25));
                this.Controls.Add(labelName);
                this.Controls.Add(Value);
            }
            groupBox.Size = new Size(210, (Data.GetLength(0) * 25) + 30);
            this.Controls.Add(groupBox);    
        }

        string FormatSummaryStrings (string SummaryString, int padding)
        { 
            return SummaryString.PadLeft(padding); 
        }


        void UpdateGUI_Summary()
        {
            foreach (Control c in Controls)
            {
                if (c is TextBox label)
                {
                    label.ReadOnly = false;
                    if (label.Name.Contains("Value" + $"{SummaryData[0, 0]}"))
                    {
                        label.Text = FormatSummaryStrings(((float)SummaryData[0, 1]).ToString("0.0"),15);
                         
                    }
                    if (label.Name.Contains("Value" + $"{SummaryData[1, 0]}"))
                    {
                        label.Text = FormatSummaryStrings(((float)SummaryData[1, 1]).ToString("0.0"), 15);
                    }
                    if (label.Name.Contains("Value" + $"{SummaryData[2, 0]}"))
                    {
                        label.Text = FormatSummaryStrings(((float)SummaryData[2, 1]).ToString("0.0"), 15);
                    }
                    if (label.Name.Contains("Value" + $"{SummaryData[3, 0]}"))
                    {
                        label.Text = FormatSummaryStrings(((float)SummaryData[3, 1]).ToString("0.0"), 15);
                    }
                    if (label.Name.Contains("Value" + $"{SummaryData[4, 0]}"))
                    {
                        if (toogleSurplusColors) {label.BackColor = (float)SummaryData[4, 1] > 0 ? Color.Green : (float)SummaryData[4, 1] == 0.0 ? Color.Gray : Color.Red; }
                        else { label.BackColor = SystemColors.Control; }
                        label.Text = FormatSummaryStrings(((float)SummaryData[4, 1]).ToString("0.0"),15);
                    }
                    label.ReadOnly = true;
                }

            }
        }

        public Form1()
        {
            InitializeComponent();
            this.Text = "CoI - Diesel calculator " + version;
        }

        void BuildGUI()
        {
            BuildGUI_NumericUD(VehicleData, "Vehicles");
            BuildGUI_NumericUD(ShipData, "Ships (fs = fuel save on)", 240);
            BuildGUI_NumericUD(BuildingData_Usage, "Buildings usage", 480);
            BuildGUI_NumericUD(BuildingData_Production, "Buildings produktion", 720);
            BuildGUI_CheckBoxes(EdictsFuelData);
            BuildGUI_Summary(SummaryData);
            BuildGUI_Buttons();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            BuildGUI();
            this.AutoSize = true;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            checkTimer.Interval = 300; // Check every 100ms (1 second)
            checkTimer.Tick += CheckTimer_Tick;
            checkTimer.Start();

        }

        private System.Windows.Forms.Timer checkTimer = new System.Windows.Forms.Timer();
        private void CheckTimer_Tick(object sender, EventArgs e)
        {
            foreach (Control c in Controls)
            {
                GetNumericUDValuesFromGUI(VehicleData, c);
                GetNumericUDValuesFromGUI(ShipData, c);
                GetNumericUDValuesFromGUI(BuildingData_Usage, c);
                GetNumericUDValuesFromGUI(BuildingData_Production, c);
                GetBoolStatesFromGUI(EdictsFuelData, c);
            }
            Calculate(VehicleData,0);
            Calculate(ShipData, 1);
            Calculate(BuildingData_Usage, 2);
            Calculate(BuildingData_Production, 3);
            UpdateGUI_Summary();
        }
         
        void GetNumericUDValuesFromGUI(object[,] Data, Control control)
        {
            if (control is NumericUpDown NumUD)
            {
                for (int i = 0; i < Data.GetLength(0); i++)
                {
                    if (NumUD.Name.Contains((string)Data[i, 0]))
                    {
                        Data[i, 2] = NumUD.Value;
                        
                    }

                }
            }
        }

        void Calculate(object[,] Data, int indexSummary)
        {
            float FuelMod_Vehicles = 1;
            float FuelMod_Ships = 1;
            if ((bool)EdictsFuelData[0, 2] == true) { FuelMod_Vehicles -= (float)EdictsFuelData[0, 1]; }
            if ((bool)EdictsFuelData[1, 2] == true) { FuelMod_Vehicles -= (float)EdictsFuelData[1, 1]; }
            if ((bool)EdictsFuelData[2, 2] == true) { FuelMod_Ships -= (float)EdictsFuelData[2, 1]; }

            float sum = 0f;
            for (int i = 0; i < Data.GetLength(0);i++)
            {
                sum += Convert.ToSingle(Data[i, 1]) * Convert.ToSingle(Data[i, 2]);
            }
            SummaryData[indexSummary, 1] = sum;
            SummaryData[4, 1] = 0 - ((float)SummaryData[0, 1]*FuelMod_Vehicles + (float)SummaryData[1, 1]*FuelMod_Ships + (float)SummaryData[2, 1]) + (float)SummaryData[3, 1];

        }

        void GetBoolStatesFromGUI(object[,] Data, Control control)
        {
            if (control is CheckBox ChB)
            {
                for (int i = 0; i < Data.GetLength(0); i++)
                {
                    if (ChB.Text == (string)Data[i,0])
                    {
                        Data[i, 2] = ChB.Checked;
                    }
                }
            }
        }

        

    }
}
