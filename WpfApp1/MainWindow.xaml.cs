using System;
using System.Windows;
using System.Drawing;
using System.Data;
//添加画图类
using System.Drawing.Imaging;
using System.IO;


namespace WpfApp1
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class Data
    {
        public Data(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X
        {
            get;
            set;
        }

        public double Y
        {
            get;
            set;
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {


            InitializeComponent();
            Curve curve2d = new Curve(); //instance

			Bitmap bmp = curve2d.CreateImage();
            //bmp.Save(Response.OutputStream,ImageFormat.Jpeg);
            bmp.Save(@"D:\Curve.bmp", ImageFormat.Bmp);
            //CreateImage();
        }

        public class Curve
		{
			private Graphics objGraphics; //Graphics method of object
            private Graphics labelObj; 
			private Bitmap objBitmap; //graph obejct

			private int m_Width = 1920; //graph width
			private int m_Height = 1080; //graph height
			private float m_XSlice = 50; //Xaxis mark width
			private float m_YSlice = 50; //Yaxis mark width
			private float m_YSliceValue = 100; //Yaxis value 
			private float m_YSliceBegin = 0; //Yaxis start value
			private float m_Tension = 0.5f;
			private string m_Title = "test"; //Title
			private string m_Unit = "unit"; //unite
			private string m_XAxisText = "Time(min)"; //Xaxis text
			private string m_YAxisText = "Intensity"; //Yaxis text
			private string[] m_Keys = new string[]{"","1", "2","3","4","5","6","7","8","9","10","11","12","13","14","15","16","17","18","19","20","21","22","23","24","25","26","27","28","29","30","31","32","33","34","35","36","37","38","39","40","41","42","43","44","45","46","47","48","49","50","51","52","53","54","55","56","57","58","59","60","61","62","63","64","65","66","67","68","69","70","71","72","73","74","75","76","77","78","79","80","81","82","83","84","85","86","87","88","89","90","91","92","93","94","95","96","97","98","99","100","101","102","103","104","105","106","107","108","109","110","111","112","113","114","115","116","117","118","119","120","121","121","123","124","125","126","127","128","129","130","131","132","133","134","135","136","137","138","139","140","141","142","143","144","145"}; //Xaxis value 
			private float[] m_Values = new float[]{100.13636779f,100.13636779f,99.97434997f,99.91627502f,99.84027862f,99.73872375f,99.68705749f,99.57246398f,99.53229522f,99.43615722f,99.36226654f,99.31098937f,99.23509216f,
99.24930572f,
99.26844787f,
99.13729095f,
99.08396911f,
99.02206420f,
98.97335052f,
98.93831634f,
98.93999481f,
98.93376159f,
98.85617065f,
98.82410430f,
98.81771087f,
98.78941345f,
98.76016998f,
98.71606445f,
98.71911621f,
98.71027374f,
98.67870330f,
98.63468170f,
98.61227416f,
98.60410308f,
98.54369354f,
98.53150177f,
98.51820373f,
98.48543548f,
98.53021240f,
98.47772216f,
98.44147491f,
98.46871185f,
98.45572662f,
98.38578033f,
98.37026977f,
98.37981414f,
98.40867614f,
98.32593536f,
98.32688903f,
98.32901763f,
98.30844116f,
98.32727050f,
98.26142883f,
98.25404357f,
98.24216461f,
98.23987579f,
98.20009613f,
98.18412017f,
98.19174957f,
98.17549133f,
98.13902282f,
98.15258789f,
98.13761138f,
98.11972808f,
98.12406921f,
98.07202911f,
98.14785766f,
98.07659912f,
98.07180023f,
98.04254150f,
98.10493469f,
98.02767944f,
98.03502655f,
98.04147338f,
98.04965209f,
98.06535339f,
98.05857849f,
97.99221801f,
98.05194091f,
98.03771972f,
98.06079101f,
97.98467254f,
98.01860046f,
98.00945281f,
97.99611663f,
97.99231719f,
97.99811553f,
97.96549224f,
97.97451019f,
97.95214843f,
97.90617370f,
97.87899017f,
97.93210601f,
98.03022766f,
98.75674438f,
101.56599426f,
106.13615417f,
111.65046691f,
118.45186614f,
127.23332214f,
139.02624511f,
158.48130798f,
187.01391601f,
213.13674926f,
223.35366821f,
219.35095214f,
208.64784240f,
196.13702392f,
183.80505371f,
172.68417358f,
163.17459106f,
155.22027587f,
148.58549499f,
142.80096435f,
137.69763183f,
133.09973144f,
129.04780578f,
125.45522308f,
122.26302337f,
119.45645141f,
117.00534820f,
114.81840515f,
113.02484130f,
111.29143524f,
109.88728332f,
108.62644195f,
107.64141845f,
106.55022430f,
105.81355285f,
105.10055541f,
104.48545837f,
103.90827941f,
103.43152618f,
103.01782989f,
102.66262817f,
102.30013275f,
102.05798339f,
101.81026458f,
101.59600067f,
101.43446350f,
101.25779724f,
101.13021850f,
100.94995880f,
100.79347991f,
100.66439819f,
100.54563140f,}; //Yaxis value
			private Color m_BgColor = Color.Snow; //background color
			private Color m_TextColor = Color.Black; //text color
			private Color m_BorderColor = Color.Black; //border color
			private Color m_AxisColor = Color.Black; //axis color
			private Color m_AxisTextColor = Color.Black; //axis text color
			private Color m_SliceTextColor = Color.Black; //mark text color
			private Color m_SliceColor = Color.Black; //mark color
			private Color m_CurveColor = Color.Blue; //curve color

			public int Width
			{
				set
				{
					if(value < 300)
					{
						m_Width = 300;
					}
					else
					{
						m_Width = value;
					}
				}
				get
				{
					return m_Width;
				}
			}

			public int Height
			{
				set
				{
					if(value < 300)
					{
						m_Height = 300;
					}
					else
					{
						m_Height = value;
					}
				}
				get
				{
					return m_Height;
				}
			}

			public float XSlice
			{
				set
				{
					m_XSlice = value;
				}
				get
				{
					return m_XSlice;
				}
			}

			public float YSlice
			{
				set
				{
					m_YSlice = value;
				}
				get
				{
					return m_YSlice;
				}
			}

			public float YSliceValue
			{
				set
				{
					m_YSliceValue = value;
				}
				get
				{
					return m_YSliceValue;
				}
			}

			public float YSliceBegin
			{
				set
				{
					m_YSliceBegin = value;
				}
				get
				{
					return m_YSliceBegin;
				}
			}

			public float Tension
			{
				set
				{
					if(value < 0.0f && value > 1.0f)
					{
						m_Tension = 0.5f;
					}
					else
					{
						m_Tension = value;
					}
				}
				get
				{
					return m_Tension;
				}
			}

			public string Title
			{
				set
				{
					m_Title  = value;
				}
				get
				{
					return m_Title;
				}
			}

			public string Unit
			{
				set
				{
					m_Unit = value;
				}
				get
				{
					return m_Unit;
				}
			}

			public string[] Keys
			{
				set
				{
					m_Keys = value;
				}
				get
				{
					return m_Keys;
				}
			}

			public float[] Values
			{
				set
				{
					m_Values = value;
				}
				get
				{
					return m_Values;
				}
			}

			public Color BgColor
			{
				set
				{
					m_BgColor = value;
				}
				get
				{
					return m_BgColor;
				}
			}

			public Color TextColor
			{
				set
				{
					m_TextColor = value;
				}
				get
				{
					return m_TextColor;
				}
			}

			public Color BorderColor
			{
				set
				{
					m_BorderColor = value;
				}
				get
				{
					return m_BorderColor;
				}
			}

			public Color AxisColor
			{
				set
				{
					m_AxisColor = value;
				}
				get
				{
					return m_AxisColor;
				}
			}

			public string XAxisText
			{
				set
				{
					m_XAxisText = value;
				}
				get
				{
					return m_XAxisText;
				}
			}

			public string YAxisText
			{
				set
				{
					m_YAxisText = value;
				}
				get
				{
					return m_YAxisText;
				}
			}

			public Color AxisTextColor
			{
				set
				{
					m_AxisTextColor = value;
				}
				get
				{
					return m_AxisTextColor;
				}
			}

			public Color SliceTextColor
			{
				set
				{
					m_SliceTextColor = value;
				}
				get
				{
					return m_SliceTextColor;
				}
			}

			public Color SliceColor
			{
				set
				{
					m_SliceColor = value;
				}
				get
				{
					return m_SliceColor;
				}
			}

			public Color CurveColor
			{
				set
				{
					m_CurveColor = value;
				}
				get
				{
					return m_CurveColor;
				}
			}


			//generating image and return image
			public Bitmap CreateImage()
			{
				InitializeGraph();

				DrawContent(ref objGraphics);

				return objBitmap;
			}

            //generating and fullfill image area, draw border, initialize title
			private void InitializeGraph()
			{
			
				//generating graph base on the width and height
				objBitmap = new Bitmap(Width,Height);

				//generating obeject
				objGraphics = Graphics.FromImage(objBitmap);
                labelObj = Graphics.FromImage(objBitmap);
				//fullfill background color
				objGraphics.DrawRectangle(new Pen(BorderColor,1),0,0,Width,Height);
				objGraphics.FillRectangle(new SolidBrush(BgColor),1,1,Width-2,Height-2);

				//画X轴,pen,x1,y1,x2,y2 注意图像的原始X轴和Y轴计算是以左上角为原点，向右和向下计算的
				objGraphics.DrawLine(new Pen(new SolidBrush(AxisColor),1),100,Height - 100,Width - 75,Height - 100);

				//画Y轴,pen,x1,y1,x2,y2
				objGraphics.DrawLine(new Pen(new SolidBrush(AxisColor),1),100,Height - 100,100,75);

				//initilize Xaxis text
				SetAxisText(ref objGraphics, ref labelObj);

                //initilize Xaxis mark and text
				SetXAxis(ref objGraphics);

				//initilize Yaxis mark and text
				SetYAxis(ref objGraphics);

				//initilize title
				CreateTitle(ref objGraphics);
			}

			private void SetAxisText(ref Graphics objGraphics, ref Graphics labelObj)
			{
				objGraphics.DrawString(XAxisText,new Font("Arial",8),new SolidBrush(AxisTextColor),Width/2 - 50,Height - 50);

				int X = 30;
				int Y = (Height/2) - 100;           

                labelObj.TranslateTransform(20, Height/2);
                labelObj.RotateTransform(-90);
                labelObj.DrawString(YAxisText, new Font("Arial",8),new SolidBrush(AxisTextColor), 0, 0);
				
			}

			private  void SetXAxis(ref Graphics objGraphics)
			{
				int x1 = 100;
				int y1 = Height - 110;
				int x2 = 100;
				int y2 = Height - 90;
				int iCount = 0;
				int iSliceCount = 1;
				float Scale = 0;
				int iWidth = (int)((Width-200)*(50/XSlice));

				objGraphics.DrawString(Keys[0].ToString(),new Font("Arial",10),new SolidBrush(SliceTextColor),85,Height - 90);

				for(int i = 0;i <= iWidth;i += 10)
				{
					Scale = i * ( XSlice / 50 );

					if(iCount == 5)
					{
						objGraphics.DrawLine(new Pen(new SolidBrush(AxisColor)),x1+Scale,y1+10,x2+Scale,y2);
						//The Point!这里显示X轴刻度
						if(iSliceCount <= Keys.Length-1)
						{
							objGraphics.DrawString(Keys[iSliceCount].ToString(),new Font("Arial",10),new SolidBrush(SliceTextColor),x1 + Scale - 15,y2 + 10);
						}
						else
						{
							//超过范围，不画任何刻度文字
						}
						iCount = 0;
						iSliceCount++;
						if(x1+Scale > Width - 100)
						{
							break;
						}
					}
					else
					{
						//objGraphics.DrawLine(new Pen(new SolidBrush(SliceColor)),x1+Scale,y1+5,x2+Scale,y2-5);
					}
					iCount++;
				}
			}

			private void SetYAxis(ref Graphics objGraphics)
			{
				int x1 = 95; 
				int y1 = (int)(Height - 100 - 10*(YSlice/50));
				int x2 = 105;
				int y2 = (int)(Height - 100 - 10*(YSlice/50));
				int iCount = 1;
				float Scale = 0;
				int iSliceCount = 1;

				int iHeight = (int)((Height-200)*(50/YSlice));

				objGraphics.DrawString(YSliceBegin.ToString(),new Font("Arial",10),new SolidBrush(SliceTextColor),60,Height - 110);

				for(int i = 0;i < iHeight;i+=10)
				{
					Scale = i * ( YSlice / 50 );

					if(iCount == 5)
					{
						objGraphics.DrawLine(new Pen(new SolidBrush(AxisColor)),x1 - 5, y1 - Scale,x2 + 5 - 10,y2 - Scale);
						//The Point!这里显示Y轴刻度
						objGraphics.DrawString(Convert.ToString(YSliceValue * iSliceCount+YSliceBegin),new Font("Arial",10),new SolidBrush(SliceTextColor),60,y1 - Scale );

						iCount = 0;
						iSliceCount++;
					}
					else
					{
						//objGraphics.DrawLine(new Pen(new SolidBrush(SliceColor)),x1,y1 - Scale,x2,y2 - Scale);
					}
					iCount ++;
				}
			}

			private void DrawContent(ref Graphics objGraphics)
			{
				
					Pen CurvePen = new Pen(CurveColor,1);
					PointF[] CurvePointF = new PointF[Keys.Length];
					float keys = 0;
					float values = 0;
					float Offset1 = (Height-100) + YSliceBegin;
					float Offset2 = (YSlice/50)*(50/YSliceValue);
                 
					for(int i=0;i<Keys.Length;i++)
					{
						keys = i+100;
						values = Offset1 - Values[i]*Offset2;
						CurvePointF[i] = new PointF(keys,values);
                        
				    }
					objGraphics.DrawCurve(CurvePen,CurvePointF,Tension);
				
				

          //      if(Keys.Length == Values.Length)
			//	    {
			//		    Pen CurvePen = new Pen(CurveColor,1);
			//		    PointF[] CurvePointF = new PointF[Keys.Length];
			//		    float keys = 0;
			//		    float values = 0;
			//		    float Offset1 = (Height-100) + YSliceBegin;
			//		    float Offset2 = (YSlice/50)*(50/YSliceValue);

			//		    for(int i=0;i<Keys.Length;i++)
		//			    {
			//			    keys = i + 100;
			//			    values = Offset1 - Values[i]*Offset2;
			//			    CurvePointF[i] = new PointF(keys,values);
			//		    }
			//		    objGraphics.DrawCurve(CurvePen,CurvePointF,Tension);
			//	    }
			//	    else
			//	    {
			//		    objGraphics.DrawString("Error!The length of Keys and Values must be same!",new Font("Arial",16),new SolidBrush(TextColor),new System.Drawing.Point(150,Height/2));
			//	    }

			}

			//initilize titler
			private void CreateTitle(ref Graphics objGraphics)
			{
				objGraphics.DrawString(Title,new Font("Arial",14),new SolidBrush(TextColor),new System.Drawing.Point(Width/2 - 50,5));
			}
		}


       
    }
}




