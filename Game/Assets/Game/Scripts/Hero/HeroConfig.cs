
using System.Collections.Generic;

namespace HFC.Hero
{
    /// <summary>
    /// Calss dùng để config các chỉ số của hero
    /// </summary>
    public class HeroConfig
    {
        public string heroName;
        public HeroClass heroClass;

        /// <summary>
        /// Các thuộc tính cơ bản của hero
        /// Gồm HP, ATK, ATKSP, SP, MP, MSP, RANGE
        /// </summary>
        public Dictionary<AttributeType, Attribute> attributes = null;


        /// <summary>
        /// Các skill của hero theo level tương ứng
        /// </summary>
        public Dictionary<int, Dictionary<SkillType, float>> skills = null;


        public HeroConfig()
        {

        }
    }
}

