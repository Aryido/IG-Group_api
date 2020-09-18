using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IG_api
{
    class CreateRectangle
    {
        public static System.Collections.ArrayList rectGreenList = new System.Collections.ArrayList();
        public static System.Collections.ArrayList rectRedList = new System.Collections.ArrayList();
       

        public ArrayList createGreenRectangleFunction()
        {
            for (int i = 0; i <= 100; i++)
            {
                Bitmap bm = new Bitmap(200, 50);
                Rectangle rect = new Rectangle(100, 0, i, 50);

                using (Graphics graphis = Graphics.FromImage(bm))
                {
                    graphis.FillRectangle(new SolidBrush(Color.Green), (Rectangle)rect);                  
                }


                rectGreenList.Add(bm);
            }
            return rectGreenList;
        }

        public ArrayList createRedRectangleFunction()
        {
            for (int i = 0; i <= 100; i++)
            {
                Bitmap bm = new Bitmap(200, 50);
                Rectangle rect = new Rectangle(100 - (i), 0, i, 50);
                using (Graphics graphis = Graphics.FromImage(bm))
                {
                    graphis.FillRectangle(new SolidBrush(Color.Red), (Rectangle)rect);
                }

                rectRedList.Add(bm);
            }
            return rectRedList;
        }

    }
}
