﻿using System;
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
			private float m_YSliceValue = 2; //Yaxis value 
			private float m_YSliceBegin = 0; //Yaxis start value
			private float m_Tension = 0.5f;
			private string m_Title = "test"; //Title
			private string m_Unit = "unit"; //unite
			private string m_XAxisText = "Time(min)"; //Xaxis text
			private string m_YAxisText = "Intensity"; //Yaxis text
			private string[] m_Keys = new string[]{"","10","20","30","40","50","60","70","80","90","100","110"}; //Xaxis value 
			private float[] m_Values = new float[]{2.0f,2.5f,5f,5.54f,2.16f,1.28f,6.0f,3.64f,3.0f,5.64f,4.58f,6.65f}; //Yaxis value
			private Color m_BgColor = Color.Snow; //background color
			private Color m_TextColor = Color.Black; //text color
			private Color m_BorderColor = Color.Black; //border color
			private Color m_AxisColor = Color.Black; //axis color
			private Color m_AxisTextColor = Color.Black; //axis text color
			private Color m_SliceTextColor = Color.Black; //mark text color
			private Color m_SliceColor = Color.Black; //mark color
			private Color m_CurveColor = Color.Red; //curve color

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
			//	if(Keys.Length == Values.Length)
			//	{
			//		Pen CurvePen = new Pen(CurveColor,1);
			//		PointF[] CurvePointF = new PointF[Keys.Length];
			//		float keys = 0;
			//		float values = 0;
			//		float Offset1 = (Height-100) + YSliceBegin;
			//		float Offset2 = (YSlice/50)*(50/YSliceValue);

			//		for(int i=0;i<Keys.Length;i++)
			//		{
			//			keys = XSlice*i+100;
			//			values = Offset1 - Values[i]*Offset2;
			//			CurvePointF[i] = new PointF(keys,values);
			//		}
			//		objGraphics.DrawCurve(CurvePen,CurvePointF,Tension);
			//	}
			//	else
			//	{
			//		objGraphics.DrawString("Error!The length of Keys and Values must be same!",new Font("Arial",16),new SolidBrush(TextColor),new System.Drawing.Point(150,Height/2));
			//	}

                if(Keys.Length == Values.Length)
				    {
					    Pen CurvePen = new Pen(CurveColor,1);
					    PointF[] CurvePointF = new PointF[Keys.Length];
					    float keys = 0;
					    float values = 0;
					    float Offset1 = (Height-100) + YSliceBegin;
					    float Offset2 = (YSlice/50)*(50/YSliceValue);

					    for(int i=0;i<Keys.Length;i++)
					    {
						    keys = i + 100;
						    values = Offset1 - Values[i]*Offset2;
						    CurvePointF[i] = new PointF(keys,values);
					    }
					    objGraphics.DrawCurve(CurvePen,CurvePointF,Tension);
				    }
				    else
				    {
					    objGraphics.DrawString("Error!The length of Keys and Values must be same!",new Font("Arial",16),new SolidBrush(TextColor),new System.Drawing.Point(150,Height/2));
				    }

			}

			//initilize titler
			private void CreateTitle(ref Graphics objGraphics)
			{
				objGraphics.DrawString(Title,new Font("Arial",14),new SolidBrush(TextColor),new System.Drawing.Point(Width/2 - 50,5));
			}
		}


       
    }
}




