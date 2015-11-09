using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace people_walk
{
    class normalVector
    {
        private float A;
        private float B;
        private float C;
        private float height;
        public normalVector(float A,float B,float C){
            this.A = A;
            this.B = B;
            this.C = C;
        }
        public float _A {
            get { return A; }
        }
        public float _B {
            get { return B; }
        }
        public float _C {
            get { return C; }
        }
        public float Height{
            set{ height = value;}
            get{return height;}
            
    }
    }
}
