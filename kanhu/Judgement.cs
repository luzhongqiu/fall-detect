using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using System.IO;
namespace people_walk
{
    class Judgement
    {
        private  float slight = 0.984F;//轻度摇晃
        private  float medium = 0.939F;//中度摇晃
        private  float serious =0.866F;//重度摇晃
        private float[] save_data = new float[2];

        public float Criterion(Skeleton skeleton,normalVector Vector) {
            float result;
            float dotProduct = 0;
            float module1 = 0;
            normalVector nowVector;
            Joint Spine = skeleton.Joints[JointType.Spine];
            Joint shoulderCenter = skeleton.Joints[JointType.ShoulderCenter];
            if (Vector != null && skeleton != null)
            {
                nowVector = new normalVector((shoulderCenter.Position.X - Spine.Position.X), (shoulderCenter.Position.Y - Spine.Position.Y)
                                               , (shoulderCenter.Position.Z - Spine.Position.Z));
                dotProduct = nowVector._A * Vector._A + nowVector._B * Vector._B + nowVector._C * Vector._C;
                module1 = (float)Math.Sqrt(nowVector._A * nowVector._A + nowVector._B * nowVector._B + nowVector._C * nowVector._C);
            }
           
            result = dotProduct / (module1 );
            //if (!File.Exists("D:\\record_NEXT.txt"))
            //{
            //    FileStream fs = new FileStream("D:\\record_NEXT.txt", FileMode.Create, FileAccess.Write);
            //    StreamWriter fs2 = new StreamWriter(fs);
            //    fs2.WriteLine("dotProduct: " + dotProduct + "         module1: " + module1 + "       result: " + result);
            //    fs2.Close();
            //    fs.Close();
            //}
            //else
            //{
            //    FileStream fs = new FileStream("D:\\record_NEXT.txt", FileMode.OpenOrCreate);
            //    fs.Seek(0, SeekOrigin.End);
            //    StreamWriter fs2 = new StreamWriter(fs);
            //    fs2.WriteLine("dotProduct: " + dotProduct + "         module1: " + module1 + "       result: " + result);
            //    fs2.Close();
            //    fs.Close();
            //} 
            return result;
        }

      public string  CompareResult(float result){
           

            if (result >= slight)
            {
                return "正常";
            }
            else if (result < slight && result >= medium)
            {
                return "轻微摇晃";
            }
            else if (result < medium && result >= serious)
            {
                return "中度摇晃";
            }
            else
                return "严重摇晃";
      }
      public int CompareAllResult(float result)
      {
          

          if (result >= slight)
          {
              return 1;
          }
          else if (result < slight && result >= medium)
          {
              return 2;
          }
          else if (result < medium && result >= serious)
          {
              return 3;
          }
          else 
              return 4;
      }
      public float CompareAngle(float result)
      {


          if (result >= slight)
          {
              return 20;
          }
          else if (result < slight && result >= medium)
          {
              return 40*(medium/result);
          }
          else if (result < medium && result >= serious)
          {
              return 60*(serious / result);
          }
          else if(result < serious)
              return 80;
          return 0;
      }
       
    }
}
