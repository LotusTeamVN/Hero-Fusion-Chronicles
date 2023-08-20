using System.Collections.Generic;

namespace HFC.Hero
{
    public class Attribute
    {
        public AttributeType type;
        public float value;

        /// <summary>
        /// Hệ số tăng trưởng giá trị thuộc tính này theo level của hero
        /// </summary>
        public Dictionary<int, float> levelCoefficient = null;

        /// <summary>
        /// Hệ số tăng trưởng giá trị thuộc tính này theo star trong trận đấu của hero
        /// </summary>
        public Dictionary<int, float> starCoefficient = null;


        public Attribute()
        {
            
        }
    }
}


