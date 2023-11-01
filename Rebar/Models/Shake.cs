using MongoDB.Bson.Serialization.Attributes;

namespace Rebar.Models
{

    public class Shake
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public int S { get; }
        public int M { get; }
        public int L { get; }

        public Shake()
        {
            S = 10;
            M = 20;
            
            L = 28;
        }

       /* public float S
        {
            get { return _S; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("invalid size");
                }
                _S = value;
            }
        }
        public float M
        {
            get { return _M; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("invalid size");
                }
                _M = value;
            }
        }
        public float L
        {
            get { return _L; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("invalid size");
                }
                _L = value; ;
            }
        }*/
    }
}


        
    
